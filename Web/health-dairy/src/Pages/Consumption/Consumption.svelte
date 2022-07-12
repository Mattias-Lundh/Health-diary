<script lang="ts">
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import ConsumptionDialog from "./ConsumptionDialog.svelte";
    import ConsumptionView from "./ConsumptionView.svelte"
    import Button from "@smui/button/src/Button.svelte"
    import {selectedCampaign, consumptionTemplates} from "../../Store/Store";
    import Edit from "./Edit.svelte"    
    import {createConsumption} from "../../Api/Data";
    import {consumptions} from "../../Store/Store";

    let openDialog = false;
    let openEdit = false;
    let openDelete = false;

    let selectedConsumption;

    let templateSelection;

    const setConsumption = (con) => {
        selectedConsumption = con;
    }

    const createFromTemplate = () => {
        let selectedTemplate = $consumptionTemplates.filter(ct => ct.id == templateSelection.value)[0]
        
        
        createConsumption(
          {
            name: selectedTemplate.name, 
            calories: selectedTemplate.calories, 
            protein: selectedTemplate.protein, 
            carbs: selectedTemplate.carbs, 
            amount: selectedTemplate.amount, 
            unit: selectedTemplate.unit == "grams" ? "g": "ml", 
            day: new Date(Date.now()).toISOString(),
            campaign: $selectedCampaign.id }, 
            (data) => {
              consumptions.update(con =>  [...con, data])
        })
    }

</script>

{#if ($selectedCampaign)}
<div class="top-app-bar-container">
    <ConsumptionDialog bind:open={openDialog}/>
    <TopAppBar variant="static">
        <Row>
        <Section>
            <Title>Consumption</Title>
        </Section>
        </Row>
    </TopAppBar>
    <div class="container">
        <center class="select">
            <Card>Modify Consumption</Card>
            {#if selectedConsumption}
            <div>
            {selectedConsumption.name}
            </div>
            <Button on:click={() => openEdit = true}>Modify</Button>
            <Button on:click={() => openDelete = true}>Delete</Button>
            <Edit bind:selectedConsumption bind:openEdit bind:openDelete />
            {:else}
            no consumption selected
            {/if}
            
        </center>
        <div class="rightColumn">
            <center>
                <Card>Add Consumption</Card>
                <Button on:click={() => {openDialog = true;}}><div class="large">+</div></Button>
            </center>
            {#if $consumptionTemplates && $consumptionTemplates.length > 0}
            <center>
                <Card>From Template</Card>
                <select bind:this={templateSelection}>
                {#each $consumptionTemplates as template (template.id)}
                
                    <option value={template.id}>{template.templateName}</option>
                
                {/each}
            </select>
                <Button on:click={() => createFromTemplate()}><div class="large">+</div></Button>
            </center>            
            {/if}
            
        </div>
    </div>
    <center class="view"><Card>View Consumptions</Card></center>
    <ConsumptionView setSelected={setConsumption}/>
    
</div>
{:else}
SELECT A CAMPAIGN TO VIEW CONSUMPTIONS
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