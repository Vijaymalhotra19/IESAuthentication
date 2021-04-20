using Basics.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }



        [SecurityLevel(5)]
        public IActionResult SecretLevel()
        {
            return View("Secret");
        }

        [SecurityLevel(10)]
        public IActionResult SecretHigherLevel()
        {
            return View("Secret");
        }

        [AllowAnonymous]
        public IActionResult Authenticate()
        {
            var grandamClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Vijay"),
                new Claim(ClaimTypes.Email, "Vijay@fmail.com"),
                new Claim(ClaimTypes.DateOfBirth, "19/08/1997"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "AdminTwo"),
                new Claim(DynamicPolicies.SecurityLevel, "7"),
                new Claim("Grandma.says","Very nyc Boy."),
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Vijay Malhotra"),
                new Claim("DrivingLicense","A+"),
            };
            var grandmaIdentity = new ClaimsIdentity(grandamClaims,"Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity , licenseIdentity });
            //------------------------------------------------------------
            HttpContext.SignInAsync(userPrincipal);
 
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DoStuff(
            [FromServices] IAuthorizationService authorizationService)
        {

           
        // we are doing stuff here
        var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build(); 

            var authresult = await authorizationService.AuthorizeAsync(User, customPolicy);
            
            if(authresult.Succeeded)
            {
                return View("Index");
            }
            
            return View("Index");
        }
    
    }
}
