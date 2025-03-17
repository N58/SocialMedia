<script>
  import * as Card from "$lib/components/ui/card/index";
  import RandomSkeleton from "$lib/components/ui/skeleton/c-random-skeleton.svelte";

  let { content = null, isSkeleton = false } = $props();
</script>

<Card.Root>
  <!--    <Card.Header>-->
  <!--        <Card.Title>Scara 2, Etajul 7</Card.Title>-->
  <!--    </Card.Header>-->
  <Card.Content>
    {#if isSkeleton}
      <RandomSkeleton class="h-6" minWidth={200} maxWidth={500} />
    {:else}
      {content}
    {/if}
  </Card.Content>
</Card.Root>
