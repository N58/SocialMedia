<script lang="ts">
  import { Button } from "$lib/components/ui/button";
  import LogosGoogleIcon from "~icons/logos/google-icon";
  import AvatarDropdownMenuContent from "$lib/components/user/AvatarDropdownMenuContent.svelte";
  import AvatarUi from "$lib/components/user/AvatarUI.svelte";
  import AvatarDropdown from "$lib/components/user/AvatarDropdown.svelte";
  import FormButton from "$lib/components/FormButton.svelte";
  import { currentUser } from "$lib/stores/currentUser.svelte.ts";
</script>

<header
  class="border-border/40 bg-background/95 supports-[backdrop-filter]:bg-background/60 sticky top-0 z-50 w-full border-b backdrop-blur"
>
  <div class="container flex h-14 items-center">
    <div>
      <Button variant="ghost">Home</Button>
    </div>
    <div class="flex flex-1 items-center justify-end">
      {#if currentUser.isAuthenticated}
        <AvatarDropdown user={currentUser.user}>
          <AvatarDropdownMenuContent user={currentUser.user} />
        </AvatarDropdown>
      {:else}
        <form method="POST" action="/auth/login">
          <Button type="submit">
            <LogosGoogleIcon />
            Sign In
          </Button>
        </form>
      {/if}
    </div>
  </div>
</header>
