<script lang="ts">
    import Select, { Option } from '@smui/select';    
    import {selectedCampaign, campaigns} from "../../Store/Store";     

    let campaignLabels = $campaigns.map(c => c.name);
    let value = $selectedCampaign


    $: {
        campaignLabels = $campaigns.map(c => c.name);
    }

    $: {
        newSelection(value);
    }

    const newSelection = (value) => {
        let campaign = $campaigns.filter(c => c.name == value)[0]
        selectedCampaign.set( campaign )
    }
  </script>

<div class="columns margins" style="justify-content: flex-start;">
    <div>
        <Select bind:value label="selected">
        <Option value='' />
        {#each campaignLabels as campaignLabel}
            <Option value={campaignLabel}>{campaignLabel} </Option>
        {/each}
        </Select>
    </div>
</div>
