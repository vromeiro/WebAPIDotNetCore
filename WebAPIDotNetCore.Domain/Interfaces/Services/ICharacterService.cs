using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIDotNetCore.Domain.Entities;

namespace WebAPIDotNetCore.Domain.Interfaces.Services
{
    public interface ICharacterService
    {
        Task<Character> GetAsync(Guid id);

        Task<IList<Character>> AllAsync(string houseId = null);

        Task AddAsync(Character character);

        Task UpdateAsync(Guid id, Character newCharacter);

        Task DeleteAsync(Guid id);
    }
}
