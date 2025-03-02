<script lang="ts">
    import SuperDebug, {
        type Infer,
        type SuperValidated,
        superForm
    } from "sveltekit-superforms";
    import { zodClient } from "sveltekit-superforms/adapters";
    import { toast } from "svelte-sonner";
    import { page } from "$app/state";
    import { formSchema, type FormSchema } from "$lib/schemas/postSchema";
    import * as Form from "$lib/components/ui/form/index";
    import { Textarea } from "$lib/components/ui/textarea/index";
    import * as Card from "$lib/components/ui/card/index";
    import FormButton from "$lib/components/FormButton.svelte";

    let {
        data: data = page.data.textarea
    }: { data: { form: SuperValidated<Infer<FormSchema>>, status?: string, statusText?: string } } = $props();
    
    let loading = $state(false);
        
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
            toast.error(`API Error ${event.result.status}: ${event.result.error.message}`);
            
        },
        onSubmit: () => {
            loading = true;
        },
        onResult: () => {
            loading = false;
        }
    });

    const { form: formData, enhance } = form;
</script>

<form method="POST" action="?/addPost" use:enhance>
    <Card.Root>
        <Card.Header>
            <Card.Title>New Post</Card.Title>
        </Card.Header>
        <Card.Content>
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
        </Card.Content>
        <Card.Footer>
            <FormButton {loading}>
                Send
            </FormButton>
        </Card.Footer>
    </Card.Root>
</form>
