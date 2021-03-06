    public static double Evaluate(string expr)
        {
            expr = expr.ToLower();
            expr = expr.Replace(" ", "");
            expr = expr.Replace("true", "1");
            expr = expr.Replace("false", "0");
            Stack<String> stack = new Stack<String>();
            string value = "";
            for (int i = 0; i < expr.Length; i++)
            {
                String s = expr.Substring(i, 1);
                // pick up any doublelogical operators first.
                if (i < expr.Length - 1)
                {
                    String op = expr.Substring(i, 2);
                    if (op == "<=" || op == ">=" || op == "==")
                    {
                        stack.Push(value);
                        value = "";
                        stack.Push(op);
                        i++;
                        continue;
                    }
                }
                char chr = s.ToCharArray()[0];
                if (!char.IsDigit(chr) && chr != '.' && value != "")
                {
                    stack.Push(value);
                    value = "";
                }
                if (s.Equals("("))
                {
                    string innerExp = "";
                    i++; //Fetch Next Character
                    int bracketCount = 0;
                    for (; i < expr.Length; i++)
                    {
                        s = expr.Substring(i, 1);
                        if (s.Equals("(")) bracketCount++;
                        if (s.Equals(")"))
                        {
                            if (bracketCount == 0) break;
                            bracketCount--;
                        }
                        innerExp += s;
                    }
                    stack.Push(Evaluate(innerExp).ToString());
                }
                else if (s.Equals("+") ||
                         s.Equals("-") ||
                         s.Equals("*") ||
                         s.Equals("/") ||
                         s.Equals("<") ||
                         s.Equals(">"))
                {
                    stack.Push(s);
                }
                else if (char.IsDigit(chr) || chr == '.')
                {
                    value += s;
                    if (value.Split('.').Length > 2)
                        throw new Exception("Invalid decimal.");
                    if (i == (expr.Length - 1))
                        stack.Push(value);
                }
                else
                {
                    throw new Exception("Invalid character.");
                }
            }
            double result = 0;
            List<String> list = stack.ToList<String>();
            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (list[i] == "/")
                {
                    list[i] = (Convert.ToDouble(list[i - 1]) / Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                    i -= 2;
                }
            }
            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (list[i] == "*")
                {
                    list[i] = (Convert.ToDouble(list[i - 1]) * Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                    i -= 2;
                }
            }
            stack.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                stack.Push(list[i]);
            }
            while (stack.Count >= 3)
            {
                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());
                if (op == "+") result = left + right;
                else if (op == "-") result = left - right;
                else if (op == "<") result = (left < right) ? 1 : 0;
                else if (op == ">") result = (left > right) ? 1 : 0;
                else if (op == "<=") result = (left <= right) ? 1 : 0;
                else if (op == ">=") result = (left >= right) ? 1 : 0;
                else if (op == "==") result = (left == right) ? 1 : 0;
                stack.Push(result.ToString());
            }
            return Convert.ToDouble(stack.Pop());
        }
