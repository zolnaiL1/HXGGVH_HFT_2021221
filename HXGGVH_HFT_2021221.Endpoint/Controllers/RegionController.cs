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
    public class RegionController : ControllerBase
    {
        IRegionLogic regionLogic;

        public RegionController(IRegionLogic regionLogic)
        {
            this.regionLogic = regionLogic;
        }

        // GETALL
        [HttpGet]
        public IEnumerable<Region> Get()
        {
            return regionLogic.ReadAll();
        }

        // GET 
        [HttpGet("{id}")]
        public Region Get(int id)
        {
            return regionLogic.Read(id);
        }

        // POST 
        [HttpPost]
        public void Post([FromBody] Region value)
        {
            regionLogic.Create(value);
        }

        // PUT 
        [HttpPut]
        public void Put([FromBody] Region value)
        {
            regionLogic.Update(value);
        }

        // DELETE
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            regionLogic.Delete(id);
        }
    }
}
