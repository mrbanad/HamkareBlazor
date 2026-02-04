// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HamkareBlazor
{
#nullable enable
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
                    resultingString = p.Member.Name + (resultingString != string.Empty ? "." : string.Empty) + resultingString;
                }
                p = p.Expression as MemberExpression;
            }
            return resultingString;
        }

        /// <summary>
        /// Returns the display name attribute of the provided field property as a string. If this attribute is missing, the member name will be returned.
        /// </summary>
        public static string GetLabelString<T>(this Expression<Func<T>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;

            // Currently we have no solution for this which is trimming incompatible
            // A possible solution is to use source gen
#pragma warning disable IL2075
            var propertyInfo = memberExpression.Expression?.Type.GetProperty(memberExpression.Member.Name);
#pragma warning restore IL2075
            return propertyInfo?.GetCustomAttributes(typeof(LabelAttribute), true).Cast<LabelAttribute>().FirstOrDefault()?.Name ?? string.Empty;
        }
        
        public static (string? Label, string? HelperText) ResolveDisplayFromExpression<T>(
            string? labelFromParams,
            string? helperFromParams,
            Expression<Func<T>>? forExpression)
        {
            var label = string.IsNullOrWhiteSpace(labelFromParams) ? null : labelFromParams;
            var helper = string.IsNullOrWhiteSpace(helperFromParams) ? null : helperFromParams;

            if (forExpression is null)
                return (label, helper);

            var prop = GetPropertyInfo(forExpression);
            if (prop is null)
                return (label, helper);

            var display = prop.GetCustomAttribute<DisplayAttribute>();

            if (string.IsNullOrWhiteSpace(label))
                label = display is null ? prop.Name : display.GetName();

            if (string.IsNullOrWhiteSpace(helper))
                helper = display?.GetDescription();

            return (label, helper);
        }
        
        public static (string? Label, string? HelperText) ResolveDisplayFromLambda(
            string? labelFromParams,
            string? helperFromParams,
            LambdaExpression? lambdaExpression)
        {
            var label = string.IsNullOrWhiteSpace(labelFromParams) ? null : labelFromParams;
            var helper = string.IsNullOrWhiteSpace(helperFromParams) ? null : helperFromParams;

            if (lambdaExpression is null)
                return (label, helper);

            var prop = GetPropertyInfo(lambdaExpression);
            if (prop is null)
                return (label, helper);

            var display = prop.GetCustomAttribute<DisplayAttribute>();

            if (string.IsNullOrWhiteSpace(label))
                label = display is null ? prop.Name : display.GetName();

            if (string.IsNullOrWhiteSpace(helper))
                helper = display?.GetDescription();

            return (label, helper);
        }
        
        
        private static PropertyInfo? GetPropertyInfo<T>(Expression<Func<T, object>> expr)
        {
            return GetPropertyInfo((LambdaExpression)expr);
        }

        private static PropertyInfo? GetPropertyInfo(LambdaExpression expr)
        {
            var body = expr.Body;

            if (body is UnaryExpression { NodeType: ExpressionType.Convert } unary)
                body = unary.Operand;

            if (body is not MemberExpression member) return null;
            return member.Member switch
            {
                PropertyInfo pi => pi,
                _ => null
            };
        }
    }
}
