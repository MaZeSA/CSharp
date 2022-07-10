import { Form, FormikProvider, useFormik } from "formik";
import React, { useState } from "react";
import InputComponent from "../../auth/inputComponent";
import { CategorySchema } from "./validataion";
import { ICategoryItem } from "./types";
import CropperDialog from "../../common/CropperDialog";
import http from "../../../http_common";

const AddCategory: React.FC = () => {
  const initialValues: ICategoryItem = {
    id: -1,
    name: "",
    image: "",
  };

  const [message, setMessage] = useState<string>();

  const onHandleSubmit = async (values: ICategoryItem) => {
    if (values.id != -1) {
      // update
    } else {
      const article = { Name: values.name, ImageBase64: values.image };
      await http
        .post<ICategoryItem>("/api/Category/create", article)
        .then((response) => {
          window.location.href = "./category";
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
              inputName="name"
              title="Назва категорії"
              touched={touched.name}
              errors={errors.name}
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
             {message && <div className="alert alert-danger" role="alert">{message}</div>}
            </div>
          </Form>
        </FormikProvider>
      </div>
    </div>
  );
};

export default AddCategory;
