using Reflectis.SDK.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reflectis.SDK.Diagnostics
{
    public class DynamicDisplayableContent : DisplayableContentBase
    {
        [SettableField(isRequired = true)]
        private List<string> headers;
        [SettableField(isRequired = true)]
        private List<string> types;
        /// <summary>
        /// Visual scripting does not handle correctly list of list of objects
        /// in type entry definitions, so we have to pass a list of object as 
        /// type definition
        /// </summary>
        [SettableField(isRequired = true, entryType = typeof(List<object>))]
        private List<List<object>> records;

        public List<string> Headers { get => headers; set => headers = value; }
        public List<string> Types { get => types; set => types = value; }
        public List<List<object>> Records { get => records; set => records = value; }

        public override void CheckValidity()
        {
            int columnCount = headers.Count;

            if (!(Types.Count == columnCount && (Records).All(x => ((List<object>)x).Count == columnCount)))
            {
                throw new System.Exception($"Record types count are not matching.\n" +
                    $"Headers count: {headers.Count} \n" +
                    $"Types count: {Types.Count} \n" +
                    string.Join("", Records.Select((x, i) => $"Record {i}: {((List<object>)x).Count} \n")));
            }
        }

        public override void AssignValues(List<Field> args)
        {
            var headerField = args.FirstOrDefault(x => x.name == "headers");
            if (headerField != null)
            {
                Headers = headerField.value as List<string>;
            }
            var typesField = args.FirstOrDefault(x => x.name == "types");
            if (typesField != null)
            {
                Types = typesField.value as List<string>;
            }
            var recordField = args.FirstOrDefault(x => x.name == "records");
            if (recordField != null)
            {
                List<object> recordsValue = recordField.value as List<object>;
                Debug.LogError("type " + recordField.value.GetType() + "| " + Newtonsoft.Json.JsonConvert.SerializeObject(recordField.value));
                Records = recordsValue.Select((x, i) =>
                {
                    if (x is ArrayList aotList)
                    {
                        List<object> records = new List<object>();
                        foreach (var obj in aotList)
                        {
                            records.Add(obj);
                        }
                        return records;
                    }
                    else
                    {
                        throw new Exception($"The record {i} is not a list");
                    }
                }).ToList();

            }
        }
    }
}
