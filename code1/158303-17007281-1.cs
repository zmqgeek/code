    public class ExpertDataBase : DbContext
    {
        public DbSet<Operator> Operators { get; set; }
        public ExpertDataBase()
            : base(ConnectionFactory.GetCESqlConnection(),true)
        {
            
        }
    }
    public static class ConnectionFactory
    {
       public static DbConnection GetCESqlConnection()
       {
           DbConnection connection = new SqlCeConnection()
               {
                   ConnectionString = @"Data   Source= ",
               };
           return connection;
       }
    }
