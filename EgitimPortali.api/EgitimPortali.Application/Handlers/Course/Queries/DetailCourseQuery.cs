using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Handlers.Course.Commands;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Application.Services;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EgitimPortali.Application.Handlers.Course.Queries
{
    public class DetailCourseQuery : IRequest<BaseDataResponse<CourseDto>>
    {
        public Guid Id { get; set; }
        public class DetailCourseQueryHandler : IRequestHandler<DetailCourseQuery, BaseDataResponse<CourseDto>>
        {
            ICourseRepository _courseRepository;
            IUserRepository _userRepository;
            IDocumentationRepository _documentationRepository;
            IUserCourseRepository _userCourseRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public DetailCourseQueryHandler(ICourseRepository courseRepository, IUserRepository userRepository, IDocumentationRepository documentationRepository, IUserCourseRepository userCourseRepository
                ,IMapper mapper,
                IHttpContextAccessor httpContextAccessor)
            {
                _courseRepository = courseRepository;
                _userRepository = userRepository;
                _documentationRepository = documentationRepository;
                _userCourseRepository = userCourseRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<CourseDto>> Handle(DetailCourseQuery request, CancellationToken cancellationToken)
            {
                var tokenInfo = TokenService.DecodeToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
                var courseDto = await _courseRepository.GetCourseDetails(request.Id, tokenInfo.UserId);
                var documentations = await _documentationRepository.GetDocumentationsByCourseId(courseDto.Id);
                courseDto.Documentations = documentations.Select(x => _mapper.Map<DocumentationDto>(x)).ToList();

                if (courseDto == null)
                {
                    return new BaseDataResponse<CourseDto>(null!, false, ResponseMessages.CourseNotFound);
                }
                else
                {

                    return new BaseDataResponse<CourseDto>(courseDto, true, ResponseMessages.Success);
                }
            }
        }
    }
    public class DetailCourseQueryValidator : AbstractValidator<DetailCourseQuery>
    {
        public DetailCourseQueryValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
        }
    }
}
