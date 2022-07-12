<script>
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import Select, { Option } from '@smui/select';
    import Checkbox from '@smui/checkbox';
    import FormField from '@smui/form-field';
    import {createConsumption,} from "../../Api/Data"
    import {selectedCampaign, consumptions} from "../../Store/Store";

    export let open = false;
    let disabled = true;

    let name = '';
    let calories = '';
    let protein = '';
    let carbs = '';
    let amount = '';
    let autoDate = true;
    let date = '';
    let units = ['grams', 'milliliter'];
    let unitValue = '';

    const save = () => {
      if(name == '' || amount == 0 || unitValue == undefined){
        console.log("save failed. required fields: name, amount, unit")
      }else{
        createConsumption(
          {
            name, 
            calories, 
            protein, 
            carbs, 
            amount, 
            unit: unitValue == "grams" ? "g": "ml", 
            day: autoDate ? new Date(Date.now()).toISOString() : new Date(date).toISOString(), 
            campaign: $selectedCampaign.id }, 
            (data) => {
              consumptions.update(con =>  [...con, data])
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
  <Title id="title">Add Consumption</Title>
  <Content id="content"></Content>
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
<FormField>
  <Textfield type="date" label="date" bind:value={date} disabled={disabled} style="min-height: 60px;"/>
  <Checkbox bind:checked={autoDate} on:change={() => {disabled = !disabled;}}/>
  <span slot="label">Today's date</span>
  
</FormField>
  <Actions>
    <Button on:click={() => save()}>
      <Label>Save</Label>
    </Button>
    <Button on:click={() => {open = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>