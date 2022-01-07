using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GenricFrame.AppCode.Extensions
{
    public class ClaimsExtension
    {
        //public static string GetClaim(this ClaimsPrincipal claimsPrincipal, JwtClaim jwtClaim)
        //{
        //    var claim = claimsPrincipal.Claims.Where(c => c.Type == jwtClaim.ToString()).FirstOrDefault();

        //    if (claim == null)
        //    {
        //        throw new JwtClaimNotFoundException(jwtClaim);
        //    }

        //    return claim.Value;
        //}
    }
}
