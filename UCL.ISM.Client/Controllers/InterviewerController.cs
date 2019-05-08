using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class InterviewerController : Controller
    {
        [Authorize(Roles = "be407188-013b-43da-8c34-444f6a944b0f")]
        public IActionResult Index()
        {
            return View();
        }
    }
}