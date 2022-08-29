import { CreateParentActions, CreateParentActionTypes } from "../add/types";
import { ParentActions, ParentActionTypes, ParentState } from "../list/types";

const initialState: ParentState = {
  parents: [],
};

export const parentReducer = (state = initialState, action: ParentActions | CreateParentActions
) => {
  switch (action.type) {
    case ParentActionTypes.FETCH_PARENT_SUCCESS: 
      return {...state, parents: action.payload };
    case CreateParentActionTypes.CREATE_PARENT_SUCCESS:
      return {...state};
  }
  return state;
};
