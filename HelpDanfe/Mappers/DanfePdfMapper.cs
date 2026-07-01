using HelpDanfe.Invoices.Danfe;
using HelpDanfe.Invoices.Services;
using HelpDanfe.Invoices.Tax;
using System.Text.RegularExpressions;

using Util = HelpDanfe.Utils.Utils;

namespace HelpDanfe.Mappers;

public static class DanfePdfMapper
{
    public static void DataMapper(string text, DanfeEntity danfe)
    {
        var keyMatch = Util.AccessKeyRegex().Match(text);
        if (keyMatch.Success)
        {
            string key = Util.NonDigitRegex().Replace(keyMatch.Groups[1].Value, "");
            if (key.Length >= 44)
                danfe.AccessKey = key[..44];
        }
        else
        {
            var fallbackKey = Util.AccessKeyDigitsRegex().Match(text.Replace(" ", ""));
            if (fallbackKey.Success)
                danfe.AccessKey = fallbackKey.Value;
        }

        var protocolMatch = Util.AuthorizationProtocolAccessRegex().Match(text);
        if (protocolMatch.Success)
            danfe.SendingCompany!.AuthorizationProtocolAcess = protocolMatch.Groups[1].Value;

        var docMatches = Util.CpfCnpjPatternRegex().Matches(text);
        if (docMatches.Count > 0)
        {
            foreach (Match doc in docMatches)
            {
                if (doc.Value.Length > 14)
                    danfe.SendingCompany!.Cnpj = doc.Value;
                else
                    danfe.ReceivingCompany!.Cpf = doc.Value;
            }
        }

        var numSerialMatch = Util.InvoiceNumberSeriesRegex().Match(text);
        if (numSerialMatch.Success)
        {
            danfe.Number = numSerialMatch.Groups[1].Value.Replace(".", "");
            danfe.SerialNumber = numSerialMatch.Groups[2].Value;
        }

        var recNameMatch = Util.ReceivingCompanyNameRegex().Match(text);
        if (recNameMatch.Success)
            danfe.ReceivingCompany!.CompanyName = recNameMatch.Groups[1].Value.Trim();

        var addressMatch = Util.ReceivingCompanyAddressRegex().Match(text);
        if (addressMatch.Success)
            danfe.ReceivingCompany!.Address = addressMatch.Groups[1].Value.Trim();

        var phoneMatch = Util.ReceivingCompanyPhoneRegex().Match(text);
        if (phoneMatch.Success)
            danfe.ReceivingCompany!.PhoneNumber = phoneMatch.Groups[1].Value.Trim();

        var senderNameMatch = Util.SendingCompanyNameRegex().Match(text);
        if (senderNameMatch.Success)
        {
            danfe.SendingCompany!.CompanyName = senderNameMatch.Groups[1].Value.Trim();
        }
        else
        {
            var senderNameMatch2 = Util.SendingCompanyNameFallbackRegex().Match(text);
            if (senderNameMatch2.Success)
                danfe.SendingCompany!.CompanyName = senderNameMatch2.Groups[1].Value.Trim();
        }
    }

    public static void ProductMapper(string text, List<ServicesOrProducts> produtos)
    {
        string patternProduct = @"(?<=\s|^)(?<codigo>[A-Z0-9.\-]*\d[A-Z0-9.\-]*)(?=\s)\s+(?<desc>.{3,80}?)\s+(?<ncm>\d{8})\s+(?:[\d.]+\s+){2,4}(?<unidade>[A-Z]{2,4})\s+(?<qtd>[\d.,]+)\s+(?<vun>[\d.,]+)\s+(?<vtot>[\d.,]+)";

        var matches = Regex.Matches(text, patternProduct, RegexOptions.IgnoreCase);

        foreach (Match match in matches)
        {
            var product = new ServicesOrProducts
            {
                Id = Guid.NewGuid(),
                Code = match.Groups["codigo"].Value.Trim(),
                Description = match.Groups["desc"].Value.Trim(),
                Quantity = (int)Math.Round(Util.ParseFromText(match.Groups["qtd"].Value)),
                UnitValue = Util.ParseFromText(match.Groups["vun"].Value),
                TotalValue = Util.ParseFromText(match.Groups["vtot"].Value)
            };

            produtos.Add(product);
        }
    }

    public static void TaxMapper(string text, TaxCalculationEntity? tax)
    {
        if (tax is null) return;

        tax.BaseCalculeteIcms = Util.ExtractFromText(text, @"BASE DE C[AÁ]LCULO(?: DE)? ICMS[^\d]*([\d.,]+)");
        tax.ValueIcms = Util.ExtractFromText(text, @"VALOR DO ICMS[^\d]*([\d.,]+)");
        tax.BaseCalculeteIcmsSt = Util.ExtractFromText(text, @"BASE DE C[AÁ]LCULO ICMS S(?:T|UBST\.?)[^\d]*([\d.,]+)");
        tax.ValueIcmsSt = Util.ExtractFromText(text, @"VALOR DO ICMS SUBST(?:ITUI[ÇC][ÃA]O|\.?)[^\d]*([\d.,]+)");
        tax.TotalProductsd = Util.ExtractFromText(text, @"VALOR TOTAL DOS PRODUTOS[^\d]*([\d.,]+)");
        tax.DeliveryValue = Util.ExtractFromText(text, @"VALOR DO FRETE[^\d]*([\d.,]+)");
        tax.Totalinsurance = Util.ExtractFromText(text, @"VALOR DO SEGURO[^\d]*([\d.,]+)");
        tax.Discount = Util.ExtractFromText(text, @"DESCONTO[^\d]*([\d.,]+)");
        tax.OutherExpenses = Util.ExtractFromText(text, @"OUTRAS DESPESAS[^\d]*([\d.,]+)");
        tax.ValueIpi = Util.ExtractFromText(text, @"VALOR DO IPI[^\d]*([\d.,]+)");
        tax.ValueTotalInvoice = Util.ExtractFromText(text, @"VALOR TOTAL DA NOTA[^\d]*([\d.,]+)");
    }
}