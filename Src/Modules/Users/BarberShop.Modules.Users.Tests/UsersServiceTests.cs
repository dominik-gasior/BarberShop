using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BarberShop.Modules.Users.Tests;

public class UsersServiceTests
{
    private readonly HttpClient _client;

    public UsersServiceTests()
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateDefaultClient();
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
    // [Theory]
    // [InlineData("123")]
    // public async Task GetUserById_FetchUser_ReturnStatusOk(string id)
    // {
    //     //act
    //     var response = await _client.GetAsync("/api/user/" + id);
    //
    //     //assert
    //     response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    //     // response.Content.Should().NotBeNull();
    // }
}