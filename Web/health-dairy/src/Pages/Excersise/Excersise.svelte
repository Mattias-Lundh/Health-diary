<script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import ExcersiseDialog from "./ExcersiseDialog.svelte";
    import ExcersiseView from "./ExcersiseView.svelte"
    import Button from "@smui/button/src/Button.svelte"
    import {selectedCampaign, excersiseTemplates, excersises} from "../../Store/Store";
    import {createExcersise} from "../../Api/Data";
    import Edit from "./Edit.svelte"
    

    let openDialog = false;
    let openEdit = false;
    let openDelete = false;
    
    let selectedExcersise;
    let templateSelection;

    const setExcersise = (ex) => {
        selectedExcersise = ex;
    }    

    const createFromTemplate = () => {
        let selectedTemplate = $excersiseTemplates.filter(et => et.id == templateSelection.value)[0]
        
        createExcersise(
          {
            name: selectedTemplate.name, 
            muscles: selectedTemplate.muscles,
            duration: selectedTemplate.duration,
            weight: selectedTemplate.weight,
            reps: selectedTemplate.reps,
            sets: selectedTemplate.sets,
            day: new Date(Date.now()).toISOString(),
            campaign: $selectedCampaign.id }, 
            (data) => {
              excersises.update(e =>  [...e, data])
        })
    }

    

</script>

{#if ($selectedCampaign)}
<div class="top-app-bar-container">
    <ExcersiseDialog bind:open={openDialog}/>
    <TopAppBar variant="static">
        <Row>
        <Section>
            <Title>Excersise</Title>
        </Section>
        </Row>
    </TopAppBar>
    <div class="container">
        <center class="select">
            <Card>Modify Excersise</Card>
            {#if selectedExcersise}
            <div>
            {selectedExcersise.name}
            </div>
            <Button on:click={() => openEdit = true}>Modify</Button>
            <Button on:click={() => openDelete = true}>Delete</Button>
            <Edit bind:selectedExcersise bind:openEdit bind:openDelete />
            {:else}
            no excersise selected
            {/if}
            
        </center>
        <div class="rightColumn">
            <center>
                <Card>Add Excersise</Card>
                <Button on:click={() => {openDialog = true;}}><div class="large">+</div></Button>
            </center>
            {#if $excersiseTemplates && $excersiseTemplates.length > 0}
            <center>
                <Card>From Template</Card>
                <select bind:this={templateSelection}>
                {#each $excersiseTemplates as template (template.id)}
                
                    <option value={template.id}>{template.templateName}</option>
                
                {/each}
            </select>
                <Button on:click={() => createFromTemplate()}><div class="large">+</div></Button>
            </center>            
            {/if} 
        </div>
    </div>
    <center class="view"><Card>View Excersises</Card></center>
    <ExcersiseView setSelected={setExcersise}/>
    
</div>
{:else}
SELECT A CAMPAIGN TO VIEW EXERSISES
{/if}
<style>
    .container{
        display: flex;
        flex-direction: row;
        margin-top: 50px;
        font-size: 30px;
        
    }
    
    .select {
        width: 50%;
        
    }
    
    .rightColumn {
        width: 50%;
        
    }
    
    .large{
        font-size: 50px;        
    }

    .view {
        font-size: 30px;
    }
    
    </style>