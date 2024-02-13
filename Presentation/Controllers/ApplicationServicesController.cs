﻿using Application.Consts.Authorize;
using Application.CustomAttributes;
using Application.Enums;
using Application.Services.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly private IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.ApplicationServices, ActionType = ActionType.ReadingAll, Definition = "Get All Application Services")]

        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            return Ok(_applicationService.GetAuthorizeDefinitionEndPoints(typeof(Program)));
        }
    }
}
