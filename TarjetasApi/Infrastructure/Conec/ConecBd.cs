namespace TarjetasApi.Infrastructure.Conec
{
    public class ConecBd
    {
        private string connectionString = string.Empty;
        public ConecBd()
        {
            var constructor = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connectionString = constructor.GetSection("ConnectionStrings:Connec").Value;
        }

        public string cadenaConn()
        {
            return connectionString;
        }

    }
}
