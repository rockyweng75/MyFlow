using MyFlow.Domain.Models;
using MyFlow.Template.Tt;
using System.Reflection;

namespace MyFlow.Template
{
    public class Program
    {
        public static void Main(params string[] args) 
        {
            string modelNameSpace = "MyFlow.Domain.Models";
            string outputPath = "";
            var modelList = typeof(IViewModel).Assembly.GetTypes()
               .Where(t => String.Equals(t.Namespace, modelNameSpace, StringComparison.Ordinal))
               .ToArray();

            foreach (var model in modelList)
            {
                var modelName = model.Name.Replace("VM", "");
                var t4 = new CRUDServiceTemplate(modelName);
                string content = t4.TransformText();
                System.IO.File.WriteAllText($"{outputPath}/{modelName}Service.cs", content);
            }
        }

    }
}