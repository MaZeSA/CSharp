import classNames from "classnames";
import { on } from "stream";
import { IInput } from "./types";

const InputComponent: React.FC<IInput> = ({
  inputName,
  inputType = "text",
  title,
  autocomolite = "off",
  errors,
  touched,
  text,
  handleChange,
}) => {
  return (
    <div className="mb-3">
      <label htmlFor={inputName} className="form-label">
        {title}
      </label>
      <input
        type={inputType}
        className={classNames(
          "form-control",
          { "is-invalid": touched && errors },
          { "is-valid": touched && !errors }
        )}
        autoComplete={autocomolite}
        name={inputName}
        id={inputName}
         value={text}
        onChange={handleChange}
      />
      {touched && errors && <div className="invalid-feedback">{errors}</div>}
    </div>
  );
};

export default InputComponent;
