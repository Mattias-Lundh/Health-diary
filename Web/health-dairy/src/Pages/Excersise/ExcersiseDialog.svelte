<script>
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import Select, { Option } from '@smui/select';
    import Checkbox from '@smui/checkbox';
    import FormField from '@smui/form-field';
    import {createExcersise,} from "../../Api/Data"
    import {selectedCampaign, excersises} from "../../Store/Store";
    import SegmentedButton, { Segment } from '@smui/segmented-button';

    export let open = false;
    let disabled = true;

    let name = '';
    let musclesSelected = [];
    let duration = 0;
    let weight = 0;
    let reps = 0;
    let sets = 0;
    let autoDate = true;
    let date = '';    

    let muscleChoices = [
                            'deltoids', 'trapezius', 'TeresMajor', 'pectoralisMajor', 'rhomboids', 
                            'tricepsBrachii', 'bicepsBrachii', 'seratusAnterior', 'rectusAbdominis', 
                            'obliquusExternusAbdominis', 'vastusMedialis', 'vastusIntermedius', 
                            'vastusLateralis', 'rectusFemoris', 'gluteusMaximus'
                        ];

    const save = () => {
      if(name == ''){
        console.log("save failed. a name is required")
      }else{
        createExcersise(
          {
            name, 
            muscles : musclesSelected,
            duration,
            weight,
            reps,
            sets,
            day: autoDate ? new Date(Date.now()).toISOString() : new Date(date).toISOString(), 
            campaign: $selectedCampaign.id 
        }, 
            (data) => {
                excersises.update(ex => [...ex, data])
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
    <Title id="title">Add Excersise</Title>
    <Content id="content"></Content>
    <Textfield type="text" label="name" bind:value={name} style="min-height: 60px;"/>

    <SegmentedButton segments={muscleChoices} let:segment bind:selected={musclesSelected}>
    <Segment {segment}>
        <Label>{segment}</Label>
    </Segment>
    </SegmentedButton>

    <Textfield type="number" label="duration" bind:value={duration} style="min-height: 60px;"/>
    <Textfield type="number" label="weight" bind:value={weight} style="min-height: 60px;"/>
    <Textfield type="number" label="reps" bind:value={reps} style="min-height: 60px;"/> 
    <Textfield type="number" label="sets" bind:value={sets} style="min-height: 60px;"/> 
 
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