using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIDotNetCore.Domain.Entities;
using WebAPIDotNetCore.Domain.Interfaces.Repositories;

namespace WebAPIDotNetCore.Domain.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly HPContext context;

        public CharacterRepository(HPContext context)
        {
            this.context = context ?? throw new ArgumentNullException("context");
        }

        public async Task<IList<Character>> AllAsync(string houseId = null)
        {
            if (houseId == null)
                return await this.context.Characters.ToListAsync();
            else
                return await this.context.Characters.Where(c => c.HouseId == houseId).ToListAsync();
        }

        public async Task<Character> GetAsync(Guid id)
        {
            return await this.context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Character character)
        {
            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Character character)
        {
            this.context.Update(character);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Character character)
        {
            this.context.Characters.Remove(character);
            await this.context.SaveChangesAsync();
        }
    }
}
