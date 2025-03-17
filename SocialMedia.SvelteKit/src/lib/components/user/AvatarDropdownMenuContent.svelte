<script>
  import { LogOut, Settings, User } from "lucide-svelte";
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import { signOut } from "@auth/sveltekit/client";

  let { session } = $props();
</script>

<DropdownMenu.Content class="w-48">
  <DropdownMenu.Group>
    <DropdownMenu.GroupHeading>
      {session?.user.given_name}
      {session?.user.family_name}
    </DropdownMenu.GroupHeading>
    <DropdownMenu.Separator />
    <DropdownMenu.Group>
      <DropdownMenu.Item>
        <User />
        Profile
      </DropdownMenu.Item>
      <DropdownMenu.Item>
        <Settings />
        Settings
      </DropdownMenu.Item>
      <DropdownMenu.Item class="w-full" onclick={() => signOut()}>
        <LogOut /> Sign out
      </DropdownMenu.Item>
    </DropdownMenu.Group>
  </DropdownMenu.Group>
</DropdownMenu.Content>
