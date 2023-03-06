using BarberShop.Modules.Warehouse.Api.Entities;
using BarberShop.Modules.Warehouse.Api.Exceptions.Orders;
using BarberShop.Modules.Warehouse.Api.Exceptions.Products;
using BarberShop.Modules.Warehouse.Api.Persistence;
using BarberShop.Modules.Warehouse.Shared.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Warehouse.Api.Features;

internal interface IWarehouseService
{
    //ORDERS
    Task<IEnumerable<Order>> GetAllOrders();
    Task<IEnumerable<Order>> GetAllOrdersByClientId(Guid id);
    Task<Order> GetOrderById(Guid id);
    Task<Guid> CreateNewOrder(Order order,List<int> orderProducts);
    Task<string> DeleteOrder(Guid id);
    Task<string> ChangeStatusOrder(Guid id);
    
    //PRODUCTS
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetAllProductsByOrderId(Guid id);
    Task<Product> GetProductById(int id);
    Task<int> CreateNewProduct(Product product);
    Task<string> DeleteProduct(int id);
    Task<string> UpdateProduct(Product product);
    
}

internal sealed class WarehouseService : IWarehouseService
{
    private readonly WarehouseDbContext _dbContext;
    private readonly IBus _bus;

    public WarehouseService(WarehouseDbContext dbContext, IBus bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
        => await _dbContext
            .Orders
            .Include(o=>o.Client)
            .ToListAsync();

    public async Task<IEnumerable<Order>> GetAllOrdersByClientId(Guid id)
        => await _dbContext
            .Orders
            .Where(o => o.Id.Equals(id))
            .ToListAsync();

        public async Task<Order> GetOrderById(Guid id)
    {
        var order = await _dbContext
            .Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(o=>o.Product)
            .Include(o => o.Client)
            .FirstOrDefaultAsync(c => c.Id.Equals(id));

        if (order is null) throw new NotFoundOrderByIdException(id);

        return order;
    } 
        public async Task<Guid> CreateNewOrder(Order order, List<int> orderProducts)
        {
            var products = await GetAllProducts();
            order.Cost = orderProducts.Sum(orderProduct => products.FirstOrDefault(p => p.Id == orderProduct)!.Price);
            
            //1,1,2,2,3
            var amountLists = orderProducts.Distinct().ToList();

            var listProduct =
                (from amountList in amountLists
                    let amount = orderProducts.Count(o => o == amountList)
                    select new OrderProduct { ProductId = amountList, Amount = amount }).ToList();

            var reduceAmountProducts = products
                .Where(p => p.Id == amountLists
                    .FirstOrDefault(a => a == p.Id))
                        .ToList();
            
            foreach (var reduceAmountProduct in reduceAmountProducts)
            {
                reduceAmountProduct.Amount -= orderProducts.Count(o=> o == reduceAmountProduct.Id);
            }
            
            order.OrderProducts = listProduct;
            
            await _dbContext.Orders.AddAsync(order);
            _dbContext.UpdateRange(reduceAmountProducts);
        await _dbContext.SaveChangesAsync();
        await _bus.Publish
            (
                new OrderCreated(order.Id, order.DeliveryTime)
            );
        return order.Id;
    }

    public async Task<string> DeleteOrder(Guid id)
    {
        var order = await GetOrderById(id);
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return $"Order #{id} was removed in database!";
    }

    public async Task<string> ChangeStatusOrder(Guid id)
    {
        var order = await GetOrderById(id);
        order.OrderStatus = OrderStatus.Odbiór;
        
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        await _bus.Publish
            (
                new OrderStatusChanged(order.Id, order.OrderStatus.ToString())    
            );
        return $"Order {order.Id} was updated status to {order.OrderStatus}.";
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
        => await _dbContext.Products.ToListAsync();

    public async Task<IEnumerable<Product>> GetAllProductsByOrderId(Guid id)
    {
        var products = await _dbContext.Orders
            .Where(p => p.Id.Equals(id))
            .SelectMany(p=>p.OrderProducts.Select(a=>a.Product))
            .ToListAsync();
        
        var orderProducts = await _dbContext.OrderProducts
            .Where(p => p.OrderId.Equals(id)).ToListAsync();

        foreach (var product in products)
        {
            product.Amount = orderProducts.FirstOrDefault(p => p.ProductId == product.Id)!.Amount;
        }
        
        return products;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _dbContext
            .Products
            .FirstOrDefaultAsync(p=>p.Id == id);
        
        if (product is null) throw new NotFoundProductByIdException(id);

        return product;
    }

    public async Task<int> CreateNewProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product.Id;
    }

    public async Task<string> DeleteProduct(int id)
    {
        var product = await GetProductById(id);
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return $"Product #{id} was removed in database!";
    }

    public async Task<string> UpdateProduct(Product product)
    {
        var oldProduct = await GetProductById(product.Id);

        if (product.Price <= 0)
        {
            oldProduct.LastPrice = oldProduct.Price;
            oldProduct.Price = product.Price;
        }
        if(product.Amount <= 0) oldProduct.Amount = product.Amount;

        _dbContext.Products.Update(oldProduct);
        await _dbContext.SaveChangesAsync();
        return $"Product #{product.Id} was updated in database";
    }
}