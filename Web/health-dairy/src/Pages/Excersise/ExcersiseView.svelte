<script lang="ts">
    import {excersises, selectedCampaign} from "../../Store/Store";
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
    import type {Excersise} from "../../main";
    import {getExcersises, getExcersiseTemplates} from "../../Api/Data";
    import {excersiseTemplates} from "../../Store/Store"
    import {formatUserfriendlyDate} from "../../Util/util";
    import Tooltip, { Wrapper } from '@smui/tooltip';    

    export let setSelected;

    let items : Excersise[] = [];

    getExcersises($selectedCampaign.id, (data) => {
      excersises.set(data)
    })

    getExcersiseTemplates($selectedCampaign.id, (data) => {      
      excersiseTemplates.set(data)
    });
  
    $: {
      if($excersises)
      {       
        items = $excersises.map(e => { 
          return {
          id: e.id,
          name: e.name,
          muscles: e.muscles,
          duration: e.duration,
          weight: e.weight,
          reps: e.reps,
          sets: e.sets,
          day: e.day
          } as Excersise;
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
      <Cell>Muscles</Cell>
      <Cell numeric>Duration</Cell>
      <Cell numeric>Weight</Cell>
      <Cell numeric>Reps</Cell>
      <Cell>Sets</Cell>
      <Cell date>Day</Cell>
    </Row>
  </Head>
  <Body>
    {#each slice as excersise (excersise.id)}
      <Row on:click={() => setSelected(excersise)}>
        <Cell numeric>{excersise.id}</Cell>
        <Cell style="width: 100%;">{excersise.name}</Cell>

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
        <Cell date>{formatUserfriendlyDate(new Date(excersise.day))}</Cell>
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