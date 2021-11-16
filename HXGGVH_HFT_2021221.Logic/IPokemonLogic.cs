using HXGGVH_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HXGGVH_HFT_2021221.Logic
{
    public interface IPokemonLogic
    {
        public void Create(Pokemon pokemon);
        Pokemon Read(int id);
        IQueryable<Pokemon> ReadAll();
        public void Delete(int id);
        public void Update(Pokemon pokemon);

    }
}
