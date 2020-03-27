import { init as initApm } from '@elastic/apm-rum'

var apm = initApm({

  // Set required service name 
  // (allowed characters: a-z, A-Z, 0-9, -, _, 
  // and space)
  serviceName: 'Site',

  // Set custom APM Server URL (
  // default: http://localhost:8200)
  serverUrl: 'http://localhost:8200',

  // Set service version (required for sourcemap 
  // feature)
  serviceVersion: '1.0.0'
})

fetchUnicorns();

function fetchUnicorns() {
  fetch('http://localhost:5000/unicorn')
  .then((response) => {
    return response.json();
  })
  .then((data) => {
    document.getElementById("json").textContent = JSON.stringify(data, undefined, 2);
    setTimeout(fetchUnicorns, 5000);
  });
}