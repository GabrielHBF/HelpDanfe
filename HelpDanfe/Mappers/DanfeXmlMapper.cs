using HelpDanfe.Invoices.Danfe;
using HelpDanfe.Invoices.Services;
using HelpDanfe.Invoices.Tax;
using System.Xml.Linq;

using Util = HelpDanfe.Utils.Utils;

namespace HelpDanfe.Mappers;

public static class DanfeXmlMapper
{
	public static void MapData(XElement infNFe, XElement? protNFe, XNamespace ns, DanfeEntity danfe)
	{
		var ide = infNFe.Element(ns + "ide");
		if (ide != null)
		{
			danfe.Number = ide.Element(ns + "nNF")?.Value ?? "Undefined";
			danfe.SerialNumber = ide.Element(ns + "serie")?.Value ?? "Undefined";

			if (DateTime.TryParse(ide.Element(ns + "dhEmi")?.Value, out DateTime issueDate))
				danfe.IssueDate = issueDate;
		}

		var infProt = protNFe?.Element(ns + "infProt");
		danfe.AccessKey = infProt?.Element(ns + "chNFe")?.Value ?? infNFe.Attribute("Id")?.Value.Replace("NFe", "") ?? "Undefined";
		danfe.SendingCompany!.AuthorizationProtocolAcess = infProt?.Element(ns + "nProt")?.Value ?? "Undefined";

		var seding = infNFe.Element(ns + "emit");
		if (seding != null)
		{
			danfe.SendingCompany.Cnpj = seding.Element(ns + "CNPJ")?.Value ?? "Undefined";
			danfe.SendingCompany.CompanyName = seding.Element(ns + "xNome")?.Value ?? "Undefined";
			danfe.SendingCompany.StateRegistration = seding.Element(ns + "IE")?.Value ?? "Undefined";
		}

		var receiving = infNFe.Element(ns + "dest");
		if (receiving != null)
		{
			danfe.ReceivingCompany!.Cnpj = receiving.Element(ns + "CNPJ")?.Value ?? "Undefined";
			danfe.ReceivingCompany.Cpf = receiving.Element(ns + "CPF")?.Value ?? "Undefined";
			danfe.ReceivingCompany.CompanyName = receiving.Element(ns + "xNome")?.Value ?? "Undefined";
			danfe.ReceivingCompany.Email = receiving.Element(ns + "email")?.Value ?? "Undefined";

			var enderDest = receiving.Element(ns + "enderDest");
			if (enderDest != null)
			{
				danfe.ReceivingCompany.Address = $"{enderDest.Element(ns + "xLgr")?.Value}, {enderDest.Element(ns + "nro")?.Value} - {enderDest.Element(ns + "xBairro")?.Value}";
				danfe.ReceivingCompany.PhoneNumber = enderDest.Element(ns + "fone")?.Value ?? "Undefined";
			}
		}
	}

	public static void MapTaxes(XElement infNFe, XNamespace ns, TaxCalculationEntity? tax)
	{
		if (tax is null) return;

		var icmsTot = infNFe.Element(ns + "total")?.Element(ns + "ICMSTot");
		if (icmsTot != null)
		{
			tax.BaseCalculeteIcms = Util.ParseFromXml(icmsTot.Element(ns + "vBC")?.Value);
			tax.ValueIcms = Util.ParseFromXml(icmsTot.Element(ns + "vICMS")?.Value);
			tax.BaseCalculeteIcmsSt = Util.ParseFromXml(icmsTot.Element(ns + "vBCST")?.Value);
			tax.ValueIcmsSt = Util.ParseFromXml(icmsTot.Element(ns + "vST")?.Value);
			tax.TotalProductsd = Util.ParseFromXml(icmsTot.Element(ns + "vProd")?.Value);
			tax.DeliveryValue = Util.ParseFromXml(icmsTot.Element(ns + "vFrete")?.Value);
			tax.Totalinsurance = Util.ParseFromXml(icmsTot.Element(ns + "vSeg")?.Value);
			tax.Discount = Util.ParseFromXml(icmsTot.Element(ns + "vDesc")?.Value);
			tax.OutherExpenses = Util.ParseFromXml(icmsTot.Element(ns + "vOutro")?.Value);
			tax.ValueIpi = Util.ParseFromXml(icmsTot.Element(ns + "vIPI")?.Value);
			tax.ValueTotalInvoice = Util.ParseFromXml(icmsTot.Element(ns + "vNF")?.Value);
		}
	}

	public static void MapProducts(XElement infNFe, XNamespace ns, List<ServicesOrProducts> products)
	{
		var details = infNFe.Elements(ns + "det");

		foreach (var det in details)
		{
			var prod = det.Element(ns + "prod");

			if (prod != null)
			{
				products.Add(new ServicesOrProducts
				{
					Id = Guid.NewGuid(),
					Code = prod.Element(ns + "cProd")?.Value ?? "Undefined",
					Description = prod.Element(ns + "xProd")?.Value ?? "Undefined",
					Quantity = (int) Math.Round(Util.ParseFromXml(prod.Element(ns + "qCom")?.Value)),
					UnitValue = Util.ParseFromXml(prod.Element(ns + "vUnCom")?.Value),
					TotalValue = Util.ParseFromXml(prod.Element(ns + "vProd")?.Value)
				});
			}
		}
	}
}