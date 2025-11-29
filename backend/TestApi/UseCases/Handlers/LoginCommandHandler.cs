using MediatR;
using System.Linq;
using TestApi.Interfaces;
using TestApi.Results;
using TestApi.UseCases.Commands;

namespace TestApi.UseCases.Handlers
{
    public class LoginCommandHandler(IAdminAuthService authService)
        : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IAdminAuthService _authService = authService;

        public Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Спочатку викликаємо службу аутентифікації для перевірки облікових даних.
            var validationResult = _authService.Validate(request.Username, request.Password);

            if (validationResult.IsValid)
            {
                // 2. Якщо перевірка пройшла успішно (облікові дані правильні)
                // У реальному додатку тут створюється JWT-токен.
                string authToken = "generated-admin-jwt-token";
                return Task.FromResult(Result<string>.Success(authToken));
            }
            else
            {
                // 3. Якщо перевірка не пройшла (неправильний логін/пароль)
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Task.FromResult(Result<string>.Failure(errors));
            }
        }
    }
}