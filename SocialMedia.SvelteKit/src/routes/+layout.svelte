<script lang="ts">
  import "../app.css";
  import Header from "$lib/components/layout/Header.svelte";

  let { children, data } = $props();
</script>

<Header session={data.session} />

<main class="flex justify-center h-screen">
  {@render children()}
</main>
