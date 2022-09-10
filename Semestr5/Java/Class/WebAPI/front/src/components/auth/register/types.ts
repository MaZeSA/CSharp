export interface IRegister {
    firstName?: string,
    lastName?: string,
    email?: string,
    photo?: string,
    phone?: string,
    password?: string,
    confirmPassword?: string,
}

export enum RegisterActionTypes {
    REGISTER_START = "REGISTER_START",
    REGISTER_SUCCESS = "REGISTER_SUCCESS",
  }
  
  export interface RegisterState {
      data: string,
      isRegisterd:boolean,
  }
  
  export interface RegisterStartAction {
    type: RegisterActionTypes.REGISTER_START;
  }
  
  export interface RegisterSuccessAction {
    type: RegisterActionTypes.REGISTER_SUCCESS;
    payload: string;
  }
  
  export type RegisterAction =
    | RegisterStartAction
    | RegisterSuccessAction
  ;

  export interface IRequest extends IRegister {
    RecaptchaToken: string;
  }
  
  export type RegisterError = {
    email: Array<string>, 
    password: Array<string>, 
    confirmPassword: Array<string>, 
  };
  
  export type RegisterErrors = {
    errors: RegisterError,
    status: number, 
  };