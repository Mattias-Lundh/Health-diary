<script>
import TopAppBar, { Row, Section, Title } from '@smui/top-app-bar';
import {selectedCampaign} from "../Store/Store";

</script>

<div class="header">
<TopAppBar  variant="static">
    <Row>
      <Section>
        <Title>Health Diary {#if $selectedCampaign} ({$selectedCampaign.name}) {/if}</Title>
      </Section>
    </Row>
</TopAppBar>
</div>

<style>
.header{
  margin-bottom: 25px;
 }
</style>