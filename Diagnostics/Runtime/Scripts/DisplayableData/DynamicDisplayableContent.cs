using System.Collections.Generic;
using System.Linq;

namespace Reflectis.SDK.Diagnostics
{
    public class DynamicDisplayableContent : DisplayableContentBase
    {
        [SettableField(isRequired = true)]
        private List<string> headers;
        [SettableField(isRequired = true)]
        private List<string> types;
        [SettableField(isRequired = true)]
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
    }
}
