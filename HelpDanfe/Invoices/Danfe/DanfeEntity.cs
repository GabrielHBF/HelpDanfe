using HelpDanfe.Invoices.Company;
using HelpDanfe.Invoices.Services;
using HelpDanfe.Invoices.Tax;

namespace HelpDanfe.Invoices.Danfe;

public class DanfeEntity
{
	public Guid Id { get; set; }
	public string Number { get; set; } = string.Empty;
	public string SerialNumber { get; set; } = string.Empty;
	public string AccessKey { get; set; } = string.Empty;
	public DateTime ImportDate { get; set; } = DateTime.Now;
	public DateTime IssueDate { get; set; } = DateTime.Now;
	public TaxCalculationEntity? TaxCalculation { get; set; }
	public SendingCompanyEntity? SendingCompany { get; set; }
	public RecipientCompanyEntity? ReceivingCompany { get; set; }
	public IEnumerable<ServicesOrProducts> ServicesOrProducts { get; set; } = [];

	public void SetNumber(string number)
	{
		Number = number;
	}
	public void SetSerialNumber(string serialNumber)
	{
		SerialNumber = serialNumber;
	}
	public void SetAccessKey(string accessKey)
	{
		AccessKey = accessKey;
	}

	public void SetIssueDate(DateTime issueDate)
	{
		IssueDate = issueDate;
	}

	public void SetSendingCompany(SendingCompanyEntity sendingCompany)
	{
		SendingCompany = sendingCompany;
	}
	public void SetReceivingCompany(RecipientCompanyEntity receivingCompany)
	{
		ReceivingCompany = receivingCompany;
	}
	public void SetServicesOrProducts(IEnumerable<ServicesOrProducts> servicesOrProducts)
	{
		ServicesOrProducts = servicesOrProducts;
	}
	public void SetTaxCalculation(TaxCalculationEntity taxCalculation)
	{
		TaxCalculation = taxCalculation;
	}
}