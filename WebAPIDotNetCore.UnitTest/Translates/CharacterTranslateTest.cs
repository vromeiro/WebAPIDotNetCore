using System;
using WebAPIDotNetCore.Api.Models;
using WebAPIDotNetCore.Api.Translates;
using WebAPIDotNetCore.Domain.Entities;
using Xunit;

namespace WebAPIDotNetCore.UnitTest.Translates
{
    public class CharacterTranslateTest
    {
        [Fact]
        public void ModelToEntityWhenModelIsNull()
        {
            var character = CharacterTranslate.To((CharacterModel)null);
            Assert.Null(character);
        }

        [Fact]
        public void ModelToEntity()
        {
            var characterModel = new CharacterModel
            {
                House = "3824jgdjdg",
                Name = "HP",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var character = CharacterTranslate.To(characterModel);

            Assert.Equal("3824jgdjdg", character.HouseId);
            Assert.Equal("HP", character.Name);
            Assert.Equal("Cachorro", character.Patronus);
            Assert.Equal("Estudante", character.Role);
            Assert.Equal("Hogwarts", character.School);
        }

        [Fact]
        public void EntityToModelWhenEntityIsNull()
        {
            var characterModel = CharacterTranslate.To((Character)null);
            Assert.Null(characterModel);
        }

        [Fact]
        public void EntityToModel()
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

            var characterModel = CharacterTranslate.To(character);

            Assert.Equal(characterModel.Id, id);
            Assert.Null(characterModel.House);
            Assert.Equal("HP", characterModel.Name);
            Assert.Equal("Cachorro", characterModel.Patronus);
            Assert.Equal("Estudante", characterModel.Role);
            Assert.Equal("Hogwarts", characterModel.School);
        }
    }
}
