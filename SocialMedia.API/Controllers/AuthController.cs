using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Attributes;

namespace SocialMedia.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            returnUrl = "http://localhost:5173/";
        }
        
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/auth/callback", Items =
            {
                new KeyValuePair<string, string?>(".redirect-uri", returnUrl)
            }
        };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var a = User.Identity;
        var returnUrl = HttpContext.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties?.Items
            .FirstOrDefault(p => p.Key == ".redirect-uri").Value;
        return Redirect(returnUrl ?? "http://localhost:5173/");
    }

    [RequireAuthenticated]
    [HttpGet("logout")]
    public async Task<IActionResult> Logout([FromQuery] string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            returnUrl = "http://localhost:5173/";
        }
        
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect(returnUrl);
    }
}