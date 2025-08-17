using AirAlmatyFlights.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Authorization.Queries;

public class GetRoleCodeByCredentialsQueryHandler : IRequestHandler<GetRoleCodeByCredentialsQuery, Result<string>>
{
    private readonly IAuthRepository _authRepository;

    public GetRoleCodeByCredentialsQueryHandler(IAuthRepository authRepository) => _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));

    public async Task<Result<string>> Handle(GetRoleCodeByCredentialsQuery request, CancellationToken cancellationToken) => await _authRepository.GetRoleCodeByCheckUser(request.Username, request.Password);
}
