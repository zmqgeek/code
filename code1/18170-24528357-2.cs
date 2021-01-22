    public static class LambdaCompare
    {
        public static bool AreEqual<TSource, TValue>(
            Expression<Func<TSource, TValue>> x,
            Expression<Func<TSource, TValue>> y)
        {
            return ExpressionEqual(x, y, null, null);
        }
        private static bool ExpressionEqual(Expression x, Expression y, LambdaExpression rootX, LambdaExpression rootY)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            if (x.NodeType != y.NodeType
                || x.Type != y.Type) return false;
            if (x is LambdaExpression)
            {
                var lx = (LambdaExpression) x;
                var ly = (LambdaExpression) y;
                var paramsX = lx.Parameters;
                var paramsY = ly.Parameters;
                return CollectionsEqual(paramsX, paramsY, lx, ly) && ExpressionEqual(lx.Body, ly.Body, lx, ly);
            }
            if (x is MemberExpression)
            {
                var mex = (MemberExpression) x;
                var mey = (MemberExpression) y;
                return MemberExpressionsEqual(mex, mey, rootX, rootY);
            }
            if (x is BinaryExpression)
            {
                var bx = (BinaryExpression) x;
                var by = (BinaryExpression) y;
                return bx.Method == @by.Method && ExpressionEqual(bx.Left, @by.Left, rootX, rootY) &&
                       ExpressionEqual(bx.Right, @by.Right, rootX, rootY);
            }
            if (x is ParameterExpression)
            {
                var px = (ParameterExpression) x;
                var py = (ParameterExpression) y;
                return rootX.Parameters.IndexOf(px) == rootY.Parameters.IndexOf(py);
            }
            if (x is MethodCallExpression)
            {
                var cx = (MethodCallExpression)x;
                var cy = (MethodCallExpression)y;
                return cx.Method == cy.Method
                       && ExpressionEqual(cx.Object, cy.Object, rootX, rootY)
                       && CollectionsEqual(cx.Arguments, cy.Arguments, rootX, rootY);
            }
            if (x is ConstantExpression)
            {
                var cx = (ConstantExpression)x;
                var cy = (ConstantExpression)y;
                return Equals(cx.Value, cy.Value);
            }
            throw new NotImplementedException(x.ToString());
        }
        private static bool CollectionsEqual(IEnumerable<Expression> x, IEnumerable<Expression> y, LambdaExpression rootX, LambdaExpression rootY)
        {
            return x.Count() == y.Count()
                   && x.Select((e, i) => new {Expr = e, Index = i})
                       .Join(y.Select((e, i) => new { Expr = e, Index = i }),
                             o => o.Index, o => o.Index, (xe, ye) => new { X = xe.Expr, Y = ye.Expr })
                       .All(o => ExpressionEqual(o.X, o.Y, rootX, rootY));
        }
        private static bool MemberExpressionsEqual(MemberExpression x, MemberExpression y, LambdaExpression rootX, LambdaExpression rootY)
        {
            if (x.Expression.NodeType != y.Expression.NodeType)
                return false;
            switch (x.Expression.NodeType)
            {
                case ExpressionType.Constant:
                    var constx = GetValueOfConstantExpression(x);
                    var consty = GetValueOfConstantExpression(y);
                    return Equals(constx, consty);
                case ExpressionType.Parameter:
                    return Equals(x.Member, y.Member) && ExpressionEqual(x.Expression, y.Expression, rootX, rootY);
                default:
                    throw new NotImplementedException(x.ToString());
            }
        }
        private static object GetValueOfConstantExpression(MemberExpression mex)
        {
            var o = ((ConstantExpression) mex.Expression).Value;
            return mex.Member is FieldInfo
                              ? ((FieldInfo) mex.Member).GetValue(o)
                              : ((PropertyInfo) mex.Member).GetValue(o);
        }
    }
