import axios, { AxiosError } from "axios";
import { Dispatch } from "react";
import http from "../../../../http_common";
import { CreateParentActions, CreateParentActionTypes, ICreateParentErrors, IParentAdd } from "../add/types";
import { IParentItem, ParentActions, ParentActionTypes } from "../list/types";
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
        console.log('CREATE_PARENT_SUCCESS_ERROR' , err)
        return Promise.reject(err);
      }
  };
};
