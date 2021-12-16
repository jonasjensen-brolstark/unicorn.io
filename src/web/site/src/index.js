import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";

fetchUnicorns();

function fetchUnicorns() {
  fetch("http://localhost:5000/unicorn")
    .then(response => {
      return response.json();
    })
    .then(data => {
      const trans = apm.getCurrentTransaction();
      if (trans) trans.end();
      console.log(data);
      setTimeout(fetchUnicorns, 5000);
    });
}

ReactDOM.render(<App />, document.getElementById("root"));
