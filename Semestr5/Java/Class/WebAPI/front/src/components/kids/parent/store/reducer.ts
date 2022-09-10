import { CreateParentActions, CreateParentActionTypes } from "../add/types";
import { ParentActions, ParentActionTypes, ParentState } from "../list/types";
import { DeleteParentActions, DeleteParentActionTypes } from "../remove/type";
import { SearchParentActions, SearchParentActionTypes } from "../search/type";

const initialState: ParentState = {
  loading: false,
  list: [],
  searchResult: [],
};

export const parentReducer = (
  state = initialState,
  action:
    | ParentActions
    | CreateParentActions
    | DeleteParentActions
    | SearchParentActions
): ParentState => {
  switch (action.type) {
    case ParentActionTypes.FETCH_PARENT:
      return { ...state, loading: true };
    case ParentActionTypes.FETCH_PARENT_SUCCESS:
      return { ...state, list: action.payload, loading: false };
    case CreateParentActionTypes.CREATE_PARENT:
      return { ...state, loading: true };
    case CreateParentActionTypes.CREATE_PARENT_SUCCESS:
      return {
        ...state,
        list: [...state.list, action.payload],
        loading: false,
      };
    case DeleteParentActionTypes.DELETE_PARENT_SUCCESS:
      return {
        ...state,
        list: state.list.filter((item) => item.id !== action.payload),
      };
    case SearchParentActionTypes.SEARCH_PARENT_SUCCESS:
      return {
        ...state,
        searchResult: action.payload,
      };
  }
  return state;
};
