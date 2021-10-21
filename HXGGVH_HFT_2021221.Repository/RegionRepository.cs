using HXGGVH_HFT_2021221.Data;
using HXGGVH_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Repository
{
    public class RegionRepository : IRegionRepository
    {
        TrainerDbContext db;

        public RegionRepository(TrainerDbContext db)
        {
            this.db = db;
        }

        //CRUD: Create, Read, ReadAll, Update, Delete

        public void Create(Region region)
        {
            db.Regions.Add(region);
            db.SaveChanges();
        }

        public Region Read(int id)
        {
            return db.Regions.FirstOrDefault(t => t.RegionID == id);
        }

        public IQueryable<Region> ReadAll()
        {
            return db.Regions;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Region region)
        {
            var oldRegion = Read(region.RegionID);

            //ID;NAME
            oldRegion.RegionID = region.RegionID;
            oldRegion.Name = region.Name;          

            db.SaveChanges();
        }
    }
}
