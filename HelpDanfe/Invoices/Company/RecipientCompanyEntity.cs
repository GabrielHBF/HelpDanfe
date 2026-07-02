namespace HelpDanfe.Invoices.Company;

public class RecipientCompanyEntity
{
	public Guid Id { get; set; }
	public string CompanyName { get; set; } = string.Empty;
	public string Cpf { get; set; } = string.Empty;
	public string Cnpj { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string StateRegistration { get; set; } = string.Empty;

	public class Builder
	{
		private readonly RecipientCompanyEntity _recipientCompanyEntity = new();
		public Builder()
		{
			_recipientCompanyEntity = new RecipientCompanyEntity();
		}
		public Builder SetCompanyName(string companyName)
		{
			_recipientCompanyEntity.CompanyName = companyName;
			return this;
		}
		public Builder SetCpf(string cpf)
		{
			_recipientCompanyEntity.Cpf = cpf;
			return this;
		}
		public Builder SetCnpj(string cnpj)
		{
			_recipientCompanyEntity.Cnpj = cnpj;
			return this;
		}
		public Builder SetAddress(string address)
		{
			_recipientCompanyEntity.Address = address;
			return this;
		}
		public Builder SetPhoneNumber(string phoneNumber)
		{
			_recipientCompanyEntity.PhoneNumber = phoneNumber;
			return this;
		}
		public Builder SetEmail(string email)
		{
			_recipientCompanyEntity.Email = email;
			return this;
		}
		public Builder SetStateRegistration(string stateRegistration)
		{
			_recipientCompanyEntity.StateRegistration = stateRegistration;
			return this;
		}
		public RecipientCompanyEntity Build() => _recipientCompanyEntity;
	}
}