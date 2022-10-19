using MyFlow.Data.DAOs.Basic;
using MyFlow.Domain.Models;
using MyFlow.Service.Impl;
using MyFlow.Template.Tt;
using System.Reflection;

namespace MyFlow.Template
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            string outputPath = "./";

            GenServices(outputPath);
            GenDataDI(outputPath);
            GenServiceDI(outputPath);
        }


        private static void GenServiceDI(string outputPath)
        {
            string serviceSpace = "MyFlow.Service.Impl";

            var serviceList = typeof(IService).Assembly.GetTypes()
                   .Where(t => String.Equals(t.Namespace, serviceSpace, StringComparison.Ordinal))
                   .Where(t => t.IsClass && !t.IsAbstract && !t.IsSealed)
                   .ToArray();

            var t4 = new DIServiceTemplate(serviceList.Select(o => o.Name).ToList());
            string content = t4.TransformText();
            string fileName = $"DIServiceExtensions.cs";
            if (File.Exists(fileName)) File.Delete(fileName);
            System.IO.File.WriteAllText($"{outputPath}/DIServiceExtensions.service.cs", content);
        }

        private static void GenDataDI(string outputPath)
        {
            string daoSpace = "MyFlow.Data.DAOs";

            var daoList = typeof(IDao<>).Assembly.GetTypes()
                   .Where(t => String.Equals(t.Namespace, daoSpace, StringComparison.Ordinal))
                   .Where(t => t.IsClass)
                   .ToArray();

            var t4 = new DIDaoTemplate(daoList.Select(o => o.Name).ToList());
            string content = t4.TransformText();
            string fileName = $"DIServiceExtensions.cs";
            if (File.Exists(fileName)) File.Delete(fileName);
            System.IO.File.WriteAllText($"{outputPath}/DIServiceExtensions.data.cs", content);
        }

        private static void GenServices(string outputPath)
        {
            string modelNameSpace = "MyFlow.Domain.Models";
            var modelList = typeof(IViewModel).Assembly.GetTypes()
               .Where(t => String.Equals(t.Namespace, modelNameSpace, StringComparison.Ordinal))
               .ToArray();

            foreach (var model in modelList)
            {
                var modelName = model.Name.Replace("VM", "");
                var t4 = new CRUDServiceTemplate(modelName);
                string content = t4.TransformText();
                string fileName = $"{outputPath}/{modelName}Service.cs";
                if (File.Exists(fileName)) File.Delete(fileName);
                System.IO.File.WriteAllText(fileName, content);
            }
        }
    }
}