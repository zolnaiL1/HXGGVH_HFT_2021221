using HXGGVH_HFT_2021221.Models;
using HXGGVH_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Logic
{
    public class RegionLogic : IRegionLogic
    {
        IRegionRepository regionRepo;
        IPokemonRepository pokemonRepo;
        ITrainerRepository trainerRepo;

        public RegionLogic(IRegionRepository regionRepo, IPokemonRepository pokemonRepo, ITrainerRepository trainerRepo)
        {
            this.regionRepo = regionRepo;
            this.pokemonRepo = pokemonRepo;
            this.trainerRepo = trainerRepo;
        }




        //CRUD: Create, Read, ReadAll, Update, Delete
        public void Create(Region region)
        {
            if (region.Name == null)
            {
                throw new ArgumentException("Name is null!");
            }
            regionRepo.Create(region);
        }

        public Region Read(int id)
        {
            return regionRepo.Read(id);
        }

        public IQueryable<Region> ReadAll()
        {
            return regionRepo.ReadAll();
        }

        public void Delete(int id)
        {
            regionRepo.Delete(id);
        }

        public void Update(Region region)
        {
            regionRepo.Update(region);
        }

        //NON-CRUD
        //4
        public IEnumerable<Region> RegionWherePikachuLives()
        {
            var q = from pokemons in pokemonRepo.ReadAll()
                    join trainers in trainerRepo.ReadAll()
                    on pokemons.TrainerID equals trainers.TrainerID
                    join regions in regionRepo.ReadAll()
                    on trainers.RegionID equals regions.RegionID
                    where pokemons.Name == "Pikachu"
                    select regions;

            return q;
        }
    }
}
