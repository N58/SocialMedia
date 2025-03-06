import { z } from "zod";

export const formSchema = z.object({
  content: z
    .string()
    .nonempty("Content is required")
    .min(3, "Content must be at least 3 characters.")
    .max(1000, "Content must be at most 1000 characters."),
});

export type FormSchema = typeof formSchema;
