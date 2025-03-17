<script lang="ts">
  import "../app.css";
  import { Button } from "$lib/components/ui/button";
  import Avatar from "$lib/components/user/Avatar.svelte";
  import AvatarDropdownMenuContent from "$lib/components/user/AvatarDropdownMenuContent.svelte";
  import LogosGoogleIcon from "~icons/logos/google-icon";
  import { buttonVariants } from "$lib/components/ui/button";
  import { SignIn } from "@auth/sveltekit/components";

  let { children, data } = $props();
</script>

<header
  class="sticky top-0 z-50 w-full border-b border-border/10 backdrop-blur"
>
  <div class="container flex h-14 items-center">
    <div>
      <Button variant="ghost">Home</Button>
    </div>
    <div class="flex flex-1 items-center justify-end">
      {#if !data.session?.user?.email}
        <SignIn>
          <div
            slot="submitButton"
            class={buttonVariants({ variant: "default" })}
          >
            <LogosGoogleIcon />
            Sign In
          </div>
        </SignIn>
      {:else}
        <Avatar session={data.session} {menuContent} />
        {#snippet menuContent()}
          <AvatarDropdownMenuContent session={data.session} />
        {/snippet}
      {/if}
    </div>
  </div>
</header>

<main class="flex justify-center h-screen">
  {@render children()}
</main>
