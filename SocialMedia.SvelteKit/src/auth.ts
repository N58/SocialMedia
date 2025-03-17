import { SvelteKitAuth } from "@auth/sveltekit";
import Google from "@auth/core/providers/google";

export const { handle, signIn, signOut } = SvelteKitAuth(async (event) => {
  return {
    providers: [
      Google({
        profile(profile) {
          return {
            uid: profile.sub,
            email: profile.email,
            image: profile.picture,
            given_name: profile.given_name,
            family_name: profile.family_name,
          };
        },
      }),
    ],
    callbacks: {
      jwt({ token, user }) {
        if (user) {
          token.uid = user.uid;
          token.given_name = user.given_name;
          token.family_name = user.family_name;
        }
        return token;
      },
      session({ session, token }) {
        session.user.uid = token.uid;
        session.user.given_name = token.given_name;
        session.user.family_name = token.family_name;
        return session;
      },
    },
    events: {
      signIn: async (message) => {
        if (message.user && message.profile) {
          await event.fetch("/api/user/SyncUser", {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify({
              Uid: message.user.uid,
              GivenName: message.user.given_name,
              FamilyName: message.user.family_name,
              Email: message.user.email,
              Image: message.user.image,
            }),
          });
        }
      },
    },
  };
});
