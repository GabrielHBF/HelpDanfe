# 📄 HelpDanfe

![NuGet Version](https://img.shields.io/nuget/v/HelpDanfe?style=flat-square&color=0078D4)
![NuGet Downloads](https://img.shields.io/nuget/dt/HelpDanfe?style=flat-square&color=00d860)
![.NET Version](https://img.shields.io/badge/.NET-8.0%2B-512BD4?style=flat-square&logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green?style=flat-square)

**HelpDanfe** é uma biblioteca .NET leve, moderna e de alto desempenho para leitura e extração de dados de Documentos Auxiliares da Nota Fiscal Eletrônica (DANFE). 

Projetada com foco no **Princípio da Responsabilidade Única (SRP)** e **Clean Code**, a biblioteca é ideal para integração com ERPs, automação de processos fiscais e auditorias de notas de entrada.

---

## 🚀 Principais Recursos

* **Dupla Leitura Unificada:** Extraia dados tanto de arquivos **PDF** quanto de arquivos **XML** utilizando a mesma interface simples.
* **Mapeamento Inteligente:** Captura de Chave de Acesso (44 dígitos), Protocolo de Autorização, CNPJ/CPF, Razão Social e Endereços completos do Emitente e Destinatário.
* **Detalhamento Fiscal:** Extração de valores financeiros e tributários (Bases de cálculo ICMS/ICMS-ST, valores totais, IPI, frete, seguros, descontos e outras despesas).
* **Itens e Produtos:** Mapeamento em lista de todos os produtos ou serviços da nota (Código, Descrição, NCM, Quantidade, Valor Unitário e Valor Total).
* **Arquitetura Desacoplada:** Separação clara entre serviços de extração, mappers específicos e utilitários de conversão, garantindo máxima performance e facilidade de testes unitários.

---

## 🏗️ Arquitetura do Projeto

O projeto é dividido em módulos especializados para evitar *God Classes* e facilitar a manutenção:

| Componente | Responsabilidade |
| :--- | :--- |
| **`ExtratorService`** | Orquestração da leitura de streams e arquivos (PDF/XML). |
| **`DanfePdfMapper`** | Mapeamento e extração de texto via expressões regulares (Regex). |
| **`DanfeXmlMapper`** | Navegação em nós e elementos estruturados do padrão Fiscal XML. |
| **`Util / DecimalParser`** | Higienização de strings e conversões financeiras seguras com *Globalization*. |

---

## 📦 Instalação

Instale o pacote diretamente através do **NuGet Package Manager** ou pelo **.NET CLI**:

### .NET CLI:
```bash
dotnet add package HelpDanfe