export enum DeleteParentActionTypes {
    DELETE_PARENT_SUCCESS = "DELETE_PARENT_SUCCESS",
  }
  
  export interface DeleteSuccessParentAction {
    type: DeleteParentActionTypes.DELETE_PARENT_SUCCESS;
    payload: number;
  }
  export type DeleteParentActions = DeleteSuccessParentAction
  