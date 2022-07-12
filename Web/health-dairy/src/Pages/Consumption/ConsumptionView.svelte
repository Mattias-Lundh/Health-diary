<script lang="ts">
    import {consumptions, consumptionTemplates, selectedCampaign} from "../../Store/Store";
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
    import type {Consumption} from "../../main";
    import {getConsumptions, getConsumptionTemplates} from "../../Api/Data";
    import {formatUserfriendlyDate} from "../../Util/util";

    export let setSelected;

    getConsumptionTemplates($selectedCampaign.id, (data) => { 
      console.log("data")  
      console.log(data)  
        consumptionTemplates.set(data)
    })

    getConsumptions($selectedCampaign.id, (data) => {      
      consumptions.set(data)
    })

    let items: Consumption[] = [];
  
    $: {
      if($consumptions)
      {
        items = $consumptions.map(c => { 
          return {
          id: c.id,
          name: c.name,
          calories: c.calories,
          protein: c.protein,
          carbs: c.carbs,
          amount: c.amount,
          unit: c.unit,
          day: c.day
          } as Consumption;
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
      <Cell style="width: 100%;">Name</Cell>
      <Cell numeric>Calories</Cell>
      <Cell numeric>Protein</Cell>
      <Cell numeric>Carbs</Cell>
      <Cell numeric>Amount</Cell>
      <Cell numeric>Unit</Cell>
      <Cell date>Day</Cell>
    </Row>
  </Head>
  <Body>
    {#each slice as consumption (consumption.id)}
      <Row on:click={() => setSelected(consumption)}>
        <Cell numeric>{consumption.id}</Cell>
        <Cell style="width: 100%;">{consumption.name}</Cell>
        <Cell numeric>{consumption.calories}</Cell>
        <Cell numeric>{consumption.protein}</Cell>
        <Cell numeric>{consumption.carbs}</Cell>
        <Cell numeric>{consumption.amount}</Cell>
        <Cell numeric>{consumption.unit}</Cell>
        <Cell date>{formatUserfriendlyDate(new Date(consumption.day))}</Cell>
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