        public List<MyError> GroupErrorsByErrorCode(List<MyError> errors)
        {
            var result = errors.GroupBy(e => e.ErrorCode).Select(group => new MyError
                {
                    ErrorCode = group.Key,
                    // EDIT: was 
                    // Errors = group.SelectMany(g => g.Errors)
                    Errors = new ArrayList(group
                               .SelectMany(g => g.Errors.Cast<MyEntryError>())
                               .ToList())
                });
            return result;
        }
