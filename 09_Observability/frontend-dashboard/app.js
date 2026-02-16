async function loadMetrics() {
  const response = await fetch("/api/metrics");
  const data = await response.json();

  document.getElementById("metrics").innerHTML =
    `Transactions: ${data.transactions}
     Fraud Events: ${data.fraud}
     Currency Volatility: ${data.volatility}`;
}

async function loadGitHubStats() {
    const repo = "YOUR_GITHUB_USERNAME/OpenLedger-Atlas"; // update this

    const response = await fetch(`https://api.github.com/repos/${repo}`);
    const data = await response.json();

    document.getElementById("stars").textContent = data.stargazers_count;
    document.getElementById("forks").textContent = data.forks_count;
    document.getElementById("issues").textContent = data.open_issues_count;
    document.getElementById("watchers").textContent = data.watchers_count;
}

loadMetrics();
loadGithubStats();
setInterval(loadGithubStats, 60000); // every minute refresh
