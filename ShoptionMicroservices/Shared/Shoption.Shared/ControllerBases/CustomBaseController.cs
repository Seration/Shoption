using System;
using Microsoft.AspNetCore.Mvc;
using Shoption.Shared.Dto;

namespace Shoption.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
