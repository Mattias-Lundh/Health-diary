    <script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import {selectedCampaign} from "../../Store/Store";
    import Button from '@smui/button/src/Button.svelte';
    import Excersises from "./Excersises.svelte";
    import Consumptions from "./Consumptions.svelte";

    let viewConsumption = true;
    let campaign;
    selectedCampaign.subscribe(value => {
        campaign = value;
    })

</script>

<TopAppBar variant="static">
    <Row>
    <Section>
        <Title>Home</Title>
    </Section>
    </Row>
</TopAppBar>
<div class="container">
    <center class="consumption">
        <Button on:click={() => viewConsumption = true}><div class="btn-consumption">Consumptions</div></Button>
    </center>
    <center class="excersise">
        <Button on:click={() => viewConsumption = false}><div class="btn-excersise">Excersises</div></Button>
    </center>
</div>
<center class="view">
    <Card>View</Card>
    {#if !campaign} 
    NO CAMPAIGN SELECTED
    {:else if viewConsumption}
    <Consumptions />
    {:else}
    <Excersises />
    
    {/if} 
</center>


<style>
.container{
    display: flex;
    flex-direction: row;
    margin-top: 50px;
    font-size: 30px;
    
}
.consumption {
    width: 50%;
}
.btn-consumption {
    
    font-size: 24px;
    
}
.excersise {
    width: 50%;
}
.btn-excersise {
    
    font-size: 24px;
    
}
.view {
    font-size: 24px;
}
</style>