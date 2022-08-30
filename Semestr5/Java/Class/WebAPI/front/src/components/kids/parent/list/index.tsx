import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import http from "../../../../http_common";
import { IParentItem } from "./types";
import { useNavigate } from "react-router-dom";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";

const ParentPage = () => {
  const navigate = useNavigate();
  const { parents } = useTypedSelector((store) => store.parent);
  const { getParents } = useActions();
  const { deleteParent } = useActions();
  const [status, setStatus] = useState<string>();

  const onHandleSubmit = async (parent: IParentItem) => {
    navigate(`/parent/edit/${parent.id}`);
  };
  const delParent = async (id: number) => {
    const result =  await deleteParent(id);
    if (result === 200) {
      setStatus('Батька видалено!')
    }else{
      setStatus('Response: ' + result )
    }
  };

  useEffect(() => {
    getParents();
  }, [getParents]);

  const data = parents.map((item: IParentItem) => {
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
          <td>
            <button id="myButton" onClick={() => onHandleSubmit(item)}>
              Edit!
            </button>
          </td>
          <td>
            <button id="myButton" onClick={() => delParent(item.id)}>
              Remove!
            </button>
          </td>
        </tr>
      </>
    );
  });

  return (
    <>
      <h1>Батьки</h1>
      <Link className="btn btn-success" to="/parent/add">
        Додати
      </Link><Link className="btn btn-warning ms-2" to="search">
        Пошук
      </Link>
      <h5 className="text-danger text-center"></h5>
      <table className="table">
        <thead>
          <tr>
            <th scope="col"></th>
            <th scope="col">Id</th>
            <th scope="col">Ім'я</th>
            <th scope="col">Прізвище</th>
            <th scope="col">Редагувати</th>
            <th scope="col">Видалити</th>
          </tr>
        </thead>
        <tbody>{data}</tbody>
      </table>
    </>
  );
};

export default ParentPage;
