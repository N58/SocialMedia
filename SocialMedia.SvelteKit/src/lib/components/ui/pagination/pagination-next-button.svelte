<script lang="ts">
  import { Pagination as PaginationPrimitive } from "bits-ui";
  import LucideChevronRight from "~icons/lucide/chevron-right";
  import { cn } from "$lib/utils.js";
  import { buttonVariants } from "$lib/components/ui/button/index.js";

  let {
    ref = $bindable(null),
    class: className,
    children,
    ...restProps
  }: PaginationPrimitive.NextButtonProps = $props();
</script>

{#snippet Fallback()}
  <span>Next</span>
  <LucideChevronRight />
{/snippet}

<PaginationPrimitive.NextButton
  bind:ref
  {...restProps}
  class={cn(
    buttonVariants({ variant: "ghost", className: "gap-1 pr-2.5" }),
    className,
  )}
  children={children || Fallback}
/>
