using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyFlow.Domain.Models;

namespace MyFlow.Domain.Tools
{
    public static class FormDataTools
    {
        public static IFormData? Parse(dynamic FormData)
        {
            if(FormData is null) return null;

            var keyValue = FormData as IDictionary<string, object>;
            var props = typeof(IFormData).GetProperties();
            // Assembly? assembly = Assembly.GetAssembly(typeof(FormData)) ?? throw new Exception("assembly is null");
            // IFormData? formData = assembly.CreateInstance(typeof(FormData).Name) as IFormData;

            IFormData? formData = (IFormData)Activator.CreateInstance(typeof(FormData))!;


            foreach(var prop in props)
            {
                if(keyValue!.ContainsKey(prop.Name))
                {
                    var value = keyValue[prop.Name];
                    prop.SetValue(formData, value);
                } 
            }

            return formData;
        }
    }
}
