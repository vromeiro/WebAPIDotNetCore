using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIDotNetCore.Domain.Entities;
using WebAPIDotNetCore.Domain.Extensions;
using WebAPIDotNetCore.Domain.Interfaces.Repositories;
using WebAPIDotNetCore.Domain.Interfaces.Services;

namespace WebAPIDotNetCore.Domain.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository characterRepository;

        public CharacterService(ICharacterRepository characterRepository)
        {
            this.characterRepository = characterRepository ?? throw new ArgumentNullException("characterRepository");
        }

        public Task<IList<Character>> AllAsync(string houseId = null)
        {
            return this.characterRepository.AllAsync(houseId);
        }

        public Task<Character> GetAsync(Guid id)
        {
            return this.characterRepository.GetAsync(id);
        }

        public Task AddAsync(Character character)
        {
            return this.characterRepository.AddAsync(character);
        }

        public async Task UpdateAsync(Guid id, Character newCharacter)
        {
            var oldCharacter = await GetAsync(id);
            if (oldCharacter == null) throw new ArgumentNullException("oldCharacter");

            oldCharacter.CopyValues(newCharacter);

            await this.characterRepository.UpdateAsync(oldCharacter);
        }

        public async Task DeleteAsync(Guid id)
        {
            var character = await GetAsync(id);
            if (character == null) throw new ArgumentNullException("character");

            await this.characterRepository.DeleteAsync(character);
        }
    }
}
