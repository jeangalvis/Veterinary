using Microsoft.AspNetCore.Authorization;

namespace API.Helpers;

public class GlobalVerbRoleRequirement : IAuthorizationRequirement
{
    public bool IsAllowed(string role, string verb)
    {
        // allow all verbs if user is "admin"
        if (string.Equals("Administrator", role, StringComparison.OrdinalIgnoreCase)) return true;
        // allow the "GET" verb if user is "support"
        if (string.Equals("Employee", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET", verb, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        };
        // ... add other rules as you like
        return false;
    }
}

