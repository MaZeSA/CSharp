import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import http from "../../../http_common";
import { ICategoryItem } from "./types";

const CategoryPage: React.FC = () => {
  const [list, setList] = useState<ICategoryItem[]>();
  const [loadStatus, setLoadStatus] = useState<boolean>();

  const getData = async () => {
    setLoadStatus(false);
    const { data } = await http.get<ICategoryItem[]>("/api/Category/list");
    console.log(data);
    setList(data);
    setLoadStatus(false);
  };

  useEffect(() => {
    console.log("use");
    return () => {
      getData();
    };
  }, []);

  const data = list?.map((item) => {
    return (
      <tr>
        <th scope="row">{item.id}</th>
        <td>{item.name}</td>
        <td>
          <img src={http.getUri() + item.image} alt="img" width="150"></img>
        </td>
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
            to="/addCategory"
          >
            Добавити категорію
          </Link>
        </div>
        <table className="table">
          <thead>
            <tr>
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

export default CategoryPage;
