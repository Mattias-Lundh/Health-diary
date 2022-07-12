<script lang="ts">
	import Header from "./Header/Header.svelte";
	import Nav from "./Navigation/Nav.svelte";
	import Login from "./Login/Login.svelte"
	import Register from "./Login/Register.svelte";
	import { Router, Link, Route } from "svelte-navigator";
	import Init from "./Init/Init.svelte"	
	import {authToken}  from "./Store/Store";
	import Button from "@smui/button";
	import GotoLogin from "./Login/GotoLogin.svelte";
	
</script>

<main>
	<Header />
	<Router primary={false}>

		<div>
			<Route path="/">
				
				{#if $authToken}
				<Init />		
				<Nav />
				{:else}
				<GotoLogin />
				{/if}
			</Route>
			<Route path="login" component={Login} />
			<Route path="register" component={Register}>
				
			</Route>
			
		</div>
	</Router>
</main>
