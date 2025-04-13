<script lang="ts">
  import { Toaster } from "$lib/components/ui/sonner/index";

  import type { PageData } from "./$types";
  import AddPostForm from "$lib/components/posts/AddPostForm.svelte";
  import PostList from "$lib/components/posts/PostList.svelte";
  import { navigating } from "$app/state";
  import PostPagination from "$lib/components/posts/PostPagination.svelte";
  import { currentUser } from "$lib/stores/currentUser.svelte.ts";

  let { data }: { data: PageData } = $props();
</script>

<Toaster />

<section class="flex flex-col w-1/3 gap-2 my-5">
  {#if currentUser.isAuthenticated}
    <AddPostForm {data} />
  {/if}

  <PostList posts={data.pagedPosts.data} isLoading={navigating.to !== null} />

  {#if data.pagedPosts.count <= 0}
    <div class="flex items-center justify-center my-10 gap-x-2">
      <span class="text-lg uppercase font-medium tracking-wide">
        No existing posts!
      </span>
    </div>
  {/if}

  {#if data.pagedPosts.count > 0}
    <PostPagination
      totalCount={data.pagedPosts.totalCount}
      page={data.page}
      size={data.pagedPosts.size}
    />
  {/if}
</section>
