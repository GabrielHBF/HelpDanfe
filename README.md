# 📄 HelpDanfe
![NuGet Version](https://img.shields.io/nuget/v/HelpDanfe?style=flat-square&color=0078D4)
![NuGet Downloads](https://img.shields.io/nuget/dt/HelpDanfe?style=flat-square&color=00d860)
![.NET Version](https://img.shields.io/badge/.NET-8.0%2B-512BD4?style=flat-square&logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green?style=flat-square)

**HelpDanfe** é uma biblioteca .NET leve, moderna e de alto desempenho para leitura e extração de dados de Documentos Auxiliares da Nota Fiscal Eletrônica (DANFE). 
**HelpDanfe** é uma biblioteca .NET leve, moderna e de alto desempenho para leitura e extração de dados de Documentos Auxiliares da Nota Fiscal Eletrônica (DANFE).

Projetada com foco no **Princípio da Responsabilidade Única (SRP)** e **Clean Code**, a biblioteca é ideal para integração com ERPs, automação de processos fiscais e auditorias de notas de entrada.

---

## 🚀 Principais Recursos

* **Dupla Leitura Unificada:** Extraia dados tanto de arquivos **PDF** quanto de arquivos **XML** utilizando a mesma interface simples.
* **Mapeamento Inteligente:** Captura de Chave de Acesso (44 dígitos), Protocolo de Autorização, CNPJ/CPF, Razão Social e Endereços completos do Emitente e Destinatário.
* **Detalhamento Fiscal:** Extração de valores financeiros e tributários (Bases de cálculo ICMS/ICMS-ST, valores totais, IPI, frete, seguros, descontos e outras despesas).
* **Itens e Produtos:** Mapeamento em lista de todos os produtos ou serviços da nota (Código, Descrição, NCM, Quantidade, Valor Unitário e Valor Total).
* **Alta Performance (Source Generators):** Utilização das mais recentes APIs do .NET (`[GeneratedRegex]`) para processamento em tempo de compilação, reduzindo alocação de memória.
* **Arquitetura Desacoplada:** Separação clara entre serviços de extração, mappers específicos e utilitários de conversão, garantindo máxima performance e facilidade de testes unitários.

---

## 🏗️ Estrutura e Arquitetura do Projeto

O projeto é modularizado por responsabilidades de domínio, evitando o acoplamento de classes e facilitando a manutenção e expansão:

```text
HelpDanfe
├── 📁 Enums
│   └── FileTypes.cs                  # Definições de tipos de arquivos suportados (PDF/XML)
├── 📁 Error
│   ├── Error.cs                      # Modelo padrão de resposta de erros
│   └── FileErrors.cs                 # Erros específicos de processamento e validação de arquivos
├── 📁 Interface
│   └── 📁 Services
│       └── IExtratorService.cs       # Contrato principal para extração de dados de NFe
├── 📁 Invoices
│   ├── 📁 Company
│   │   ├── RecipientCompanyEntity.cs # Entidade de representação do Destinatário
│   │   └── SendingCompanyEntity.cs   # Entidade de representação do Emitente
│   ├── 📁 Danfe
│   │   └── DanfeEntity.cs            # Entidade raiz agregadora dos dados da nota
│   ├── 📁 Services
│   │   └── ServicesOrProducts.cs     # Entidade para os itens (Produtos/Serviços) da nota
│   └── 📁 Tax
│       └── TaxCalculationEntity.cs   # Entidade com os totais de impostos e bases de cálculo
├── 📁 Mappers
│   ├── DanfePdfMapper.cs             # Mapeamento e extração via Regex a partir de arquivos PDF
│   └── DanfeXmlMapper.cs             # Mapeamento via navegação em nós de arquivos XML do SEFAZ
├── 📁 Request
│   └── FileImportRequest.cs          # Modelo de entrada para requisição de leitura de arquivo
├── 📁 Services
│   └── ExtratorService.cs            # Implementação da orquestração de extração
└── 📁 Utils
    └── Utils.cs                      # Utilitários gerais (parsers decimais, limpezas de strings)

```

---

## 🔍 Mapeamento por Expressões Regulares (Regex)

Para a leitura eficiente de arquivos PDF via `DanfePdfMapper`, o **HelpDanfe** utiliza o recurso de **Source Generators** (`[GeneratedRegex]`), que otimiza as expressões regulares no momento da compilação.

Abaixo estão documentadas todas as expressões utilizadas pela biblioteca e seus respectivos objetivos na raspagem do texto da DANFE:

| Método Regex | Padrão (Pattern) | Objetivo / Campo Extraído |
| --- | --- | --- |
| **`WhiteSpaceRegex`** | `\s+` | Localiza e agrupa sequências de espaços em branco para normalização de textos. |
| **`AccessKeyRegex`** | `CHAVE DE ACESSO\s*([\d\s]+)` | Identifica o bloco de texto que contém a numeração da **Chave de Acesso** da nota. |
| **`NonDigitRegex`** | `\D` | Remove qualquer caractere que não seja dígito numérico (ideal para limpar máscaras de CPF/CNPJ ou CEP). |
| **`AccessKeyDigitsRegex`** | `\d{44}` | Valida e extrai com precisão a **Chave de Acesso de 44 dígitos** padrão SEFAZ. |
| **`AuthorizationProtocolAccessRegex`** | `(?:PROTOCOLO DE AUTORIZA[ÇC][ÃA]O(?: DE USO)?|PROTOCOLO)\s*(\d{15})` | Captura os **15 dígitos do Protocolo de Autorização de Uso** emitido pela SEFAZ. |
| **`CpfCnpjPatternRegex`** | `(\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}|\d{3}\.\d{3}\.\d{3}\-\d{2})` | Identifica números formatados de **CNPJ** (`XX.XXX.XXX/XXXX-XX`) ou **CPF** (`XXX.XXX.XXX-XX`). |
| **`InvoiceNumberSeriesRegex`** | `(?:NF-e\s+)?N[º°]?\s*([\d.]+)\s*S[EÉ]RIE:?\s*(\d+)` | Extrai o **Número da Nota Fiscal** e a sua respectiva **Série** no cabeçalho do documento. |
| **`ReceivingCompanyNameRegex`** | `NOME\s*/\s*RAZ[AÃ]O\s*SOCIAL\s+(.+?)\s+CNPJ` | Captura a **Razão Social ou Nome do Destinatário** da nota. |
| **`ReceivingCompanyAddressRegex`** | `ENDERE[CÇ]O\s+(.+?)\s+(?:BAIRRO|MUNIC[IÍ]PIO|DATA)` | Extrai a linha do **Logradouro do Destinatário** até o delimitador de bairro ou município. |
| **`ReceivingCompanyPhoneRegex`** | `FONE\s*/\s*FAX\s+([\d\(\)\-\s]{10,20})` | Captura o número de **Telefone ou Fax** do destinatário no formato com ou sem DDD/mascara. |
| **`SendingCompanyNameRegex`** | `RECEBEMOS\s+(?:DE\s+)?(.+?)\s+OS\s+PRODUTOS` | Extrai a Razão Social do **Emitente** diretamente através da leitura do canhoto de recebimento da nota. |
| **`SendingCompanyNameFallbackRegex`** | `AO\s+LADO\s+DE:\s*(.+?)(?:\s+S[EÉ]RIE|\s+N[ÚU]MERO|$)` | **Fallback:** Tenta recuperar o nome do **Emitente** por meio das especificações de assinatura do canhoto caso o padrão principal falhe. |

> **Nota:** As expressões que dependem do texto em português usam explicitamente a *culture* `"pt-BR"` e `RegexOptions.IgnoreCase` para garantir maior tolerância a variações de escaneamento, pontuação e fontes dos geradores de PDF.

---

## 📦 Instalação

Instale o pacote diretamente através do **NuGet Package Manager** ou pelo **.NET CLI**:

### .NET CLI:

```bash
dotnet add package HelpDanfe

```

### Package Manager Console:

```powershell
Install-Package HelpDanfe

```

---

## 💡 Como Usar

### Exemplo Básico via Injeção de Dependência

```csharp
using HelpDanfe.Interface.Services;
using HelpDanfe.Enums;

public class ImportacaoNotaService
{
    private readonly IExtratorService _extratorService;

    public ImportacaoNotaService(IExtratorService extratorService)
    {
        _extratorService = extratorService;
    }

    public async Task ProcessarArquivoAsync(Stream arquivoStream, FileTypes tipoArquivo)
    {
        // A mesma interface processa tanto XML quanto PDF de acordo com o enum passado
        var danfe = await _extratorService.ExtrairDadosAsync(arquivoStream, tipoArquivo);

        if (danfe != null)
        {
            Console.WriteLine($"Nota Nº: {danfe.Numero} - Série: {danfe.Serie}");
            Console.WriteLine($"Chave: {danfe.ChaveAcesso}");
            Console.WriteLine($"Emitente: {danfe.Emitente.RazaoSocial}");
            Console.WriteLine($"Total da Nota: {danfe.Impostos.ValorTotalNota:C}");
        }
    }
}

```

---

## 🤝 Contribuindo

Contribuições são sempre bem-vindas! Sinta-se à vontade para abrir uma *Issue* com relatórios de bugs, solicitações de funcionalidades ou enviar um *Pull Request* para melhorar os parsers ou documentação.

---

## 📝 Licença

Distribuído sob a licença **MIT**. Veja o arquivo `LICENSE` para mais detalhes.
