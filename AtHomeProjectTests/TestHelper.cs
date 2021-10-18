using AtHomeProject.Request;
using System.Collections.Generic;
using System.IO;

namespace AtHomeProjectTests
{
    public static class TestHelper
    {
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static IList<BoxDimensions> GetDimensionsList()
        {
            return new List<BoxDimensions> { new BoxDimensions { Height=1, Length=2, Width=1},
                                             new BoxDimensions { Height=1, Length=5, Width=4},
                                             new BoxDimensions { Height=1, Length=6, Width=8}};
        }
    }
}
