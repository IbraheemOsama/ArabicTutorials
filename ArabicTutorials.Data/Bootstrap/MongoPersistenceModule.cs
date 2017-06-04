using ArabicTutorials.Data.Infrastructure;
using Autofac;

namespace ArabicTutorials.Data.Bootstrap
{
    public class MongoPersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoConnectionFactory>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterGeneric(typeof(MongoHelper<>)).As(typeof(IMongoHelper<>));
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IRepository<>));
        }
    }
}
