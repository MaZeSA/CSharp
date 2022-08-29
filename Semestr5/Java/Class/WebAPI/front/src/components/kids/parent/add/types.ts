export interface IParentAdd {
  firstName: string;
  lastName: string;
  phone: string;
  imageBase64: string;
  adress: string;
}
export type ICreateParentError = {
  title: Array<string>;
  utlSlug: Array<string>;
  priority: Array<string>;
};
export type ICreateParentErrors = {
  errors: ICreateParentError;
  status: number;
};
export enum CreateParentActionTypes {
  CREATE_PARENT_SUCCESS = "CREATE_PARENT_SUCCESS",
}

export interface CreateSuccessParentAction {
  type: CreateParentActionTypes.CREATE_PARENT_SUCCESS;
}

export type CreateParentActions = CreateSuccessParentAction;
