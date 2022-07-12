<script lang="ts">
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import MealDialog from "./MealDialog.svelte";
    import MealView from "./MealView.svelte"
    import Button from "@smui/button/src/Button.svelte"
    import {selectedCampaign, consumptionTemplates} from "../../Store/Store";
    import Edit from "./Edit.svelte"    
    import {createMeal} from "../../Api/Data";
    import type {Meal} from '../../main';

    let openDialog = false;
    let openEdit = false;
    let openDelete = false;

    let selectedMeal: Meal;

    const setMeal = (m) => {
        selectedMeal = m;
    }    

</script>

{#if ($selectedCampaign)}
<div class="top-app-bar-container">
    <MealDialog bind:open={openDialog}/>
    <div class="top-app-bar-container">
        <TopAppBar variant="static">
            <Row>
            <Section>
                <Title>Meal</Title>
            </Section>
            </Row>
        </TopAppBar>
    </div>
    <div class="container">
        <center class="select">
            <Card>Modify Meal</Card>
            {#if selectedMeal}
            <div>
            {selectedMeal.name}
            </div>
            <Button on:click={() => openEdit = true}>Modify</Button>
            <Button on:click={() => openDelete = true}>Delete</Button>
            <Edit bind:selectedMeal bind:openEdit bind:openDelete />
            {:else}
            no meal selected
            {/if}
            
        </center>
        <div class="rightColumn">
            <center>
                <Card>Add Meal</Card>
                <Button on:click={() => {openDialog = true;}}><div class="large">+</div></Button>
            </center>
        </div>
    </div>
    <center class="view"><Card>View Meals</Card></center>
    <MealView setSelected={setMeal}/>
    
</div>
{:else}
SELECT A CAMPAIGN TO VIEW MEALS
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