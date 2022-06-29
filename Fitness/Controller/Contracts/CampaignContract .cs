using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controller.Contracts
{
    public class CampaignContract
    {
        public CampaignContract()
        {

        }
        public CampaignContract(Campaign campaign)
        {
            Id = campaign.Id;
            Name = campaign.Name;
            AppUser = campaign.AppUser.Id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppUser { get; set; }

        public Campaign ToCampaign()
        {
            var campaign = new Campaign();
            campaign.Id = Id;
            campaign.Name = Name;
            campaign.AppUser = new AppUser(AppUser);

            return campaign;
        }
    }
}
