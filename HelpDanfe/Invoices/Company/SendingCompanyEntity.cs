namespace HelpDanfe.Invoices.Company;

public class SendingCompanyEntity
{
	public Guid Id { get; set; }
	public string CompanyName { get; set; } = string.Empty;
	public string NatureOfOperation { get; set; } = string.Empty;
	public string AuthorizationProtocolAcess { get; set; } = string.Empty;
	public string StateRegistration { get; set; } = string.Empty;
	public string Cnpj { get; set; } = string.Empty;

	public class Builder
	{
		private readonly SendingCompanyEntity _sendingCompanyEntity = new();

		public Builder()
		{
			_sendingCompanyEntity = new SendingCompanyEntity();
		}

		public Builder SetCompanyName(string companyName)
		{
			_sendingCompanyEntity.CompanyName = companyName;
			return this;
		}

		public Builder SetNatureOfOperation(string natureOfOperation)
		{
			_sendingCompanyEntity.NatureOfOperation = natureOfOperation;
			return this;
		}

		public Builder SetAuthorizationProtocolAcess(string authorizationProtocolAcess)
		{
			_sendingCompanyEntity.AuthorizationProtocolAcess = authorizationProtocolAcess;
			return this;
		}

		public Builder SetStateRegistration(string stateRegistration)
		{
			_sendingCompanyEntity.StateRegistration = stateRegistration;
			return this;
		}

		public Builder SetCnpj(string cnpj)
		{
			_sendingCompanyEntity.Cnpj = cnpj;
			return this;
		}

		public SendingCompanyEntity Build() => _sendingCompanyEntity;
	}
}