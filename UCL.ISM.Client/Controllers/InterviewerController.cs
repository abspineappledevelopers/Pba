using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class InterviewerController : Controller
    {
        [Authorize(Roles = "UserReaders")]
        public IActionResult Index()
        {
            return View();
        }
    }
}