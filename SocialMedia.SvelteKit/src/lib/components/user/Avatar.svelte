<script lang="ts">
  import * as Avatar from "$lib/components/ui/avatar";
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import { getInitials } from "$lib/utils";
  import { Button } from "$lib/components/ui/button";

  let { session, menuContent, href = "" } = $props();

  let picture = $state("");
  let fullName = $state("");
  if (session?.user) {
    picture = session.user.image;
    fullName = `${session.user.given_name} ${session.user.family_name}`;
  }

  const initials = getInitials(fullName);
</script>

{#snippet avatar()}
  <Avatar.Root class="select-none">
    <Avatar.Image src={picture} alt={fullName} />
    <Avatar.Fallback>{initials}</Avatar.Fallback>
  </Avatar.Root>
{/snippet}

{#if menuContent}
  <DropdownMenu.Root>
    <DropdownMenu.Trigger>
      {@render avatar()}
    </DropdownMenu.Trigger>
    {@render menuContent()}
  </DropdownMenu.Root>
{:else if href}
  <Button {href}>
    {@render avatar()}
  </Button>
{:else}
  {@render avatar()}
{/if}
