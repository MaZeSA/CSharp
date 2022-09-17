import http from "../../../../http_common";
//import { useNavigate } from "react-router-dom";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";
import { Form, FormikProvider, useFormik } from "formik";
import { ISearchParent } from "./type";
import InputComponent from "../../../common/InputComponent";
import { IParentItem } from "../list/types";

const SearchParentPage = () => {
 // const navigate = useNavigate();
  //const [status, setStatus] = useState<string>();
  const { getSearchParentsResult } = useActions();
  const { searchResult } = useTypedSelector((store) => store.parent);
  // const [search, setSearch] = useState<ISearchParent>({
  //   id: 0,
  //   name:'',
  // });
const search ={  
  id: 0,
     name:'',
 }

  const onHandleSubmit = async (values: ISearchParent) => {
   // const response = 
    await getSearchParentsResult(values);
    // formik.resetForm();
  };

  const formik = useFormik({
    initialValues: search,
    onSubmit: onHandleSubmit,
  });

  const { handleChange, handleSubmit } = formik;
  
   const data = searchResult.map((item: IParentItem) => {
    return (
      <>
        <tr key={item.id}>
          <td>
            <img
              src={http.defaults.baseURL + "\\files\\" + item.image}
              width="100"
            />
          </td>
          <th scope="row">{item.id}</th>
          <td>{item.firstName}</td>
          <td>{item.lastName}</td>
          </tr>
      </>
    );
  });
  return (
  <>
   <div className="row">
      <div className="offset-md-3 col-md-6">
        <h1 className="text-center">Пошук батьків</h1>
        <FormikProvider value={formik}>
          <Form onSubmit={handleSubmit}>
            <InputComponent
              inputName="name"
              title="Імя"
              handleChange={handleChange}
            />
             <InputComponent
              inputName="id"
              title="Id"
              handleChange={handleChange}
            />
             <div className="mb-3">
              <button type="submit" className="btn btn-primary">
                Пошук
              </button>
            </div>
          </Form>
        </FormikProvider>
      </div>
      <>
      <h1>Батьки</h1>
      <h5 className="text-danger text-center"></h5>
      <table className="table">
        <thead>
          <tr>
            <th scope="col"></th>
            <th scope="col">Id</th>
            <th scope="col">Ім'я</th>
            <th scope="col">Прізвище</th>
          </tr>
        </thead>
        <tbody>{data}</tbody>
      </table>
    </>
    </div>
  </>
  );
};

export default SearchParentPage;
