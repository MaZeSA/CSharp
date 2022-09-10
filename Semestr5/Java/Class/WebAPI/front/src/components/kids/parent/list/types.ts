export interface IParentItem {
    id: number,
    firstName: string,
    lastName: string,
    phone: string
    image: string;
    adress: string
}

export interface ParentState{
    loading: boolean,
    list: Array<IParentItem>,
     searchResult:Array<IParentItem>;
}

export enum ParentActionTypes {
    FETCH_PARENT = "FETCH_PARENTS",
    FETCH_PARENT_SUCCESS = "FETCH_PARENT_SUCCESS"
}

export interface FetchSuccessParentAction {
    type: ParentActionTypes.FETCH_PARENT_SUCCESS;
    payload: Array<IParentItem>;
}

export interface FetchParentAction{
    type: ParentActionTypes.FETCH_PARENT;
}

export type ParentActions = FetchSuccessParentAction | FetchParentAction;