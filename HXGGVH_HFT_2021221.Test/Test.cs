using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Models;
using HXGGVH_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HXGGVH_HFT_2021221.Test
{
    [TestFixture]
    public class Test
    {
        private PokemonLogic pokeLogic { get; set; }
        private RegionLogic regionLogic { get; set; }
        private TrainerLogic trainerLogic { get; set; }

        [SetUp]
        public void Setup()
        {
            Mock<IPokemonRepository> mockPokemonRepo = new Mock<IPokemonRepository>();
            Mock<ITrainerRepository> mockTrainerRepo = new Mock<ITrainerRepository>();
            Mock<IRegionRepository> mockRegionRepo = new Mock<IRegionRepository>();

            mockPokemonRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(
           new Pokemon()
           {
               PokemonID = 1,
               Name = "Pikachu",
               Health = 30,
               Attack = 40,
               Defense = 30,
               Speed = 6,
               Type = "Electric"
           });

            mockPokemonRepo.Setup(t => t.ReadAll()).Returns(FakePokemonObjects);
            mockTrainerRepo.Setup(t => t.ReadAll()).Returns(FakeTrainerObjects);
            mockRegionRepo.Setup(t => t.ReadAll()).Returns(FakeRegionObjects);

            this.pokeLogic = new PokemonLogic(mockPokemonRepo.Object, mockTrainerRepo.Object, mockRegionRepo.Object);

            this.regionLogic = new RegionLogic(mockRegionRepo.Object, mockPokemonRepo.Object, mockTrainerRepo.Object);

            this.trainerLogic = new TrainerLogic(mockTrainerRepo.Object);
        }

        [Test]
        public void GetOnePokemon_ReturnsCorrectInstance()
        {
            var pokemon = this.pokeLogic.Read(1);
            Assert.That(pokemon.Type, Is.EqualTo("Electric"));
        }

        [Test]
        public void GetAllPokemon_ReturnsExactNumberOfInstances()
        {
            Assert.That(this.pokeLogic.ReadAll().Count, Is.EqualTo(12));
        }

        [Test]
        public void PokemonsInKantoRegion_Test()
        {
            Assert.That(this.pokeLogic.PokemonsInKantoRegion().Count, Is.EqualTo(4));
        }

        [Test]
        public void PokemonsWhereTrainerWinIs10_Test()
        {
            Assert.That(this.pokeLogic.PokemonsWhereTrainerWinIs10().First().Trainer.Wins, Is.GreaterThanOrEqualTo(10));
        }

        [Test]
        public void PokemonsWhereTrainerLevelUnder10_Test()
        {
            Assert.That(this.pokeLogic.PokemonsWhereTrainerLevelUnder10().First().Trainer.Level, Is.LessThan(10));
        }

        [Test]
        public void RegionWherePikachuLives_Test()
        {
            Assert.That(this.regionLogic.RegionWherePikachuLives().First().Name, Is.EqualTo("Kanto"));
            Assert.That(this.regionLogic.RegionWherePikachuLives().Count(), Is.EqualTo(1));
        }

        [Test]
        public void RegionWherePokemonsTypeIsWater_Test()
        {
            Assert.That(this.regionLogic.RegionWherePokemonsTypeIsWater().Count(), Is.EqualTo(2));
        }

        [TestCase(-1)]
        [TestCase(99)]
        public void ReadOneException_Test(int ID)
        {
            Assert.That(() => this.pokeLogic.Read(ID), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [TestCase("", false)]
        [TestCase("Raichu", true)]
        public void PokemonCreateException_Test(string Name, bool canCreate)
        {
            if (canCreate)
            {
                Assert.That(() => this.pokeLogic.Create(new Pokemon() { Name = Name }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => this.pokeLogic.Create(new Pokemon() { Name = Name }), Throws.Exception);
            }
            
        }

        [TestCase("", false)]
        [TestCase("Sinnoh", true)]
        public void RegionCreateException_Test(string Name, bool canCreate)
        {
            if (canCreate)
            {
                Assert.That(() => this.regionLogic.Create(new Region() { Name = Name }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => this.regionLogic.Create(new Region() { Name = Name }), Throws.Exception);
            }

        }

        [TestCase("", false)]
        [TestCase("Leon", true)]
        public void TrainerCreateException_Test(string Name, bool canCreate)
        {
            if (canCreate)
            {
                Assert.That(() => this.trainerLogic.Create(new Trainer() { Name = Name }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => this.trainerLogic.Create(new Trainer() { Name = Name }), Throws.Exception);
            }

        }


        //FakeObject methods
        private IQueryable<Pokemon> FakePokemonObjects()
        {
            Region Kanto = new Region() { RegionID = 1, Name = "Kanto" };
            Region Johto = new Region() { RegionID = 2, Name = "Johto" };
            Region Hoenn = new Region() { RegionID = 3, Name = "Hoenn" };

            Kanto.Trainers = new List<Trainer>();
            Johto.Trainers = new List<Trainer>();
            Hoenn.Trainers = new List<Trainer>();

            //------------------------------------------------------------

            Trainer Trainer1 = new Trainer() { TrainerID = 1, Name = "Ash Ketchum", Wins = 32, Level = 20, RegionID = 1 };
            Trainer Trainer2 = new Trainer() { TrainerID = 2, Name = "Brock", Wins = 12, Level = 14, RegionID = 1 };
            Trainer Trainer3 = new Trainer() { TrainerID = 3, Name = "Misty", Wins = 6, Level = 2, RegionID = 2 };
            Trainer Trainer4 = new Trainer() { TrainerID = 4, Name = "Rosa", Wins = 15, Level = 9, RegionID = 2 };
            Trainer Trainer5 = new Trainer() { TrainerID = 5, Name = "Wallace", Wins = 9, Level = 7, RegionID = 3 };
            Trainer Trainer6 = new Trainer() { TrainerID = 6, Name = "Morty", Wins = 11, Level = 14, RegionID = 3 };

            Trainer1.Region = Kanto;
            Trainer2.Region = Kanto;
            Trainer3.Region = Johto;
            Trainer4.Region = Johto;
            Trainer5.Region = Hoenn;
            Trainer6.Region = Hoenn;

            Trainer1.RegionID = Kanto.RegionID; Kanto.Trainers.Add(Trainer1);
            Trainer2.RegionID = Kanto.RegionID; Kanto.Trainers.Add(Trainer2);
            Trainer3.RegionID = Johto.RegionID; Johto.Trainers.Add(Trainer3);
            Trainer4.RegionID = Johto.RegionID; Johto.Trainers.Add(Trainer4);
            Trainer5.RegionID = Hoenn.RegionID; Hoenn.Trainers.Add(Trainer5);
            Trainer6.RegionID = Hoenn.RegionID; Hoenn.Trainers.Add(Trainer6);

            //------------------------------------------------------------

            Trainer1.Pokemons = new List<Pokemon>();
            Trainer2.Pokemons = new List<Pokemon>();
            Trainer3.Pokemons = new List<Pokemon>();
            Trainer4.Pokemons = new List<Pokemon>();
            Trainer5.Pokemons = new List<Pokemon>();
            Trainer6.Pokemons = new List<Pokemon>();

            //------------------------------------------------------------

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

            Pokemon1.Trainer = Trainer1;
            Pokemon2.Trainer = Trainer1;
            Pokemon3.Trainer = Trainer2;
            Pokemon4.Trainer = Trainer2;
            Pokemon5.Trainer = Trainer3;
            Pokemon6.Trainer = Trainer3;
            Pokemon7.Trainer = Trainer4;
            Pokemon8.Trainer = Trainer4;
            Pokemon9.Trainer = Trainer5;
            Pokemon10.Trainer = Trainer5;
            Pokemon11.Trainer = Trainer6;
            Pokemon12.Trainer = Trainer6;

            Pokemon1.TrainerID = Trainer1.TrainerID; Trainer1.Pokemons.Add(Pokemon1);
            Pokemon2.TrainerID = Trainer1.TrainerID; Trainer1.Pokemons.Add(Pokemon2);
            Pokemon3.TrainerID = Trainer2.TrainerID; Trainer2.Pokemons.Add(Pokemon3);
            Pokemon4.TrainerID = Trainer2.TrainerID; Trainer2.Pokemons.Add(Pokemon4);
            Pokemon5.TrainerID = Trainer3.TrainerID; Trainer3.Pokemons.Add(Pokemon5);
            Pokemon6.TrainerID = Trainer3.TrainerID; Trainer3.Pokemons.Add(Pokemon6);
            Pokemon7.TrainerID = Trainer4.TrainerID; Trainer4.Pokemons.Add(Pokemon7);
            Pokemon8.TrainerID = Trainer4.TrainerID; Trainer4.Pokemons.Add(Pokemon8);
            Pokemon9.TrainerID = Trainer5.TrainerID; Trainer5.Pokemons.Add(Pokemon9);
            Pokemon10.TrainerID = Trainer5.TrainerID; Trainer5.Pokemons.Add(Pokemon10);
            Pokemon11.TrainerID = Trainer6.TrainerID; Trainer6.Pokemons.Add(Pokemon11);
            Pokemon12.TrainerID = Trainer6.TrainerID; Trainer6.Pokemons.Add(Pokemon12);

            //------------------------------------------------------------

            List<Pokemon> items = new List<Pokemon>();

            items.Add(Pokemon1);
            items.Add(Pokemon2);
            items.Add(Pokemon3);
            items.Add(Pokemon4);
            items.Add(Pokemon5);
            items.Add(Pokemon6);
            items.Add(Pokemon7);
            items.Add(Pokemon8);
            items.Add(Pokemon9);
            items.Add(Pokemon10);
            items.Add(Pokemon11);
            items.Add(Pokemon12);

            return items.AsQueryable();
        }

        private IQueryable<Trainer> FakeTrainerObjects()
        {
            Region Kanto = new Region() { RegionID = 1, Name = "Kanto" };
            Region Johto = new Region() { RegionID = 2, Name = "Johto" };
            Region Hoenn = new Region() { RegionID = 3, Name = "Hoenn" };

            Kanto.Trainers = new List<Trainer>();
            Johto.Trainers = new List<Trainer>();
            Hoenn.Trainers = new List<Trainer>();

            //------------------------------------------------------------

            Trainer Trainer1 = new Trainer() { TrainerID = 1, Name = "Ash Ketchum", Wins = 32, Level = 20, RegionID = 1 };
            Trainer Trainer2 = new Trainer() { TrainerID = 2, Name = "Brock", Wins = 12, Level = 14, RegionID = 1 };
            Trainer Trainer3 = new Trainer() { TrainerID = 3, Name = "Misty", Wins = 6, Level = 2, RegionID = 2 };
            Trainer Trainer4 = new Trainer() { TrainerID = 4, Name = "Rosa", Wins = 15, Level = 9, RegionID = 2 };
            Trainer Trainer5 = new Trainer() { TrainerID = 5, Name = "Wallace", Wins = 9, Level = 7, RegionID = 3 };
            Trainer Trainer6 = new Trainer() { TrainerID = 6, Name = "Morty", Wins = 11, Level = 14, RegionID = 3 };

            Trainer1.Region = Kanto;
            Trainer2.Region = Kanto;
            Trainer3.Region = Johto;
            Trainer4.Region = Johto;
            Trainer5.Region = Hoenn;
            Trainer6.Region = Hoenn;

            Trainer1.RegionID = Kanto.RegionID; Kanto.Trainers.Add(Trainer1);
            Trainer2.RegionID = Kanto.RegionID; Kanto.Trainers.Add(Trainer2);
            Trainer3.RegionID = Johto.RegionID; Johto.Trainers.Add(Trainer3);
            Trainer4.RegionID = Johto.RegionID; Johto.Trainers.Add(Trainer4);
            Trainer5.RegionID = Hoenn.RegionID; Hoenn.Trainers.Add(Trainer5);
            Trainer6.RegionID = Hoenn.RegionID; Hoenn.Trainers.Add(Trainer6);

            //------------------------------------------------------------

            Trainer1.Pokemons = new List<Pokemon>();
            Trainer2.Pokemons = new List<Pokemon>();
            Trainer3.Pokemons = new List<Pokemon>();
            Trainer4.Pokemons = new List<Pokemon>();
            Trainer5.Pokemons = new List<Pokemon>();
            Trainer6.Pokemons = new List<Pokemon>();

            //------------------------------------------------------------

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

            Pokemon1.Trainer = Trainer1;
            Pokemon2.Trainer = Trainer1;
            Pokemon3.Trainer = Trainer2;
            Pokemon4.Trainer = Trainer2;
            Pokemon5.Trainer = Trainer3;
            Pokemon6.Trainer = Trainer3;
            Pokemon7.Trainer = Trainer4;
            Pokemon8.Trainer = Trainer4;
            Pokemon9.Trainer = Trainer5;
            Pokemon10.Trainer = Trainer5;
            Pokemon11.Trainer = Trainer6;
            Pokemon12.Trainer = Trainer6;

            Pokemon1.TrainerID = Trainer1.TrainerID; Trainer1.Pokemons.Add(Pokemon1);
            Pokemon2.TrainerID = Trainer1.TrainerID; Trainer1.Pokemons.Add(Pokemon2);
            Pokemon3.TrainerID = Trainer2.TrainerID; Trainer2.Pokemons.Add(Pokemon3);
            Pokemon4.TrainerID = Trainer2.TrainerID; Trainer2.Pokemons.Add(Pokemon4);
            Pokemon5.TrainerID = Trainer3.TrainerID; Trainer3.Pokemons.Add(Pokemon5);
            Pokemon6.TrainerID = Trainer3.TrainerID; Trainer3.Pokemons.Add(Pokemon6);
            Pokemon7.TrainerID = Trainer4.TrainerID; Trainer4.Pokemons.Add(Pokemon7);
            Pokemon8.TrainerID = Trainer4.TrainerID; Trainer4.Pokemons.Add(Pokemon8);
            Pokemon9.TrainerID = Trainer5.TrainerID; Trainer5.Pokemons.Add(Pokemon9);
            Pokemon10.TrainerID = Trainer5.TrainerID; Trainer5.Pokemons.Add(Pokemon10);
            Pokemon11.TrainerID = Trainer6.TrainerID; Trainer6.Pokemons.Add(Pokemon11);
            Pokemon12.TrainerID = Trainer6.TrainerID; Trainer6.Pokemons.Add(Pokemon12);

            //------------------------------------------------------------

            List<Trainer> items = new List<Trainer>();

            items.Add(Trainer1);
            items.Add(Trainer2);
            items.Add(Trainer3);
            items.Add(Trainer4);
            items.Add(Trainer5);
            items.Add(Trainer6);

            return items.AsQueryable();
        }

        private IQueryable<Region> FakeRegionObjects()
        {
            Region Kanto = new Region() { RegionID = 1, Name = "Kanto" };
            Region Johto = new Region() { RegionID = 2, Name = "Johto" };
            Region Hoenn = new Region() { RegionID = 3, Name = "Hoenn" };

            Kanto.Trainers = new List<Trainer>();
            Johto.Trainers = new List<Trainer>();
            Hoenn.Trainers = new List<Trainer>();

            //------------------------------------------------------------

            Trainer Trainer1 = new Trainer() { TrainerID = 1, Name = "Ash Ketchum", Wins = 32, Level = 20, RegionID = 1 };
            Trainer Trainer2 = new Trainer() { TrainerID = 2, Name = "Brock", Wins = 12, Level = 14, RegionID = 1 };
            Trainer Trainer3 = new Trainer() { TrainerID = 3, Name = "Misty", Wins = 6, Level = 2, RegionID = 2 };
            Trainer Trainer4 = new Trainer() { TrainerID = 4, Name = "Rosa", Wins = 15, Level = 9, RegionID = 2 };
            Trainer Trainer5 = new Trainer() { TrainerID = 5, Name = "Wallace", Wins = 9, Level = 7, RegionID = 3 };
            Trainer Trainer6 = new Trainer() { TrainerID = 6, Name = "Morty", Wins = 11, Level = 14, RegionID = 3 };

            Trainer1.Region = Kanto;
            Trainer2.Region = Kanto;
            Trainer3.Region = Johto;
            Trainer4.Region = Johto;
            Trainer5.Region = Hoenn;
            Trainer6.Region = Hoenn;

            Trainer1.RegionID = Kanto.RegionID; Kanto.Trainers.Add(Trainer1);
            Trainer2.RegionID = Kanto.RegionID; Kanto.Trainers.Add(Trainer2);
            Trainer3.RegionID = Johto.RegionID; Johto.Trainers.Add(Trainer3);
            Trainer4.RegionID = Johto.RegionID; Johto.Trainers.Add(Trainer4);
            Trainer5.RegionID = Hoenn.RegionID; Hoenn.Trainers.Add(Trainer5);
            Trainer6.RegionID = Hoenn.RegionID; Hoenn.Trainers.Add(Trainer6);

            //------------------------------------------------------------

            Trainer1.Pokemons = new List<Pokemon>();
            Trainer2.Pokemons = new List<Pokemon>();
            Trainer3.Pokemons = new List<Pokemon>();
            Trainer4.Pokemons = new List<Pokemon>();
            Trainer5.Pokemons = new List<Pokemon>();
            Trainer6.Pokemons = new List<Pokemon>();

            //------------------------------------------------------------

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

            Pokemon1.Trainer = Trainer1;
            Pokemon2.Trainer = Trainer1;
            Pokemon3.Trainer = Trainer2;
            Pokemon4.Trainer = Trainer2;
            Pokemon5.Trainer = Trainer3;
            Pokemon6.Trainer = Trainer3;
            Pokemon7.Trainer = Trainer4;
            Pokemon8.Trainer = Trainer4;
            Pokemon9.Trainer = Trainer5;
            Pokemon10.Trainer = Trainer5;
            Pokemon11.Trainer = Trainer6;
            Pokemon12.Trainer = Trainer6;

            Pokemon1.TrainerID = Trainer1.TrainerID; Trainer1.Pokemons.Add(Pokemon1);
            Pokemon2.TrainerID = Trainer1.TrainerID; Trainer1.Pokemons.Add(Pokemon2);
            Pokemon3.TrainerID = Trainer2.TrainerID; Trainer2.Pokemons.Add(Pokemon3);
            Pokemon4.TrainerID = Trainer2.TrainerID; Trainer2.Pokemons.Add(Pokemon4);
            Pokemon5.TrainerID = Trainer3.TrainerID; Trainer3.Pokemons.Add(Pokemon5);
            Pokemon6.TrainerID = Trainer3.TrainerID; Trainer3.Pokemons.Add(Pokemon6);
            Pokemon7.TrainerID = Trainer4.TrainerID; Trainer4.Pokemons.Add(Pokemon7);
            Pokemon8.TrainerID = Trainer4.TrainerID; Trainer4.Pokemons.Add(Pokemon8);
            Pokemon9.TrainerID = Trainer5.TrainerID; Trainer5.Pokemons.Add(Pokemon9);
            Pokemon10.TrainerID = Trainer5.TrainerID; Trainer5.Pokemons.Add(Pokemon10);
            Pokemon11.TrainerID = Trainer6.TrainerID; Trainer6.Pokemons.Add(Pokemon11);
            Pokemon12.TrainerID = Trainer6.TrainerID; Trainer6.Pokemons.Add(Pokemon12);

            //------------------------------------------------------------

            List<Region> items = new List<Region>();

            items.Add(Kanto);
            items.Add(Johto);
            items.Add(Hoenn);

            return items.AsQueryable();
        }
    }
}
