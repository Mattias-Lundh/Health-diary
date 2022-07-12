<script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import Select from "./Select.svelte";
    import Button from '@smui/button/src/Button.svelte';
    import Edit from "./Edit.svelte";
    import { selectedCampaign } from "../../Store/Store";
    import CreateDialog from "./CreateDialog.svelte";

    let open;

</script>

<TopAppBar variant="static">
    <Row>
    <Section>
        <Title>Campaign</Title>
    </Section>
    </Row>
</TopAppBar>
<div class="container">
    <center class="select">
        <Card>Select Campaign</Card>
        <Select />
        <Card>Selection: {#if $selectedCampaign} {$selectedCampaign.name} {:else} none {/if}</Card>
        {#if $selectedCampaign} <Edit bind:campaignName={$selectedCampaign.name} bind:campaignId={$selectedCampaign.id}/> {/if}
    </center>
    <center class="new">
        <Card>Create new Campaign</Card>
        <Button on:click={() => open = true}><div class="large">+</div></Button>
    </center>
    <CreateDialog bind:open={open}/>
</div>


<style>
.container{
    display: flex;
    flex-direction: row;
    margin-top: 50px;
    font-size: 30px;
    
}

.select {
    width: 50%;
    background-color: #eaf7fe;
}

.new {
    width: 50%;
    background-color: #dff4ff;
}

.large{
    font-size: 50px;
}

</style>