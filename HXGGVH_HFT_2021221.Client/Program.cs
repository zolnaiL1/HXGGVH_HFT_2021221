using HXGGVH_HFT_2021221.Data;
using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Repository;
using System;

namespace HXGGVH_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //TEST
            TrainerDbContext test = new TrainerDbContext();

            PokemonRepository poke = new PokemonRepository(test);
            RegionRepository region = new RegionRepository(test);
            TrainerRepository trainer = new TrainerRepository(test);

            //1
            //PokemonLogic nonCrudTest1 = new PokemonLogic(poke, trainer, region);
            //var q1 = nonCrudTest1.PokemonsInKantoRegion();
            //2
            //PokemonLogic nonCrudTest2 = new PokemonLogic(poke, trainer, region);
            //var q2 = nonCrudTest2.PokemonsWhereTrainerWinIs10();
            //3
            PokemonLogic nonCrudTest3 = new PokemonLogic(poke, trainer, region);
            var q3 = nonCrudTest3.PokemonsWhereTrainerLevelUnder10();
            //4
            RegionLogic nonCrudTest4 = new RegionLogic(region, poke, trainer);
            var q4 = nonCrudTest4.RegionWherePikachuLives();
            //5
            RegionLogic nonCrudTest5 = new RegionLogic(region, poke, trainer);
            var q5 = nonCrudTest5.RegionWherePokemonsTypeIsWater();
            //TEST
        }
    }
}
