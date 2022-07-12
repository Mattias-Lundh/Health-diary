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
    public class ExcersiseTemplateController : ControllerBase
    {
        private Database _data;
        public ExcersiseTemplateController(Database data)
        {
            _data = data;
        }
        
        [HttpGet]
        [Route("")]
        public IEnumerable<ExcersiseTemplateContract> GetAll(int campaignId)
        {
            return _data.ExcersiseTemplate.GetExcersiseTemplates(campaignId).Select(et => new ExcersiseTemplateContract(et));
        }

        [HttpGet]
        [Route("{id}")]
        public ExcersiseTemplateContract Get(int id)
        {
            return new ExcersiseTemplateContract(_data.ExcersiseTemplate.GetExcersiseTemplate(id));
        }

        [HttpPost]
        [Route("")]
        public ExcersiseTemplateContract Create(ExcersiseTemplateContract excersiseTemplate)
        {
            excersiseTemplate.Id = _data.ExcersiseTemplate.InsertExcersiseTemplate(excersiseTemplate.ToExcersiseTemplate());

            return excersiseTemplate;
        }

        [HttpPut]
        [Route("")]
        public string Update(ExcersiseTemplateContract excersiseTemplate)
        {
            _data.ExcersiseTemplate.UpdateExcersiseTemplate(excersiseTemplate.ToExcersiseTemplate());
            return "success";
        }

        [HttpDelete]
        [Route("")]
        public string Delete(int id)
        {
            _data.ExcersiseTemplate.DeleteExcersiseTemplate(id);
            return "success";
        }       

    }
}
