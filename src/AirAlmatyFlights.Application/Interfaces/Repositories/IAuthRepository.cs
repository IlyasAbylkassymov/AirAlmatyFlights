using KDS.Primitives.FluentResult;

namespace AirAlmatyFlights.Application.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<Result<string>> GetRoleCodeByCheckUser(string userName, string password);
}
