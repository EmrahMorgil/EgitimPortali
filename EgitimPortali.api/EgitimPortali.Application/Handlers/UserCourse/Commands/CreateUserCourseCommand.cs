using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Handlers.Course.Commands;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Handlers.UserCourse.Commands
{
    public class CreateUserCourseCommand : IRequest<BaseResponse>
    {
        public Guid CourseId { get; set; }
        public class CreateUserCourseCommandHandler : IRequestHandler<CreateUserCourseCommand, BaseResponse>
        {
            IUserCourseRepository _userCourseRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateUserCourseCommandHandler(IUserCourseRepository userCourseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _userCourseRepository = userCourseRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseResponse> Handle(CreateUserCourseCommand request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var userCourse = _mapper.Map<Core.Entities.UserCourse>(request);
                userCourse.UserId = tokenInfo.UserId;
                userCourse.Status = CourseStatus.Pending;
                return new BaseResponse(await _userCourseRepository.Create(userCourse), ResponseMessages.Success);
            }
        }
    }
    public class CreateUserCourseCommandValidator : AbstractValidator<CreateUserCourseCommand>
    {
        public CreateUserCourseCommandValidator()
        {
            RuleFor(entity => entity.CourseId).NotEmpty().NotNull();
        }
    }
}
