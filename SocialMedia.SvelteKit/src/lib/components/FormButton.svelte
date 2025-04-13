<script lang="ts">
  import LucideLoaderCircle from "~icons/lucide/loader-circle";
  import * as Form from "$lib/components/ui/form/index";

  let { loading = false, children, ...restProps } = $props();
</script>

<Form.Button {...restProps} disabled={loading}>
  {#if !loading}
    {@render children?.()}
  {:else}
    <LucideLoaderCircle class="animate-spin" /> Loading
  {/if}
</Form.Button>
