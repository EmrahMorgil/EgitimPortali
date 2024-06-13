using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Handlers.User.Commands;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Password;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using FluentValidation;
using MediatR;

namespace EgitimPortali.Application.Handlers.User.Queries
{
    public class LoginUserQuery : IRequest<BaseDataResponse<UserDto>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, BaseDataResponse<UserDto>>
        {
            IUserRepository _userRepository;
            IMapper _mapper;

            public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseDataResponse<UserDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                var user = _userRepository.GetUserByEmail(request.Email);

                if (user != null)
                {
                    if (Encryption.VerifyPassword(request.Password, user.Password))
                    {
                        var userDto = _mapper.Map<UserDto>(user);
                        userDto.Token = TokenService.CreateToken(user);
                        return new BaseDataResponse<UserDto>(userDto, true, ResponseMessages.Success);
                    }
                    else
                    {
                        return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.InvalidCredentials);
                    }
                }
                else
                {
                    return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.InvalidCredentials);
                }
            }
        }

    }
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(entity => entity.Email).NotEmpty().NotNull();
            RuleFor(entity => entity.Password).NotEmpty().NotNull();
        }
    }
}
