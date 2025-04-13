<script lang="ts">
  import * as Avatar from "$lib/components/ui/avatar";
  import type { User } from "$lib/models/User.ts";

  let { user, ...restProps }: { user: User | null; [key: string]: any } =
    $props();
</script>

<Avatar.Root class="select-none" {...restProps}>
  <Avatar.Image src={user?.image} alt={user?.fullName()} />
  <Avatar.Fallback>{user?.initials()}</Avatar.Fallback>
</Avatar.Root>
