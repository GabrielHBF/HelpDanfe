using HelpDanfe.Invoices.Danfe;

namespace HelpDanfe.Interface.Serviçes;

public interface IExtratorService
{
	Task<DanfeEntity> ExtractDanfePdfAsync(Stream pdfPath, CancellationToken cancellationToken);
	Task<DanfeEntity> ExtractDanfeXmlAsync(Stream xmlPath, CancellationToken cancellationToken);
}