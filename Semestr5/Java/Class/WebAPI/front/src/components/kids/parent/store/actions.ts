import { Dispatch } from "react";
import http from "../../../../http_common";
import {
  CreateParentActions,
  CreateParentActionTypes,
  IParentAdd,
} from "../add/types";
import {
  IParentUpdate,
  UpdateParentActions,
  UpdateParentActionTypes,
} from "../edit/types";
import { IParentItem, ParentActions, ParentActionTypes } from "../list/types";
import { DeleteParentActions, DeleteParentActionTypes } from "../remove/type";
import {
  ISearchParent,
  SearchParentActions,
  SearchParentActionTypes,
} from "../search/type";

export const getParents = () => async (dispatch: Dispatch<ParentActions>) => {
  try {
    dispatch({
      type: ParentActionTypes.FETCH_PARENT,
    });
    const response = await http.get<Array<IParentItem>>("/list");
    dispatch({
      type: ParentActionTypes.FETCH_PARENT_SUCCESS,
      payload: response.data,
    });
    console.log("FETCH_PARENT_SUCCESS", response.data);
    return Promise.resolve();
  } catch (error) {
    return Promise.reject();
  }
};

export const CreateParent = (model: IParentAdd): any => {
  return async (dispatch: Dispatch<CreateParentActions>) => {
    try {
      dispatch({
        type: CreateParentActionTypes.CREATE_PARENT,
      });
      const response = await http.post<IParentItem>("/create", model);
      dispatch({
        type: CreateParentActionTypes.CREATE_PARENT_SUCCESS,
        payload: response.data,
      });
      console.log("CREATE_PARENT_SUCCESS", response.data);
      return Promise.resolve();
    } catch (err: any) {
      console.log("CREATE_PARENT_ERROR", err);
      return Promise.reject(err);
    }
  };
};

export const updateParent = (data: IParentUpdate) => {
  return async (dispatch: Dispatch<UpdateParentActions>) => {
    try {
      dispatch({
        type: UpdateParentActionTypes.UPDATE_PARENT,
      });
      await http.put<IParentItem>("/update", data);
      dispatch({
        type: UpdateParentActionTypes.UPDATE_PARENT_SUCCESS,
        payload: data,
      });
    } catch (error) {
      dispatch({
        type: UpdateParentActionTypes.UPDATE_PARENT_ERROR,
        payload: "error",
      });
      return Promise.reject(error);
    }
  };
};

export const deleteParent = (id: number): any => {
  return async (dispatch: Dispatch<DeleteParentActions>) => {
    try {
      const response = await http.delete<number>(`/remove/${id}`);

      if (response.status === 200) {
        dispatch({
          type: DeleteParentActionTypes.DELETE_PARENT_SUCCESS,
          payload: Number(id),
        });
        console.log("REMOVE_PARENT_SUCCESS");
        return Promise.resolve<number>(response.status);
      }
    } catch (error: any) {
      // const serverError = error as AxiosError<IStatus>;
      console.log("REMOVE_PARENT_ERROR");
      return Promise.resolve(error);
    }
  };
};

export const getSearchParentsResult = (searchRequest: ISearchParent) => {
  return async (dispatch: Dispatch<SearchParentActions>) => {
    try {
      const response = await http.get<IParentItem[]>("/search", {
        params: searchRequest,
      });
      const { data } = response;
      console.log("searchRequest", data);
      dispatch({
        type: SearchParentActionTypes.SEARCH_PARENT_SUCCESS,
        payload: response.data,
      });
      console.log("SEARCH_PARENT_SUCCESS");
    } catch (error) {
      console.log("SEARCH_PARENT_ERROR");
    }
  };
};
