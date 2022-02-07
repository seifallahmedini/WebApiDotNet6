namespace WebApi.Datas.DbConfigs
{
    public interface IDatabaseConfigurations
    {
        string UsersCollectionName { get; set; }
        string DummiesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
