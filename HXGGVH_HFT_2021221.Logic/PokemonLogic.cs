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
        public PokemonLogic(IPokemonRepository pokemonRepo)
        {
            this.pokemonRepo = pokemonRepo;
        }

        //CRUD: Create, Read, ReadAll, Update, Delete
        public void Create(Pokemon pokemon)
        {
            if (pokemon.Name == null)
            {
                throw new ArgumentException("Name is null!");
            }
            pokemonRepo.Create(pokemon);
        }

        public Pokemon Read(int id)
        {
            return pokemonRepo.Read(id);
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
    }
}
