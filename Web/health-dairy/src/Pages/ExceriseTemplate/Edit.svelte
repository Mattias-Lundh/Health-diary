<script lang="ts">
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import SegmentedButton, { Segment } from '@smui/segmented-button';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {updateExcersiseTemplate, deleteExcersiseTemplate} from "../../Api/Data";
    import { excersiseTemplates } from '../../Store/Store';
    import type {ExcersiseTemplate} from '../../main';
import type { empty } from 'svelte/internal';
    
    export let selectedExcersiseTemplate: ExcersiseTemplate;

    export let openEdit = false;
    export let openDelete = false;

    let templateName = selectedExcersiseTemplate.templateName;
    let name = selectedExcersiseTemplate.name;
    let musclesSelected = selectedExcersiseTemplate.muscles;
    let duration = selectedExcersiseTemplate.duration;
    let weight = selectedExcersiseTemplate.weight;
    let reps = selectedExcersiseTemplate.weight;
    let sets = selectedExcersiseTemplate.sets;
    

    let muscleChoices = [
                            'deltoids', 'trapezius', 'TeresMajor', 'pectoralisMajor', 'rhomboids', 
                            'tricepsBrachii', 'bicepsBrachii', 'seratusAnterior', 'rectusAbdominis', 
                            'obliquusExternusAbdominis', 'vastusMedialis', 'vastusIntermedius', 
                            'vastusLateralis', 'rectusFemoris', 'gluteusMaximus'
                        ];

    const saveExcersiseTemplate = () => {
        if(name == '' || templateName == ''){
            console.log("save failed. required fields: name, templateName")
        }else{            
            updateExcersiseTemplate(
                {
                    id: selectedExcersiseTemplate.id,
                    templateName,
                    name,
                    muscles: musclesSelected,
                    duration,
                    weight,
                    reps,
                    sets,                    
                }, 
                (data) => {
                if(data == "success"){
                    excersiseTemplates.update(e => {                        
                        let modified = e.filter(et => et.id == selectedExcersiseTemplate.id)[0]
                        modified.templateName = templateName;
                        modified.name = name;
                                                
                        e = [...e.filter(et => et.id != selectedExcersiseTemplate.id), modified]
                        
                        return e;
                    })
                }
            })
        }
    }

    const removeExcersiseTemplate = () => {
        deleteExcersiseTemplate(selectedExcersiseTemplate.id, (data) => {          
            if(data == "success"){            
                    excersiseTemplates.update(e => { 
                        e = [...e.filter(et => et.id != selectedExcersiseTemplate.id)]
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
    <Title id="title">Modify Excersise Template {selectedExcersiseTemplate.templateName}</Title>
    <Content id="content"></Content>
    <Textfield type="text" label="templateName" bind:value={templateName} style="min-height: 60px;"/>
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

  <Actions>
    <Button on:click={() => saveExcersiseTemplate()}>
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
  <Title id="title">Delete Consumption Template {selectedExcersiseTemplate.templateName}</Title>
  <Content id="content">Are you sure you want to delete {selectedExcersiseTemplate.templateName}</Content>  
  <Actions>
    <Button on:click={() => removeExcersiseTemplate()}>
      <Label>Ok</Label>
    </Button>
    <Button on:click={() => {openDelete = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>

