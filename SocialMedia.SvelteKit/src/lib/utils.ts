import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function getInitials(firstName: string, lastName: string): string;
export function getInitials(name: string): string;

export function getInitials(arg1: string, arg2?: string): string {
  if (arg2 !== undefined) {
    const name = `${arg1} ${arg2}`;
    return getInitials(name);
  }

  const name = arg1;
  if (!name) {
    return "";
  }

  const words = name
    .trim()
    .split(/\s+/)
    .filter((word) => word.length > 0);

  return words.map((word) => word[0].toUpperCase()).join("");
}
