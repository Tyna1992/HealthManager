import React from "react";
import "../index.css";
import { capitalizeWords } from "../../utilities/utils";

function AdminTableComponent({ dataArray , onDelete, onEdit}) {
  let dataKeys = Object.keys(dataArray[0]);

  return (
    <div className="table">
      <table>
        <tbody>
          <tr>
            {dataKeys.map((dataKey, index) => (
              <th key={index}>{capitalizeWords(dataKey)}</th>
            ))}
          </tr>
          {dataArray.map((data) => (
            <tr key={data.id}>
              {dataKeys.map((dataKey, index) => (
                <>
                  <td key={dataKey + index}>{capitalizeWords(data[dataKey])}</td>
                </>
              ))}
              <td>
                <button onClick={() => onDelete(data.id)}>Delete</button>
              </td>
              <td>
                <button onClick={() => onEdit(data.id)}>Edit</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default AdminTableComponent;
