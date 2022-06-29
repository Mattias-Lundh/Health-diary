<script lang="ts">
    import Select, { Option } from '@smui/select';    
    import {selectedCampaign} from "../../Store/Store"; 

    let campaigns = ['Apple', 'Orange', 'Banana', 'Mango'];   

  </script>
<form>
    <div class="columns margins">
            <div>
        <Select bind:value={$selectedCampaign} onchange={() => console.log("select")} label="Campaign" >
        <Option value="" />
        {#each campaigns as campaign}
            <Option value={campaign}>{campaign}</Option>
        {/each}
        </Select>
    </div>
</form>