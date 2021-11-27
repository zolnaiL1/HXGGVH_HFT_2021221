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
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IPokemonLogic pokemonLogic;
        IRegionLogic regionLogic;

        public StatController(IPokemonLogic pokemonLogic, IRegionLogic regionLogic)
        {
            this.pokemonLogic = pokemonLogic;
            this.regionLogic = regionLogic;
        }

        [HttpGet]
        public IEnumerable<Pokemon> PokemonsInKantoRegion()
        {
            return pokemonLogic.PokemonsInKantoRegion();
        }

        [HttpGet]
        public IEnumerable<Pokemon> PokemonsWhereTrainerWinIs10()
        {
            return pokemonLogic.PokemonsWhereTrainerWinIs10();
        }

        [HttpGet]
        public IEnumerable<Pokemon> PokemonsWhereTrainerLevelUnder10()
        {
            return pokemonLogic.PokemonsWhereTrainerLevelUnder10();
        }

        [HttpGet]
        public IEnumerable<Region> RegionWherePikachuLives()
        {
            return regionLogic.RegionWherePikachuLives();
        }

        [HttpGet]
        public IEnumerable<Region> RegionWherePokemonsTypeIsWater()
        {
            return regionLogic.RegionWherePokemonsTypeIsWater();
        }
    }
}
