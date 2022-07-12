<script lang="ts">
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {updateMeal, deleteMeal} from "../../Api/Data";
    import { meals, consumptionTemplates } from '../../Store/Store';
    import SegmentedButton, { Segment } from '@smui/segmented-button';
    import type {Meal} from '../../main';
    
    
    export let selectedMeal: Meal;

    export let openEdit = false;
    export let openDelete = false;

    let name = selectedMeal.name;
    let foodItemsSelected = []
    let templateChoices = $consumptionTemplates? $consumptionTemplates.map(t => t.templateName) :[]
    

  const saveMeal = () => {
      if(name == ''){
          console.log("save failed. meal requires a name")
      }else{            
          updateMeal(
              {
                  id: selectedMeal.id,
                  name,                    
                  foodItems: selectedMeal ? selectedMeal.foodItems.filter(fi => foodItemsSelected.includes(fi.templateName)) : []
              }, 
              (data) => {
              if(data == "success"){
                  meals.update(m => {                        
                      let modified = m.filter(me => me.id == selectedMeal.id)[0]
                      modified.name = name;
                      modified.foodItems = foodItemsSelected                        
                      m = [...m.filter(me => me.id != selectedMeal.id), modified]
                      
                      return m;
                  })
              }
          })
      }
  }

  const removeMeal = () => {
      deleteMeal(selectedMeal.id, (data) => {          
        if(data == "success"){            
            meals.update(m => { 
                m = [...m.filter(me => me.id != selectedMeal.id)]
                return m;
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
  <Title id="title">Modify Meal {selectedMeal.name}</Title>
  <Content id="content"></Content>
  <Textfield type="text" label="name" bind:value={name} style="min-height: 60px;"/>
  <SegmentedButton segments={templateChoices} let:segment bind:selected={foodItemsSelected}>
    <Segment {segment}>
        <Label>{segment}</Label>
    </Segment>
    </SegmentedButton>
  <Actions>
    <Button on:click={() => saveMeal()}>
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
  <Title id="title">Delete Meal {selectedMeal.name}</Title>
  <Content id="content">Are you sure you want to delete {selectedMeal.name}</Content>  
  <Actions>
    <Button on:click={() => removeMeal()}>
      <Label>Ok</Label>
    </Button>
    <Button on:click={() => {openDelete = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>

