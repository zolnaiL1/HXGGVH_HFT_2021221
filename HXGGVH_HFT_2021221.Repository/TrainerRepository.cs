using HXGGVH_HFT_2021221.Data;
using HXGGVH_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Repository
{
    public class TrainerRepository : ITrainerRepository
    {
        TrainerDbContext db;

        public TrainerRepository(TrainerDbContext db)
        {
            this.db = db;
        }

        //CRUD: Create, Read, ReadAll, Update, Delete

        public void Create(Trainer trainer)
        {
            db.Trainers.Add(trainer);
            db.SaveChanges();
        }

        public Trainer Read(int id)
        {
            return db.Trainers.FirstOrDefault(t => t.TrainerID == id);
        }

        public IQueryable<Trainer> ReadAll()
        {
            return db.Trainers;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Trainer trainer)
        {
            var oldTrainer = Read(trainer.TrainerID);

            //ID;NAME;WINS;LEVEL;REGIONID
            oldTrainer.TrainerID = trainer.TrainerID;
            oldTrainer.Name = trainer.Name;
            oldTrainer.Wins = trainer.Wins;
            oldTrainer.Level = trainer.Level;
            oldTrainer.RegionID = trainer.RegionID;
            
            db.SaveChanges();
        }
    }
}
