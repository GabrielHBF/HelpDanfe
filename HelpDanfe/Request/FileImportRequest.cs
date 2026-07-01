using Microsoft.AspNetCore.Http;

namespace HelpDanfe.Request;

public record FileImportRequest(IFormFile FormFile, string FileName);