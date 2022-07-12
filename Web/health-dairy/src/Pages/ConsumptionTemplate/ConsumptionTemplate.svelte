<script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import ConsumptionTemplateDialog from "./ConsumptionTemplateDialog.svelte";
    import ConsumptionTemplateView from "./ConsumptionTemplateView.svelte"
    import Button from "@smui/button/src/Button.svelte"
    import {selectedCampaign} from "../../Store/Store";
    import Edit from "./Edit.svelte"    

    let openDialog = false;
    let openEdit = false;
    let openDelete = false;

    let selectedConsumptionTemplate;

    const setConsumptionTemplate = (ct) => {
        selectedConsumptionTemplate = ct;
    }
</script>

{#if ($selectedCampaign)}
<div class="top-app-bar-container">
    <ConsumptionTemplateDialog bind:open={openDialog}/>
    <TopAppBar variant="static">
        <Row>
        <Section>
            <Title>Consumption Template</Title>
        </Section>
        </Row>
    </TopAppBar>

    <div class="container">
        <center class="select">
            <Card>Modify Consumption Template</Card>
            {#if selectedConsumptionTemplate}
            <div>
            {selectedConsumptionTemplate.templateName}
            </div>
            <Button on:click={() => openEdit = true}>Modify</Button>
            <Button on:click={() => openDelete = true}>Delete</Button>
            <Edit bind:selectedConsumptionTemplate bind:openEdit bind:openDelete />
            {:else}
            no consumption template selected
            {/if}
            
        </center>
        <div class="rightColumn">
            <center>
                <Card>Add Consumption template</Card>
                <Button on:click={() => {openDialog = true;}}><div class="large">+</div></Button>
            </center>
        </div>
    </div>
    <center class="view"><Card>View Consumption templates</Card></center>
    <ConsumptionTemplateView setSelected={setConsumptionTemplate}/>
    
</div>
{:else}
SELECT A CAMPAIGN TO VIEW CONSUMPTION TEMPLATES
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