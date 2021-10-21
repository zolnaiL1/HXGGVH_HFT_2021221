using HXGGVH_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Repository
{
    public interface ITrainerRepository
    {
        public void Create(Trainer trainer);
        public void Delete(int id);
        Trainer Read(int id);
        IQueryable<Trainer> ReadAll();
        public void Update(Trainer trainer);
    }
}
