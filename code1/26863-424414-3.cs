        public static explicit operator AuthenticationMethod(string str)
        {
            AuthenticationMethod result;
            if (instance.TryGet(str, out result))
                return result
            else
                throw new InvalidCastException();
        }
