using System.Security.Claims;

public static class AuthHelper
{
    public static int GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        return int.Parse(claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
    }

    public static string GetUserName(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.Single(claim => claim.Type == ClaimTypes.Name).Value;
    }
}
