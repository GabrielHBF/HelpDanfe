namespace HelpDanfe.Error;

public sealed record Error(string Code, string Message)
{
	public static Error None => new(string.Empty, string.Empty);

	private Error(Error error)
	{
		Code = error.Code;
		Message = error.Message;
	}
};