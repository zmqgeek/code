        class SetComparer : IEqualityComparer<IEnumerable>
        {
            
            public readonly static SetComparer Default = new SetComparer();
            public bool Equals(IEnumerable x, IEnumerable y)
            {
                return Enumerable.SequenceEqual(x.Cast<object>(), y.Cast<object>());
            }
            public int GetHashCode(IEnumerable data)
            {
                int hash = 0;
                foreach (object obj in data)
                {
                    if (obj != null)
                    {
                        hash = hash * 7 + 13 * obj.GetHashCode();
                    }
                }
                return hash;
            }
        }
