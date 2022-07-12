<script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import ExcersiseTemplateDialog from "./ExcersiseTemplateDialog.svelte";
    import ExcersiseTemplateView from "./ExcersiseTemplateView.svelte"
    import Button from "@smui/button/src/Button.svelte"
    import {selectedCampaign} from "../../Store/Store";
    import Edit from "./Edit.svelte"    

    let openDialog = false;
    let openEdit = false;
    let openDelete = false;

    let selectedExcersiseTemplate;

    const setExcersiseTemplate = (et) => {
        selectedExcersiseTemplate = et;
    }    
</script>

{#if ($selectedCampaign)}
<div class="top-app-bar-container">
    <ExcersiseTemplateDialog bind:open={openDialog}/>
    <div class="top-app-bar-container">
        <TopAppBar variant="static">
            <Row>
            <Section>
                <Title>Excersise Template</Title>
            </Section>
            </Row>
        </TopAppBar>
    </div>

    <div class="container">
        <center class="select">
            <Card>Modify Excersise Template</Card>
            {#if selectedExcersiseTemplate}
            <div>
            {selectedExcersiseTemplate.templateName}
            </div>
            <Button on:click={() => openEdit = true}>Modify</Button>
            <Button on:click={() => openDelete = true}>Delete</Button>
            <Edit bind:selectedExcersiseTemplate bind:openEdit bind:openDelete />
            {:else}
            no excersise template selected
            {/if}
            
        </center>
        <div class="rightColumn">
            <center>
                <Card>Add Excersise template</Card>
                <Button on:click={() => {openDialog = true;}}><div class="large">+</div></Button>
            </center>
        </div>
    </div>
    <center class="view"><Card>View Excersise templates</Card></center>
    <ExcersiseTemplateView setSelected={setExcersiseTemplate}/>
    
</div>
{:else}
SELECT A CAMPAIGN TO VIEW EXCERSISE TEMPLATES
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