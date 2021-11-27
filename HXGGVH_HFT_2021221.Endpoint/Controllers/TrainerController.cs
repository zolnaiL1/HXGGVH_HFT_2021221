using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HXGGVH_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        ITrainerLogic trainerLogic;

        public TrainerController(ITrainerLogic trainerLogic)
        {
            this.trainerLogic = trainerLogic;
        }

        // GETALL
        [HttpGet]
        public IEnumerable<Trainer> Get()
        {
            return trainerLogic.ReadAll();
        }

        // GET 
        [HttpGet("{id}")]
        public Trainer Get(int id)
        {
            return trainerLogic.Read(id);
        }

        // POST 
        [HttpPost]
        public void Post([FromBody] Trainer value)
        {
            trainerLogic.Create(value);
        }

        // PUT 
        [HttpPut]
        public void Put([FromBody] Trainer value)
        {
            trainerLogic.Update(value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            trainerLogic.Delete(id);
        }
    }
}
