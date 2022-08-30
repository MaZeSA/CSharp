export interface IParentItem {
    id: number,
    firstName: string,
    lastName: string,
    phone: string
    image: string;
    adress: string
}

export interface ParentState{
    parents: IParentItem[],
     searchResult:IParentItem[];
}

export enum ParentActionTypes {
    FETCH_PARENT_SUCCESS = "FETCH_PARENT_SUCCESS"
}

export interface FetchSuccessParentAction {
    type: ParentActionTypes.FETCH_PARENT_SUCCESS;
    payload: IParentItem[];
}

export type ParentActions = FetchSuccessParentAction;