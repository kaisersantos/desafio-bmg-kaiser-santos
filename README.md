# Projeto BMG API

Este projeto foi desenvolvido a partir de um **boilerplate existente** para que o foco estivesse exclusivamente no **desafio e suas regras de negócio**, permitindo a conclusão dentro do prazo de **30 minutos**.

⚠️ O projeto conta com **overengineering proposital**, simulando um projeto grande e modularizado em múltiplas camadas.

---

## 🚀 Tecnologias

- **.NET 9**
- **Entity Framework Core (SQLite)**
- **FluentValidation**
- **AutoMapper**
- **JWT Authentication**
- **Swagger / OpenAPI**
- **xUnit + Moq (para testes)**

---

## 📦 Estrutura

- `Bmg.Api` → Projeto principal (Web API)
- `Bmg.Application` → Serviços, validações e regras de negócio
- `Bmg.Domain` → Entidades de domínio
- `Bmg.Adapter.Infra.EFCore` → Infraestrutura e repositórios (EF Core + SQLite)
- `Bmg.Adapter.PixGateway` → Gateway Fake Pix
- `Bmg.Adapter.CreditCardGateway` → Gateway Fake Cartão de Crédito

---

## ▶️ Como rodar

1. Abra a **solução** no Visual Studio / Rider / VS Code.
2. Configure o projeto de inicialização como **Bmg.Api**.
3. Rode a aplicação.

👉 Utilize o Swagger para testar
👉 Ou utilize o arquivo **Postman Collection** (`postman_collection.json`) que está na raiz do projeto.

---

## 🗄️ Banco de Dados

- O banco **SQLite** será criado automaticamente na pasta: **Bmg.Api/bin/Debug/net9.0/Data/bmg.db**

- As **migrations** são aplicadas automaticamente ao rodar pela primeira vez.
- Uma **seed inicial** cria o usuário **admin** com as credenciais: **admin@bmg.com** e senha **Admin@123**

---

## 💳 Gateways de Pagamento (Fakes)

- **FakePixGateway**
- **FakeCreditCardGateway**

Ambos simulam um tempo de resposta aleatório (entre **1ms e 1000ms**) e retornam resultado **aleatório** (`true` ou `false`).

---

## 📈 Próximos Passos

- [ ] Implementar **cache**
- [ ] Implementar **paginação**
- [ ] Comunicação com **gateway real** de pagamento via **Refit**
- [ ] Melhorar logs utilizando **Serilog** (atualmente usa `ILogger` nativo)
- [ ] Adicionar **monitoramento** (Prometheus + Grafana)
- [ ] Implementar **HealthChecks**
- [ ] Finalizar **testes unitários**
- [ ] Criar **testes de integração**
