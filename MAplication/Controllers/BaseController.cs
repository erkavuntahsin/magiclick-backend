using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MAplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MAplication.Controllers
{
    public class BaseController : Controller
    {

        public BaseController()
        {
        }
        public string CacheKey
        {
            get
            {
                return "Demo-Key";
            }
        }
        public string GetLoggedUserId()
        {
              return  HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}