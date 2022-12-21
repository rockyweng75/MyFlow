using System.Dynamic;

namespace MyFlow.Domain.Models
{      
    public class CustomDynamicObject : DynamicObject
    {

        Dictionary<string, object> dictionary
            = new Dictionary<string, object>();

        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public bool TryGetValue(string name, out object? result)
        {
            try
            {
                return dictionary.TryGetValue(name, out result);
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            string name = (string)indexes[0];
            try
            {
                var success = dictionary.TryGetValue(name, out result);
            }
            catch (Exception )
            {
                result = null;
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            string name = binder.Name;
            try
            {
                var success = dictionary.TryGetValue(name, out result);
            }
            catch (Exception)
            {
                result = null;
            }
            return true;
        }

        public void AddMember(string memberName, object value) 
        {
            dictionary[memberName] = value;
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if(value == null) return false;
            dictionary[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object result)
        {
            if ("HasProperty".Equals(binder.Name))
            {
                if (!dictionary.ContainsKey((string)args![0]!))
                {
                    result = false;
                    return true;
                }
                else
                {
                    result = true;
                    return true;
                }
            }
            if ("Add".Equals(binder.Name))
            {
                dictionary.Add((string)args![0]!, args[1]!);
                result = dictionary;
                return true;
            }
            if ("Remove".Equals(binder.Name))
            {
                dictionary.Remove((string)args![0]!);
                result = dictionary;
                return true;
            }
            return base.TryInvokeMember(binder, args, out result!);
        }
    }
}