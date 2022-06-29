<script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import Form from "./Form.svelte";
    import Button from '@smui/button/src/Button.svelte';
    import Edit from "./Edit.svelte";
    import { selectedCampaign } from "../../Store/Store"; 
    
    

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
        <Form />
        <Card>Selection: {#if $selectedCampaign} {$selectedCampaign} {:else} none {/if}</Card>
        {#if $selectedCampaign} <Edit /> {/if}
    </center>
    <center class="new">
        <Card>Create new Campaign</Card>
        <Button><div class="large">+</div></Button>
    </center>
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