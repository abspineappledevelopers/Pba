using Microsoft.AspNetCore.Mvc;
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

            /*var user = new ClaimsIdentity();
            foreach (var group in groups)
            {
                user.AddClaim(new Claim("group", group.Id));
                user.AddClaim(new Claim("role", group.DisplayName));
            }

            foreach (var claim in user.Claims)
            {
                if (claim == new Claim("role", "Student administration") ||
                    claim == new Claim("role", "Interviewer"))
                {
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(claim);
                    User.AddIdentity(identity);
                }
            }*/

            /// TODO: Fix appRoles i AzureAD, 
            
            ViewData["Me"] = me;
            ViewData["Groups"] = groups;

            return View();
        }
    }
}