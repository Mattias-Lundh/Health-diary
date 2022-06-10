using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Controller.Contract;
using Fitness.Data.access;

namespace Fitness.Controller
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ConsumptionController : ControllerBase
    {
        private Database _data;
        public ConsumptionController(Database data)
        {
            _data = data;
        }
        
        [HttpGet]
        [Route("")]
        public IEnumerable<ConsumptionContract> GetAll(int campaignId)
        {
            return _data.Consumption.GetConsumptions(campaignId).Select(c => new ConsumptionContract(c));
        }

        [HttpGet]
        [Route("{id}")]
        public ConsumptionContract Get(int id)
        {
            return new ConsumptionContract(_data.Consumption.GetConsumption(id));
        }

        [HttpPost]
        [Route("")]
        public ConsumptionContract Create(ConsumptionContract consumption)
        {
            consumption.Id = _data.Consumption.InsertConsumption(consumption.ToConsumption());

            return consumption;
        }

        [HttpPut]
        [Route("")]
        public string Update(ConsumptionContract consumption)
        {
            _data.Consumption.UpdateConsumption(consumption.ToConsumption());
            return "success";
        }

        [HttpDelete]
        [Route("")]
        public string Delete(int Id)
        {
            _data.Consumption.DeleteConsumption(Id);

            return "success";
        }       

    }
}
