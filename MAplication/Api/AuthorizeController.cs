using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResultType;
using Entities.ComplexTypes;
using MAplication.Data;
using MAplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MAplication.Api
{
    [Produces("application/json")]
    [Route("api/Authorize")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AuthorizeController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody]LoginApi apiModel)
        {
            Result<string> result;
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(apiModel.EmailAddress, apiModel.Password, apiModel.IsRemember, lockoutOnFailure: false);
                if (signInResult.Succeeded)
                {
                    ApiHelpers helper = new ApiHelpers(_userManager, _signInManager);
                    result = new Result<string>(await helper.GenerateToken(apiModel.EmailAddress));
                }
                else
                {
                    result = new Result<string>(false, ResultTypeEnum.Error, "An error occured while login request. Please check the informations!");
                }
            }
            else
            {
                result = new Result<string>(false, ResultTypeEnum.Error, "An error occured while login request. complete all validations!");
            }

            return Json(result);
        }

        [HttpPost("ForgotPassword", Name = "ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordApi apiModel)
        {
            Result<string> result;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(apiModel.EmailAddress);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    result = new Result<string>(false, ResultTypeEnum.Information, "Your mail address has not been verified !");
                }
                else
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailAsync(apiModel.EmailAddress, "Reset Password",
                       $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                    result = new Result<string>("Check your mail! mail has been send");
                }
            }
            else
            {
                result = new Result<string>(false, ResultTypeEnum.Error, "An error occured while login request. complete all validations!");
            }

            return Json(result);
        }
    }
}