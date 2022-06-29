<script lang="ts">
    import Textfield from '@smui/textfield';
    import HelperText from '@smui/textfield/helper-text';
    import Button from "@smui/button"
    import { Router, Link, Route, navigate  } from "svelte-navigator";
    import Card from "@smui/card"
    import {authToken} from "../Store/Store";
    import {login} from "../Api/Data";

    let email = '';
    let password = '';

    let getAuthToken = () => {
        login(email, password, (key) => {
            authToken.set(key)
            navigate("/")
        });
    }

</script>


<div class="card-container">

    <Card >        
        <div class="input">
        <Textfield type="email" bind:value={email} label="email" />
        </div>
        
        <div class="input">
        <Textfield type="password" bind:value={password} label="password" >
            <HelperText slot="helper">at least 8 characters long</HelperText>
        </Textfield>
        </div>
        <Button on:click={() => getAuthToken()}>Login</Button>
        
    </Card>
    <Link to="../register">register</Link>

</div>

<style>
.input {
    margin: auto;
    width: 50%;    
    text-align: center;
}

.card-container {
    margin: auto;
    width: 50%;    
}

</style>