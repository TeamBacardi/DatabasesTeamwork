using CarsFactory.MySql.Repositories;
using Telerik.OpenAccess;
using Utils;

namespace CarsFactory.MySql
{
    public class MySqlData
    {
        private const string ConnectionString = "server=localhost;database=carsfactory;uid=root;pwd={0};";

        private readonly MySqlContext context;

        private IWritter writter;
        private IReader reader;

        public MySqlData(IReader reader)
        {
            this.reader = reader;
            var password = this.MySqlPasswordPrompt();

            this.context = new MySqlContext(string.Format(ConnectionString, password));

            this.ShopReports = new MySqlShopReportRepository(this.context);

            this.VerifyDatabase();
        }
        public MySqlData(IReader reader, IWritter writter)
        {
            this.reader = reader;
            this.writter = writter;
            var password = this.MySqlPasswordPrompt();

            this.context = new MySqlContext(string.Format(ConnectionString, password));

            this.ShopReports = new MySqlShopReportRepository(this.context);

            this.VerifyDatabase();
        }

        public IMySqlShopReportRepository ShopReports { get; private set; }

        private void VerifyDatabase()
        {
            var schemaHandler = this.context.GetSchemaHandler();
            this.EnsureDB(schemaHandler);
        }

        private void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script;

            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

        private string MySqlPasswordPrompt()
        {
            writter?.Write("Please enter your password for 'root' account: ");
            //Console.ForegroundColor = Console.BackgroundColor;
            var password = reader.ReadLine().Trim();
            //Console.ResetColor();

            return password;
        }
    }
}
