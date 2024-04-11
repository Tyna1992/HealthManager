import React from "react";
import "../../index.css";
import { capitalizeWords } from "../../../utilities/utils";

function TableComponent({ dataArray }) {

    let dataKeys = Object.keys(dataArray[0]);
    let filteredDataKeys = dataKeys.filter((key) => key !== "id");  

    return (
        <div className="table">
            <table>
                <tbody>
                    {filteredDataKeys.map((dataKey, index) => (
                        <tr key={index}>
                            <th>{capitalizeWords(dataKey)}</th>
                            {dataArray.map((data, i) => (
                                <td key={i}>{capitalizeWords(data[dataKey])}</td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TableComponent;
