using System.Collections.Generic;

namespace AtHomeProject.Request
{
    public class Json2ApiRequest
    {
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public IList<BoxDimensions> Cartons { get; set; }
    }
}
