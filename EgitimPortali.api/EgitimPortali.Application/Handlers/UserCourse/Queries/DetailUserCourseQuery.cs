using AutoMapper;
using EgitimPortali.Application.Consts;
using EgitimPortali.Application.Dtos;
using EgitimPortali.Application.Interfaces;
using EgitimPortali.Application.Response;
using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using FluentValidation;
using MediatR;

namespace EgitimPortali.Application.Handlers.UserCourse.Queries
{
    public class DetailUserCourseQuery : IRequest<BaseDataResponse<CourseDto>>
    {
        public Guid Id { get; set; }
        public class DetailUserCourseQueryHandler : IRequestHandler<DetailUserCourseQuery, BaseDataResponse<CourseDto>>
        {
            IUserRepository _userRepository;
            ICourseRepository _courseRepository;
            IDocumentationRepository _documentationRepository;
            IUserCourseRepository _userCourseRepository;
            private readonly IMapper _mapper;

            public DetailUserCourseQueryHandler(IUserRepository userRepository, ICourseRepository courseRepository, IDocumentationRepository documentationRepository, IUserCourseRepository userCourseRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _courseRepository = courseRepository;
                _documentationRepository = documentationRepository;
                _userCourseRepository = userCourseRepository;
                _mapper = mapper;
            }

            public async Task<BaseDataResponse<CourseDto>> Handle(DetailUserCourseQuery request, CancellationToken cancellationToken)
            {

                var userCourseDetails = await _userCourseRepository.GetUserCourseDetails(request.Id);
                var documentations = await _documentationRepository.GetDocumentationsByCourseId(userCourseDetails.Id);

                foreach (var documentation in documentations)
                {
                    userCourseDetails.Documentations.Add(_mapper.Map<DocumentationDto>(documentation));
                }

                return new BaseDataResponse<CourseDto>(userCourseDetails, true, ResponseMessages.Success);
            }
        }
    }
    public class DetailUserCourseQueryValidator : AbstractValidator<DetailUserCourseQuery>
    {
        public DetailUserCourseQueryValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
        }
    }
}
