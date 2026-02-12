# Contributing to OpenSourceBank

Thank you for your interest in contributing to OpenSourceBank (“OSB”). This project is not a meme coin or hype vehicle; it is a serious experiment in transparent, ledger-based monetary architecture.

Please read this document carefully before opening issues or submitting pull requests.

---

## 1. Core principles

By contributing, you agree to respect the following principles:

- **Monetary integrity**: No hidden minting, no silent supply changes, no backdoors.  
- **Auditability**: All changes that affect balances, supply, or accounting must be traceable and explainable.  
- **Clarity over cleverness**: Readability and explicitness are preferred over “smart” but opaque code.  
- **Non-speculative posture**: This project is not an investment product, token sale, or trading platform.

---

## 2. Areas you can contribute

- **Domain modeling**: Accounts, transactions, ledger entries, audit reports.  
- **Infrastructure**: Persistence, migrations, performance, reliability.  
- **APIs**: Clean, well-documented endpoints for interacting with the ledger.  
- **Tooling**: Makefile targets, scripts, developer ergonomics.  
- **Documentation**: Clarifying monetary rules, governance, and system behavior.  
- **Testing**: Unit tests, integration tests, invariants (e.g., no negative balances).
- **UXUI**: Anything dealing with building up the user experience.

---

## 3. Contribution process

1. **Fork the repository** and create a feature branch from `main` or the relevant feature branch (e.g., `feature/osb-coin`).  
2. **Open an issue** describing the problem or enhancement you’re addressing, if one does not already exist.  
3. **Implement your changes** with clear, small commits and meaningful messages.  
4. **Add or update tests** where appropriate.  
5. **Update documentation** if your change affects behavior, rules, or public interfaces.  
6. **Open a pull request** with a concise description of:
   - What you changed  
   - Why you changed it  
   - Any risks or migration considerations  

---

## 4. Monetary system changes

Changes that affect the monetary system (OSB supply, treasury behavior, exchange rules, etc.) are considered **high impact** and must:

- Be discussed in an issue before implementation.  
- Include a clear rationale and risk assessment.  
- Include migration notes if existing data or behavior is affected.  
- Preserve auditability and backward traceability.

Pull requests that alter monetary rules without sufficient justification or documentation will be rejected.

---

## 5. Code style and architecture

- Follow existing folder structure:
  - `01_Core_Domain` for entities, value objects, interfaces  
  - `02_Application_Logic` for use cases and services  
  - `03_Infrastructure` for persistence and external integrations  
- Prefer **Value Objects** for money, currency, and other core concepts.  
- Keep business rules in the domain and application layers, not in controllers or infrastructure.

---

## 6. Legal note

By contributing, you acknowledge that:

- You are not creating or promoting a public investment product.  
- You are not entitled to any financial return or ownership stake.  
- Your contributions are made under the project’s license and may be modified, refactored, or removed in future versions.

---

## 7. Getting started

Good first steps:

- Read `README.md` to understand the monetary rules.  
- Explore `01_Core_Domain/ValueObjects` and `Entities` to see how money and accounts are modeled.  
- Run the project using the provided `Makefile` targets.  
- Open a small documentation or test-related PR to get familiar with the workflow.

If you’re serious about building real monetary infrastructure instead of hype, you’re in the right place.
