    public class MultiValueDictionary<TKey, TElement>
    : Collection<TElement>, ILookup<TKey, TElement>
    {
      public MultiValueDictionary(Func<TElement, TKey> keyForItem)
        : base(new Collection(keyForItem))
      {
      }
      new Collection Items => (Collection)base.Items;
      public IEnumerable<TElement> this[TKey key] => Items[key];
      public bool Contains(TKey key) => Items.Contains(key);
      IEnumerator<IGrouping<TKey, TElement>>
        IEnumerable<IGrouping<TKey, TElement>>.GetEnumerator() => Items.GetEnumerator();
      class Collection
      : KeyedCollection<TKey, Grouping>, IEnumerable<TElement>, IList<TElement>
      {
        Func<TElement, TKey> KeyForItem { get; }
        public Collection(Func<TElement, TKey> keyForItem) => KeyForItem = keyForItem;
        protected override TKey GetKeyForItem(Grouping item) => item.Key;
        public void Add(TElement item)
        {
          var key = KeyForItem(item);
          if (Dictionary != null && Dictionary.TryGetValue(key, out var collection))
            collection.Add(item);
          else
            Add(new Grouping(key) { item });
        }
        public bool Remove(TElement item)
        {
          var key = KeyForItem(item);
          if (Dictionary != null && Dictionary.TryGetValue(key, out var collection)
            && collection.Remove(item))
          {
            if (collection.Count == 0)
              Remove(key);
            return true;
          }
          return false;
        }
        IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
        {
          foreach (var group in base.Items)
            foreach (var item in group)
              yield return item;
        }
        const string IndexError = "Indexing not supported.";
        public int IndexOf(TElement item) => throw new NotSupportedException(IndexError);
        public void Insert(int index, TElement item) => Add(item);
        public bool Contains(TElement item) => Items.Contains(item);
        public void CopyTo(TElement[] array, int arrayIndex) =>
        throw new NotSupportedException(IndexError);
        new IEnumerable<TElement> Items => this;
        public bool IsReadOnly => false;
        TElement IList<TElement>.this[int index]
        {
          get => throw new NotSupportedException(IndexError);
          set => throw new NotSupportedException(IndexError);
        }
      }
      class Grouping : Collection<TElement>, IGrouping<TKey, TElement>
      {
        public Grouping(TKey key) => Key = key;
        public TKey Key { get; }
      }
    }