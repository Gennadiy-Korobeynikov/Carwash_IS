using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarwashClient.Controllers;
using CarwashClient.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.Protected;
using static CarwashClient.Tests.ClientsControllerTests;


namespace CarwashClient.Tests
{
    public class ClientsControllerTests
    {
        [Fact]
        public async Task Create_Post_SuccessfulResponse_RedirectsToIndex()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();


            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", // защищённый метод, который вызывает HttpClient
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Created
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var controller = new ClientsController(new FakeHttpClientFactory(httpClient));

            var clientVM = new ClientVM
            {
                Id = 1,
                LastName = "Test",
                FirstName = "Test",
                MidName = "Test",
                CarInfo = "Test",
                Name = "Test",
                Preferences = "Test",
                TelNumber = "Test",
            };

            // Act
            var result = await controller.Create(clientVM);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }


        public class FakeHttpClientFactory : IHttpClientFactory
        {
            private readonly HttpClient _client;
            public FakeHttpClientFactory(HttpClient client) => _client = client;
            public HttpClient CreateClient(string name = null) => _client;
        }




        [Fact]
        public async Task Create_Post_FailedResponse_ViewWithModelError()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var clientController = new ClientsController(new FakeHttpClientFactory(httpClient));

            var clientVM = new ClientVM
            {
                Id = 1,
                LastName = "Test",
                FirstName = "Test",
                MidName = "Test",
                CarInfo = "Test",
                Name = "Test",
                Preferences = "Test",
                TelNumber = "Test",
            };


            // Act 
            var result = await clientController.Create(clientVM);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(clientVM, viewResult.Model);
            Assert.False(clientController.ModelState.IsValid);



        }
    }




}