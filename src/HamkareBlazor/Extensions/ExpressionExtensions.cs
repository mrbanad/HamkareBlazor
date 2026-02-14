// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace HamkareBlazor
{
    public static class ExpressionExtensions
    {
        public static string GetFullPathOfMember<T>(this Expression<Func<T>> property)
        {
            var resultingString = string.Empty;
            var p = property.Body as MemberExpression;

            while (p is not null)
            {
                if (p.Expression is MemberExpression)
                {
                    resultingString = p.Member.Name + (resultingString != string.Empty ? "." : string.Empty) +
                                      resultingString;
                }

                p = p.Expression as MemberExpression;
            }

            return resultingString;
        }
        
        /// <summary>
        /// Returns the Display Name of the provided property expression.
        /// If no DisplayAttribute is defined, fallback to property name.
        /// </summary>
        public static string GetLabelString<T>(this Expression<Func<T>> expression)
        {
            var member = ExtractMemberExpression(expression.Body);
            if (member == null)
                return string.Empty;

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                return string.Empty;

            var display = propInfo.GetCustomAttribute<DisplayAttribute>();

            // Fallbacks: Name → Prompt → Description → PropertyName
            return display?.GetName()
                   ?? display?.Prompt
                   ?? propInfo.Name;
        }

        public static string GetHelpTextString<T>(this Expression<Func<T>> expression)
        {
            var member = ExtractMemberExpression(expression.Body);
            if (member == null)
                return string.Empty;

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                return string.Empty;

            var display = propInfo.GetCustomAttribute<DisplayAttribute>();

            // Fallbacks: Name → Prompt → Description → PropertyName
            return display?.GetDescription()
                   ?? display?.Description
                   ?? string.Empty;
        }
        
        private static MemberExpression? ExtractMemberExpression(Expression body)
        {
            switch (body)
            {
                case MemberExpression member:
                    return member;

                case UnaryExpression unary:
                    return ExtractMemberExpression(unary.Operand);

                case LambdaExpression lambda:
                    return ExtractMemberExpression(lambda.Body);

                case MethodCallExpression call:
                    // Model.GetSomething(x.Name)
                    if (call.Arguments.Count > 0)
                        return ExtractMemberExpression(call.Arguments[0]);
                    break;

                case IndexExpression index:
                    if (index.Arguments.Count > 0)
                        return ExtractMemberExpression(index.Arguments[0]);
                    break;

                case ConditionalExpression conditional:
                    // x ? A : B
                    return ExtractMemberExpression(conditional.IfTrue)
                           ?? ExtractMemberExpression(conditional.IfFalse);
            }

            return null;
        }
    }
}
