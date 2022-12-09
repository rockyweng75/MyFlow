using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace MyFlow.Domain.Tools
{
    public static class ModelHepler
    {
        public static bool IsReadonly<TModel>(string name) where TModel : class
        {
            var type = typeof(TModel);
            var props = type.GetProperty(name);
            if (props == null) return false;

            var attrs = props.GetCustomAttributes(false);

            if (attrs.Any(o => o.GetType().Name == "ReadonlyAttribute"))
            {
                return true;
            }

            return false;
        }

        public static string? GetColumnName<TModel>(string name) where TModel : class
        {
            var type = typeof(TModel);
            var props = type.GetProperty(name);
            if (props == null) return null;

            var attrs = props.GetCustomAttributes(false);

            if (attrs.Any(o => o.GetType().Name == "IgnoreSelectAttribute" || o.GetType().Name == "ReadonlyAttribute"))
            {
                return null;
            }

            var colAttr = attrs
                  .Where(o => o.GetType().Name == "ColumnAttribute")
                  .Cast<ColumnAttribute>()
                  .FirstOrDefault();
            return colAttr == null ? null : colAttr.Name;
        }

        public static string? GetCustomColumnName<TModel>(string name) where TModel : class
        {
            var type = typeof(TModel);
            var props = type.GetProperty(name);
            if (props == null) return null;

            var attrs = props.GetCustomAttributes(false);

            if (attrs.Any(o => o.GetType().Name == "CustomAttribute"))
            {
            }
            else 
            {
                return null;
            }

            var colAttr = attrs
                  .Where(o => o.GetType().Name == "ColumnAttribute")
                  .Cast<ColumnAttribute>()
                  .FirstOrDefault();
            return colAttr == null ? null : colAttr.Name;
        }

        public static string? getColumnName(PropertyInfo prop)
        {
            //var attrs = prop.GetCustomAttributes(false);
            //if (attrs.Any(o => o.GetType().Name == "IgnoreSelectAttribute" || o.GetType().Name == "ReadonlyAttribute"))
            //{
            //    return null;
            //}

            //var column = attrs
            //    .Where(o => o.GetType().Name == "ColumnAttribute")
            //    .Cast<ColumnAttribute>()
            //    .FirstOrDefault();
            var column = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute))!;

            if (column == null) return prop.Name;
            return column.Name;
        }
    }

    public class CustomAttribute : Attribute
    {
    
    }
}
