<script lang="ts">
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import AvatarUI from "$lib/components/user/AvatarUI.svelte";

  let {
    children,
    session = null,
    givenName = "",
    familyName = "",
    image = "",
  } = $props();
</script>

<DropdownMenu.Root>
  <DropdownMenu.Trigger>
    <AvatarUI {session} {givenName} {familyName} {image} />
  </DropdownMenu.Trigger>
  {@render children()}
</DropdownMenu.Root>
