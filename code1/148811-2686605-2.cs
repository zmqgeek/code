    public class JsonPSerializer
    {
        private string Callback { get; set; }
    
        public JsonPSerializer(string callback)
        {
            this.Callback = callback;
        }
    
        private static string GetJson<T>(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, obj);
    
                return Encoding.UTF8.GetString(stream.GetBuffer().TakeWhile( b => b != '\0')).ToArray());
            }
        }
    
        public string Serialize<T>(List<T> list) where T : IModel
        {
    
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}([", Callback);
            foreach (var obj in list)
            {
                builder.Append(GetJson(obj));
                builder.Append(",");
            }
            return builder.ToString().TrimEnd(',') + "])";
        }
    
        public string Serialize<T>(T obj) where T : IModel
        {
            string content = GetJson(obj);
            return  Callback + "(" + content + ")";
        }
    }
    
