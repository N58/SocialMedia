<script lang="ts">
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import AvatarUI from "$lib/components/user/AvatarUI.svelte";
  import type { Snippet } from "svelte";
  import type { User } from "$lib/models/User.ts";

  let { children, user }: { children: Snippet; user: User | null } = $props();
</script>

<DropdownMenu.Root>
  <DropdownMenu.Trigger>
    <AvatarUI {user} />
  </DropdownMenu.Trigger>
  {@render children()}
</DropdownMenu.Root>
