<script lang="ts">
  import LoaderCircle from "lucide-svelte/icons/loader-circle";
  import * as Form from "$lib/components/ui/form/index";

  let { loading = false, children, ...restProps } = $props();
</script>

<Form.Button {...restProps} disabled={loading}>
  {#if !loading}
    {@render children?.()}
  {:else}
    <LoaderCircle class="animate-spin" /> Loading
  {/if}
</Form.Button>
