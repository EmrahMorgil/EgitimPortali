using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;

namespace EgitimPortali.Application.Handlers.UserCourse.Commands
{
    public class UpdateUserCourseCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public class UpdateUserCourseCommandHandler : IRequestHandler<UpdateUserCourseCommand, BaseResponse>
        {
            IUserCourseRepository _userCourseRepository;

            public UpdateUserCourseCommandHandler(IUserCourseRepository userCourseRepository)
            {
                _userCourseRepository = userCourseRepository;
            }

            public async Task<BaseResponse> Handle(UpdateUserCourseCommand request, CancellationToken cancellationToken)
            {
                var userCourse = await _userCourseRepository.GetById(request.Id);

                if (userCourse == null)
                {
                    return new BaseResponse(false, ResponseMessages.NotFound);
                }
                else
                {
                    userCourse.Status = request.CourseStatus;
                    return new BaseResponse(await _userCourseRepository.Update(userCourse), ResponseMessages.Success);
                }

            }
        }
    }
    public class UpdateUserCourseCommandValidator : AbstractValidator<UpdateUserCourseCommand>
    {
        public UpdateUserCourseCommandValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
            RuleFor(entity => entity.CourseStatus).NotEmpty().NotNull();
        }
    }
}
