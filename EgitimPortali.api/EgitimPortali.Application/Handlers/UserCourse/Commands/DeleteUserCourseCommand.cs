using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.UserCourse.Commands
{
    public class DeleteUserCourseCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public class DeleteUserCourseCommandHandler : IRequestHandler<DeleteUserCourseCommand, BaseResponse>
        {
            IUserCourseRepository _userCourseRepository;


            public DeleteUserCourseCommandHandler(IUserCourseRepository userCourseRepository)
            {
                _userCourseRepository = userCourseRepository;
            }

            public async Task<BaseResponse> Handle(DeleteUserCourseCommand request, CancellationToken cancellationToken)
            {
                var userCourse = await _userCourseRepository.GetById(request.Id);

                if (userCourse == null)
                {
                    return new BaseResponse(false, ResponseMessages.NotFound);
                }
                else
                {
                    return new BaseResponse(await _userCourseRepository.Delete(userCourse), ResponseMessages.Success);
                }

            }
        }
    }
    public class DeleteUserCourseCommandValidator : AbstractValidator<DeleteUserCourseCommand>
    {
        public DeleteUserCourseCommandValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
        }
    }
}
