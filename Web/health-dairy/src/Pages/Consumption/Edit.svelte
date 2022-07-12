<script lang="ts">
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import FormField from "@smui/form-field";
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {updateConsumption, deleteConsumption} from "../../Api/Data";
    import { consumptions } from '../../Store/Store';
    import Select, { Option } from '@smui/select';
    import Checkbox from '@smui/checkbox';
    import type {Consumption} from '../../main';
    
    export let selectedConsumption: Consumption;

    export let openEdit = false;
    export let openDelete = false;

    let disabled = true;
    
    let name = selectedConsumption.name;
    let calories = selectedConsumption.calories;
    let protein = selectedConsumption.protein;
    let carbs = selectedConsumption.carbs;
    let amount = selectedConsumption.amount;

    let date = selectedConsumption.day
    let units = ['grams', 'milliliter'];
    let unitValue = '';
    let autoDate = true;

    const saveConsumption = () => {
        if(name == '' || amount == 0 || unitValue == undefined){
            console.log("save failed. required fields: name, amount, unite")
        }else{            
            updateConsumption(
                {
                    id: selectedConsumption.id,
                    name,
                    calories,
                    protein,
                    carbs,
                    amount,
                    day: autoDate? new Date(Date.now()).toISOString() :new Date(date).toISOString(),
                    unit: unitValue == "grams" ? "g" : "ml"
                }, 
                (data) => {
                if(data == "success"){
                    consumptions.update(c => {                        
                        let modified = c.filter(co => co.id == selectedConsumption.id)[0]
                        modified.name = name;
                        modified.calories = calories;
                        modified.protein = protein;
                        modified.carbs = carbs;
                        modified.amount = amount;
                        modified.date = date;
                        modified.unit = unitValue;                        
                        c = [...c.filter(co => co.id != selectedConsumption.id), modified]
                        
                        return c;
                    })
                }
            })
        }
    }

    const removeConsumption = () => {
        deleteConsumption(selectedConsumption.id, (data) => {          
            if(data == "success"){            
                    consumptions.update(c => { 
                        c = [...c.filter(co => co.id != selectedConsumption.id)]
                        return c;
                    })
                }
        })
        

    }

</script>


<Dialog
  bind:open={openEdit}
  aria-labelledby="title"
  aria-describedby="content"
  style="min-height: 1200px;"
>
  <Title id="title">Modify Consumption {selectedConsumption.name}</Title>
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
    <Button on:click={() => saveConsumption()}>
      <Label>Save</Label>
    </Button>
    <Button on:click={() => {openEdit = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>

<Dialog
bind:open={openDelete}
  aria-labelledby="title"
  aria-describedby="content"
>
  <Title id="title">Delete Consumption {selectedConsumption}</Title>
  <Content id="content">Are you sure you want to delete {selectedConsumption.name}</Content>  
  <Actions>
    <Button on:click={() => removeConsumption()}>
      <Label>Ok</Label>
    </Button>
    <Button on:click={() => {openDelete = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>

