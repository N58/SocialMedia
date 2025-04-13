import type { Handle } from "@sveltejs/kit";
import { User } from "$lib/models/User";

export const handle: Handle = async ({ event, resolve }) => {
  const response = await event.fetch(
    `${import.meta.env.VITE_BACKEND_API}/user/me`,
  );

  let responseUser = (await response.json()) as User;
  if (response.status == 200 && responseUser.id != null) {
    event.locals.user = responseUser;
  } else {
    event.locals.user = null;
  }

  return resolve(event);
};
