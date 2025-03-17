<script lang="ts">
  import { Button } from "$lib/components/ui/button";
  import AvatarUi from "$lib/components/user/AvatarUI.svelte";

  let {
    session = null,
    givenName = "",
    familyName = "",
    image = "",
    href,
    variant,
  } = $props();
</script>

<Button {href} {variant}>
  <AvatarUi {session} {givenName} {familyName} {image} />
</Button>
