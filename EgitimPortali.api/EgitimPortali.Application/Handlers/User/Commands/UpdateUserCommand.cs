using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Password;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.User.Commands
{
    public class UpdateUserCommand : IRequest<BaseDataResponse<UserDto>>
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string? NewPassword { get; set; }
        public string? NewPasswordVerify { get; set; }
        public string Image { get; set; } = null!;

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseDataResponse<UserDto>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var user = await _userRepository.GetById(tokenInfo.UserId);
                var oldPasswordControl = Encryption.VerifyPassword(request.OldPassword, user.Password);

                if (user != null)
                {
                    if (oldPasswordControl)
                    {
                        string password;

                        if (String.IsNullOrEmpty(request.NewPassword))
                        {
                            password = request.OldPassword;
                        }
                        else
                        {
                            password = request.NewPassword;
                        }

                        if (!String.IsNullOrEmpty(request.NewPassword) && !String.IsNullOrEmpty(request.NewPasswordVerify))
                        {
                            if (request.NewPassword != request.NewPasswordVerify)
                                return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.NewPasswordsDoNotMatch);
                            if (request.NewPassword == request.OldPassword)
                                return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.NewPasswordCannotBeTheSameAsOldPassword);
                        }
                        var imageName = FileUploader.UploadFile(request.Image);
                        user.Email = request.Email;
                        user.Name = request.Name;
                        user.Password = Encryption.EncryptPassword(password);
                        user.Image = imageName != null ? imageName : user.Image;
                        var success = await _userRepository.Update(user);
                        var userDto = _mapper.Map<UserDto>(user);
                        userDto.Token = success ? TokenService.CreateToken(user) : null!;
                        return new BaseDataResponse<UserDto>(userDto, success, ResponseMessages.Success);
                    }
                    else
                    {
                        return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.IncorrectOldPasswordEntry);
                    }
                }
                else
                {
                    return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.NotFound);
                }

            }
        }
    }
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(entity => entity.Email).NotEmpty().NotNull();
            RuleFor(entity => entity.Name).NotEmpty().NotNull();
            RuleFor(entity => entity.OldPassword).NotEmpty().NotNull();
            RuleFor(entity => entity.Image).NotEmpty().NotNull();
        }
    }
}
