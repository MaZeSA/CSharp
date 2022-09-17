import internal from "stream";

export interface IParentUpdate {
  id: number;
  firstName: string;
  lastName: string;
  phone: string;
  imageBase64: string;
  adress: string;
}

export enum UpdateParentActionTypes {
  UPDATE_PARENT = "UPDATE_PARENT",
  UPDATE_PARENT_SUCCESS = "UPDATE_PARENT_SUCCESS",
  UPDATE_PARENT_ERROR = "UPDATE_PARENT_ERROR",
};

export interface UpdateErrors {
  errors: {
    id?: string[];
    invalid?: string[];
  };
  status: number;
};

export interface UpdateParentAction {
  type: UpdateParentActionTypes.UPDATE_PARENT;
};

export interface UpdateSuccessParentAction {
  type: UpdateParentActionTypes.UPDATE_PARENT_SUCCESS;
  payload: IParentUpdate;
};

export interface UpdateErrorParentAction {
  type: UpdateParentActionTypes.UPDATE_PARENT_ERROR;
  payload: string;
};

export type UpdateParentActions =
  | UpdateParentAction
  | UpdateSuccessParentAction
  | UpdateErrorParentAction