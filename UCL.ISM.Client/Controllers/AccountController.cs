using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Web.Client;
using System.Security.Claims;

namespace UCL.ISM.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenAcquisition tokenAcquisition;
        private readonly Service _service;

        public AccountController(ITokenAcquisition tokenAcquisition, Service service)
        {
            this.tokenAcquisition = tokenAcquisition;
            this._service = service;
        }

        [MsalUiRequiredExceptionFilter(Scopes = new[] { "User.Read", "Directory.Read.All" })]
        public async Task<IActionResult> Index()
        {
            string accessToken = await tokenAcquisition.GetAccessTokenOnBehalfOfUser(HttpContext, new[] { "User.Read", "Directory.Read.All" });

            User me = await _service.GetMeAsync(accessToken);
            IList<Group> groups = await _service.GetMyGroupsAsync(accessToken);
            
            ViewData["Me"] = me;
            ViewData["Groups"] = groups;

            return View();
        }
    }
}