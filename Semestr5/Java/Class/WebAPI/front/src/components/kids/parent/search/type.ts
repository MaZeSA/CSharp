import { IParentItem } from "../list/types";

export interface ISearchParent{
    name?: String,
    id?: number
}

export enum SearchParentActionTypes {
    SEARCH_PARENT_SUCCESS = "SEARCH_PARENT_SUCCESS",
  }
  
  export interface SearchSuccessParentAction {
    type: SearchParentActionTypes.SEARCH_PARENT_SUCCESS;
    payload: IParentItem[];
  }
  export type SearchParentActions = SearchSuccessParentAction