    public class LocalizedDisplayNameAttribute: DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceId) 
            : base(GetMessageFromResource(resourceId))
        { }
    
        private static string GetMessageFromResource(string resourceId)
        {
            // TODO: Return the string from the resource file
        }
    }
