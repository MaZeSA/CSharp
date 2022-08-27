
import React from "react";

export interface IInput {
    inputName?: string,
    title?: string,
    errors?: string,
    touched?: boolean,
    text?: string
    handleChange:(e: React.ChangeEvent<any>)=>void,
}