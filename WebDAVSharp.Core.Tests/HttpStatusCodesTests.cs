using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebDAVSharp.Tests
{
    [TestFixture]
    public class HttpStatusCodesTests
    {
        [TestCase(200)]
        [TestCase(207)]
        [TestCase(500)]
        [TestCase(422)]
        public void GetStatusCodes_ContainsAFewStandardCodes(int id)
        {
            IEnumerable<StatusCode> codes = HttpStatusCodes.StatusCodes;

            Assert.That(codes.Any(c => c.Id == id), Is.True);
        }

        [TestCase(200, "OK")]
        [TestCase(404, "Not Found")]
        [TestCase(500, "Internal Server Error")]
        [TestCase(699, "Unknown status code (#699)")]
        [TestCase(799, "Unknown status code (#799)")]
        public void GetDescription_ForSampleCodes_ReturnsCorrectDescription(int id, string expected)
        {
            string output = HttpStatusCodes.GetName(id);

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void GetStatusCodes_ReturnsUniqueIds()
        {
            int[] ids = 
                (from statusCode in HttpStatusCodes.StatusCodes
                 select statusCode.Id).ToArray();

            IEnumerable<int> uniqueIds = ids.Distinct();

            Assert.That(uniqueIds.Count(), Is.EqualTo(ids.Count()));
        }

        [Test]
        public void GetStatusCodes_ReturnsUniqueNames()
        {
            string[] names =
                (from statusCode in HttpStatusCodes.StatusCodes
                 select statusCode.Name).ToArray();

            IEnumerable<string> uniqueDescriptions = names.Distinct();

            Assert.That(uniqueDescriptions.Count(), Is.EqualTo(names.Count()));
        }
    }
}