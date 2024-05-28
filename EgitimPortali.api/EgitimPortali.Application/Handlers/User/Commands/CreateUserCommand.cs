using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Password;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using FluentValidation;
using MediatR;

namespace EgitimPortali.Application.Handlers.User.Commands
{
    public class CreateUserCommand : IRequest<BaseDataResponse<UserDto>>
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Role { get; set; } = null!;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseDataResponse<UserDto>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseDataResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var userList = await _userRepository.List();
                var existUser = userList.FirstOrDefault(x => x.Email == request.Email);

                if (existUser == null)
                {
                    var imageName = FileUploader.UploadFile(request.Image);
                    if (imageName == null)
                    {
                        return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.AnErrorOccurredWhileLoadingTheImage);
                    }
                    var user = _mapper.Map<Core.Entities.User>(request);
                    user.Image = imageName;
                    user.Password = Encryption.EncryptPassword(request.Password);
                    var userDto = _mapper.Map<UserDto>(user);
                    var success = await _userRepository.Create(user);
                    userDto.Token = success ? TokenService.CreateToken(user) : null!;
                    return new BaseDataResponse<UserDto>(userDto, success, ResponseMessages.Success);
                }
                else
                {
                    return new BaseDataResponse<UserDto>(null!, false, ResponseMessages.ThisEmailIsBeingUsed);
                }
            }
        }
    }
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(entity => entity.Email).NotEmpty().NotNull();
            RuleFor(entity => entity.Name).NotEmpty().NotNull();
            RuleFor(entity => entity.Password).NotEmpty().NotNull();
            RuleFor(entity => entity.Image).NotEmpty().NotNull();
            RuleFor(entity => entity.Role).NotEmpty().NotNull();
        }
    }
}
