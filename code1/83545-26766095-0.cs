    public abstract class BaseDbContext<TEntity> : DbContext where TEntity : class
    {
        public BaseDbContext(string connectionString)
            : base(connectionString)
        {
        }
        public override int SaveChanges()
        {
            UpdateDates();
            return base.SaveChanges();
        }
        private void UpdateDates()
        {
            foreach (var change in ChangeTracker.Entries<TEntity>())
            {
                var values = change.CurrentValues;
                foreach (var name in values.PropertyNames)
                {
                    var value = values[name];
                    if (value is DateTime)
                    {
                        var date = (DateTime)value;
                        if (date < SqlDateTime.MinValue.Value)
                        {
                            values[name] = SqlDateTime.MinValue.Value;
                        }
                        else if (date > SqlDateTime.MaxValue.Value)
                        {
                            values[name] = SqlDateTime.MaxValue.Value;
                        }
                    }
                }
            }
        }
    }
