import { User } from "./user";

export class LoginResponseDto {
  user: User;
  token: string;
}
