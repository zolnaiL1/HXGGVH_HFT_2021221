using HXGGVH_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Data
{
    //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HXGGVH_HFT_2021221.Database.mdf;Integrated Security=True

    public class TrainerDbContext : DbContext
    {
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Pokemon> Pokemons { get; set; }

        public TrainerDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().
                UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HXGGVH_HFT_2021221.Database.mdf; Integrated Security=True; MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasOne(trainer => trainer.Region)
                    .WithMany(region => region.Trainers)
                    .HasForeignKey(trainer => trainer.RegionID)
                    //.OnDelete(DeleteBehavior.ClientSetNull);
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasOne(pokemon => pokemon.Trainer)
                    .WithMany(trainer => trainer.Pokemons)
                    .HasForeignKey(pokemon => pokemon.TrainerID)
                    //.OnDelete(DeleteBehavior.ClientSetNull);
                    .OnDelete(DeleteBehavior.Cascade);
            });

            
            //ID;NAME
            Region Kanto = new Region() { RegionID = 1, Name = "Kanto" };
            Region Johto = new Region() { RegionID = 2, Name = "Johto" };
            Region Hoenn = new Region() { RegionID = 3, Name = "Hoenn" };

            //ID;NAME;WINS;LEVEL;REGIONID
            Trainer Trainer1 = new Trainer() { TrainerID = 1, Name = "Ash Ketchum", Wins = 32, Level = 20, RegionID = 1};
            Trainer Trainer2 = new Trainer() { TrainerID = 2, Name = "Brock", Wins = 12, Level = 14, RegionID = 1 };
            Trainer Trainer3 = new Trainer() { TrainerID = 3, Name = "Misty", Wins = 6, Level = 2, RegionID = 2 };
            Trainer Trainer4 = new Trainer() { TrainerID = 4, Name = "Rosa", Wins = 15, Level = 9, RegionID = 2 };
            Trainer Trainer5 = new Trainer() { TrainerID = 5, Name = "Wallace", Wins = 9, Level = 7, RegionID = 3 };
            Trainer Trainer6 = new Trainer() { TrainerID = 6, Name = "Morty", Wins = 11, Level = 14, RegionID = 3 };

            //ID;NAME;HP;ATK;DEF;SPEED;TYPE;TRAINERID
            Pokemon Pokemon1 = new Pokemon() { PokemonID = 1, Name = "Pikachu", Health = 30, Attack = 40, Defense = 30, Speed = 6, Type = "Electric", TrainerID = 1 };
            Pokemon Pokemon2 = new Pokemon() { PokemonID = 2, Name = "Charizard", Health = 50, Attack = 50, Defense = 50, Speed = 6, Type = "Fire,Flying", TrainerID = 1 };
            Pokemon Pokemon3 = new Pokemon() { PokemonID = 3, Name = "Rhydon", Health = 70, Attack = 80, Defense = 80, Speed = 3, Type = "Ground,Rock", TrainerID = 2 };
            Pokemon Pokemon4 = new Pokemon() { PokemonID = 4, Name = "Steelix", Health = 50, Attack = 50, Defense = 120, Speed = 2, Type = "Steel,Ground", TrainerID = 2 };
            Pokemon Pokemon5 = new Pokemon() { PokemonID = 5, Name = "Bulbasaur", Health = 30, Attack = 30, Defense = 30, Speed = 3, Type = "Grass,Posion", TrainerID = 3 };
            Pokemon Pokemon6 = new Pokemon() { PokemonID = 6, Name = "Blastoise", Health = 50, Attack = 50, Defense = 60, Speed = 5, Type = "Water", TrainerID = 3 };
            Pokemon Pokemon7 = new Pokemon() { PokemonID = 7, Name = "Pidgeotto", Health = 40, Attack = 40, Defense = 40, Speed = 5, Type = "Normal,Flying", TrainerID = 4 };
            Pokemon Pokemon8 = new Pokemon() { PokemonID = 8, Name = "Clefairy", Health = 50, Attack = 30, Defense = 30, Speed = 3, Type = "Fairy", TrainerID = 4 };
            Pokemon Pokemon9 = new Pokemon() { PokemonID = 9, Name = "Machoke", Health = 50, Attack = 60, Defense = 50, Speed = 3, Type = "Fighting", TrainerID = 5 };
            Pokemon Pokemon10 = new Pokemon() { PokemonID = 10, Name = "Slowpoke", Health = 60, Attack = 40, Defense = 40, Speed = 1, Type = "Water,Psychic", TrainerID = 5 };
            Pokemon Pokemon11 = new Pokemon() { PokemonID = 11, Name = "Gengar", Health = 40, Attack = 40, Defense = 40, Speed = 7, Type = "Ghost,Poison", TrainerID = 6 };
            Pokemon Pokemon12 = new Pokemon() { PokemonID = 12, Name = "Gyarados", Health = 60, Attack = 80, Defense = 40, Speed = 5, Type = "Water,Flying", TrainerID = 6 };

            //*************************************************
            modelBuilder.Entity<Region>().HasData(Kanto, Johto, Hoenn);
            modelBuilder.Entity<Trainer>().HasData(Trainer1, Trainer2, Trainer3, Trainer4, Trainer5, Trainer6);
            modelBuilder.Entity<Pokemon>().HasData(Pokemon1, Pokemon2, Pokemon3, Pokemon4, Pokemon5, Pokemon6, Pokemon7, Pokemon8, Pokemon9, Pokemon10, Pokemon11, Pokemon12);
        }
    }
}
