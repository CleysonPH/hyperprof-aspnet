using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Core.Http.Results;

public class ResetContentResult : StatusCodeResult
{
    private const int DefaultStatusCode = StatusCodes.Status205ResetContent;

    public ResetContentResult() : base(DefaultStatusCode)
    { }
}