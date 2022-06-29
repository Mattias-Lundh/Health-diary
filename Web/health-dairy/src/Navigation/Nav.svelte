<script lang="ts">
  import Tab, { Label } from '@smui/tab';
  import TabBar from '@smui/tab-bar'; 

  import { getCampaigns } from "../Api/Data";
  import { createCampaign } from "../Api/Data"
  import Home from "../Pages/Home/Home.svelte";
  import Campaign from "../Pages/Campaign/Campaign.svelte";
  import Consumption from "../Pages/Consumption/Consumption.svelte";
  import ConsumptionTemplate from "../Pages/Consumption/ConsumptionTemplate.svelte";
  import Excersise from "../Pages/Excersise/Excersise.svelte";
  import ExcersiseTemplate from "../Pages/Excersise/ExcersiseTemplate.svelte";
  import Meal from "../Pages/Consumption/Meal.svelte";
  import { consumptions } from "../Store/Store";

  let active = 'Home';  
  export let appUserId;

  let campaign = {
    "name": "testCampaign",
    "appUser": 1
  }
  createCampaign(campaign)
  consumptions.set(getCampaigns(appUserId))

</script>

<div>
  <TabBar tabs={['Home', 'Campaign', 'Consumption', 'Excersise', 'Consumption Template', 'Excersise Template', 'Meal']} let:tab bind:active>
    <Tab {tab}>
      <Label>{tab}</Label>
    </Tab>
  </TabBar>
 
  {#if active == 'Home'}
  <Home />
  {/if}
  {#if active == 'Campaign'}
  <Campaign />
  {/if}
  {#if active == 'Consumption'}
  <Consumption />
  {/if}
  {#if active == 'Excersise'}
  <Excersise />
  {/if}
  {#if active == 'Consumption Template'}
  <ConsumptionTemplate />
  {/if}
  {#if active == 'Excersise Template'}
  <ExcersiseTemplate />
  {/if}
  {#if active == 'Meal'}
  <Meal />
  {/if}
  
</div>
   

