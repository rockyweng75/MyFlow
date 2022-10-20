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
            string outputPath = "./dist/";

            GenServices(outputPath);
            GenDataDI(outputPath);
            GenServiceDI(outputPath);
            GenControllers(outputPath);
        }


        private static void GenControllers(string outputPath)
        {
            string modelSpace = "MyFlow.Domain.Models";

            var modelList = typeof(IViewModel).Assembly.GetTypes()
                   .Where(t => String.Equals(t.Namespace, modelSpace, StringComparison.Ordinal))
                   .Where(t => t.IsClass && !t.IsAbstract && !t.IsSealed)
                   .ToArray();

            var dir = $"{outputPath}/controllers";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            foreach (var model in modelList)
            {
                var t4 = new CRUDControllerTemplate(model.Name);
                string content = t4.TransformText();
      
                string fileName = $"{dir}/{t4.ControllerName}.cs";
                if (File.Exists(fileName)) File.Delete(fileName);
                System.IO.File.WriteAllText(fileName, content);
            }
        }


        private static void GenServiceDI(string outputPath)
        {
            string serviceSpace = "MyFlow.Service.Impl";

            var serviceList = typeof(IService).Assembly.GetTypes()
                   .Where(t => String.Equals(t.Namespace, serviceSpace, StringComparison.Ordinal))
                   .Where(t => t.IsClass && !t.IsAbstract && !t.IsSealed)
                   .ToArray();

            var dir = $"{outputPath}/service/DI";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var t4 = new DIServiceTemplate(serviceList.Select(o => o.Name).ToList());
            string content = t4.TransformText();
            string fileName = $"{dir}/DIServiceExtensions.service.cs";
            if (File.Exists(fileName)) File.Delete(fileName);
            System.IO.File.WriteAllText(fileName, content);
        }

        private static void GenDataDI(string outputPath)
        {
            string daoSpace = "MyFlow.Data.DAOs";

            var daoList = typeof(IDao<>).Assembly.GetTypes()
                   .Where(t => String.Equals(t.Namespace, daoSpace, StringComparison.Ordinal))
                   .Where(t => t.IsClass)
                   .ToArray();

            var dir = $"{outputPath}/Data/DI";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var t4 = new DIDaoTemplate(daoList.Select(o => o.Name).ToList());
            string content = t4.TransformText();
            string fileName = $"{dir}/DIServiceExtensions.data.cs";
            if (File.Exists(fileName)) File.Delete(fileName);
            System.IO.File.WriteAllText(fileName, content);
        }

        private static void GenServices(string outputPath)
        {
            string modelNameSpace = "MyFlow.Domain.Models";
            var modelList = typeof(IViewModel).Assembly.GetTypes()
               .Where(t => String.Equals(t.Namespace, modelNameSpace, StringComparison.Ordinal))
               .ToArray();

            var dir = $"{outputPath}/Data/DAOs";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            foreach (var model in modelList)
            {
                var modelName = model.Name.Replace("VM", "");
                var t4 = new CRUDServiceTemplate(modelName);
                string content = t4.TransformText();
                string fileName = $"{dir}/{modelName}Service.cs";
                if (File.Exists(fileName)) File.Delete(fileName);
                System.IO.File.WriteAllText(fileName, content);
            }
        }
    }
}