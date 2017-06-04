namespace ArabicTutorials.Data.Infrastructure
{
    public interface IAppConfig
    {
        string MongoConnectionString();
        string DbName();
    }
}