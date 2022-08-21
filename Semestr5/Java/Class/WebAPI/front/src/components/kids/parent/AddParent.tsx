import { Form, FormikProvider, useFormik } from "formik";
import React, { useState } from "react";
import InputComponent from "../../auth/inputComponent";
import { CategorySchema } from "./validataion";
import { IParentItem } from "./types";
import CropperDialog from "../../common/CropperDialog";
import http from "../../../http_common";
import { useNavigate } from "react-router-dom";

const AddParent: React.FC = () => {
  const initialValues: IParentItem = {
    id: -1,
    firstName: "",
    lastName: "",
    phone: "",
    image: "",
    adress: "",
  };

  const navigate = useNavigate();
  const [message, setMessage] = useState<string>();

  const onHandleSubmit = async (values: IParentItem) => {
    if (values.id != -1) {
      // update
    } else {
      const article = {
        firstName: values.firstName,
        lastName: values.lastName,
        phone: values.phone,
        image: values.image,
        adress: values.adress,
      };
      
      console.log("values :" +values.firstName);
      console.log(article);

      await http
        .post<IParentItem>("/create", article)
        .then((response) => {
          console.log("response "+ response);
          navigate("/parent");
        })
        .catch((error) => {
          setMessage(error.message);
        });
    }
  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onHandleSubmit,
    validationSchema: CategorySchema,
  });
  //Деструктуризація
  const { errors, touched, handleSubmit, handleChange, setFieldValue } = formik;

  return (
    <div className="row">
      <div className="offset-md-3 col-md-6">
        <h1 className="text-center">Добавити категорію</h1>
        <FormikProvider value={formik}>
          <Form onSubmit={handleSubmit}>
            <input
              type="text"
              hidden
              className="form-control"
              id="id"
              name="id"
            />
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
              inputName="phone"
              title="Телефон"
              touched={touched.phone}
              errors={errors.phone}
              handleChange={handleChange}
            ></InputComponent>
            <InputComponent
              inputName="adress"
              title="Адрес"
              touched={touched.adress}
              errors={errors.adress}
              handleChange={handleChange}
            ></InputComponent>
            <CropperDialog
              onChange={setFieldValue}
              field="image"
              error={errors.image}
              touched={touched.image}
            />
            <div className="mb-3">
              <button type="submit" className="btn btn-primary">
                Добавити
              </button>
              {message && (
                <div className="alert alert-danger" role="alert">
                  {message}
                </div>
              )}
            </div>
          </Form>
        </FormikProvider>
      </div>
    </div>
  );
};

export default AddParent;
