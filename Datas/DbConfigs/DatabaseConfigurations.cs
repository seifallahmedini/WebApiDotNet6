namespace WebApi.Datas.DbConfigs
{
    public class DatabaseConfigurations : IDatabaseConfigurations
    {
        public string UsersCollectionName { get; set; }
        public string DummiesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
