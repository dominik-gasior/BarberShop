using BarberShop.Modules.Warehouse.Api.Entities;
using BarberShop.Modules.Warehouse.Api.Exceptions.Orders;
using BarberShop.Modules.Warehouse.Api.Exceptions.Products;
using BarberShop.Modules.Warehouse.Api.Persistence;
using BarberShop.Modules.Warehouse.Shared.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
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
            .Include(o => o.Products)
            .Where(o => o.Id.Equals(id))
            .ToListAsync();

        public async Task<Order> GetOrderById(Guid id)
    {
        var order = await _dbContext
            .Orders
            .Include(o => o.Products)
            .Include(o => o.Client)
            .FirstOrDefaultAsync(c => c.Id.Equals(id));

        if (order is null) throw new NotFoundOrderByIdException(id);

        return order;
    } 
        public async Task<Guid> CreateNewOrder(Order order, List<int> orderProducts)
        {
            var products = await GetAllProducts();
            
            var productsOrders = products.Where(p => orderProducts.Any(o=>o == p.Id)).ToList();
            
            var cost = productsOrders.Select(p => p.Price).Sum();
            
            order.Products = productsOrders;
            order.Cost = cost;
            
            await _dbContext.Orders.AddAsync(order);
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
        //TODO publish event send email
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        return $"Order {order.Id} was updated status to {order.OrderStatus}.";
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
        => await _dbContext.Products.ToListAsync();

    public async Task<IEnumerable<Product>> GetAllProductsByOrderId(Guid id)
    {
        var orders = await GetOrderById(id);
        return orders.Products;
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
        
        //TODO publish event

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