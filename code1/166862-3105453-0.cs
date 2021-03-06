    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(x => x.Id);
            Map(x => x.Author);
            Map(x => x.Title);
            Map(x => x.Text).CustomSqlType("CLOB");
        }
    }
