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
    public class ExcersiseController : ControllerBase
    {
        private Database _data;
        public ExcersiseController(Database data)
        {
            _data = data;
        }
        
        [HttpGet]
        [Route("")]
        public IEnumerable<ExcersiseContract> GetAll(int campaignId)
        {
            return _data.Excersise.GetExcersises(campaignId).Select(e => new ExcersiseContract(e));
        }

        [HttpGet]
        [Route("{id}")]
        public ExcersiseContract Get(int id)
        {
            return new ExcersiseContract(_data.Excersise.GetExcersise(id));
        }

        [HttpPost]
        [Route("")]
        public ExcersiseContract Create(ExcersiseContract excersise)
        {
            _data.Excersise.InsertExcersise(excersise.ToExcersise());

            return excersise;
        }

        [HttpPut]
        [Route("")]
        public string Update(ExcersiseContract excersise)
        {
            _data.Excersise.UpdateExcersise(excersise.ToExcersise());
            return "success";
        }

        [HttpDelete]
        [Route(""   )]
        public string Delete(int id)
        {
            _data.Excersise.DeleteExcersise(id);
            return "success";
        }       

    }
}
