import { Form, FormikProvider, useFormik } from "formik";
import React from "react";
import { ILogin } from "./types";
import InputComponent from "../inputComponent";
import { RegisterSchema } from "../register/validataion";

const LoginPage: React.FC = () => {
  const initialValues: ILogin = {
    email: "",
    password: "",
  };
  const onHandleSubmit = async (values: ILogin) => {
    console.log("Send server form", values);
  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onHandleSubmit,
    validationSchema: RegisterSchema,
  });
  //Деструктуризація
  const { errors, touched, handleSubmit, handleChange } = formik;

  return (
    <div className="row">
      <div className="offset-md-3 col-md-6">
        <h1 className="text-center">Увійти в аканут</h1>
        <FormikProvider value={formik}>
          <Form onSubmit={handleSubmit}>
            <InputComponent
              inputName="email"
              title="Електронна пошта"
              touched={touched.email}
              errors={errors.email}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="password"
              title="Пароль"
              touched={touched.password}
              errors={errors.password}
              handleChange={handleChange}
            ></InputComponent>
            <div className="mb-3">
              <button type="submit" className="btn btn-primary">
                Вхід
              </button>
            </div>
          </Form>
        </FormikProvider>
      </div>
    </div>
  );
};

export default LoginPage;
