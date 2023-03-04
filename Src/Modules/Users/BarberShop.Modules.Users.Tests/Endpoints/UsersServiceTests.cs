using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Transactions;
using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Modules.Users.Tests.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BarberShop.Modules.Users.Tests.Endpoints;

public class UsersServiceTests
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;
    public UsersServiceTests()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateDefaultClient();
    }
    [Fact]
    public async Task GetAllUsers_FetchUsers_ReturnStatusOk()
    {
        //act
        var response = await _client.GetAsync("/api/getAllClients");

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.Content.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetUserById_FetchUser_ReturnStatusOk()
    {
        //arrange
        var user = await UsersTestsHelper.Add(_factory);
        //act
        var response = await _client.GetAsync("/api/user/" + user.Id);
    
        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        await UsersTestsHelper.Delete(_factory, user.Id);
    }
    
    [Theory]
    [InlineData("d3e1e570-07ba-4729-2d7f-08db1cb938")]
    [InlineData("1")]
    [InlineData("0")]
    [InlineData("-1")]
    public async Task GetUserById_FetchUser_ReturnStatusBadRequest(string userId)
    {
        //act
        var response = await _client.GetAsync("/api/user/" + userId);
        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateUser_ReturnStatusOk()
    {
        //arrange
        var user = new User
        {
            FirstName = "Adam",
            LastName = "Kowalski",
            NumberPhone = "123456789",
            Email = "a.kowalski@mail.com",
            Role = Role.Klient
        };
        //act
        var response = await _client.PostAsJsonAsync("/api/createNewUser", user);
        var guid = await response.Content.ReadFromJsonAsync<Guid>();
        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        await UsersTestsHelper.Delete(_factory, guid);
    }
    
    [Theory]
    [InlineData("d.kowalskigmailpl")]
    [InlineData("d.kowalski  gmail")]
    [InlineData("d.kowalski@@gmail")]
    public async Task CreateUser_ValidateEmail_ReturnStatusOk(string email)
    {
        //arrange
        var user = new User
        {
            FirstName = "Adam",
            LastName = "Kowalski",
            NumberPhone = "123456789",
            Email = email,
            Role = Role.Klient
        };
        //act
        var response = await _client.PostAsJsonAsync("/api/createNewUser", user);
        //assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task CreateUser_UserIsExist_ReturnStatusOk()
    {
        //arrange
        var user = new User
        {
            FirstName = "Adam",
            LastName = "Kowalski",
            NumberPhone = "123456789",
            Email = "a.kowalski@mail.com",
            Role = Role.Klient
        };
        //act
        var client = await UsersTestsHelper.Add(_factory);
        var response = await _client.PostAsJsonAsync("/api/createNewUser", user);
        //assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        await UsersTestsHelper.Delete(_factory, client.Id);
    }
    
}