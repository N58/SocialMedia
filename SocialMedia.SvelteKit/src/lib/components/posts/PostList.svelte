<script lang="ts">
  import Post from "$lib/components/posts/Post.svelte";

  let { posts = [], isLoading = false } = $props();
</script>

{#if isLoading}
  {#each { length: 10 } as _}
    <Post isSkeleton={true} />
  {/each}
{:else}
  {#each posts as post}
    <Post content={post.content} />
  {/each}
{/if}
