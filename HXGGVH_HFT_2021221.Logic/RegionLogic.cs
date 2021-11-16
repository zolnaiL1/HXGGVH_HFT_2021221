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
        public RegionLogic(IRegionRepository regionRepo)
        {
            this.regionRepo = regionRepo;
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
    }
}
