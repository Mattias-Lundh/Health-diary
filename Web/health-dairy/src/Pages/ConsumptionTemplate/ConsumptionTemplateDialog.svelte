<script>
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import Select, { Option } from '@smui/select';
    import {createConsumptionTemplate,} from "../../Api/Data"
    import {selectedCampaign, consumptionTemplates} from "../../Store/Store";

    export let open = false;

    let name = '';
    let templateName = '';
    let calories = '';
    let protein = '';
    let carbs = '';
    let amount = '';
    let units = ['grams', 'milliliter'];
    let unitValue = '';

    const save = () => {
      if(name == '' || templateName == '' || amount == 0 || unitValue == undefined){
        console.log("save failed. required fields: name, templateName, amount, unit")
      }else{
        createConsumptionTemplate(
          {
            name,
            templateName,
            calories, 
            protein, 
            carbs, 
            amount, 
            unit: unitValue == "grams" ? "g": "ml", 
            campaign: $selectedCampaign.id 
          }, 
            (data) => {
                consumptionTemplates.update(ct =>  [...ct, data])
        })
      }
    }
    
</script>

<Dialog
  bind:open
  aria-labelledby="title"
  aria-describedby="content"
  style="min-height: 1200px;"
>
  <Title id="title">Add Consumption Template</Title>
  <Content id="content"></Content>
  <Textfield type="text" label="templateName" bind:value={templateName} style="min-height: 60px;"/>
  <Textfield type="text" label="name" bind:value={name} style="min-height: 60px;"/>
  <Textfield type="number" label="calories" bind:value={calories} style="min-height: 60px;"/>
  <Textfield type="number" label="protein" bind:value={protein} style="min-height: 60px;"/>
  <Textfield type="number" label="carbs" bind:value={carbs} style="min-height: 60px;"/>
 
  <span>
  <Select bind:value={unitValue} label="Unit">
    {#each units as unit}
      <Option bind:value={unit}>{unit}</Option>
    {/each}
  </Select>
  <Textfield type="number" label="amount" bind:value={amount} style="min-height: 60px;"/> 
</span>
  <Actions>
    <Button on:click={() => save()}>
      <Label>Save</Label>
    </Button>
    <Button on:click={() => {open = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>