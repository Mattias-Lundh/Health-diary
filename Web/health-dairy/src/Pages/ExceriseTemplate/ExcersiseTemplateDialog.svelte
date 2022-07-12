<script>
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import SegmentedButton, { Segment } from '@smui/segmented-button';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {createExcersiseTemplate,} from "../../Api/Data"
    import {selectedCampaign, excersiseTemplates} from "../../Store/Store";

    export let open = false;

    let name = '';
    let templateName = '';
    let muscleSelected = [];
    let duration = 0;
    let weight = 0;
    let reps = 0;
    let sets = 0;  

    let muscleChoices = [
                            'deltoids', 'trapezius', 'TeresMajor', 'pectoralisMajor', 'rhomboids', 
                            'tricepsBrachii', 'bicepsBrachii', 'seratusAnterior', 'rectusAbdominis', 
                            'obliquusExternusAbdominis', 'vastusMedialis', 'vastusIntermedius', 
                            'vastusLateralis', 'rectusFemoris', 'gluteusMaximus'
                        ];


    const save = () => {
      if(name == '' || templateName == ''){
        console.log("save failed. required fields: name, templateName")
      }else{
        createExcersiseTemplate(
          {
            name,
            templateName,
            muscles : muscleSelected,
            duration,
            weight,
            reps,
            sets,
            campaign : $selectedCampaign.id
          },
            (data) => {
                excersiseTemplates.update(et =>  [...et, data])
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
    <Title id="title">Add Excersise Template</Title>
    <Content id="content"></Content>
    <Textfield type="text" label="templateName" bind:value={templateName} style="min-height: 60px;"/>
    <Textfield type="text" label="name" bind:value={name} style="min-height: 60px;"/>

    <SegmentedButton segments={muscleChoices} let:segment bind:selected={muscleSelected}>
    <Segment {segment}>
        <Label>{segment}</Label>
    </Segment>
    </SegmentedButton>

    <Textfield type="number" label="duration" bind:value={duration} style="min-height: 60px;"/>
    <Textfield type="number" label="weight" bind:value={weight} style="min-height: 60px;"/>
    <Textfield type="number" label="reps" bind:value={reps} style="min-height: 60px;"/> 
    <Textfield type="number" label="sets" bind:value={sets} style="min-height: 60px;"/> 

  <Actions>
    <Button on:click={() => save()}>
      <Label>Save</Label>
    </Button>
    <Button on:click={() => {open = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>