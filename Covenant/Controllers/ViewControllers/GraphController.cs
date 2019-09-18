﻿// Author: Ryan Cobb (@cobbr_io)
// Project: Covenant (https://github.com/cobbr/Covenant)
// License: GNU GPLv3

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Covenant.Models;
using Covenant.Models.Covenant;
using Covenant.Models.Grunts;

namespace Covenant.Controllers
{
    [Authorize]
    public class GraphController : Controller
    {
        private readonly CovenantContext _context;
        private readonly UserManager<CovenantUser> _userManager;

        public GraphController(CovenantContext context, UserManager<CovenantUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize, HttpGet, Route("Graph"), Route("Graph/Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Listeners = await _context.GetHttpListeners();
            return View((await _context.GetGrunts()).Where(G => G.Status != GruntStatus.Uninitialized));
        }
    }
}
