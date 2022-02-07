using MongoDB.Driver;
using WebApi.Datas.DbConfigs;
using WebApi.Entities.V1;

namespace WebApi.Repositories.V1
{
    public class DummyRepository : GenericRepository<Dummy>, IDummyRepository
    {
        private readonly IMongoCollection<Dummy> _dummies;

        public DummyRepository(IDatabaseConfigurations settings) : base(settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _dummies = database.GetCollection<Dummy>(settings.DummiesCollectionName);
        }

    }
}
