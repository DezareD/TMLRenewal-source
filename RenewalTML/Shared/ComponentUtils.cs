using RenewalTML.Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace RenewalTML.Shared
{
    public static class ComponentUtils
    {
        public static IEnumerable<ValidationAttribute> GetExpressionCustomAttributes<T>(Expression<Func<T>> accessor)
        {
            if (accessor != null)
            {
                var accessorBody = accessor.Body;

                if (accessorBody is UnaryExpression unaryExpression
                    && unaryExpression.NodeType == ExpressionType.Convert
                    && unaryExpression.Type == typeof(object))
                {
                    accessorBody = unaryExpression.Operand;
                }

                if (!(accessorBody is MemberExpression memberExpression))
                    throw new ArgumentException($"AccessorBody argument exception's");

                return memberExpression.Member.GetCustomAttributes<ValidationAttribute>();
            }
            else return null;
        }

        public static List<FillTicketImage> MultiImageFormRecreate(int MaxImagesCount = 6, bool isReadOnly = false)
        {
            var model = new List<FillTicketImage>(MaxImagesCount);

            for (int x = 0; x < MaxImagesCount; x++)
                model.Add(new FillTicketImage());

            if(!isReadOnly)
                model[0].isUploadBlock = true;

            return model;
        }
    }
}
