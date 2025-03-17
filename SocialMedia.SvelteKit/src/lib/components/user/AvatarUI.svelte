<script lang="ts">
  import * as Avatar from "$lib/components/ui/avatar";
  import { getInitials } from "$lib/utils";

  let {
    session = null,
    givenName = "",
    familyName = "",
    image = "",
  } = $props();

  let fullName = $state(`${givenName} ${familyName}`);
  if (session?.user) {
    fullName = `${session.user.given_name} ${session.user.family_name}`;
    image = session.user.image;
  }

  const initials = getInitials(fullName);
</script>

<Avatar.Root class="select-none">
  <Avatar.Image src={image} alt={fullName} />
  <Avatar.Fallback>{initials}</Avatar.Fallback>
</Avatar.Root>
