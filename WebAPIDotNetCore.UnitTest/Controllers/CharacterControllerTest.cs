using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPIDotNetCore.Api.Clients;
using WebAPIDotNetCore.Api.Controllers;
using WebAPIDotNetCore.Api.Models;
using WebAPIDotNetCore.Domain.Entities;
using WebAPIDotNetCore.Domain.Interfaces.Services;
using Xunit;

namespace WebAPIDotNetCore.UnitTest.Controllers
{
    public class CharacterControllerTest
    {
        [Fact]
        public void Constructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var characterController = new CharacterController(null, null);
            });
        }

        [Fact]
        public async Task GetAsyncWhenNotExistsResult()
        {
            var potterApiClient = Substitute.For<IPotterAPIClient>();
            var characterService = Substitute.For<ICharacterService>();
            characterService.AllAsync().Returns(new List<Character>());

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.GetAsync() as NotFoundResult;

            Assert.NotNull(response);
            await characterService.Received().AllAsync();
        }

        [Fact]
        public async Task GetAsyncWhenExistsResult()
        {
            var potterApiClient = Substitute.For<IPotterAPIClient>();
            potterApiClient.GetHousesAsync().Returns(new List<HouseModel>
            {
                new HouseModel
                {
                    Id = "3824jgdjdg",
                    Name = "Lupa Lupa"
                }
            });

            var characterService = Substitute.For<ICharacterService>();
            characterService.AllAsync().Returns(new List<Character> {
                new Character
                {
                    Id = Guid.NewGuid(),
                    HouseId = "3824jgdjdg",
                    Name = "HP",
                    Patronus = "Cachorro",
                    Role = "Estudante",
                    School = "Hogwarts"
                }
            });

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.GetAsync() as OkObjectResult;

            Assert.NotNull(response);
            await characterService.Received().AllAsync();
            await potterApiClient.Received().GetHousesAsync();
        }

        [Fact]
        public async Task GetAsyncWithIdWhenNotExistsResult()
        {
            var id = Guid.NewGuid();

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            var characterService = Substitute.For<ICharacterService>();
            characterService.GetAsync(id).Returns((Character)null);

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.GetAsync(id) as NotFoundResult;

            Assert.NotNull(response);
            await characterService.Received().GetAsync(id);
        }

        [Fact]
        public async Task GetAsyncWithIdWhenExistsResult()
        {
            var id = Guid.NewGuid();
            var houseId = "3824jgdjdg";

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            potterApiClient.GetHouseAsync(houseId).Returns(new HouseModel
            {
                Id = houseId,
                Name = "Lupa Lupa"
            });

            var characterService = Substitute.For<ICharacterService>();
            characterService.GetAsync(id).Returns(new Character
            {
                Id = id,
                HouseId = houseId,
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            });

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.GetAsync(id) as OkObjectResult;

            Assert.NotNull(response);
            await characterService.Received().GetAsync(id);
            await potterApiClient.Received().GetHouseAsync(houseId);
        }

        [Fact]
        public async Task PostAsyncWhenNotExistsHouse()
        {
            var houseId = "3824jgdjdg";
            var characterModel = new CharacterModel
            {
                House = houseId,
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            potterApiClient.GetHouseAsync(houseId).Returns((HouseModel)null);

            var characterService = Substitute.For<ICharacterService>();

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.PostAsync(characterModel) as NotFoundObjectResult;

            Assert.NotNull(response);
            await potterApiClient.Received().GetHouseAsync(houseId);
        }

        [Fact]
        public async Task PostAsyncWhenExistsHouse()
        {
            var houseId = "3824jgdjdg";
            var characterModel = new CharacterModel
            {
                House = houseId,
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            potterApiClient.GetHouseAsync(houseId).Returns(new HouseModel
            {
                Id = houseId,
                Name = "Lupa Lupa"
            });

            var characterService = Substitute.For<ICharacterService>();

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.PostAsync(characterModel) as OkResult;

            Assert.NotNull(response);
            await characterService.ReceivedWithAnyArgs().AddAsync(default);
            await potterApiClient.Received().GetHouseAsync(houseId);
        }

        [Fact]
        public async Task PutAsyncWhenNotExistsHouse()
        {
            var houseId = "3824jgdjdg";
            var characterModel = new CharacterModel
            {
                House = houseId,
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            potterApiClient.GetHouseAsync(houseId).Returns((HouseModel)null);

            var characterService = Substitute.For<ICharacterService>();

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.PutAsync(Guid.NewGuid(), characterModel) as NotFoundObjectResult;

            Assert.NotNull(response);
            await potterApiClient.Received().GetHouseAsync(houseId);
        }

        [Fact]
        public async Task PutAsyncWhenExistsHouse()
        {
            var houseId = "3824jgdjdg";
            var characterModel = new CharacterModel
            {
                House = houseId,
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            potterApiClient.GetHouseAsync(houseId).Returns(new HouseModel
            {
                Id = houseId,
                Name = "Lupa Lupa"
            });

            var characterService = Substitute.For<ICharacterService>();

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.PutAsync(Guid.NewGuid(), characterModel) as OkResult;

            Assert.NotNull(response);
            await characterService.ReceivedWithAnyArgs().UpdateAsync(default, default);
            await potterApiClient.Received().GetHouseAsync(houseId);
        }

        [Fact]
        public async Task DeleteAsyncWhenNotExists()
        {
            var id = Guid.NewGuid();

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            var characterService = Substitute.For<ICharacterService>();
            characterService.DeleteAsync(id).Throws(new ArgumentNullException());

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.DeleteAsync(id) as NotFoundResult;

            Assert.NotNull(response);
            await characterService.Received().DeleteAsync(id);
        }

        [Fact]
        public async Task DeleteAsyncWhenExists()
        {
            var id = Guid.NewGuid();

            var potterApiClient = Substitute.For<IPotterAPIClient>();
            var characterService = Substitute.For<ICharacterService>();

            var characterController = new CharacterController(characterService, potterApiClient);
            var response = await characterController.DeleteAsync(id) as OkResult;

            Assert.NotNull(response);
            await characterService.Received().DeleteAsync(id);
        }
    }
}
