import { CreateParentActions, CreateParentActionTypes } from "../add/types";
import { ParentActions, ParentActionTypes, ParentState } from "../list/types";
import { DeleteParentActions, DeleteParentActionTypes } from "../remove/type";

const initialState: ParentState = {
  parents: [],
};

export const parentReducer = (
  state = initialState,
  action: ParentActions | CreateParentActions | DeleteParentActions
) => {
  switch (action.type) {
    case ParentActionTypes.FETCH_PARENT_SUCCESS:
      return { ...state, parents: action.payload };
    case CreateParentActionTypes.CREATE_PARENT_SUCCESS:
      return { ...state };
    case DeleteParentActionTypes.DELETE_PARENT_SUCCESS:
      return {
        ...state, parents: state.parents.filter((item) => item.id !== action.payload),
      };
  }
  return state;
};
