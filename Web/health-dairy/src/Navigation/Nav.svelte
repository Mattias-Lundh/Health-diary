<script lang="ts">
  import Tab, { Label } from '@smui/tab';
  import TabBar from '@smui/tab-bar'; 

  import Home from "../Pages/Home/Home.svelte";
  import Campaign from "../Pages/Campaign/Campaign.svelte";
  import Consumption from "../Pages/Consumption/Consumption.svelte";
  import ConsumptionTemplate from "../Pages/ConsumptionTemplate/ConsumptionTemplate.svelte";
  import Excersise from "../Pages/Excersise/Excersise.svelte";
  import ExcersiseTemplate from "../Pages/ExceriseTemplate/ExcersiseTemplate.svelte";
  import Meal from "../Pages/Meal/Meal.svelte";

  let active = 'Home'; 

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
   

