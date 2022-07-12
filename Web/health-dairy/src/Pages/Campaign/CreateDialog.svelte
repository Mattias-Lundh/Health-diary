<script lang="ts">
    import Dialog, { Title, Content, Actions } from '@smui/dialog';
    import Button, { Label } from '@smui/button';
    import Textfield from "@smui/textfield";
    import {createCampaign} from "../../Api/Data";
    import {authToken} from "../../Store/Store";
    import {parseJwt} from "../../Util/util"
    import { campaigns } from '../../Store/Store';

    export let open = false;
    let name = '';    

    const appUserId = parseJwt($authToken).appUserId

    const save = () => {
        if(name == ''){
            console.log("failed to create: campaign requires a name")
        }else{

            createCampaign({name, appUser: appUserId}, (data) => {
                    campaigns.update(c =>  [...c, data])
            })
        }
        
    }
  </script>

<Dialog
  bind:open
  aria-labelledby="title"
  aria-describedby="content"
>
  <Title id="title">Create new campaign</Title>
  <Content id="content">Name the campaign</Content>
  <Textfield type="input" label="name" bind:value={name}/>
  <Actions>
    <Button on:click={() => save()}>
      <Label>Save</Label>
    </Button>
    <Button on:click={() => {open = false;}}>
      <Label>Cancel</Label>
    </Button>
  </Actions>
</Dialog>