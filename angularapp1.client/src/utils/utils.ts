export const getJwtTokenPayload = (token: string) => JSON.parse(atob(token.split(".")[1]));
