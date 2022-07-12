<script>
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {updateCampaign, deleteCampaign} from "../../Api/Data";
    import { campaigns } from '../../Store/Store';
    
    export let campaignName = '';
    export let campaignId;
    let openEdit = false;
    let openDelete = false;
    let name = '';

    const saveCampaign = () => {
        if(name == ''){
            console.log("save failed. campaign requires a name")
        }else{
            updateCampaign({name, Id: campaignId}, (data) => {
                if(data == "success"){
                    campaigns.update(c => {                        
                        let modified = c.filter(ca => ca.id == campaignId)[0]
                        modified.name = name;
                        c = [...c.filter(ca => ca.id != campaignId), modified]
                        
                        return c;
                    })
                }
            })
        }
    }

    const removeCampaign = () => {
        deleteCampaign(campaignId, (data) => {
            if(data == "success"){
                
                    campaigns.update(c => { 
                        c = [...c.filter(ca => ca.id != campaignId)]
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
>
  <Title id="title">Modify {campaignName}</Title>
  <Content id="content">Name the campaign</Content>
  <Textfield type="input" label="name" bind:value={name}/>
  <Actions>
    <Button on:click={() => saveCampaign()}>
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
  <Title id="title">Delete campaign</Title>
  <Content id="content">Are you sure you want to delete {campaignName}</Content>  
  <Actions>
    <Button on:click={() => removeCampaign()}>
      <Label>Ok</Label>
    </Button>
    <Button on:click={() => {openDelete = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>



<Button on:click={() => openEdit = true}>Modify</Button>
<Button on:click={() => openDelete = true}>Delete</Button>