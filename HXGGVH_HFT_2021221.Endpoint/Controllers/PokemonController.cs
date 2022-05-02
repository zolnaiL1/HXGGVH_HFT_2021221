using HXGGVH_HFT_2021221.Endpoint.Services;
using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HXGGVH_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        IPokemonLogic pokeLogic;

        IHubContext<SignalRHub> hub;

        public PokemonController(IPokemonLogic pokeLogic, IHubContext<SignalRHub> hub)
        {
            this.pokeLogic = pokeLogic;
            this.hub = hub;
        }

        // GETALL
        [HttpGet]
        public IEnumerable<Pokemon> Get()
        {
            return pokeLogic.ReadAll();
        }

        // GET 
        [HttpGet("{id}")]
        public Pokemon Get(int id)
        {
            return pokeLogic.Read(id);
        }

        // POST 
        [HttpPost]
        public void Post([FromBody] Pokemon value)
        {
            pokeLogic.Create(value);
            this.hub.Clients.All.SendAsync("PokemonCreated", value);
        }

        // PUT 
        [HttpPut]
        public void Put([FromBody] Pokemon value)
        {
            pokeLogic.Update(value);
            this.hub.Clients.All.SendAsync("PokemonUpdated", value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var pokemonToDelete = this.pokeLogic.Read(id);
            pokeLogic.Delete(id);
            this.hub.Clients.All.SendAsync("PokemonDeleted", pokemonToDelete);
        }
    }
}
