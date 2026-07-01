namespace HelpDanfe.Invoices.Services;

public class ServicesOrProducts
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string NcmSh { get; set; } = string.Empty;
    public string Cst { get; set; } = string.Empty;
    public string Cfop { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public decimal UnitValue { get; set; } = 0;
    public decimal TotalValue { get; set; } = 0;
    public decimal IcmsValue { get; set; } = 0;
    public decimal BaseCalculoIcms { get; set; } = 0;
    public decimal PorcentIcms { get; set; } = 0;
    public decimal IpiValue { get; set; } = 0;
    public decimal PorcentIpi { get; set; } = 0;
    public decimal ApproximateTaxableValue { get; set; } = 0;

    public class Builder
    {
        private readonly ServicesOrProducts _servicesOrProducts = new();

        public Builder()
        {
            _servicesOrProducts = new ServicesOrProducts();
        }

        public Builder SetCode(string code)
        {
            _servicesOrProducts.Code = code;
            return this;
        }

        public Builder SetDescription(string description)
        {
            _servicesOrProducts.Description = description;
            return this;
        }

        public Builder SetNcmSh(string ncmSh)
        {
            _servicesOrProducts.NcmSh = ncmSh;
            return this;
        }

        public Builder SetCst(string cst)
        {
            _servicesOrProducts.Cst = cst;
            return this;
        }

        public Builder SetCfop(string cfop)
        {
            _servicesOrProducts.Cfop = cfop;
            return this;
        }

        public Builder SetQuantity(int quantity)
        {
            _servicesOrProducts.Quantity = quantity;
            return this;
        }

        public Builder SetUnitValue(decimal unitValue)
        {
            _servicesOrProducts.UnitValue = unitValue;
            return this;
        }

        public Builder SetTotalValue(decimal totalValue)
        {
            _servicesOrProducts.TotalValue = totalValue;
            return this;
        }

        public Builder SetIcmsValue(decimal icmsValue)
        {
            _servicesOrProducts.IcmsValue = icmsValue;
            return this;
        }

        public Builder SetBaseCalculoIcms(decimal baseCalculoIcms)
        {
            _servicesOrProducts.BaseCalculoIcms = baseCalculoIcms;
            return this;
        }

        public Builder SetPorcentIcms(decimal porcentIcms)
        {
            _servicesOrProducts.PorcentIcms = porcentIcms;
            return this;
        }

        public Builder SetIpiValue(decimal ipiValue)
        {
            _servicesOrProducts.IpiValue = ipiValue;
            return this;
        }

        public Builder SetPorcentIpi(decimal porcentIpi)
        {
            _servicesOrProducts.PorcentIpi = porcentIpi;
            return this;
        }

        public Builder SetApproximateTaxableValue(decimal approximateTaxableValue)
        {
            _servicesOrProducts.ApproximateTaxableValue = approximateTaxableValue;
            return this;
        }

        public ServicesOrProducts Build() => _servicesOrProducts;
    }
}