<script lang="ts">
  import { Skeleton } from "$lib/components/ui/skeleton/index";
  import { onMount } from "svelte";

  let {
    minWidth = null,
    maxWidth = null,
    ...restProps
  } = $props<{
    minWidth?: number | null;
    maxWidth?: number | null;
    [key: string]: any;
  }>();

  let randomWidth = $state(0);

  function getRandomNumber(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  onMount(() => {
    randomWidth = getRandomNumber(parseInt(minWidth), parseInt(maxWidth));
  });
</script>

<Skeleton style="width: {randomWidth}px" {...restProps} />
