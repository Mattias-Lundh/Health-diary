<script lang="ts">
    import {meals, consumptionTemplates, selectedCampaign} from "../../Store/Store";
    import DataTable, {
    Head,
    Body,
    Row,
    Cell,
    Pagination,
    } from '@smui/data-table';
    import Select, { Option } from '@smui/select';
    import Button from '@smui/button';
    import { Label } from '@smui/common'
    import {getMeals, getConsumptionTemplates} from "../../Api/Data";
    import type {Meal} from "../../main";

    export let setSelected;

    getMeals($selectedCampaign.id, (data) => { 
        meals.set(data)
    })

    getConsumptionTemplates($selectedCampaign.id, (data) => {
      console.log("data"); 
      console.log(data); 
        consumptionTemplates.set(data)
    })

    let items: Meal[] = [];
  
    $: {
      if($meals)
      {
        items = $meals.map(c => { 
          return {
          id: c.id,
          name: c.name,
          foodItems: c.foodItems          
          } as Meal;
        })
      }        
    }
    
    let rowsPerPage = 10;
    let currentPage = 0;

    $: start = currentPage * rowsPerPage;
    $: end = Math.min(start + rowsPerPage, items.length);
    $: slice = items.slice(start, end);
    $: lastPage = Math.max(Math.ceil(items.length / rowsPerPage) - 1, 0);

    $: if (currentPage > lastPage) {
    currentPage = lastPage;
    }


</script>

<DataTable table$aria-label="Consumptions" style="width: 100%;">
  <Head>
    <Row>
      <Cell numeric>ID</Cell>
      <Cell >Name</Cell>
      <Cell style="width: 100%;" numeric>FoodItems</Cell>
    </Row>
  </Head>
  <Body>
    {#each slice as meal (meal.id)}
      <Row on:click={() => {setSelected(meal)}}>
        <Cell numeric>{meal.id}</Cell>
        <Cell style="width: 100%;">{meal.name}</Cell>
        <Cell>{meal.foodItems.map(fi => fi.name + " " +fi.amount + fi.unit).join(", ")}</Cell>        
      </Row>
    {/each}
  </Body>

  <Pagination slot="paginate">
    <svelte:fragment slot="rowsPerPage">
      <Label>Rows Per Page</Label>
      <Select variant="outlined" bind:value={rowsPerPage} noLabel>
        <Option value={10}>10</Option>
        <Option value={25}>25</Option>
        <Option value={100}>100</Option>
      </Select>
    </svelte:fragment>
    <svelte:fragment slot="total">
      {start + 1}-{end} of {items.length}
    </svelte:fragment>

    <Button   
      on:click={() => (currentPage = 0)}
      disabled={currentPage === 0}>Start</Button
    >
    <Button
      on:click={() => currentPage--}
      disabled={currentPage === 0}>&lt</Button
    >
    <Button
      on:click={() => currentPage++}
      disabled={currentPage === lastPage}>&gt</Button
    >
    <Button
      on:click={() => (currentPage = lastPage)}
      disabled={currentPage === lastPage}>End</Button
    >
  </Pagination>
</DataTable>