export type User = {
  userName: string;
  password: string;
};

//type register user that extends user
export type RegisterUser = User & {
  email: string;
};

export type AuthResponse = {
  userName: string;
  email: string;
  token: string;
}
