using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
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

        public PokemonController(IPokemonLogic pokeLogic)
        {
            this.pokeLogic = pokeLogic;
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
        }

        // PUT 
        [HttpPut]
        public void Put([FromBody] Pokemon value)
        {
            pokeLogic.Update(value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            pokeLogic.Delete(id);
        }
    }
}
