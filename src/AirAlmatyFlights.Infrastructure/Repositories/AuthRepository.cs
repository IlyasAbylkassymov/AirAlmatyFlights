using AirAlmatyFlights.Application.Common.Errors;
using AirAlmatyFlights.Application.Interfaces.Repositories;
using AirAlmatyFlights.Infrastructure.Common.Exceptions;
using AirAlmatyFlights.Infrastructure.Persistence;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirAlmatyFlights.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _dataContext;
    private readonly ILogger<AuthRepository> _logger;

    public AuthRepository(DataContext dataContext, ILogger<AuthRepository> logger)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<string>> GetRoleCodeByCheckUser(string userName, string password)
    {
        string? roleCode;
        try
        {
            roleCode = await _dataContext.Users
                .Where(u => u.UserName == userName && u.Password == password)
                .Join(_dataContext.Roles,
                    user => user.RoleId,
                    role => role.Id,
                    (user, role) => role.Code)
                .FirstOrDefaultAsync();
            if (roleCode == string.Empty)
            {
                _logger.LogError($"Couldn't find data by request");
                return Result.Failure<string>(DomainError.NotFound);
            }
        }
        catch (DatabaseException ex)
        {
            _logger.LogError($"Internal database error: {ex.Message}");
            return Result.Failure<string>(DomainError.DatabaseFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Database request error: {ex.Message}");
            return Result.Failure<string>(DomainError.DatabaseFailed);
        }

        return Result.Success(roleCode!);
    }
}
