using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using WebAPIDotNetCore.Domain.Entities;
using WebAPIDotNetCore.Domain.Interfaces.Repositories;
using WebAPIDotNetCore.Domain.Services;
using Xunit;

namespace WebAPIDotNetCore.UnitTest.Services
{
    public class CharacterServiceTest
    {
        [Fact]
        public void Constructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var characterService = new CharacterService(null);
            });
        }

        [Fact]
        public async Task AllAsyncWhenNotExistsResult()
        {
            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.AllAsync().Returns(new List<Character>());

            var characterService = new CharacterService(characterRepository);
            var characters = await characterService.AllAsync();

            Assert.Empty(characters);
            await characterRepository.Received().AllAsync();
        }

        [Fact]
        public async Task AllAsyncWhenExistsResult()
        {
            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.AllAsync().Returns(new List<Character> {
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

            var characterService = new CharacterService(characterRepository);
            var characters = await characterService.AllAsync();

            Assert.NotEmpty(characters);
            await characterRepository.Received().AllAsync();
        }

        [Fact]
        public async Task GetAsyncWhenNotExistsResult()
        {
            var id = Guid.NewGuid();

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns((Character)null);

            var characterService = new CharacterService(characterRepository);
            var character = await characterService.GetAsync(id);

            Assert.Null(character);
            await characterRepository.Received().GetAsync(id);
        }

        [Fact]
        public async Task GetAsyncWhenExistsResult()
        {
            var id = Guid.NewGuid();

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns(new Character
            {
                Id = Guid.NewGuid(),
                HouseId = "3824jgdjdg",
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            });

            var characterService = new CharacterService(characterRepository);
            var character = await characterService.GetAsync(id);

            Assert.NotNull(character);
            await characterRepository.Received().GetAsync(id);
        }

        [Fact]
        public async Task AddAsyncWhenCharacterIsNull()
        {
            var characterRepository = Substitute.For<ICharacterRepository>();

            var characterService = new CharacterService(characterRepository);
            await characterService.AddAsync(null);

            await characterRepository.Received().AddAsync(null);
        }

        [Fact]
        public async Task AddAsyncWhenCharacterIsNotNull()
        {
            var character = new Character
            {
                Id = Guid.NewGuid(),
                HouseId = "3824jgdjdg",
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var characterRepository = Substitute.For<ICharacterRepository>();

            var characterService = new CharacterService(characterRepository);
            await characterService.AddAsync(character);

            await characterRepository.Received().AddAsync(character);
        }

        [Fact]
        public async Task UpdateAsyncWhenIdNotExists()
        {
            var id = Guid.NewGuid();

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns((Character)null);

            var characterService = new CharacterService(characterRepository);
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await characterService.UpdateAsync(id, null);
            });

            await characterRepository.Received().GetAsync(id);
        }

        [Fact]
        public async Task UpdateAsyncWhenIdExistsAndCharacterNull()
        {
            var id = Guid.NewGuid();
            var character = new Character
            {
                Id = id,
                HouseId = "3824jgdjdg",
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns(character);

            var characterService = new CharacterService(characterRepository);
            await characterService.UpdateAsync(id, null);

            await characterRepository.Received().GetAsync(id);
            await characterRepository.Received().UpdateAsync(character);
        }

        [Fact]
        public async Task UpdateAsyncWhenIdExistsAndCharacterWasChange()
        {
            var id = Guid.NewGuid();
            var character = new Character
            {
                Id = id,
                HouseId = "3824jgdjdg",
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var newCharacter = new Character
            {
                Id = id,
                HouseId = "3824jgdjdg",
                Name = "HP",
                Patronus = "Tubarão",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns(character);

            var characterService = new CharacterService(characterRepository);
            await characterService.UpdateAsync(id, newCharacter);

            await characterRepository.Received().GetAsync(id);
            await characterRepository.Received().UpdateAsync(character);
        }

        [Fact]
        public async Task DeleteAsyncWhenIdNotExists()
        {
            var id = Guid.NewGuid();

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns((Character)null);

            var characterService = new CharacterService(characterRepository);
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await characterService.DeleteAsync(id);
            });

            await characterRepository.Received().GetAsync(id);
        }

        [Fact]
        public async Task DeleteAsyncWhenIdExists()
        {
            var id = Guid.NewGuid();
            var character = new Character
            {
                Id = id,
                HouseId = "3824jgdjdg",
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var characterRepository = Substitute.For<ICharacterRepository>();
            characterRepository.GetAsync(id).Returns(character);

            var characterService = new CharacterService(characterRepository);
            await characterService.DeleteAsync(id);

            await characterRepository.Received().GetAsync(id);
            await characterRepository.Received().DeleteAsync(character);
        }
    }
}
