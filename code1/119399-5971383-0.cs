     /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="sender"></param>
            /// <param name="cevent"></param>
            public static void StringValuetoEnum<T>(object sender, ConvertEventArgs cevent)
            {
                T type = default(T);
                if (cevent.DesiredType != type.GetType()) return;
                cevent.Value = Enum.Parse(type.GetType(), cevent.Value.ToString());
            }
    
            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="sender"></param>
            /// <param name="cevent"></param>
            public static void EnumToStringValue<T>(object sender, ConvertEventArgs cevent)
            {
              //  if (cevent.DesiredType != typeof(string)) return;
                cevent.Value = ((int)cevent.Value).ToString();
            }
