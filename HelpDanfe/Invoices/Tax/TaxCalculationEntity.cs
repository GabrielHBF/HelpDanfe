namespace HelpDanfe.Invoices.Tax;

public class TaxCalculationEntity
{
    public Guid TaxId { get; set; }
    public decimal BaseCalculeteIcms { get; set; }
    public decimal ValueIcms { get; set; } = 0;
    public decimal BaseCalculeteIcmsSt { get; set; }
    public decimal ValueIcmsSt { get; set; } = 0;
    public decimal TotalProductsd { get; set; } = 0;
    public decimal DeliveryValue { get; set; }
    public decimal Totalinsurance { get; set; } = 0;
    public decimal Discount { get; set; } = 0;
    public decimal OutherExpenses { get; set; } = 0;
    public decimal ValueIpi { get; set; } = 0;
    public decimal ValueTotalInvoice { get; set; } = 0;

    public class Builder
    {
        private readonly TaxCalculationEntity _taxCalculationEntity = new();
        public Builder()
        {
            _taxCalculationEntity = new TaxCalculationEntity();
        }
        public Builder SetTaxId(Guid taxId)
        {
            _taxCalculationEntity.TaxId = taxId;
            return this;
        }
        public Builder SetBaseCalculeteIcms(decimal baseCalculeteIcms)
        {
            _taxCalculationEntity.BaseCalculeteIcms = baseCalculeteIcms;
            return this;
        }
        public Builder SetValueIcms(decimal valueIcms)
        {
            _taxCalculationEntity.ValueIcms = valueIcms;
            return this;
        }
        public Builder SetBaseCalculeteIcmsSt(decimal baseCalculeteIcmsSt)
        {
            _taxCalculationEntity.BaseCalculeteIcmsSt = baseCalculeteIcmsSt;
            return this;
        }
        public Builder SetValueIcmsSt(decimal valueIcmsSt)
        {
            _taxCalculationEntity.ValueIcmsSt = valueIcmsSt;
            return this;
        }

        public Builder SetTotalProductsd(decimal totalProductsd)
        {
            _taxCalculationEntity.TotalProductsd = totalProductsd;
            return this;
        }

        public Builder SetDeliveryValue(decimal deliveryValue)
        {
            _taxCalculationEntity.DeliveryValue = deliveryValue;
            return this;
        }

        public Builder SetTotalinsurance(decimal totalinsurance)
        {
            _taxCalculationEntity.Totalinsurance = totalinsurance;
            return this;
        }

        public Builder SetDiscount(decimal discount)
        {
            _taxCalculationEntity.Discount = discount;
            return this;
        }

        public Builder SetOutherExpenses(decimal outherExpenses)
        {
            _taxCalculationEntity.OutherExpenses = outherExpenses;
            return this;
        }

        public Builder SetValueIpi(decimal valueIpi)
        {
            _taxCalculationEntity.ValueIpi = valueIpi;
            return this;
        }

        public Builder SetValueTotalInvoice(decimal valueTotalInvoice)
        {
            _taxCalculationEntity.ValueTotalInvoice = valueTotalInvoice;
            return this;
        }

        public TaxCalculationEntity Build() => _taxCalculationEntity;
    };
}