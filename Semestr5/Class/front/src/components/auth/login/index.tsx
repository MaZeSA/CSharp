import { Form, FormikProvider, useFormik } from "formik";
import React, { useEffect } from "react";
import { ILogin } from "./types";
import InputComponent from "../inputComponent";
import { RegisterSchema } from "../register/validataion";
import { gapi } from "gapi-script";
import GoogleLogin, {
  GoogleLoginResponse,
  GoogleLoginResponseOffline,
} from "react-google-login";
import http from "../../../http_common";

const LoginPage: React.FC = () => {
  const initialValues: ILogin = {
    email: "",
    password: "",
  };

  useEffect(() => {
    console.log("login");
    const start = () => {
      gapi.client.init({
        clientId: process.env.REACT_APP_GOOGLE_AUTH_CLIENT_ID,
        scope: "",
      });
    };
  });

  const responseGoogle = (
    response: GoogleLoginResponse | GoogleLoginResponseOffline
  ) => {
    //console.log("Login google", response);
    const model = {
      provider: "Google",
      token: (response as GoogleLoginResponse).tokenId,
    };
    http.post("api/account/GoogleExternalLogin", model).then((x) => {
      console.log("user koken", x);
    });
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
              <GoogleLogin
                clientId={process.env.REACT_APP_GOOGLE_AUTH_CLIENT_ID as string}
                buttonText="Вхід через гугл"
                onSuccess={responseGoogle}
                onFailure={responseGoogle}
              />
            </div>
          </Form>
        </FormikProvider>
      </div>
    </div>
  );
};

export default LoginPage;
