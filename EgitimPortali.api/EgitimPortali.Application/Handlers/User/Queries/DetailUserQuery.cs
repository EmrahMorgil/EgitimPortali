using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.User.Queries
{
    public class DetailUserQuery : IRequest<BaseDataResponse<UserDto>>
    {
        public class DetailUserQueryHandler : IRequestHandler<DetailUserQuery, BaseDataResponse<UserDto>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public DetailUserQueryHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<UserDto>> Handle(DetailUserQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var user = await _userRepository.GetById(tokenInfo.UserId);
                if (user == null)
                {
                    return new BaseDataResponse<UserDto>(null!, true, ResponseMessages.UserNotFound);
                }
                else
                {
                    return new BaseDataResponse<UserDto>(_mapper.Map<UserDto>(user), true, ResponseMessages.Success);
                }

            }
        }
    }
}
