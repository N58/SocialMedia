<script lang="ts">
    import { Toaster } from "$lib/components/ui/sonner/index";
    import Post from "$lib/components/Post.svelte";
    import type { PageData } from "./$types";
    import AddPostForm from "$lib/components/AddPostForm.svelte";
    let { data }: { data: PageData } = $props();
</script>

<Toaster />

<main class="flex justify-center h-screen">
    <section class="flex flex-col w-1/3 gap-2 mt-5">
        <AddPostForm {data} />
        <Post />
        <Post />
        <Post />
    </section>
</main>

