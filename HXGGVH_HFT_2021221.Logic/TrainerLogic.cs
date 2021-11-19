using HXGGVH_HFT_2021221.Models;
using HXGGVH_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Logic
{
    public class TrainerLogic : ITrainerLogic
    {
        ITrainerRepository trainerRepo;
        public TrainerLogic(ITrainerRepository trainerRepo)
        {
            this.trainerRepo = trainerRepo;
        }

        //CRUD: Create, Read, ReadAll, Update, Delete
        public void Create(Trainer trainer)
        {
            if (trainer.Name == null)
            {
                throw new ArgumentException("Name is null!");
            }
            trainerRepo.Create(trainer);
        }

        public Trainer Read(int id)
        {
            return trainerRepo.Read(id);
        }

        public IQueryable<Trainer> ReadAll()
        {
            return trainerRepo.ReadAll();
        }

        public void Delete(int id)
        {
            trainerRepo.Delete(id);
        }

        public void Update(Trainer trainer)
        {
            trainerRepo.Update(trainer);
        }

        //NON-CRUD
        //1
    }
}
