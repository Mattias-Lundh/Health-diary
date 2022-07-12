<script>
  import Dialog, { Title, Content, Actions } from '@smui/dialog';
  import FormField from "@smui/form-field";
  import Button, { Label } from '@smui/button';
  import Textfield from "@smui/textfield";
  import {updateExcersise, deleteExcersise} from "../../Api/Data";
  import { excersises } from '../../Store/Store';
  import Checkbox from '@smui/checkbox';
  import SegmentedButton, { Segment } from '@smui/segmented-button';
  
  export let selectedExcersise;

  export let openEdit = false;
  export let openDelete = false;

  let disabled = true;
  
  let name = selectedExcersise.name;
  let musclesSelected = selectedExcersise.muscles;
  let duration = selectedExcersise.duration;
  let weight = selectedExcersise.weight;
  let reps = selectedExcersise.weight;
  let sets = selectedExcersise.sets;

  let muscleChoices = [
                          'deltoids', 'trapezius', 'TeresMajor', 'pectoralisMajor', 'rhomboids', 
                          'tricepsBrachii', 'bicepsBrachii', 'seratusAnterior', 'rectusAbdominis', 
                          'obliquusExternusAbdominis', 'vastusMedialis', 'vastusIntermedius', 
                          'vastusLateralis', 'rectusFemoris', 'gluteusMaximus'
                      ];

  let date = selectedExcersise.day;
  let autoDate = true;

  const saveExcersise = () => {
      if(name == ''){
          console.log("save failed. a name is required")
      }else{
          
          updateExcersise(
              {
                  id: selectedExcersise.id,
                  name,
                  muscles : musclesSelected,
                  duration,
                  weight,
                  reps,
                  sets,                  
                  day: autoDate? new Date(Date.now()).toISOString() :new Date(date).toISOString(),         
              }, 
              (data) => {
              if(data == "success"){
                  excersises.update(e => {                        
                      let modified = e.filter(ex => ex.id == selectedExcersise.id)[0]
                      modified.name = name;
                      modified.muscles = musclesSelected;
                      modified.duration = duration;
                      modified.weight = weight;
                      modified.reps = reps;
                      modified.sets = sets;
                      modified.day = date;
                                      
                      e = [...e.filter(ex => ex.id != selectedExcersise.id), modified]
                      
                      return e;
                  })
              }
          })
      }
  }

  const removeExcersise = () => {
      deleteExcersise(selectedExcersise.id, (data) => {    
        if(data == "success"){            
          excersises.update(e => { 
              e = [...e.filter(ex => ex.id != selectedExcersise.id)]
              return e;
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
    <Title id="title">Modify Excersise {selectedExcersise.name}</Title>
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
    <Button on:click={() => saveExcersise()}>
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
  <Title id="title">Delete Excersise {selectedExcersise.name}</Title>
  <Content id="content">Are you sure you want to delete {selectedExcersise.name}</Content>  
  <Actions>
    <Button on:click={() => removeExcersise()}>
      <Label>Ok</Label>
    </Button>
    <Button on:click={() => {openDelete = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>

