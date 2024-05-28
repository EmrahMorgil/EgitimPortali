using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.Course.Commands
{
    public class UpdateCourseCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public EducationType EducationType { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public double Price { get; set; }
        public int Time { get; set; }
        public List<Documentation> Documentations { get; set; } = null!;
        public string IntroductionPhoto { get; set; } = null!;
        public string IntroductionVideo { get; set; } = null!;


        public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, BaseResponse>
        {
            ICourseRepository _courseRepository;
            IDocumentationRepository _documentationRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateCourseCommandHandler(ICourseRepository courseRepository, IDocumentationRepository documentationRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _courseRepository = courseRepository;
                _documentationRepository = documentationRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
            {
                var course = await _courseRepository.GetById(request.Id);

                if (course == null)
                {
                    return new BaseResponse(false, ResponseMessages.CourseNotFound);
                }
                else
                {
                    var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                    if (course.InstructorId == tokenInfo.UserId)
                    {
                        await _documentationRepository.DeleteDocumentationsByCourseId(course.Id);
                        var introductionPhotoName = FileUploader.UploadFile(request.IntroductionPhoto);
                        var introductionVideoName = FileUploader.UploadFile(request.IntroductionVideo);
                        course.IntroductionPhoto = introductionPhotoName ?? course.IntroductionPhoto;
                        course.IntroductionVideo = introductionVideoName ?? course.IntroductionVideo;

                        foreach (var documentation in request.Documentations)
                        {
                            string fileName = "";
                            try
                            {
                                fileName = FileUploader.UploadFile(documentation.Content);
                            }
                            catch (Exception ex)
                            {
                                return new BaseResponse(false, ResponseMessages.UnexpectedError);
                            }
                            var newDocumentation = new Documentation();
                            newDocumentation.CourseId = course.Id;
                            newDocumentation.DocumentationType = documentation.DocumentationType;
                            newDocumentation.Content = fileName ?? documentation.Content;
                            var result = await _documentationRepository.Create(newDocumentation);
                            if (!result)
                            {
                                return new BaseResponse(false, ResponseMessages.UnexpectedError);
                            }
                        }
                        course.EducationType = request.EducationType;
                        course.Title = request.Title;
                        course.Description = request.Description;
                        course.Capacity = request.Capacity;
                        course.Price = request.Price;
                        course.Time = request.Time;
                        await _courseRepository.Update(course);
                        return new BaseResponse(true, ResponseMessages.Success);
                    }
                    else
                    {
                        return new BaseResponse(false, ResponseMessages.UnauthorizedEntry);
                    }

                }
            }
        }
    }
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
            RuleFor(entity => entity.EducationType).NotEmpty().NotNull();
            RuleFor(entity => entity.Title).NotEmpty().NotNull();
            RuleFor(entity => entity.Description).NotEmpty().NotNull();
            RuleFor(entity => entity.Capacity).NotEmpty().NotNull();
            RuleFor(entity => entity.Price).NotEmpty().NotNull();
            RuleFor(entity => entity.Title).NotEmpty().NotNull();
            RuleFor(entity => entity.Documentations).NotEmpty().NotNull();
            RuleFor(entity => entity.IntroductionPhoto).NotEmpty().NotNull();
            RuleFor(entity => entity.IntroductionVideo).NotEmpty().NotNull();
        }
    }
}
