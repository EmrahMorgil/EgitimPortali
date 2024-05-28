using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EgitimPortali.Application.Handlers.Course.Commands
{
    public class DeleteCourseCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, BaseResponse>
        {
            ICourseRepository _courseRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DeleteCourseCommandHandler(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor)
            {
                _courseRepository = courseRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var course = await _courseRepository.GetById(request.Id);
                if (course == null)
                {
                    return new BaseResponse(false, ResponseMessages.NotFound);
                }
                else
                {
                    if (course.InstructorId != tokenInfo.UserId)
                    {
                        return new BaseResponse(false, ResponseMessages.UnexpectedError);
                    }
                    else
                    {
                        return new BaseResponse(await _courseRepository.Delete(course), ResponseMessages.Success);
                    }
                }
            }
        }
    }

    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
        }
    }
}
