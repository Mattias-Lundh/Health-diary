<script lang="ts">
    import {consumptionTemplates, selectedCampaign} from "../../Store/Store";
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
    import type {ConsumptionTemplate} from "../../main";
    import {getConsumptionTemplates} from "../../Api/Data";

    export let setSelected;

    getConsumptionTemplates($selectedCampaign.id, (data) => {   
        consumptionTemplates.set(data)
    })

    let items: ConsumptionTemplate[] = [];
  
    $: {
      if($consumptionTemplates)
      {       
        items = $consumptionTemplates.map(c => { 
          return {
          id: c.id,
          templateName: c.templateName,
          name: c.name,
          calories: c.calories,
          protein: c.protein,
          carbs: c.carbs,
          amount: c.amount,
          unit: c.unit,
          } as ConsumptionTemplate;
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
      <Cell style="width: 100%;">Template Name</Cell>
      <Cell>Name</Cell>
      <Cell numeric>Calories</Cell>
      <Cell numeric>Protein</Cell>
      <Cell numeric>Carbs</Cell>
      <Cell numeric>Amount</Cell>
      <Cell numeric>Unit</Cell>
    </Row>
  </Head>
  <Body>
    {#each slice as consumptionTemplate (consumptionTemplate.id)}
      <Row on:click={() => setSelected(consumptionTemplate)}>
        <Cell numeric>{consumptionTemplate.id}</Cell>
        <Cell style="width: 100%;">{consumptionTemplate.templateName}</Cell>
        <Cell >{consumptionTemplate.name}</Cell>
        <Cell numeric>{consumptionTemplate.calories}</Cell>
        <Cell numeric>{consumptionTemplate.protein}</Cell>
        <Cell numeric>{consumptionTemplate.carbs}</Cell>
        <Cell numeric>{consumptionTemplate.amount}</Cell>
        <Cell numeric>{consumptionTemplate.unit}</Cell>        
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