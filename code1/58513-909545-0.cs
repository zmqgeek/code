        static string GetNextFilename(string pattern) {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern) {
                throw new ArgumentException(
                     "The pattern must include an index place-holder", "pattern");
            }
            if (!File.Exists(tmp)) return tmp; // short-circuit if no matches
            int min = 1, max = 2; // min is inclusive, max is exclusive/untested
            while (File.Exists(string.Format(pattern, max))) {
                min = max;
                max *= 2;
            }
            while (max != min + 1) {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot))) {
                    min = pivot;
                }
                else {
                    max = pivot;
                }
            }
            return string.Format(pattern, max);
        }
