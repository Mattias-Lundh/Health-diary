<script lang="ts">
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {updateConsumptionTemplate, deleteConsumptionTemplate} from "../../Api/Data";
    import { consumptionTemplates } from '../../Store/Store';
    import Select, { Option } from '@smui/select';
    import type {ConsumptionTemplate} from '../../main';
    
    export let selectedConsumptionTemplate: ConsumptionTemplate;

    export let openEdit = false;
    export let openDelete = false;

    let disabled = true;
    
    let templateName = selectedConsumptionTemplate.templateName;
    let name = selectedConsumptionTemplate.name;
    let calories = selectedConsumptionTemplate.calories;
    let protein = selectedConsumptionTemplate.protein;
    let carbs = selectedConsumptionTemplate.carbs;
    let amount = selectedConsumptionTemplate.amount;
    let units = ['grams', 'milliliter'];
    let unitValue = '';

    const saveConsumptionTemplate = () => {
        if(name == '' || templateName == '' || amount == 0 || unitValue == undefined){
            console.log("save failed. required fields: name, templateName, amount, unit")
        }else{            
            updateConsumptionTemplate(
                {
                    id: selectedConsumptionTemplate.id,
                    templateName,
                    name,
                    calories,
                    protein,
                    carbs,
                    amount,
                    unit: unitValue == "grams" ? "g" : "ml"
                }, 
                (data) => {
                if(data == "success"){
                    consumptionTemplates.update(c => {                        
                        let modified = c.filter(co => co.id == selectedConsumptionTemplate.id)[0]
                        modified.templateName = templateName;
                        modified.name = name;
                        modified.calories = calories;
                        modified.protein = protein;
                        modified.carbs = carbs;
                        modified.amount = amount;
                        modified.unit = unitValue;                        
                        c = [...c.filter(co => co.id != selectedConsumptionTemplate.id), modified]
                        
                        return c;
                    })
                }
            })
        }
    }

    const removeConsumptionTemplate = () => {
        deleteConsumptionTemplate(selectedConsumptionTemplate.id, (data) => {          
            if(data == "success"){            
                    consumptionTemplates.update(c => { 
                        c = [...c.filter(ct => ct.id != selectedConsumptionTemplate.id)]
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
  <Title id="title">Modify Consumption Template {selectedConsumptionTemplate.templateName}</Title>
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
    <Button on:click={() => saveConsumptionTemplate()}>
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
  <Title id="title">Delete Consumption Template {selectedConsumptionTemplate.templateName}</Title>
  <Content id="content">Are you sure you want to delete {selectedConsumptionTemplate.templateName}</Content>  
  <Actions>
    <Button on:click={() => removeConsumptionTemplate()}>
      <Label>Ok</Label>
    </Button>
    <Button on:click={() => {openDelete = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>

