using HXGGVH_HFT_2021221.Data;
using HXGGVH_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Repository
{
    public class PokemonRepository
    {
        TrainerDbContext db;

        public PokemonRepository(TrainerDbContext db)
        {
            this.db = db;
        }

        //CRUD: Create, Read, ReadAll, Update, Delete

        public void Create(Pokemon pokemon)
        {
            db.Pokemons.Add(pokemon);
            db.SaveChanges();
        }

        public Pokemon Read(int id)
        {
            return db.Pokemons.FirstOrDefault(t => t.PokemonID == id);
        }

        public IQueryable<Pokemon> ReadAll()
        {
            return db.Pokemons;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Pokemon pokemon)
        {
            var oldPokemon = Read(pokemon.PokemonID);

            //ID;NAME;HP;ATK;DEF;SPEED;TYPE;TRAINERID
            oldPokemon.PokemonID = pokemon.PokemonID;
            oldPokemon.Name = pokemon.Name;
            oldPokemon.Health = pokemon.Health;
            oldPokemon.Attack = pokemon.Attack;
            oldPokemon.Defense = pokemon.Defense;
            oldPokemon.Speed = pokemon.Speed;
            oldPokemon.Type = pokemon.Type;
            oldPokemon.TrainerID = pokemon.TrainerID;

            db.SaveChanges();
        }
    }
}
