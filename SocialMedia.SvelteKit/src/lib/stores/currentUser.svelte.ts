import type { User } from "$lib/models/User.ts";

export function createUser(user?: User) {
  let userState = $state(user ?? null);

  return {
    get user(): User | null {
      return userState;
    },
    set user(user: User | null) {
      userState = user ?? null;
    },
    get isAuthenticated(): boolean {
      return userState !== null;
    },
  };
}

export const currentUser = createUser();
