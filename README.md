# 🚀 Aplicação de Estudos .NET

## 📋 Visão Geral

Este projeto representa uma jornada de aprendizado contínuo no desenvolvimento de aplicações .NET modernas, começando com uma estrutura simples e evoluindo gradualmente conforme novos conhecimentos são adquiridos através de cursos e práticas.

## 🎯 Objetivo

Criar uma aplicação robusta e escalável que sirva como laboratório para experimentação e aplicação de conceitos avançados de desenvolvimento de software, seguindo as melhores práticas da indústria.

## 🛠️ Stack Tecnológica

### Backend
- **.NET 8.0** - Framework principal para desenvolvimento da API
- **Clean Architecture** - Padrão arquitetural para organização e separação de responsabilidades
- **SQL Server** - Sistema de gerenciamento de banco de dados relacional

### DevOps & Infraestrutura
- **Docker** - Containerização da aplicação
- **GitLab CI/CD** - Pipeline de integração e entrega contínua
- **Linux Server** - Ambiente de produção

## 🏗️ Arquitetura

A aplicação segue os princípios da **Clean Architecture**, organizando o código em camadas bem definidas:

```
📁 Presentation Layer (API)
📁 Application Layer (Use Cases)
📁 Domain Layer (Entities & Business Rules)
📁 Infrastructure Layer (Data Access & External Services)
```

### Benefícios da Clean Architecture:
- **Testabilidade** - Facilita a criação de testes unitários e de integração
- **Manutenibilidade** - Código organizado e fácil de modificar
- **Independência de Frameworks** - Core da aplicação isolado de dependências externas
- **Flexibilidade** - Permite mudanças de tecnologia sem impacto no domínio

## 🔄 Pipeline CI/CD

O processo de entrega utiliza GitLab CI/CD com as seguintes etapas:

1. **Build** - Compilação da aplicação
2. **Test** - Execução de testes automatizados
3. **Docker Build** - Criação da imagem Docker
4. **Deploy** - Implantação no servidor Linux

## 📚 Metodologia de Aprendizado

### Abordagem Evolutiva
- **Fase 1**: Estrutura básica com CRUD simples
- **Fase 2**: Implementação de padrões avançados (CQRS, Repository, Unit of Work)
- **Fase 3**: Adição de funcionalidades como autenticação/autorização
- **Fase 4**: Implementação de microsserviços e message brokers
- **Fase 5**: Observabilidade, logging e monitoramento

### Fontes de Conhecimento
- Cursos online
- Documentação oficial Microsoft
- Comunidade .NET

## 🎯 Funcionalidades Planejadas

### MVP (Minimum Viable Product)
- [ ] API RESTful básica
- [ ] Operações CRUD
- [ ] Documentação com Swagger
- [ ] Containerização Docker

### Funcionalidades Avançadas
- [ ] Autenticação JWT
- [ ] Health Checks
- [ ] Logging estruturado
- [ ] Métricas e monitoramento

## 🚀 Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- SQL Server
- Docker
- Git

## 📈 Roadmap de Evolução

| Versão | Funcionalidades | Status |
|--------|-----------------|--------|
| v1.0   | CRUD básico + Docker | 🔄 Em Desenvolvimento |
| v1.1   | Autenticação JWT | 📋 Planejado |
| v1.2   | CQRS + MediatR | 📋 Planejado |
| v2.0   | Microsserviços | 📋 Planejado |

## 🔧 Configuração de Ambiente

### Desenvolvimento
- Visual Studio 2022
- SQL Server Developer
- Docker Desktop
- Postman para testes de API

### Produção
- Linux Server (Ubuntu)
- SQL Server on Linux
- Docker Swarm ou Kubernetes
- Nginx como reverse proxy

## 📝 Documentação

A documentação completa incluirá:
- Guia de instalação e configuração
- Documentação da API
- Diagramas de arquitetura
- Guias de contribuição
- Exemplos de uso

## 🤝 Contribuições

Este é um projeto de estudo pessoal, mas sugestões e feedback são sempre bem-vindos para enriquecer o aprendizado.

---

**"A jornada de mil milhas começa com um único passo"** - Começando simples, evoluindo constantemente! 🌱