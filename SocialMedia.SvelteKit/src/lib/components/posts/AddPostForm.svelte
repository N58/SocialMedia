<script lang="ts">
  import { superForm } from "sveltekit-superforms";
  import { zodClient } from "sveltekit-superforms/adapters";
  import { toast } from "svelte-sonner";
  import { page } from "$app/state";
  import { formSchema } from "$lib/schemas/postSchema";
  import * as Form from "$lib/components/ui/form/index";
  import { Textarea } from "$lib/components/ui/textarea/index";
  import * as Dialog from "$lib/components/ui/dialog/index";
  import { buttonVariants } from "$lib/components/ui/button";
  import { invalidateAll } from "$app/navigation";
  import LucideCirclePlus from "~icons/lucide/circle-plus";
  import FormButton from "$lib/components/FormButton.svelte";

  let { data = page.data.textarea, isAuthorized = false } = $props();

  let loading = $state(false);
  let open = $state(false);

  const form = superForm(data.form, {
    validators: zodClient(formSchema),
    onUpdated: ({ form: f }) => {
      if (!f.valid) {
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
      if (result.type === "success") {
        await invalidateAll();
        open = false;
        toast.success(`You've added a new post successfully.`);
      }
      loading = false;
    },
  });

  const { form: formData, enhance } = form;
</script>

<div class="grid grid-cols-5 gap-2">
  <Dialog.Root bind:open>
    <Dialog.Trigger class={buttonVariants({ variant: "default" })}>
      <LucideCirclePlus />
      <span>New Post</span>
    </Dialog.Trigger>
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
            <FormButton class="mt-3" {loading}>Send</FormButton>
          </form>
        </Dialog.Description>
      </Dialog.Header>
    </Dialog.Content>
  </Dialog.Root>
</div>
