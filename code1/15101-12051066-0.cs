        /// <summary>
        /// A case insenstive replace function.
        /// </summary>
        /// <param name="originalString">The string to examine.(HayStack)</param>
        /// <param name="oldValue">The value to replace.(Needle)</param>
        /// <param name="newValue">The new value to be inserted</param>
        /// <returns>A string</returns>
        public static string CaseInsenstiveReplace(string originalString, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue,
               RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(originalString, newValue);
        }
