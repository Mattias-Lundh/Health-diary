<script>
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";    
    import {createMeal,} from "../../Api/Data"
    import SegmentedButton, { Segment } from '@smui/segmented-button';
    import {meals, consumptionTemplates} from "../../Store/Store";

    export let open = false;    

    let name = '';
    let templatesSelected = [];
    let templateChoices = [];

    $: {
      templateChoices = $consumptionTemplates? $consumptionTemplates.map(t => t.templateName) : [];
      console.log($consumptionTemplates)
    }

    const save = () => {
      if(name == ''){
        console.log("save failed. meal requires a name")
      }else{
        
        createMeal(
            {   
                name, 
                foodItems: $consumptionTemplates.filter(t => templatesSelected.includes(t.templateName)), 
            }, 
            (data) => {
              meals.update(m => m? [...m, data] : [data])
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
  <SegmentedButton segments={templateChoices} let:segment bind:selected={templatesSelected}>
    <Segment {segment}>
        <Label>{segment}</Label>
    </Segment>
    </SegmentedButton>
  <Actions>
    <Button on:click={() => save()}>
      <Label>Save</Label>
    </Button>
    <Button on:click={() => {open = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>