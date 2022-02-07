using AutoMapper;
using WebApi.Entities.V1;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Repositories.V1;

namespace WebApi.Services.V1
{
    

    public class DummyService : IDummyService
    {
        private readonly IMapper _mapper;
        private readonly IDummyRepository _dummyRepository;

        public DummyService(
            IMapper mapper,
            IDummyRepository dummyRepository
            )
        {
            _mapper = mapper;
            _dummyRepository = dummyRepository;
        }

        public DummyResponse CreateDummy(CreateDummyRequest createDummyRequest)
        {
            var dummy = _mapper.Map<Dummy>(createDummyRequest);
            dummy.CreatedAt = DateTime.UtcNow;
            dummy.UpdatedAt = DateTime.UtcNow;

            _dummyRepository.InsertOne(dummy);

            var createdDummy = new Dummy
            {
                Name = dummy.Name,
                Type = dummy.Type
            };
            return _mapper.Map<DummyResponse>(createdDummy);
        }

        public IEnumerable<DummyResponse> FindAllDummies()
        {
            var dummies = _dummyRepository.FindAll().ToList();

            return _mapper.Map<IEnumerable<DummyResponse>>(dummies);
        }
    }
}
