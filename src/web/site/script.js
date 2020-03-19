
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