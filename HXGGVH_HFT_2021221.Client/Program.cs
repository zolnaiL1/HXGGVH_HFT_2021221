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
                 Add("Pokemon CRUD", () => PokemonSubMenu(restService)).
                 Add("Trainer CRUD", () => TrainerSubMenu(restService)).
                 Add("Region CRUD", () => RegionSubMenu(restService)).
                 Add("nonCRUDs", () => nonCrudSubMenu(restService)).
                 Add("Exit", ConsoleMenu.Close);
            menu.Show();
            #endregion
        }

        //done
        #region submenus
        private static void PokemonSubMenu(RestService restService)
        {
            var subMenu = new ConsoleMenu().
                    Add("Create new Pokemon", () => PokemonCreate(restService)).
                    Add("Update last Pokemon", () => PokemonUpdate(restService)).
                    Add("Delete last Pokemon", () => PokemonDelete(restService)).
                    Add("ReadAll Pokemon", () => PokemonReadAll(restService)).
                    Add("Read first Pokemon", () => PokemonRead(restService)).
                    Add("Exit", ConsoleMenu.Close).
                    Configure(config => 
                    {
                        config.Selector = "--> ";
                    });
            subMenu.Show();
        }

        private static void TrainerSubMenu(RestService restService)
        {
            var subMenu = new ConsoleMenu().
                    Add("Create new Trainer", () => TrainerCreate(restService)).
                    Add("Update last Trainer", () => TrainerUpdate(restService)).
                    Add("Delete last Trainer", () => TrainerDelete(restService)).
                    Add("ReadAll Trainer", () => TrainerReadAll(restService)).
                    Add("Read first Trainer", () => TrainerRead(restService)).
                    Add("Exit", ConsoleMenu.Close).
                    Configure(config =>
                    {
                        config.Selector = "--> ";
                    });
            subMenu.Show();
        }

        private static void RegionSubMenu(RestService restService)
        {
            var subMenu = new ConsoleMenu().
                    Add("Create new Region", () => RegionCreate(restService)).
                    Add("Update last Region", () => RegionUpdate(restService)).
                    Add("Delete last Region", () => RegionDelete(restService)).
                    Add("ReadAll Region", () => RegionReadAll(restService)).
                    Add("Read first Region", () => RegionRead(restService)).
                    Add("Exit", ConsoleMenu.Close).
                    Configure(config =>
                    {
                        config.Selector = "--> ";
                    });
            subMenu.Show();
        }
        private static void nonCrudSubMenu(RestService restService)
        {
            var subMenu = new ConsoleMenu().
                    Add("Pokemons in Kanto region", () => PokemonsInKantoRegionMethod(restService)).
                    Add("Pokemons where trainer win is 10", () => PokemonsWhereTrainerWinIs10Method(restService)).
                    Add("Pokemons where trainer level under 10", () => PokemonsWhereTrainerLevelUnder10Method(restService)).
                    Add("Region where Pikachu lives", () => RegionWherePikachuLivesMethod(restService)).
                    Add("Region where pokemons type is water", () => RegionWherePokemonsTypeIsWaterMethod(restService)).
                    Add("Exit", ConsoleMenu.Close).
                    Configure(config =>
                    {
                        config.Selector = "--> ";
                    });
            subMenu.Show();
        }
        #endregion
        
        //done
        #region nonCRUDs 
        private static void PokemonsInKantoRegionMethod(RestService restService)
        {
            var PokemonsInKantoRegion = restService.Get<Pokemon>("stat/PokemonsInKantoRegion");

            Console.WriteLine("\n[-] Pokemons in Kanto region:");
            foreach (var item in PokemonsInKantoRegion)
            {
                Console.WriteLine(item.Name.ToString());

            }

            Console.ReadLine();
        }

        private static void PokemonsWhereTrainerWinIs10Method(RestService restService)
        {
            var PokemonsWhereTrainerWinIs10 = restService.Get<Pokemon>("stat/PokemonsWhereTrainerWinIs10");

            Console.WriteLine("\n[-] Pokemons where trainer win is 10:");
            foreach (var item in PokemonsWhereTrainerWinIs10)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }

        private static void PokemonsWhereTrainerLevelUnder10Method(RestService restService)
        {
            var PokemonsWhereTrainerLevelUnder10 = restService.Get<Pokemon>("stat/PokemonsWhereTrainerLevelUnder10");

            Console.WriteLine("\n[-] Pokemons where trainer level under 10:");
            foreach (var item in PokemonsWhereTrainerLevelUnder10)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }

        private static void RegionWherePikachuLivesMethod(RestService restService)
        {
            var RegionWherePikachuLives = restService.Get<Pokemon>("stat/RegionWherePikachuLives");

            Console.WriteLine("\n[-] Region where Pikachu lives:");
            foreach (var item in RegionWherePikachuLives)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }

        private static void RegionWherePokemonsTypeIsWaterMethod(RestService restService)
        {
            var RegionWherePokemonsTypeIsWater = restService.Get<Pokemon>("stat/RegionWherePokemonsTypeIsWater");

            Console.WriteLine("\n[-] Region where pokemons type is water:");
            foreach (var item in RegionWherePokemonsTypeIsWater)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }
        #endregion

        #region pokemonCruds
        private static void PokemonCreate(RestService restService)
        {
            Console.WriteLine("\n\n[-] New pokemon created");
            restService.Post<Pokemon>(new Pokemon()
            {
                //PokemonID = 13,
                Name = "Raichu",
                Health = 60,
                Attack = 40,
                Defense = 20,
                Speed = 6,
                Type = "Electric",
                TrainerID = 1
            }, "pokemon");

            Console.ReadLine();
        }

        private static void PokemonUpdate(RestService restService)
        {
            Console.WriteLine("\n\n[-] Last pokemon updated");

            int lastPokemonId = restService.Get<Pokemon>("pokemon").Count;
            Pokemon temp = restService.Get<Pokemon>(lastPokemonId, "pokemon");
           
            restService.Put<Pokemon>(new Pokemon()
            {
                PokemonID = temp.PokemonID,
                Name = $"new{temp.Name}",
                Health = temp.Health,
                Attack = temp.Attack,
                Defense = temp.Defense,
                Speed = temp.Speed,
                Type = temp.Type,
                TrainerID = temp.TrainerID
            }, "pokemon");

            Console.ReadLine();
        }

        private static void PokemonDelete(RestService restService)
        {
            Console.WriteLine("\n\n[-] Last pokemon deleted");
            restService.Delete(restService.Get<Pokemon>("pokemon").Count, "pokemon");

            Console.ReadLine();
        }

        private static void PokemonReadAll(RestService restService)
        {
            Console.WriteLine("\n\n[-] All pokemon read");
            var pokemons = restService.Get<Pokemon>("pokemon");

            foreach (var item in pokemons)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }

        private static void PokemonRead(RestService restService)
        {
            Console.WriteLine("\n\n[-] One pokemon read");
            var singlePokemon = restService.Get<Pokemon>(1, "pokemon");
            Console.WriteLine($"\nReadOne : Id = 1 : {singlePokemon.Name}");

            Console.ReadLine();
        }
        #endregion

        #region trainerCruds
        private static void TrainerCreate(RestService restService)
        {
            Console.WriteLine("\n\n[-] New pokemon created");
            restService.Post<Trainer>(new Trainer()
            {
                //TrainerID = 7,
                Name = "Gloria",
                Wins = 3,
                Level = 12,
                RegionID = 1
            }, "trainer");

            Console.ReadLine();
        }

        private static void TrainerUpdate(RestService restService)
        {
            Console.WriteLine("\n\n[-] Last trainer updated");

            int lastTrainerId = restService.Get<Trainer>("trainer").Count;
            Trainer temp = restService.Get<Trainer>(lastTrainerId, "trainer");

            restService.Put<Trainer>(new Trainer()
            {
                TrainerID = temp.TrainerID,
                Name = $"new{temp.Name}",
                Wins = temp.Wins,
                Level = temp.Level,
                RegionID = temp.RegionID
            }, "trainer");

            Console.ReadLine();
        }

        private static void TrainerDelete(RestService restService)
        {
            Console.WriteLine("\n\n[-] Last trainer deleted");
            restService.Delete(restService.Get<Trainer>("trainer").Count, "trainer");

            Console.ReadLine();
        }

        private static void TrainerReadAll(RestService restService)
        {
            Console.WriteLine("\n\n[-] All trainer read");
            var trainers = restService.Get<Trainer>("trainer");

            foreach (var item in trainers)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }

        private static void TrainerRead(RestService restService)
        {
            Console.WriteLine("\n\n[-] One trainer read");
            var singleTrainer = restService.Get<Trainer>(1, "trainer");
            Console.WriteLine($"\nReadOne : Id = 1 : {singleTrainer.Name}");

            Console.ReadLine();
        }
        #endregion

        #region regionCruds
        private static void RegionCreate(RestService restService)
        {
            Console.WriteLine("\n\n[-] New region created");
            restService.Post<Region>(new Region()
            {
                Name = "Pasio"
            }, "region");

            Console.ReadLine();
        }

        private static void RegionUpdate(RestService restService)
        {
            Console.WriteLine("\n\n[-] Last region updated");

            int lastRegionId = restService.Get<Region>("region").Count;
            Region temp = restService.Get<Region>(lastRegionId, "region");

            restService.Put<Trainer>(new Trainer()
            {
                RegionID = temp.RegionID,
                Name = $"new{temp.Name}",
            }, "region");

            Console.ReadLine();
        }

        private static void RegionDelete(RestService restService)
        {
            Console.WriteLine("\n\n[-] Last region deleted");
            restService.Delete(restService.Get<Region>("region").Count, "region");

            Console.ReadLine();
        }

        private static void RegionReadAll(RestService restService)
        {
            Console.WriteLine("\n\n[-] All region read");
            var regions = restService.Get<Region>("region");

            foreach (var item in regions)
            {
                Console.WriteLine(item.Name.ToString());
            }

            Console.ReadLine();
        }

        private static void RegionRead(RestService restService)
        {
            Console.WriteLine("\n\n[-] One region read");
            var singleRegion = restService.Get<Region>(1, "region");
            Console.WriteLine($"\nReadOne : Id = 1 : {singleRegion.Name}");

            Console.ReadLine();
        }
        #endregion
    }
}
