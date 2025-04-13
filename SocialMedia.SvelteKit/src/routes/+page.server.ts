import type { Actions, PageServerLoad } from "./$types";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";
import { formSchema } from "$lib/schemas/postSchema.ts";
import { error, fail } from "@sveltejs/kit";

export const load: PageServerLoad = async ({ url, fetch }) => {
  let page = Number(url.searchParams.get("page"));
  if (!page || page <= 0) {
    page = 1;
  }

  let size = Number(url.searchParams.get("size"));
  if (!size || size <= 0) {
    size = 10;
  }

  const response = await fetch(
    `/api/post?page=${page}&size=${size}&sortColumn=Created&sortOrder=desc`,
    {
      method: "GET",
      headers: { "content-type": "application/json" },
    },
  );

  await new Promise((resolve) => setTimeout(resolve, 1000));

  if (!response.ok) {
    return error(response.status, response.statusText);
  }

  return {
    form: await superValidate(zod(formSchema)),
    page,
    size,
    pagedPosts: (await response.json()) as Paged<Post>,
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
    const response = await event.fetch("/api/post", {
      method: "POST",
      headers: {
        "content-type": "application/json",
      },
      body: JSON.stringify({
        content: content,
      }),
    });

    if (!response.ok) {
      return error(response.status, response.statusText);
    }

    return {
      form,
    };
  },
} satisfies Actions;
