import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import http from '../../../../http_common';
import { IParentItem } from './types';
import {IParentUpdate} from '../edit/types'
import { useNavigate } from "react-router-dom";

const ParentPage = () => {

    const [list, setList] = useState<IParentItem[]>();
    const navigate = useNavigate();

    const getData = async () => {
      console.log("get parent---");
      const { data } = await http.get<IParentItem[]>("/");
      setList(data);
      console.log("data", data);
    };
    const onHandleSubmit = async (parent : IParentItem) => {
      navigate(`/parent/edit/${parent.id}`);
   
    };
  
    useEffect(() => {
        //розмонтування визивається один раз
        getData();
    },[]);

    const data = list?.map(item => {
        return (
            <tr key={item.id}>
              <td>
                <img src={http.defaults.baseURL+"\\files\\"+item.image} width="100"/>
              </td>
              <th scope="row">{item.id}</th>
              <td>{item.firstName}</td>
              <td>
                {item.lastName}
               </td>
               <td>
               <button id="myButton" onClick={()=>onHandleSubmit(item)}>Click!</button>
               </td>
            </tr>
        );
    });
    return (
      <>
        <h1>Батьки</h1>
        <Link className='btn btn-success' to="/parent/add">Додати</Link>
        <table className="table">
          <thead>
            <tr>
             <th scope="col"></th>
              <th scope="col">Id</th>
              <th scope="col">Ім'я</th>
              <th scope="col">Прізвище</th>  
              <th scope="col">Редагувати</th>
            </tr>
          </thead>
          <tbody>
            {data}
          </tbody>
        </table>
      </>
    );
}

export default ParentPage;