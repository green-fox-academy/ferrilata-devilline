using System;
using Microsoft.IdentityModel.Tokens;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string UserEmail);
    }


}
