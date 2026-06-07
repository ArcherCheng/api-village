using System;
using System.Security.Claims;

namespace Api.Helpers;

public static class ClaimsPrincipalExtensions
{
    // public static string GetUserName(this ClaimsPrincipal principal)
    // {
    //     return principal.FindFirst(ClaimTypes.Name)?.Value;
    // }

    // public static string GetUserId(this ClaimsPrincipal principal)
    // {
    //     return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    // }

    // private static T GetUserId<T>(this ClaimsPrincipal principal)
    // {
    //     if (principal == null)
    //         throw new ArgumentNullException(nameof(principal));
        
    //     var loginUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
    //     if (typeof(T) == typeof(Guid)) 
    //     {
    //         return (T)Convert.ChangeType(loginUserId, typeof(T))!;
    //     }
    //     else if (typeof(T) == typeof(string)) 
    //     {
    //         return (T)Convert.ChangeType(loginUserId, typeof(T))!;
    //     }
    //     else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
    //     {
    //         return loginUserId != null ? (T)Convert.ChangeType(loginUserId,typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
    //     }
    //     else
    //     {
    //         throw new Exception("Invalid type provided");
    //     }
    // }

    // private static string GetUserName(this ClaimsPrincipal principal)
    // {
    //     if (principal == null)
    //         throw new ArgumentNullException(nameof(principal));
        
    //     var name = principal.FindFirstValue(ClaimTypes.Name)!;
    //     return name;
    // }
    
    // private static string GetUserRole(this ClaimsPrincipal principal)
    // {
    //     if (principal == null)
    //         throw new ArgumentNullException(nameof(principal));
        
    //     var role = principal.FindFirstValue(ClaimTypes.Role)!;
    //     return role;
    // }

}

