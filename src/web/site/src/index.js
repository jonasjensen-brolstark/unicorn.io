import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { init as initApm } from "@elastic/apm-rum";

var apm = initApm({
  serviceName: "Site",
  serverUrl: "http://localhost:8200",
  distributedTracingOrigins: ['http://localhost:5000'],
  serviceVersion: "1.0.0",
  environment: 'production',
  logLevel: 'debug'
});

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

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
