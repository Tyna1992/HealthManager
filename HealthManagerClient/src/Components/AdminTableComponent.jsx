import React, { useEffect, useState } from "react";
import "../index.css";
import { capitalizeWords } from "../../utilities/utils";

function AdminTableComponent({ dataArray, onDelete, onEdit }) {
  const [dataKeys, setDataKeys] = useState([]);

  useEffect(() => {
    if (dataArray.length > 0) {
      setDataKeys(Object.keys(dataArray[0]));
    }
  }, [dataArray]);

  if (!dataArray || !Array.isArray(dataArray) || dataArray.length === 0) {
    return <h1>No data to display</h1>;
  }

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
                <td key={dataKey + index}>{capitalizeWords(data[dataKey])}</td>
              ))}
              {onDelete ? (
              <td>
                <button onClick={() => onDelete(data.id)}>Delete</button>
              </td>
              ) : null}
              {onEdit ? (
              <td>
                <button onClick={() => onEdit(data)}>Edit</button>
              </td>
              ) : null}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default AdminTableComponent;
