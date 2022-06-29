<script lang="ts">
	import Header from "./Header/Header.svelte";
	import Nav from "./Navigation/Nav.svelte";
	import Login from "./Login/Login.svelte"
	import Register from "./Login/Register.svelte";
	import { Router, Link, Route } from "svelte-navigator";
	
	
	
	import {authToken}  from "./Store/Store";

	console.log($authToken)
</script>

<main>
	<Header />
	<Router primary={false}>

		<div>
			<Route path="/">
				
				{#if $authToken}
				
				<Nav appUserId={1} ></Nav>
				{:else}
			
				<Link to="login">Login</Link>
				{/if}
			</Route>
			<Route path="login" component={Login} />
			<Route path="register" component={Register}>
				
			</Route>
			
		</div>
	</Router>
</main>
