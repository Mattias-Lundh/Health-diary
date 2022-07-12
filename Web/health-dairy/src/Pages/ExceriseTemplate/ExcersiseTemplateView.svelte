<script lang="ts">
    import {excersiseTemplates, selectedCampaign} from "../../Store/Store";
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
    import type {ExcersiseTemplate} from "../../main";
    import {getExcersiseTemplates} from "../../Api/Data";
    import Tooltip, { Wrapper } from '@smui/tooltip';

    export let setSelected;

    getExcersiseTemplates($selectedCampaign.id, (data) => {   
        excersiseTemplates.set(data)
    })

    let items: ExcersiseTemplate[] = [];
  
    $: {
      if($excersiseTemplates)
      {       
        items = $excersiseTemplates.map(c => { 
          return {
          id: c.id,
          templateName: c.templateName,
          name: c.name,
          muscles: c.muscles,
          duration: c.duration,
          weight: c.weight,
          reps: c.reps,
          sets: c.sets          
          } as ExcersiseTemplate;
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
        <Cell>Muscles</Cell>
        <Cell numeric>Duration</Cell>
        <Cell numeric>Weight</Cell>
        <Cell numeric>Reps</Cell>
        <Cell>Sets</Cell>
      </Row>
    </Head>
    <Body>
      {#each slice as excersise (excersise.id)}
        <Row on:click={() => setSelected(excersise)}>
          <Cell numeric>{excersise.id}</Cell>
          <Cell style="width: 100%;">{excersise.templateName}</Cell>
          <Cell>{excersise.name}</Cell>
  
          <Cell>
              <Wrapper>
                  <span tabindex="0">Hover to show</span>
                  <Tooltip unbounded>{excersise.muscles.join()}</Tooltip>
                </Wrapper>
          </Cell>
          <Cell numeric>{excersise.duration}</Cell>
          <Cell numeric>{excersise.weight}</Cell>
          <Cell numeric>{excersise.reps}</Cell>
          <Cell numeric>{excersise.sets}</Cell>
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