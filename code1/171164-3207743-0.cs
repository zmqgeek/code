    /// <summary>
    /// Provides support for lazy initialization.
    /// </summary>
    /// <typeparam name="T">Specifies the type of object that is being lazily initialized.</typeparam>
    public sealed class Lazy<T>
    {
        private readonly Func<T> createValue;
        private volatile bool isValueCreated;
        private T value;
     
     
        /// <summary>
        /// Gets the lazily initialized value of the current Lazy{T} instance.
        /// </summary>
        public T Value
        {
            get
            {
                if (!isValueCreated)
                {
                    lock (this)
                    {
                        if (!isValueCreated)
                        {
                            value = createValue();
                            isValueCreated = true;
                        }
                    }
                }
                return value;
            }
        }      
     
        /// <summary>
        /// Gets a value that indicates whether a value has been created for this Lazy{T} instance.
        /// </summary>
        public bool IsValueCreated
        {
            get
            {
                lock (this)
                {
                    return isValueCreated;
                }
            }
        }
     
     
        /// <summary>
        /// Initializes a new instance of the Lazy{T} class.
        /// </summary>
        /// <param name="createValue">The delegate that produces the value when it is needed.</param>
        public Lazy(Func<T> createValue)
        {
            if (createValue == null) throw new ArgumentNullException("createValue");
     
            this.createValue = createValue;
        }
     
     
        /// <summary>
        /// Creates and returns a string representation of the Lazy{T}.Value.
        /// </summary>
        /// <returns>The string representation of the Lazy{T}.Value property.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
