using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DataTable
{
    public abstract class DataTableSO : ScriptableObject
    {
        public abstract void Read(string jsonString);
        public T Get<T>() where T : class
        {
            return this is T ? this as T : null;
        }

        protected void JsonModify(string jsonString, string name, int index, ref JArray jArray)
        {
            JArray addJArray = new JArray();
            int count = 0;
            while (jsonString.Contains(name + count))
            {
                addJArray.Add(jArray[index][name + count]);
                count++;
            }
            jArray[index][name] = addJArray;
        }

    }
}
