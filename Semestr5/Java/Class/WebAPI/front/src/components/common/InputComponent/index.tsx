import React, { useEffect, useState } from "react";
import classNames from "classnames";
import { IInput } from "./types";

const InputComponent: React.FC<IInput> = ({
  inputName,
  title,
  errors,
  touched,
  text,
  handleChange,
}) => {
  var elem = document.createElement("input");

  useEffect(() => {
    if (text != undefined && inputName != undefined) {
      var myAnchor = document.getElementById(inputName);
      (myAnchor as HTMLInputElement).value = text;
      console.log(myAnchor);
    }
  }, []);

  return (
    <div className="mb-3">
      <label htmlFor={inputName} className="form-label">
        {title}
      </label>
      <input
        type={inputName}
        className={classNames(
          "form-control",
          { "is-invalid": touched && errors },
          { "is-valid": touched && !errors }
        )}
        name={inputName}
        id={inputName}
        // value={text}
        onChange={handleChange}
      />
      {touched && errors && <div className="invalid-feedback">{errors}</div>}
    </div>
  );
};

export default InputComponent;
