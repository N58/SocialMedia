<script lang="ts">
  import { LogOut, Settings, User as UserIcon } from "lucide-svelte";
  import * as DropdownMenu from "$lib/components/ui/dropdown-menu";
  import type { User } from "$lib/models/User.ts";

  let { user }: { user: User | null } = $props();
</script>

<DropdownMenu.Content class="w-48">
  <DropdownMenu.Group>
    <DropdownMenu.GroupHeading>
      {user?.fullName()}
    </DropdownMenu.GroupHeading>
    <DropdownMenu.Separator />
    <DropdownMenu.Group>
      <DropdownMenu.Item>
        <UserIcon />
        Profile
      </DropdownMenu.Item>
      <DropdownMenu.Item>
        <Settings />
        Settings
      </DropdownMenu.Item>
      <form method="POST" action="/auth/logout">
        <button type="submit" class="w-full">
          <DropdownMenu.Item>
            <LogOut /> Sign out
          </DropdownMenu.Item>
        </button>
      </form>
    </DropdownMenu.Group>
  </DropdownMenu.Group>
</DropdownMenu.Content>
