    public class ItemStore : Entity
    {
      [Field, Key]
      public int Id { get; set; }
      [Field]
      public Item SelectedItem { get; set; }
      [Field, Association(PairTo = "Store")]
      public EntitySet<Item> Items { get; private set; }
    }
    public class Item : Entity
    {
      [Field, Key]
      public int Id { get; set; }
      [Field]
      public ItemStore Store { get; set; }
    }
