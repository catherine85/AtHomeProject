using System.Collections.Generic;

namespace AtHomeProject.Request
{
    public class Json1ApiRequest
    {
        public string ContactAddress { get; set; }
        public string WareHouseAddress { get; set; }
        public IList<BoxDimensions> PackageDimensions { get; set; }
    }
}
