<script lang="ts">
  import * as Pagination from "$lib/components/ui/pagination";
  import { goto } from "$app/navigation";

  let { totalCount = 0, page = 1, size = 10 } = $props();

  const handlePageChange = (currentPage: number, size: number) => {
    return function () {
      goto(`/?page=${currentPage}&size=${size}`);
    };
  };
</script>

<Pagination.Root count={totalCount} {page} perPage={size}>
  {#snippet children({ pages, currentPage })}
    <Pagination.Content>
      <Pagination.Item>
        <Pagination.PrevButton
          onclick={handlePageChange(currentPage - 1, size)}
        />
      </Pagination.Item>
      {#each pages as page (page.key)}
        {#if page.type === "ellipsis"}
          <Pagination.Item>
            <Pagination.Ellipsis />
          </Pagination.Item>
        {:else}
          <Pagination.Item>
            <Pagination.Link
              {page}
              isActive={currentPage === page.value}
              onclick={handlePageChange(page.value, size)}
            >
              {page.value}
            </Pagination.Link>
          </Pagination.Item>
        {/if}
      {/each}
      <Pagination.Item>
        <Pagination.NextButton
          onclick={handlePageChange(currentPage + 1, size)}
        />
      </Pagination.Item>
    </Pagination.Content>
  {/snippet}
</Pagination.Root>
