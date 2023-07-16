using HyperProf.Core.Http.Results;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Core.Extensions;

public static class ControllerBaseExtensions
{
    public static ResetContentResult ResetContent(this ControllerBase controllerBase)
    {
        return new ResetContentResult();
    }
}