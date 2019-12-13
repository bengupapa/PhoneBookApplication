using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookService.DataAccess.Context;
using PhoneBookService.DataAccess.Repository;
using SimpleInjector;
using System.Fabric;

namespace PhoneBookService
{
    public class Startup
    {
        public StatelessServiceContext Context { get; }
        public IConfiguration Configuration { get; }
        public Container Container { get; }

        public Startup(StatelessServiceContext context)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            Context = context;
            Container = new Container();

            initializeDepndencies();
        }

        public void initializeDepndencies()
        {
            Container.Register(() => new ContactsInformationDatabaseContext(Configuration.GetConnectionString("ContactsInformationDatabase")));
            Container.Register<IRepository>(() => new Repository(Container.GetInstance<ContactsInformationDatabaseContext>()));
        }

        public PhoneBookService InitializeService()
        {
            return new PhoneBookService(Context, Container.GetInstance<IRepository>());
        }
    }
}
