using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirAlmatyFlights.Application.Authorization.Commands;

public class CreateUserTokenCommandHandler : IRequestHandler<CreateUserTokenCommand, Result<string>>
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    public CreateUserTokenCommandHandler()
    {
        _tokenHandler = new JwtSecurityTokenHandler();
    }
    public async Task<Result<string>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, request.Username),
                new(ClaimTypes.Role, request.RoleCode)
            }),
            Expires = DateTime.UtcNow.AddHours(request.TokenLifeExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(request.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
        });
        await Task.Yield();
        return Result.Success(_tokenHandler.WriteToken(token));
    }
}
