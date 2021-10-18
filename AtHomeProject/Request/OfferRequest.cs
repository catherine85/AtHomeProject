using System.Collections.Generic;

namespace AtHomeProject.Request
{
    public class OfferRequest
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public IList<BoxDimensions> Dimensions { get; set; }
    }
}
