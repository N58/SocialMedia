// See https://svelte.dev/docs/kit/types#app.d.ts
// for information about these interfaces
import "@auth/core";

declare global {
  namespace App {
    // interface Error {}
    // interface Locals {}
    // interface PageData {}
    // interface PageState {}
    // interface Platform {}
  }
}

declare module "@auth/core/jwt" {
  interface JWT {
    uid: string;
    given_name?: string;
    family_name?: string;
  }
}

declare module "@auth/core/types" {
  interface User {
    uid: string;
    given_name?: string;
    family_name?: string;
  }
}

declare module "@auth/sveltekit" {
  interface Session {
    user: {
      uid: string;
      given_name?: string;
      family_name?: string;
      email?: string;
      image?: string;
    };
  }
}

export {};
