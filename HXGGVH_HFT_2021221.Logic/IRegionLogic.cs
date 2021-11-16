using HXGGVH_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Logic
{
    public interface IRegionLogic
    {
        public void Create(Region region);
        Region Read(int id);
        IQueryable<Region> ReadAll();
        public void Delete(int id);
        public void Update(Region region);
    }
}
