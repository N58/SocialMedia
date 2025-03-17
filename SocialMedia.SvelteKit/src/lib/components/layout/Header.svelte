<script lang="ts">
  import { Button } from "$lib/components/ui/button";
  import LogosGoogleIcon from "~icons/logos/google-icon";
  import { buttonVariants } from "$lib/components/ui/button";
  import { SignIn } from "@auth/sveltekit/components";
  import AvatarDropdownMenuContent from "$lib/components/user/AvatarDropdownMenuContent.svelte";
  import AvatarUi from "$lib/components/user/AvatarUI.svelte";
  import AvatarDropdown from "$lib/components/user/AvatarDropdown.svelte";

  let { session } = $props();
</script>

<header
  class="border-border/40 bg-background/95 supports-[backdrop-filter]:bg-background/60 sticky top-0 z-50 w-full border-b backdrop-blur"
>
  <div class="container flex h-14 items-center">
    <div>
      <Button variant="ghost">Home</Button>
    </div>
    <div class="flex flex-1 items-center justify-end">
      {#if !session?.user?.email}
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
        <AvatarDropdown {session}>
          <AvatarDropdownMenuContent {session} />
        </AvatarDropdown>
      {/if}
    </div>
  </div>
</header>
