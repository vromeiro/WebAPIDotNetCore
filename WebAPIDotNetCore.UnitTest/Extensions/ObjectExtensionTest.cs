using System;
using WebAPIDotNetCore.Domain.Entities;
using WebAPIDotNetCore.Domain.Extensions;
using Xunit;

namespace WebAPIDotNetCore.UnitTest.Extensions
{
    public class ObjectExtensionTest
    {
        [Fact]
        public void CopyValuesWhenSuccess()
        {
            var id = Guid.NewGuid();

            var oldCharacter = new Character
            {
                Id = id,
                HouseId = "ahahsao2342",
                Name = "Harry Potter",
                Patronus = "Gato",
                Role = "Estudante",
                School = "Hogwarts"
            };

            var newCharacter = new Character
            {
                Id = id,
                HouseId = "9824jjsfa",
                Name = "Harry Potter",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            oldCharacter.CopyValues(newCharacter);

            Assert.Equal(oldCharacter.Id, id);
            Assert.Equal("9824jjsfa", oldCharacter.HouseId);
            Assert.Equal("Harry Potter", oldCharacter.Name);
            Assert.Equal("Cachorro", oldCharacter.Patronus);
            Assert.Equal("Estudante", oldCharacter.Role);
            Assert.Equal("Hogwarts", oldCharacter.School);
        }

        [Fact]
        public void CopyValuesWhenNewObjectIsNull()
        {
            var id = Guid.NewGuid();

            var oldCharacter = new Character
            {
                Id = id,
                HouseId = "ahahsao2342",
                Name = "Harry Potter",
                Patronus = "Gato",
                Role = "Estudante",
                School = "Hogwarts"
            };

            Character newCharacter = null;

            oldCharacter.CopyValues(newCharacter);

            Assert.Equal(oldCharacter.Id, id);
            Assert.Equal("ahahsao2342", oldCharacter.HouseId);
            Assert.Equal("Harry Potter", oldCharacter.Name);
            Assert.Equal("Gato", oldCharacter.Patronus);
            Assert.Equal("Estudante", oldCharacter.Role);
            Assert.Equal("Hogwarts", oldCharacter.School);
        }

        [Fact]
        public void CopyValuesWhenOldObjectIsNull()
        {
            var id = Guid.NewGuid();

            Character oldCharacter = null;

            var newCharacter = new Character
            {
                Id = id,
                HouseId = "9824jjsfa",
                Name = "Harry Potter",
                Patronus = "Cachorro",
                Role = "Estudante",
                School = "Hogwarts"
            };

            oldCharacter.CopyValues(newCharacter);

            Assert.Null(oldCharacter);
        }

        [Fact]
        public void CopyValuesWhenOldObjectAndNewObjectIsNull()
        {
            var id = Guid.NewGuid();

            Character oldCharacter = null;

            Character newCharacter = null;

            oldCharacter.CopyValues(newCharacter);

            Assert.Null(oldCharacter);
        }
    }
}
