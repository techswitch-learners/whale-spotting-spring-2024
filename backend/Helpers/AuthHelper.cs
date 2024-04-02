using System.Globalization;
using System.Security.Claims;

namespace WhaleSpotting.Helpers;

public static class AuthHelper
{
    public static int GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        return int.Parse(claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
    }

    public static int? GetUserIdIfLoggedIn(ClaimsPrincipal claimsPrincipal)
    {
        Console.WriteLine("*****************GetUserIdIfLoggedIn***********");
        ;
        try
        {
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>{GetUserId(claimsPrincipal)}");
            return GetUserId(claimsPrincipal);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public static string GetUserName(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.Name).Value;
    }
}
