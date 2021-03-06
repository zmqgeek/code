    // part of a default abstract setup class I use
    public ISessionFactory CreateSessionFactory()
    {
        return Fluently.Configure()
            .Database(
                MsSqlConfiguration.MsSql2008
                    .ConnectionString(c =>
                        c.Server(this.ServerName)
                        .Database(this.DatabaseName)
                        .Username(this.Username)
                        .Password(this.Password)
                        )
            )
            .Mappings(m =>
                m.AutoMappings.Add(AutoMap.AssemblyOf<User>()   // loads all POCOse
                    .Where(t => t.Namespace == this.Namespace))
                    // here go the associations and constraints,
                    // (or you can annotate them, or add them later)
                )
            .ExposeConfiguration(CreateOrUpdateSchema)
            .BuildSessionFactory();
    }
    // example of an entity
    // It _can_ be as simple as this, which generates the schema, the mappings ets
    // but you still have the flexibility to expand and to map using more complex
    // scenarios. It is not limited to just tables, you can map views, stored procedures
    // create triggers, associations, unique keys, constraints etc.
    // The Fluent docs help you step by step
    public class User
    {
        public virtual int Id { get; private set; }   // autogens PK
        public virtual string Name { get; set; }      // augogens Name col
        public virtual byte[] Picture { get; set; }   // autogens Picture BLOB col
        public virtual List<UserSettings> Settings { get; set; }  // autogens to many-to-one
    }
    
    public class UserSettings
    {
        public virtual int Id { get; private set: }   // PK again
        public virtual int UserId { get; set; }       // autogens FK
        public virtual User { get; set; }             // autogens OO-mapping to User table
    }
