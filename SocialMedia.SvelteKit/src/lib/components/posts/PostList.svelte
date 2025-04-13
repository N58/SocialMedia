<script lang="ts">
  import PostComp from "$lib/components/posts/Post.svelte";

  let { posts = [], isLoading = false }: { posts: Post[]; isLoading: boolean } =
    $props();
</script>

{#if isLoading}
  {#each { length: 10 } as _}
    <PostComp loading={true} />
  {/each}
{:else}
  {#each posts as post (post.id)}
    <PostComp {post} />
  {/each}
{/if}
