using MyFlow.Domain.Models;

namespace MyFlow.Domain.Tools
{
    public static class FormDataTools
    {
        public static IFormData? Parse(dynamic FormData)
        {
            if(FormData is null) return null;

            var props = typeof(IFormData).GetProperties();

            IFormData? formData = (IFormData)Activator.CreateInstance(typeof(FormData))!;

            var obj = FormData as object;
            var type = obj.GetType();
            if(type.IsArray) throw new Exception("FormData can't be Array");

            if(FormData is IDictionary<string, object>)
            {
                var keyValue = FormData as IDictionary<string, object>;

                foreach(var prop in props)
                {
                    if(keyValue!.ContainsKey(prop.Name))
                    {
                        var value = keyValue[prop.Name];
                        prop.SetValue(formData, value);
                    } 
                }
            } 
            else 
            {
                // object
                var iProps = type.GetProperties();
                foreach(var prop in props)
                {
                    if(iProps!.Any(p => p.Name == prop.Name))
                    {
                        var iProp = iProps.Where(p => p.Name == prop.Name).First();
                        var value = iProp.GetValue(FormData);
                        prop.SetValue(formData, value);
                    } 
                }
            }
     
            return formData;
        }
    }
}
