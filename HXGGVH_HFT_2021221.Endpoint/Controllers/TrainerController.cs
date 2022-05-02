using HXGGVH_HFT_2021221.Endpoint.Services;
using HXGGVH_HFT_2021221.Logic;
using HXGGVH_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        IHubContext<SignalRHub> hub;

        public TrainerController(ITrainerLogic trainerLogic, IHubContext<SignalRHub> hub)
        {
            this.trainerLogic = trainerLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("TrainerCreated", value);
        }

        // PUT 
        [HttpPut]
        public void Put([FromBody] Trainer value)
        {
            trainerLogic.Update(value);
            this.hub.Clients.All.SendAsync("TrainerUpdated", value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var trainerToDelete = this.trainerLogic.Read(id);
            trainerLogic.Delete(id);
            this.hub.Clients.All.SendAsync("TrainerDeleted", trainerToDelete);
        }
    }
}
