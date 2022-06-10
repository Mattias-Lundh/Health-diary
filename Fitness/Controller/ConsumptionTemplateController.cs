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
    public class ConsumptionTemplateController : ControllerBase
    {
        private Database _data;
        public ConsumptionTemplateController(Database data)
        {
            _data = data;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ConsumptionTemplateContract> GetAll(int campaignId)
        {
            return _data.ConsumptionTemplate.GetConsumptionTemplates(campaignId).Select(ct => new ConsumptionTemplateContract(ct));
        }

        [HttpGet]
        [Route("{id}")]
        public ConsumptionTemplateContract Get(int id)
        {
            return new ConsumptionTemplateContract(_data.ConsumptionTemplate.GetConsumptionTemplate(id));
        }

        [HttpPost]
        [Route("")]
        public ConsumptionTemplateContract Create(ConsumptionTemplateContract consumptionTemplate)
        {
            consumptionTemplate.Id = _data.ConsumptionTemplate.InsertConsumptionTemplate(consumptionTemplate.ToConsumptionTemplate());
            return consumptionTemplate;
        }

        [HttpPut]
        [Route("")]
        public string Update(ConsumptionTemplateContract consumptionTemplate)
        {
            _data.ConsumptionTemplate.UpdateConsumptionTemplate(consumptionTemplate.ToConsumptionTemplate());

            return "Success";
        }

        [HttpDelete]
        [Route("")]
        public string Delete(int Id)
        {
            _data.ConsumptionTemplate.DeleteConsumptionTemplate(Id);
            return "Success";
        }       

    }
}
