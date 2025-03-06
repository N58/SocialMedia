<script lang="ts">
  import { superForm } from "sveltekit-superforms";
  import { zodClient } from "sveltekit-superforms/adapters";
  import { toast } from "svelte-sonner";
  import { page } from "$app/state";
  import { formSchema } from "$lib/schemas/postSchema";
  import * as Form from "$lib/components/ui/form/index";
  import { Textarea } from "$lib/components/ui/textarea/index";
  import * as Card from "$lib/components/ui/card/index";
  import Button from "$lib/components/c-button.svelte";
  import { buttonVariants } from "$lib/components/ui/button";
  import * as Dialog from "$lib/components/ui/dialog/index";
  import { SquarePlus } from "lucide-svelte";
  import { invalidateAll } from "$app/navigation";

  let { data: data = page.data.textarea } = $props();

  let loading = $state(false);
  let open = $state(false);

  const form = superForm(data.form, {
    validators: zodClient(formSchema),
    onUpdated: ({ form: f }) => {
      if (f.valid) {
        toast.success(`You've added a new post successfully.`);
      } else {
        toast.error("Please fix the errors in the form.");
      }
    },
    onError: (event) => {
      toast.error(
        `API Error ${event.result.status}: ${event.result.error.message}`,
      );
    },
    onSubmit: () => {
      loading = true;
    },
    onResult: async ({ result }) => {
      loading = false;
      if (result.type === "success") {
        open = false;
        await invalidateAll();
      }
    },
  });

  const { form: formData, enhance } = form;
</script>

<Dialog.Root bind:open>
  <Dialog.Trigger class={buttonVariants({ variant: "default" })}
    ><SquarePlus />New Post</Dialog.Trigger
  >
  <Dialog.Content>
    <Dialog.Header>
      <Dialog.Title>New Post</Dialog.Title>
      <Dialog.Description>
        <form method="POST" action="?/addPost" use:enhance>
          <Form.Field {form} name="content">
            <Form.Control>
              {#snippet children({ props })}
                <Form.Label>Content</Form.Label>
                <Textarea
                  {...props}
                  placeholder="Write text..."
                  class="resize-none"
                  bind:value={$formData.content}
                />
              {/snippet}
            </Form.Control>
            <Form.FieldErrors />
          </Form.Field>
          <Button {loading} class="mt-3">Send</Button>
        </form>
      </Dialog.Description>
    </Dialog.Header>
  </Dialog.Content>
</Dialog.Root>
