using System.Linq.Expressions;

namespace Cogworks.Umbraco.Essentials.Helpers
{
    public static class ExpressionHelper
    {
        public static string GetNameFromMemberExpression(Expression expression)
            => expression switch
            {
                MemberExpression memberExpression => memberExpression.Member.Name,
                UnaryExpression unaryExpression => GetNameFromMemberExpression(unaryExpression.Operand),
                _ => "MemberNameUnknown"
            };
    }
}