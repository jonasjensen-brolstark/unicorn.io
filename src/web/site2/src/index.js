import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { init as initApm } from "@elastic/apm-rum";

var apm = initApm({
  // Set required service name
  // (allowed characters: a-z, A-Z, 0-9, -, _,
  // and space)
  serviceName: "Site",

  // Set custom APM Server URL (
  // default: http://localhost:8200)
  serverUrl: "http://localhost:8200",
  distributedTracingOrigins: ['http://localhost:5000'],

  // Set service version (required for sourcemap
  // feature)
  serviceVersion: "1.0.0"
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
