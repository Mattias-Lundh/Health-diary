<script lang="ts">

import {campaigns, authToken} from "../Store/Store";
import {getCampaigns} from "../Api/Data"
import {parseJwt} from "../Util/util"

let appUserId = parseJwt($authToken).appUserId;

getCampaigns(appUserId, (data) => {
    campaigns.set(data)
    
})

</script>