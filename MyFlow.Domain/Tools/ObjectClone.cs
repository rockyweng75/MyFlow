using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyFlow.Domain.Tools
{
    public static class ObjectClone
    {
        public static void Clone(object source, object target, bool NotSetNull = false)
        {

            PropertyInfo[] infos = source.GetType().GetProperties();
            PropertyInfo[] rInfos = target.GetType().GetProperties();

            foreach (PropertyInfo info in infos)
            {
                PropertyInfo rp = rInfos.Where((o) => o.Name.Equals(info.Name)).FirstOrDefault();
                if (rp != null)
                {
                    try
                    {
                        if (rp.CanWrite)
                        {
                            if (!(NotSetNull && info.GetValue(source) == null)) rp.SetValue(target, info.GetValue(source));
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
