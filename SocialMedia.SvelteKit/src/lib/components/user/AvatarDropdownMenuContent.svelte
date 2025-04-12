<script lang="ts">
  import LucideLogOut from "~icons/lucide/log-out";
  import LucideSettings from "~icons/lucide/settings";
  import LucideUser from "~icons/lucide/user";
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
        <LucideUser />
        Profile
      </DropdownMenu.Item>
      <DropdownMenu.Item>
        <LucideSettings />
        Settings
      </DropdownMenu.Item>
      <form method="POST" action="/auth/logout">
        <button type="submit" class="w-full">
          <DropdownMenu.Item>
            <LucideLogOut /> Sign out
          </DropdownMenu.Item>
        </button>
      </form>
    </DropdownMenu.Group>
  </DropdownMenu.Group>
</DropdownMenu.Content>
