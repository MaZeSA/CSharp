import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import http from "../../../http_common";
import { IParentItem} from "./types";

const ParentPage: React.FC = () => {
  const [list, setList] = useState<IParentItem[]>();
  const [loadStatus, setLoadStatus] = useState<boolean>();

  const getData = async () => {
    setLoadStatus(false);
    const { data } = await http.get<IParentItem[]>("/");
    setList(data);
    setLoadStatus(false);
  };

  useEffect(() => {
    getData();
  }, []);

  const data = list?.map((item) => {
    return (
      <tr key={item.id}> 
       <td>
          <img src={http.getUri() + item.image} alt="img" width="150"></img>
        </td>
        <th scope="row">{item.id}</th>
        <td>{item.firstName}</td>
        <td>{item.lastName}</td>
      </tr>
    );
  });
  if (loadStatus == false) {
    return (
      <div className="container">
        <div className="row justify-content-md-center">
          <Link
            className="nav-link active"
            aria-current="page"
            to="/addParent"
          >
            Добавити батьків
          </Link>
        </div>
        <table className="table">
          <thead>
            <tr>
              <th scope="col">Image</th>
              <th scope="col">Id</th>
              <th scope="col">Name</th>
              <th scope="col">Image</th>
            </tr>
          </thead>
          <tbody>{list && data}</tbody>
        </table>
      </div>
    );
  } else {
    return (
      <div className="container">
        <div className="row justify-content-md-center">
          <div className="spinner-border mt-5" role="status"></div>
        </div>
      </div>
    );
  }
};

export default ParentPage;
