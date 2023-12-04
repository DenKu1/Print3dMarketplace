import { UserModel } from './userModel';

export interface LoginResponseModel {
  user: UserModel;
  token: string;
}
