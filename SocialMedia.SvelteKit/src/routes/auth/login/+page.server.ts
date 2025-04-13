import { type Actions, redirect } from "@sveltejs/kit";

export const actions = {
  default: async () => {
    redirect(303, `${import.meta.env.VITE_BACKEND_API}/auth/login`); // TODO SET CORRECT RETURN URL
  },
} satisfies Actions;
