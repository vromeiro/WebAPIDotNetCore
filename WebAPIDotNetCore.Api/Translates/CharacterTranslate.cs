using System;
using WebAPIDotNetCore.Api.Models;
using WebAPIDotNetCore.Domain.Entities;

namespace WebAPIDotNetCore.Api.Translates
{
    public static class CharacterTranslate
    {
        public static Character To(CharacterModel characterModel, Guid? id = null)
        {
            if (characterModel == null) return null;

            return new Character
            {
                Id = id ?? Guid.NewGuid(),
                HouseId = characterModel.House,
                Name = characterModel.Name,
                Patronus = characterModel.Patronus,
                Role = characterModel.Role,
                School = characterModel.School
            };
        }

        public static CharacterGetModel To(Character character)
        {
            if (character == null) return null;

            return new CharacterGetModel
            {
                Id = character.Id,
                Name = character.Name,
                Patronus = character.Patronus,
                Role = character.Role,
                School = character.School
            };
        }
    }
}
