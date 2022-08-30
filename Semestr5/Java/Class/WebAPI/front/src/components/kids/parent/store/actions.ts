import axios, { AxiosError } from "axios";
import { Dispatch } from "react";
import http from "../../../../http_common";
import { CreateParentActions, CreateParentActionTypes, ICreateParentErrors, IParentAdd } from "../add/types";
import { IParentItem, ParentActions, ParentActionTypes } from "../list/types";
import { DeleteParentActions, DeleteParentActionTypes } from "../remove/type";
import { IStatus } from "./types";

export const getParents = () => async (dispatch: Dispatch<ParentActions>) => {
  try {
    const response = await http.get<IParentItem[]>("/");
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

export const CreateParent = (data: IParentAdd): any => {
  return async (dispatch: Dispatch<CreateParentActions>) => {
    try {
      const response = await http.post<IStatus>("/create", data);
      const result = response;

      dispatch({
        type: CreateParentActionTypes.CREATE_PARENT_SUCCESS
      });
      console.log('CREATE_PARENT_SUCCESS' , result)
      return Promise.resolve<IStatus>(result);
    } catch (err: any) {
        console.log('CREATE_PARENT_ERROR' , err)
        return Promise.reject(err);
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
        console.log('REMOVE_PARENT_SUCCESS')
        return Promise.resolve<number>(response.status);
      }
    } catch (error: any) {
        // const serverError = error as AxiosError<IStatus>;
        console.log('REMOVE_PARENT_ERROR');
        return Promise.resolve(error);
      }
  };
};