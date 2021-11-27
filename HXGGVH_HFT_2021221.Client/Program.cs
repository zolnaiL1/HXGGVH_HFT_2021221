using ConsoleTools;
using HXGGVH_HFT_2021221.Models;
using System;
using System.Threading;

namespace HXGGVH_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(8000);

            RestService restService = new RestService("http://localhost:35206");

            #region menu
            var menu = new ConsoleMenu().
                 Add("Pokemon CRUD", () => PokemonCruds(restService)).
                 Add("Trainer CRUD", () => TrainerCruds(restService)).
                 Add("Region CRUD", () => RegionCruds(restService)).
                 Add("nonCRUDs", () => nonCrudMethodStarter(restService)).
                 Add("Exit", ConsoleMenu.Close);
            menu.Show();
            #endregion
        }
        private static void nonCrudMethodStarter(RestService restService)
        {
            PokemonsInKantoRegionMethod(restService);
            PokemonsWhereTrainerWinIs10Method(restService);
            PokemonsWhereTrainerLevelUnder10Method(restService);
            RegionWherePikachuLivesMethod(restService);
            RegionWherePokemonsTypeIsWaterMethod(restService);

            Console.ReadLine();
        }

        private static void PokemonCruds(RestService restService)
        {
            Console.WriteLine("\n\n[-] Pokemon CRUD methods:");
            //create
            //restService.Post<Pokemon>(new Pokemon()
            //{ 
            //    //PokemonID = 13,
            //    Name = "Raichu",
            //    Health = 60,
            //    Attack = 40,
            //    Defense = 20,
            //    Speed = 6,
            //    Type = "Electric"
            //    //TrainerID = 1
            //}, "pokemon");
            ////update
            //restService.Put<Pokemon>(new Pokemon()
            //{
            //    PokemonID = 13,
            //    Name = "newRaichu",
            //    Health = 60,
            //    Attack = 40,
            //    Defense = 20,
            //    Speed = 6,
            //    Type = "Electric",
            //    TrainerID = 1
            //}, "pokemon");
            //delete
            //restService.Delete(13, "pokemon");
            //readall
            var pokemons = restService.Get<Pokemon>("pokemon");

            foreach (var item in pokemons)
            {
                Console.WriteLine(item.Name.ToString());            
            }
            //readone
            //Console.WriteLine(restService.GetSingle<Pokemon>("pokemon"));

            Console.ReadLine();
        }

        private static void TrainerCruds(RestService restService)
        {
            Console.WriteLine("\n\n[-] Trainer CRUD methods:");
            //create
            //restService.Post<Trainer>(new Trainer()
            //{
            //    Name = "Gloria"
            //}, "trainer");
            //update
            //restService.Put<Trainer>(new Trainer()
            //{
            //    RegionID = 4,
            //    Name = "newGloria"
            //}, "trainer");
            //delete
            //restService.Delete(4, "trainer");
            var trainers = restService.Get<Trainer>("trainer");

            foreach (var item in trainers)
            {
                Console.WriteLine(item.Name.ToString());
            }
            //readone
            //???

            Console.ReadLine();
        }

        private static void RegionCruds(RestService restService)
        {
            Console.WriteLine("\n\n[-] Regions CRUD methods:");
            //create
            restService.Post<Region>(new Region()
            {
                Name = "Pasio"
            }, "region");
            //update
            restService.Put<Region>(new Region()
            {
                RegionID = 4,
                Name = "newPasio"
            }, "region");
            //delete
            restService.Delete(4, "region");
            //readall
            var regions = restService.Get<Region>("region");

            foreach (var item in regions)
            {
                Console.WriteLine(item.Name.ToString());
            }
            //readone
            //???

            Console.ReadLine();
        }

        #region nonCRUDs
        private static void PokemonsInKantoRegionMethod(RestService restService)
        {
            var PokemonsInKantoRegion = restService.Get<Pokemon>("stat/PokemonsInKantoRegion");

            Console.WriteLine("\n[-] Pokemons in Kanto region:");
            foreach (var item in PokemonsInKantoRegion)
            {
                Console.WriteLine(item.Name.ToString());
            }
        }

        private static void PokemonsWhereTrainerWinIs10Method(RestService restService)
        {
            var PokemonsWhereTrainerWinIs10 = restService.Get<Pokemon>("stat/PokemonsWhereTrainerWinIs10");

            Console.WriteLine("\n[-] Pokemons where trainer win is 10:");
            foreach (var item in PokemonsWhereTrainerWinIs10)
            {
                Console.WriteLine(item.Name.ToString());
            }
        }

        private static void PokemonsWhereTrainerLevelUnder10Method(RestService restService)
        {
            var PokemonsWhereTrainerLevelUnder10 = restService.Get<Pokemon>("stat/PokemonsWhereTrainerLevelUnder10");

            Console.WriteLine("\n[-] Pokemons where trainer level under 10:");
            foreach (var item in PokemonsWhereTrainerLevelUnder10)
            {
                Console.WriteLine(item.Name.ToString());
            }
        }

        private static void RegionWherePikachuLivesMethod(RestService restService)
        {
            var RegionWherePikachuLives = restService.Get<Pokemon>("stat/RegionWherePikachuLives");

            Console.WriteLine("\n[-] Region where Pikachu lives:");
            foreach (var item in RegionWherePikachuLives)
            {
                Console.WriteLine(item.Name.ToString());
            }
        }

        private static void RegionWherePokemonsTypeIsWaterMethod(RestService restService)
        {
            var RegionWherePokemonsTypeIsWater = restService.Get<Pokemon>("stat/RegionWherePokemonsTypeIsWater");

            Console.WriteLine("\n[-] Region where pokemons type is water:");
            foreach (var item in RegionWherePokemonsTypeIsWater)
            {
                Console.WriteLine(item.Name.ToString());
            }
        }
        #endregion
    }
}
