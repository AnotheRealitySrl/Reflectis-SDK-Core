using Reflectis.SDK.Core;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.FAQ
{
    public interface IFAQSystem : ISystem
    {
        public FAQCategoryData GetFAQs(string category);
    }
}
