<script lang="ts">
  import * as Card from "$lib/components/ui/card/index";
  import RandomSkeleton from "$lib/components/ui/skeleton/c-random-skeleton.svelte";
  import AvatarUI from "$lib/components/user/AvatarUI.svelte";
  import { User } from "$lib/models/User.ts";

  let { post, loading = false }: { post?: Post; loading?: boolean } = $props();

  const author = new User(
    "",
    post?.authorGivenName ?? "",
    post?.authorFamilyName ?? "",
    "",
    post?.authorImage ?? "",
  );
</script>

<Card.Root>
  <Card.Header>
    <Card.Title class="text-sm font-bold">
      {#if loading}
        <RandomSkeleton class="h-4" minWidth={50} maxWidth={100} />
      {:else}
        <div class="flex">
          <AvatarUI class="size-6" user={author} />
          <div class="flex items-center ml-2">
            <span>
              {post?.authorGivenName}
              {post?.authorFamilyName}
            </span>
          </div>
        </div>
      {/if}
    </Card.Title>
  </Card.Header>
  <Card.Content class="text-sm whitespace-pre-line">
    {#if loading}
      <RandomSkeleton class="h-4" minWidth={200} maxWidth={500} />
    {:else}
      {post?.content}
    {/if}
  </Card.Content>
</Card.Root>
