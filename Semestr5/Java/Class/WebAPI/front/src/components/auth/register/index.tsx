import { Form, FormikProvider, useFormik } from "formik";
import React from "react";
import { IRegister } from "./types";
import { RegisterSchema } from "./validataion";
import CropperDialog from "../../common/CropperDialog";
import InputComponent from "../inputComponent";
import { useDispatch } from "react-redux";
import { AuthActionTypes } from "../store/types";


const RegisterPage: React.FC = () => {
  const initialValues: IRegister = {
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    photo: "",
    confirmPassword: "",
    password: "",
  };

  const dispatch = useDispatch();
   
  const onHandleSubmit = async (values: IRegister) => {
    console.log("Send server form", values);
    dispatch({
      type: AuthActionTypes.LOGIN_AUTH,
      payload: {
        email: values.email,
        image: values.photo,
        roles: "Кабан"
      }
    })

  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onHandleSubmit,
    validationSchema: RegisterSchema,
  });

  //Деструктуризація
  const { errors, touched, handleSubmit, handleChange, setFieldValue } = formik;

  return (
    <div className="row">
      <div className="offset-md-3 col-md-6">
        <h1 className="text-center">Створити новий аканут</h1>
        <FormikProvider value={formik}>
          <Form onSubmit={handleSubmit}>
            <InputComponent
              inputName="firstName"
              title="Імя"
              touched={touched.firstName}
              errors={errors.firstName}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="lastName"
              title="Прізвище"
              touched={touched.lastName}
              errors={errors.lastName}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="email"
              title="Електронна пошта"
              touched={touched.email}
              errors={errors.email}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="phone"
              title="Номер телефону"
              touched={touched.phone}
              errors={errors.phone}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="password"
              title="Пароль"
              touched={touched.password}
              errors={errors.password}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="confirmPassword"
              title="Повторіть пароль"
              touched={touched.confirmPassword}
              errors={errors.confirmPassword}
              handleChange={handleChange}
            ></InputComponent>

            <CropperDialog
              onChange={setFieldValue}
              field="photo"
              error={errors.photo}
              touched={touched.photo}
            />
            <div className="mb-3">
              <button type="submit" className="btn btn-primary">
                Реєструватися
              </button>
            </div>
          </Form>
        </FormikProvider>
      </div>
    </div>
  );
};

export default RegisterPage;
