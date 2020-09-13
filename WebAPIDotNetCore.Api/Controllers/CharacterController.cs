using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIDotNetCore.Api.Clients;
using WebAPIDotNetCore.Api.Models;
using WebAPIDotNetCore.Api.Translates;
using WebAPIDotNetCore.Domain.Interfaces.Services;

namespace WebAPIDotNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;
        private readonly IPotterAPIClient potterAPIClient;

        public CharacterController(ICharacterService characterService, IPotterAPIClient potterAPIClient)
        {
            this.characterService = characterService ?? throw new ArgumentNullException("characterService");
            this.potterAPIClient = potterAPIClient ?? throw new ArgumentNullException("potterAPIClient");
        }

        /// <summary>
        /// Endpoint que retorna todos os personagens
        /// </summary>
        /// <param name="house">Id da casa</param>
        /// <returns>Personagens</returns>
        // GET: api/Character
        [HttpGet]
        public async Task<IActionResult> GetAsync(string house = null)
        {
            var characters = await this.characterService.AllAsync(house);
            if (characters?.Count == 0) return NotFound();

            var houses = await this.potterAPIClient.GetHousesAsync();
            var characterModels = new List<CharacterGetModel>();
            foreach (var item in characters)
            {
                var characterModel = CharacterTranslate.To(item);
                characterModel.House = houses?.FirstOrDefault(h => h.Id == item.HouseId);
                characterModels.Add(characterModel);
            }

            return Ok(characterModels);
        }

        /// <summary>
        /// Endpoint que retorna o personagem com o Id enviado
        /// </summary>
        /// <param name="id">Id do personagem</param>
        /// <returns>Personagem</returns>
        // GET: api/Character/ee5ca872-39ca-4083-aa51-444aadc73a9d
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var character = await this.characterService.GetAsync(id);
            if (character == null) return NotFound();

            var characterModel = CharacterTranslate.To(character);
            characterModel.House = await this.potterAPIClient.GetHouseAsync(character.HouseId);

            return Ok(characterModel);
        }

        /// <summary>
        /// Endpoint que cria um personagem
        /// </summary>
        /// <param name="characterModel">Personagem</param>
        /// <returns></returns>
        // POST: api/Character
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CharacterModel characterModel)
        {
            var houseModel = await this.potterAPIClient.GetHouseAsync(characterModel.House);
            if (houseModel?.Id == null) return NotFound("House not exists.");

            var character = CharacterTranslate.To(characterModel);
            await this.characterService.AddAsync(character);

            return Ok();
        }

        /// <summary>
        /// Endpoint que atualiza os dados do personagem
        /// </summary>
        /// <param name="id">Id do personagem</param>
        /// <param name="characterModel">Personagem</param>
        /// <returns></returns>
        // PUT: api/Character/ee5ca872-39ca-4083-aa51-444aadc73a9d
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] CharacterModel characterModel)
        {
            try
            {
                var houseModel = await this.potterAPIClient.GetHouseAsync(characterModel.House);
                if (houseModel?.Id == null) return NotFound("House not exists.");

                var character = CharacterTranslate.To(characterModel, id);
                await this.characterService.UpdateAsync(id, character);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Endpoint que deleta o personagem
        /// </summary>
        /// <param name="id">Id do personagem</param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/ee5ca872-39ca-4083-aa51-444aadc73a9d
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await this.characterService.DeleteAsync(id);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }
    }
}
