import { Form, FormikHelpers, FormikProvider, useFormik } from "formik";
import React, { useState } from "react";
import { IRegister, IRequest, RegisterError } from "./types";
import { RegisterSchema } from "./validataion";
import { useDispatch } from "react-redux";
import { useGoogleReCaptcha } from "react-google-recaptcha-v3";
import { useActions } from "../../../hooks/useActions";
import { useNavigate } from "react-router-dom";
import InputComponent from "../../common/InputComponent";
// import { AuthActionTypes } from "../store/types";

const RegisterPage: React.FC = () => {
  const { executeRecaptcha } = useGoogleReCaptcha();
  const [bot, setBot] = useState<boolean>();
  const [loading, setLoading] = useState<boolean>(false);
  const { RegisterUser } = useActions();
  const navigator = useNavigate();

  const initialValues: IRegister = {
    email: "",
    password: "",
    confirmPassword: "",
    fullName: "",
  };
  const dispatch = useDispatch();
  const onHandleSubmit = async (
    values: IRegister,
    { setFieldError }: FormikHelpers<IRegister>
  ) => {
    if (!executeRecaptcha) {
      setBot(true);
      return;
    }
    if (values.password === values.confirmPassword) {
      setLoading(true);
      try {
       
        const recapchaToken = await executeRecaptcha();

        await RegisterUser({ ...values, recaptchaToken: recapchaToken });

        // const data: IRequest = {
        //   email: values.email,
        //   fullName: values.fullName,
        //   password: values.password,
        //   recaptchaToken: recapchaToken
        // };

        // await RegisterUser(data);
        await navigator("/");

        setLoading(false);
      } catch (err) {
        setLoading(false);
        const serverErrors = err as RegisterError;
        console.log(serverErrors);
        // Object.entries(serverErrors).map(([key, value]) =>
        //   setFieldError(key, value.toString())
        // );
      }
    } else {
      console.log("pass bed");
    }
  };
  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onHandleSubmit,
    validationSchema: RegisterSchema,
  });

  //Деструктуризація
  const { errors, touched, handleChange, handleSubmit, setFieldValue } = formik;

  return (
    <div className="row">
      <div className="offset-md-3 col-md-6">
        <h1 className="text-center">Створити новий аканут</h1>
        <FormikProvider value={formik}>
          <Form onSubmit={handleSubmit}>
            <div className="mb-3">
              <InputComponent
                title="Email"
                inputName="email"
                errors={errors.email}
                touched={touched.email}
                handleChange={handleChange}
              />

              <InputComponent
                title="Full Name"
                inputName="fullName"
                errors={errors.fullName}
                touched={touched.fullName}
                handleChange={handleChange}
              />
              <InputComponent
                title="Password"
                inputType="password"
                inputName="password"
                autocomolite="new-password"
                errors={errors.password}
                touched={touched.password}
                handleChange={handleChange}
              />
              <InputComponent
                title="Confirm Password"
                inputType="password"
                autocomolite="new-password"
                inputName="confirmPassword"
                errors={errors.confirmPassword}
                touched={touched.confirmPassword}
                handleChange={handleChange}
              />

              {touched.email && errors.email && (
                <div className="invalid-feedback">{errors.email}</div>
              )}
            </div>
            <button type="submit" className="btn btn-primary">
              Реєструватися
            </button>
          </Form>
        </FormikProvider>
      </div>
    </div>
  );
};

export default RegisterPage;
