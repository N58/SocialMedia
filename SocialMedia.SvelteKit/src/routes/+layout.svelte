<script lang="ts">
  import "../app.css";
  import Header from "$lib/components/layout/Header.svelte";
  import { currentUser } from "$lib/stores/currentUser.svelte.ts";
  import { User } from "$lib/models/User.ts";

  let { children, data } = $props();
  if (data.user) {
    currentUser.user = new User(
      data.user.id,
      data.user.givenName,
      data.user.familyName,
      data.user.email,
      data.user.image,
    );
  } else {
    currentUser.user = null;
  }
</script>

<Header />

<main class="flex justify-center h-screen">
  {@render children()}
</main>
