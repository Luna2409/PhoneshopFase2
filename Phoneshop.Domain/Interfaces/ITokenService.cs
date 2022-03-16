using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Phoneshop.Domain.Interfaces
{
    public interface ITokenService
    {
        String Generate(List<Claim> claims);
    }
}
