using System.Globalization;
using System.Text.RegularExpressions;

namespace HelpDanfe.Utils;

public static partial class Utils
{
    private static readonly CultureInfo _culturePtBr = new("pt-BR");
    private static readonly CultureInfo _xmlCulture = CultureInfo.InvariantCulture;

    public static decimal ParseFromXml(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return 0m;

        if (decimal.TryParse(value, NumberStyles.Any, _xmlCulture, out decimal result))
            return result;

        return 0m;
    }

    public static decimal ExtractFromText(string text, string pattern)
    {
        var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
        return match.Success ? ParseFromText(match.Groups[1].Value) : 0m;
    }

    public static decimal ParseFromText(string textValue)
    {
        if (string.IsNullOrWhiteSpace(textValue)) return 0m;

        textValue = textValue.Trim();

        if (textValue.Contains('.') && !textValue.Contains(',') && textValue.Length - textValue.IndexOf('.') <= 3)
        {
            textValue = textValue.Replace(".", ",");
        }
        else if (textValue.Contains('.') && textValue.Contains(','))
        {
            textValue = textValue.Replace(".", "");
        }

        if (decimal.TryParse(textValue, NumberStyles.Any, _culturePtBr, out decimal result))
            return result;

        return 0m;
    }

    [GeneratedRegex(@"\s+")]
    public static partial Regex WhiteSpaceRegex();

    [GeneratedRegex(@"CHAVE DE ACESSO\s*([\d\s]+)", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex AccessKeyRegex();

    [GeneratedRegex(@"\D")]
    public static partial Regex NonDigitRegex();

    [GeneratedRegex(@"\d{44}")]
    public static partial Regex AccessKeyDigitsRegex();

    [GeneratedRegex(@"(?:PROTOCOLO DE AUTORIZA[ÇC][ÃA]O(?: DE USO)?|PROTOCOLO)\s*(\d{15})", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex AuthorizationProtocolAccessRegex();

    [GeneratedRegex(@"(\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}|\d{3}\.\d{3}\.\d{3}\-\d{2})")]
    public static partial Regex CpfCnpjPatternRegex();

    [GeneratedRegex(@"(?:NF-e\s+)?N[º°]?\s*([\d.]+)\s*S[EÉ]RIE:?\s*(\d+)", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex InvoiceNumberSeriesRegex();

    [GeneratedRegex(@"NOME\s*/\s*RAZ[AÃ]O\s*SOCIAL\s+(.+?)\s+CNPJ", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex ReceivingCompanyNameRegex();

    [GeneratedRegex(@"ENDERE[CÇ]O\s+(.+?)\s+(?:BAIRRO|MUNIC[IÍ]PIO|DATA)", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex ReceivingCompanyAddressRegex();

    [GeneratedRegex(@"FONE\s*/\s*FAX\s+([\d\(\)\-\s]{10,20})", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex ReceivingCompanyPhoneRegex();

    [GeneratedRegex(@"RECEBEMOS\s+(?:DE\s+)?(.+?)\s+OS\s+PRODUTOS", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex SendingCompanyNameRegex();

    [GeneratedRegex(@"AO\s+LADO\s+DE:\s*(.+?)(?:\s+S[EÉ]RIE|\s+N[ÚU]MERO|$)", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex SendingCompanyNameFallbackRegex();
}