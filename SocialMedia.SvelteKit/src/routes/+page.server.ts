import type { Actions, PageServerLoad } from './$types';
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { formSchema} from "$lib/schemas/postSchema.ts";
import { fail, error } from "@sveltejs/kit";

export const load: PageServerLoad = async () => {
    return {
        form: await superValidate(zod(formSchema)),
    };
};
export const actions = {
    addPost: async (event) => {
        const form = await superValidate(event, zod(formSchema));
        if (!form.valid) {
            return fail(400, {
                form,
            });
        }
        
        const content = form.data.content;
        const response = await event.fetch('/api/post', {
            method: 'POST',
            headers: { 'content-type': 'application/json' },
            body: JSON.stringify({
                content: content,
            })
        });
        
        if (!response.ok) {
            return error(response.status, response.statusText);
        }
        
        return {
            form,
        };
    }
} satisfies Actions;