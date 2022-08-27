import { useEffect, useState } from "react";
import { Form, FormikProvider, useFormik } from "formik";
import InputComponent from "../../../common/InputComponent";
import { ParentAddSchema } from "../add/validataion";
import { IParentItem } from "../list/types";
import { IParentUpdate } from "./types";
import CropperDialog from "../../../common/CropperDialog";
import http from "../../../../http_common";
import { useNavigate, useParams } from "react-router-dom";

const ParentEditPage: React.FC = () => {
  let { id } = useParams();
  const initialValues: IParentUpdate = {
    id: '-1',
    firstName: "",
    lastName: "",
    phone: "",
    imageBase64: "",
    adress: "",
  };
 
  const [parent, setparent] = useState<IParentItem>();
  const navigate = useNavigate();
  const [message, setMessage] = useState<string>();

  const onHandleSubmit = async (values: IParentUpdate) => {
    console.log(values);
    await http
      .put<IParentUpdate>("/update", values)
      .then((response) => {
        console.log("response " + response);
        navigate("/parent");
      })
      .catch((error) => {
        setMessage(error.message);
      });
  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onHandleSubmit,
    validationSchema: ParentAddSchema,
  });

  const getData = async () => {
    const { data } = await http.get<IParentItem>("/getparent/" + id);
    console.log("dataLoad", data);

    setFieldValue("firstName", data.firstName);
    setFieldValue("lastName", data.lastName);
    setFieldValue("phone", data.phone);
    setFieldValue("adress", data.adress);
    setFieldValue("id", data.id);
    
    setparent(data);
  };

  useEffect(() => {
    getData();
  }, []);

  const { errors, touched, handleSubmit, handleChange, setFieldValue } = formik;

//console.log("bild");

  if (parent == undefined) {
    return (
      <>
        <div className="container">
          <div className="row justify-content-md-center">
            <div className="spinner-border mt-5" role="status"></div>
          </div>
        </div>
      </>
    );
  } else {
    //Деструктуризація

    return (
      <>
        <div className="row">
          <div className="offset-md-3 col-md-6">
            <h1 className="text-center">Редагувати батька </h1>
            <FormikProvider value={formik}>
              <Form onSubmit={handleSubmit}>
                <InputComponent
                  inputName="firstName"
                  title="Імя"
                  touched={touched.firstName}
                  errors={errors.firstName}
                  text={parent.firstName}
                  handleChange={handleChange}
                />

                <InputComponent
                  inputName="lastName"
                  title="Прізвище"
                  touched={touched.lastName}
                  errors={errors.lastName}
                  text={parent.lastName}
                  handleChange={handleChange}
                />

                <InputComponent
                  inputName="phone"
                  title="Телефон"
                  touched={touched.phone}
                  errors={errors.phone}
                  text={parent.phone}
                  handleChange={handleChange}
                />

                <InputComponent
                  inputName="adress"
                  title="Адрес"
                  touched={touched.adress}
                  errors={errors.adress}
                  text={parent.adress}
                  handleChange={handleChange}
                />

                <CropperDialog
                  onChange={setFieldValue}
                  field="imageBase64"
                  error={errors.imageBase64}
                  touched={touched.imageBase64}
                  value={parent.image}
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
      </>
    );
  }
};

export default ParentEditPage;
