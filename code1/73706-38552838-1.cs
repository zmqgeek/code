    /// <summary>
    /// Base class of all <see cref="SecureArray{T}"/> classes.
    /// </summary>
    public class SecureArray : IDisposable
    {
        /// <summary>
        /// Cannot find a way to do a compile-time verification that the
        /// array element type is one of these so this dictionary gets
        /// used to do it at runtime.
        /// </summary>
        private static readonly Dictionary<Type, int> TypeSizes =
            new Dictionary<Type, int>
                {
                    { typeof(sbyte), sizeof(sbyte) },
                    { typeof(byte), sizeof(byte) },
                    { typeof(short), sizeof(short) },
                    { typeof(ushort), sizeof(ushort) },
                    { typeof(int), sizeof(int) },
                    { typeof(uint), sizeof(uint) },
                    { typeof(long), sizeof(long) },
                    { typeof(ulong), sizeof(ulong) },
                    { typeof(char), sizeof(char) },
                    { typeof(float), sizeof(float) },
                    { typeof(double), sizeof(double) },
                    { typeof(decimal), sizeof(decimal) },
                    { typeof(bool), sizeof(bool) }
                };
        private GCHandle handle;
        private uint byteCount;
        private bool virtualLocked;
        /// <summary>
        /// Initialize a new instance of the <see cref="SecureArray"/> class.
        /// </summary>
        /// <remarks>
        /// You cannot create a <see cref="SecureArray"/> directly, you must
        /// derive from this class like <see cref="SecureArray{T}"/> does.
        /// </remarks>
        protected SecureArray()
        {
        }
        /// <summary>
        /// Gets the size of the buffer element. Will throw a 
        /// <see cref="NotSupportedException"/> if the element type is not
        /// a built in type.
        /// </summary>
        /// <typeparam name="T">
        /// The array element type to return the size of.
        /// </typeparam>
        /// <param name="buffer">
        /// The array.
        /// </param>
        /// <returns></returns>
        public static int BuiltInTypeElementSize<T>(T[] buffer)
        {
            int elementSize;
            if (!TypeSizes.TryGetValue(typeof(T), out elementSize))
            {
                throw new NotSupportedException(
                  $"Type {typeof(T).Name} not a built in type. "
                  + $"Valid types: {string.Join(", ", TypeSizes.Keys.Select(t => t.Name))}");
            }
            return elementSize;
        }
        /// <summary>
        /// Zero the given buffer in a way that will not be optimized away.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the elements in the buffer.
        /// </typeparam>
        /// <param name="buffer">
        /// The buffer to zero.
        /// </param>
        public static void Zero<T>(T[] buffer)
            where T : struct
        {
            var bufHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr bufPtr = bufHandle.AddrOfPinnedObject();
                UIntPtr cnt = new UIntPtr(
                     (uint)buffer.Length * (uint)BuiltInTypeElementSize(buffer));
                RtlZeroMemory(bufPtr, cnt);
            }
            finally
            {
                bufHandle.Free();
            }
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            IntPtr bufPtr = this.handle.AddrOfPinnedObject();
            UIntPtr cnt = new UIntPtr(this.byteCount);
            RtlZeroMemory(bufPtr, cnt);
            if (this.virtualLocked)
            {
                VirtualUnlock(bufPtr, cnt);
            }
            this.handle.Free();
        }
        /// <summary>
        /// Call this with the array to secure and the number of bytes in that
        /// array. The buffer will be zeroed and the handle freed when the
        /// instance is disposed.
        /// </summary>
        /// <param name="buf">
        /// The array to secure.
        /// </param>
        /// <param name="sizeInBytes">
        /// The number of bytes in the buffer in the pinned object.
        /// </param>
        /// <param name="noswap">
        /// True to lock the memory so it doesn't swap.
        /// </param>
        protected void Init<T>(T[] buf, int sizeInBytes, bool noswap)
        {
            this.handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            this.byteCount = (uint)sizeInBytes;
            IntPtr bufPtr = this.handle.AddrOfPinnedObject();
            UIntPtr cnt = new UIntPtr(this.byteCount);
            if (noswap)
            {
                VirtualLock(bufPtr, cnt);
                this.virtualLocked = true;
            }
        }
        [DllImport("kernel32.dll")]
        private static extern void RtlZeroMemory(IntPtr ptr, UIntPtr cnt);
        [DllImport("kernel32.dll")]
        static extern bool VirtualLock(IntPtr lpAddress, UIntPtr dwSize);
        [DllImport("kernel32.dll")]
        static extern bool VirtualUnlock(IntPtr lpAddress, UIntPtr dwSize);
    }
