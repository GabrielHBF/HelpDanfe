namespace HelpDanfe.Error;

public static class FileErrors
{
	public static string TechnicalMessage { get; private set; } = string.Empty;
	public static Error DanfeInvalidFile =>
	new("InvalidFile", $"The provided DANFE file is invalid or could not be processed. {TechnicalMessage}");

	public static void SetTechnicalMessage(string technicalMessage)
	{
		TechnicalMessage = technicalMessage;
	}
}