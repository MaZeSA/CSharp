import { CreateParentActions, CreateParentActionTypes } from "../add/types";
import { ParentActions, ParentActionTypes, ParentState } from "../list/types";
import { DeleteParentActions, DeleteParentActionTypes } from "../remove/type";
import { SearchParentActions, SearchParentActionTypes } from "../search/type";

const initialState: ParentState = {
  parents: [],
  searchResult:[],
};

export const parentReducer = (
  state = initialState,
  action: ParentActions | CreateParentActions | DeleteParentActions | SearchParentActions
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
    case SearchParentActionTypes.SEARCH_PARENT_SUCCESS:
      return {
        ...state, searchResult: action.payload,
      };
  }
  return state;
};
