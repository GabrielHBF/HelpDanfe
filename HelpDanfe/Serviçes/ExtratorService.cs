using HelpDanfe.Error;
using HelpDanfe.Interface.Serviçes;
using HelpDanfe.Invoices.Company;
using HelpDanfe.Invoices.Danfe;
using HelpDanfe.Invoices.Services;
using HelpDanfe.Invoices.Tax;
using HelpDanfe.Mappers;
using System.Xml.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

using PdfMapper = HelpDanfe.Mappers.DanfePdfMapper;
using Util = HelpDanfe.Utils.Utils;

namespace HelpDanfe.Serviçes;

public class ExtratorService : IExtratorService
{
    public async Task<DanfeEntity> ExtractDanfePdfAsync(Stream pdfPath, CancellationToken cancellationToken)
    {
        var danfeEntity = InitializeDanfeEntity();

        if (pdfPath.CanSeek)
        {
            pdfPath.Position = 0;
        }

        using (var file = PdfDocument.Open(pdfPath))
        {
            var page = file.GetPage(1);
            string textPage = ContentOrderTextExtractor.GetText(page);

            if (string.IsNullOrWhiteSpace(textPage))
            {
                textPage = string.Join(" ", page.GetWords().Select(p => p.Text));
            }

            textPage = textPage.Replace("\"", "");
            textPage = Util.WhiteSpaceRegex().Replace(textPage, " ");

            PdfMapper.DataMapper(textPage, danfeEntity);
            PdfMapper.TaxMapper(textPage, danfeEntity.TaxCalculation);
            PdfMapper.ProductMapper(textPage, (List<ServicesOrProducts>)danfeEntity.ServicesOrProducts);
        }

        return danfeEntity;
    }

    public async Task<DanfeEntity> ExtractDanfeXmlAsync(Stream xmlStream, CancellationToken cancellationToken)
    {
        try
        {
            if (xmlStream.CanSeek) xmlStream.Position = 0;

            var doc = await XDocument.LoadAsync(xmlStream, LoadOptions.None, cancellationToken);
            XNamespace ns = "http://www.portalfiscal.inf.br/nfe";

            var infNFe = doc.Descendants(ns + "infNFe").FirstOrDefault()
                ?? throw new Exception(FileErrors.DanfeInvalidFile.Message);

            var protNFe = doc.Descendants(ns + "protNFe").FirstOrDefault();

            var danfeEntity = InitializeDanfeEntity();

            DanfeXmlMapper.MapData(infNFe, protNFe, ns, danfeEntity);
            DanfeXmlMapper.MapTaxes(infNFe, ns, danfeEntity.TaxCalculation);
            DanfeXmlMapper.MapProducts(infNFe, ns, (List<ServicesOrProducts>)danfeEntity.ServicesOrProducts);

            return danfeEntity;
        }
        catch (Exception ex)
        {
            FileErrors.SetTechnicalMessage(ex.Message);
            throw new Exception(FileErrors.DanfeInvalidFile.Message, ex);
        }
    }

    private DanfeEntity InitializeDanfeEntity()
    {
        return new DanfeEntity
        {
            Id = Guid.NewGuid(),
            TaxCalculation = new TaxCalculationEntity { TaxId = Guid.NewGuid() },
            SendingCompany = new SendingCompanyEntity { Id = Guid.NewGuid() },
            ReceivingCompany = new RecipientCompanyEntity { Id = Guid.NewGuid() },
            ServicesOrProducts = []
        };
    }
}