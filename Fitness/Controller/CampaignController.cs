using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Controller.Contract;
using Fitness.Data.access;
using Fitness.Data;
using Fitness.Controller.Contracts;
using Microsoft.AspNetCore.Cors;

namespace Fitness.Controller
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private Database _data;
        public CampaignController(Database data)
        {
            _data = data;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<CampaignContract> GetAll(int AppUserId)
        {
            var a = _data.Campaign.GetCampaigns(AppUserId).Select(c => new CampaignContract(c));
            return a;
        }

        [HttpPost]
        [Route("")]
        public CampaignContract Create(CampaignContract campaign)
        {
            campaign.Id = _data.Campaign.InsertCampaign(campaign.ToCampaign());

            return campaign;
        }

        [HttpPut]
        [Route("")]
        public string Update(CampaignContract campaign)
        {
            _data.Campaign.UpdateCampaign(campaign.ToCampaign());
            return "success";
        }

        [HttpDelete]
        [Route("")]
        public string Delete(int Id)
        {
            _data.Campaign.DeleteCampaign(Id);
            return "success";
        }
    }
}
