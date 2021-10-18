using System.Collections.Generic;

namespace AtHomeProject.Request
{
    public class XmlApiRequest
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public IList<BoxDimensions> Packages { get; set; }
    }
}
