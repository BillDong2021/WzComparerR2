﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WzComparerR2.Network.Contracts
{
    class TypeNameBinder : DefaultSerializationBinder
    {
        readonly Dictionary<Type, string> knownTypeNames = new Dictionary<Type, string>();
        readonly Dictionary<string, Type> knownTypes = new Dictionary<string, Type>();

        public TypeNameBinder()
        {
            var types = Assembly.GetCallingAssembly().GetTypes()
                .Select(t => new { type = t, attr = t.GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>(false) })
                .Where(item => item.attr != null);

            foreach (var item in types)
            {
                knownTypeNames.Add(item.type, item.attr.Id);
                knownTypes.Add(item.attr.Id, item.type);
            }
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (knownTypeNames.TryGetValue(serializedType, out typeName))
            {
                assemblyName = null;
            }
            else
            {
                base.BindToName(serializedType, out assemblyName, out typeName);
            }
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type;
            if (knownTypes.TryGetValue(typeName, out type))
            {
                return type;
            }
            return base.BindToType(assemblyName, typeName);
        }
    }
}
