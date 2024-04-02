using System.Security.Claims;

namespace WhaleSpotting.Helpers;

public static class AuthHelper
{
    public static int GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        Console.WriteLine(
            $"------>>>GetUserId-->ClaimsPrincipal->{claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value}"
        );
        return int.Parse(claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
    }

    public static int? GetUserIdIfLoggedIn(ClaimsPrincipal claimsPrincipal)
    {
        Console.WriteLine("*****************GetUserIdIfLoggedIn***********");
        // Console.WriteLine(
        //     $"------>>>ClaimsPrincipal-->>{claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value}"
        // );
        try
        {
            var x = GetUserId(claimsPrincipal);
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>{x}");
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
