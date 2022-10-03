
import React from "react";

export interface IInput {
    inputName?: string,
    inputType?: string,
    title?: string,
    autocomolite?: string,
    errors?: string,
    touched?: boolean,
    text?: string
    handleChange:(e: React.ChangeEvent<any>)=>void,
}