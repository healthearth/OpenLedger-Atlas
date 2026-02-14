async function loadMetrics() {
  const response = await fetch("/api/metrics");
  const data = await response.json();

  document.getElementById("metrics").innerHTML =
    `Transactions: ${data.transactions}
     Fraud Events: ${data.fraud}
     Currency Volatility: ${data.volatility}`;
}

loadMetrics();

