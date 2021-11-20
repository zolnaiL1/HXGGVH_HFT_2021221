using HXGGVH_HFT_2021221.Models;
using HXGGVH_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Logic
{
    public class PokemonLogic : IPokemonLogic
    {
        IPokemonRepository pokemonRepo;
        ITrainerRepository trainerRepo;
        IRegionRepository regionRepo;

        public PokemonLogic(IPokemonRepository pokemonRepo, ITrainerRepository trainerRepo, IRegionRepository regionRepo)
        {
            this.pokemonRepo = pokemonRepo;
            this.trainerRepo = trainerRepo;
            this.regionRepo = regionRepo;
        }


        //CRUD: Create, Read, ReadAll, Update, Delete
        public void Create(Pokemon pokemon)
        {
            if (pokemon.Name == "")
            {
                throw new ArgumentException("Name is null!");
            }
            pokemonRepo.Create(pokemon);
        }

        public Pokemon Read(int id)
        {
            if (id < pokemonRepo.ReadAll().Count() && id > 0)
                return pokemonRepo.Read(id);
            else
                throw new IndexOutOfRangeException("This ID is non existent.");         
        }

        public IQueryable<Pokemon> ReadAll()
        {
            return pokemonRepo.ReadAll();
        }

        public void Delete(int id)
        {
            pokemonRepo.Delete(id);
        }

        public void Update(Pokemon pokemon)
        {
            pokemonRepo.Update(pokemon);
        }

        //NON-CRUD
        //1
        public IEnumerable<Pokemon> PokemonsInKantoRegion()
        {
            var q = from pokemons in pokemonRepo.ReadAll()
                    join trainers in trainerRepo.ReadAll()
                    on pokemons.TrainerID equals trainers.TrainerID
                    join regions in regionRepo.ReadAll()
                    on trainers.RegionID equals regions.RegionID
                    where regions.Name == "Kanto"
                    select pokemons;
            
            return q;
        }
        //2
        public IEnumerable<Pokemon> PokemonsWhereTrainerWinIs10()
        {
            int wins = 10;
            var q = from pokemons in pokemonRepo.ReadAll()
                    join trainers in trainerRepo.ReadAll()
                    on pokemons.TrainerID equals trainers.TrainerID
                    where trainers.Wins >= wins
                    select pokemons;

            return q;
        }
        //3
        public IEnumerable<Pokemon> PokemonsWhereTrainerLevelUnder10()
        {
            int level = 10;
            var q = from pokemons in pokemonRepo.ReadAll()
                    join trainers in trainerRepo.ReadAll()
                    on pokemons.TrainerID equals trainers.TrainerID
                    where trainers.Level < level
                    select pokemons;

            return q;
        }
    }
}
