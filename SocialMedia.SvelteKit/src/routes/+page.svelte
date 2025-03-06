<script lang="ts">
  import { Toaster } from "$lib/components/ui/sonner/index";
  import Post from "$lib/components/posts/c-post.svelte";
  import type { PageData } from "./$types";
  import AddPostForm from "$lib/components/posts/c-add-post-form.svelte";
  import * as Pagination from "$lib/components/ui/pagination/index";
  import { goto } from "$app/navigation";
  import { navigating } from "$app/state";

  let { data }: { data: PageData } = $props();

  const changePage = (currentPage: number) => {
    return function () {
      goto(`/?page=${currentPage}&size=${data.size}`);
    };
  };
</script>

<Toaster />

<main class="flex justify-center h-screen">
  <section class="flex flex-col w-1/3 gap-2 my-5">
    <div class="flex">
      <AddPostForm {data} />
    </div>

    {#if navigating.to}
      {#each { length: 10 } as rank}
        <Post isSkeleton={true} />
      {/each}
    {:else}
      {#each data.pagedPosts.data as post}
        <Post content={post.content} />
      {/each}
    {/if}

    <Pagination.Root
      count={data.pagedPosts.totalCount}
      page={data.page}
      perPage={data.pagedPosts.size}
    >
      {#snippet children({ pages, currentPage })}
        <Pagination.Content>
          <Pagination.Item>
            <Pagination.PrevButton onclick={changePage(currentPage - 1)} />
          </Pagination.Item>
          {#each pages as page (page.key)}
            {#if page.type === "ellipsis"}
              <Pagination.Item>
                <Pagination.Ellipsis />
              </Pagination.Item>
            {:else}
              <Pagination.Item isVisible={currentPage === page.value}>
                <Pagination.Link
                  {page}
                  isActive={currentPage === page.value}
                  onclick={changePage(page.value)}
                >
                  {page.value}
                </Pagination.Link>
              </Pagination.Item>
            {/if}
          {/each}
          <Pagination.Item>
            <Pagination.NextButton onclick={changePage(currentPage + 1)} />
          </Pagination.Item>
        </Pagination.Content>
      {/snippet}
    </Pagination.Root>
  </section>
</main>
