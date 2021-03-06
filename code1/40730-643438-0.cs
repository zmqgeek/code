        public static class Extensions
    {
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is Not an Enum",typeof(T).FullName));
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j=Array.FindIndex(Arr, a => a.Equals(src))+1;
            return (Arr.Length==j)?Arr[0]:Arr[j];            
        }
    }
