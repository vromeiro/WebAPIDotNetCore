using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIDotNetCore.Domain.Entities;

namespace WebAPIDotNetCore.Domain.Interfaces.Repositories
{
    public interface ICharacterRepository
    {
        Task<Character> GetAsync(Guid id);

        Task<IList<Character>> AllAsync(string houseId = null);

        Task AddAsync(Character character);

        Task UpdateAsync(Character character);

        Task DeleteAsync(Character character);
    }
}
