<script>
    import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
    import Card from '@smui/card';
    import ConsumptionForm from "./ConsumptionForm.svelte";
    import Button from "@smui/button/src/Button.svelte"

    </script>
<div class="top-app-bar-container">
    <TopAppBar variant="static">
        <Row>
        <Section>
            <Title>Consumption</Title>
        </Section>
        </Row>
    </TopAppBar>
    <div class="container">
        <center class="select">
            <Card>Select Consumption</Card>
            <ConsumptionForm />
        </center>
        <div class="rightColumn">
            <center>
                <Card>Create new Consumption</Card>
                <Button><div class="large">+</div></Button>
            </center>
            <center>
                <Card>From Template</Card>
                <select><option>1</option></select>
            </center>            
        </div>
    </div>
    <center class="view"><Card>View Consumptions</Card></center>
    
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