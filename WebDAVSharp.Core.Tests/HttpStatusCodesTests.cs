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
            IEnumerable<KeyValuePair<int, string>> codes = HttpStatusCodes.GetStatusCodes();

            Assert.That(codes.Any(c => c.Key == id), Is.True);
        }

        [TestCase(200, "OK")]
        [TestCase(404, "Not Found")]
        [TestCase(500, "Internal Server Error")]
        [TestCase(699, "Unknown status code (#699)")]
        [TestCase(799, "Unknown status code (#799)")]
        public void GetDescription_ForSampleCodes_ReturnsCorrectDescription(int id, string expected)
        {
            string output = HttpStatusCodes.GetDescription(id);

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void GetStatusCodes_ReturnsUniqueIds()
        {
            int[] ids =
                (from kvp in HttpStatusCodes.GetStatusCodes()
                 select kvp.Key).ToArray();

            IEnumerable<int> uniqueIds = ids.Distinct();

            Assert.That(uniqueIds.Count(), Is.EqualTo(ids.Count()));
        }

        [Test]
        public void GetStatusCodes_ReturnsUniqueDescriptions()
        {
            string[] descriptions =
                (from kvp in HttpStatusCodes.GetStatusCodes()
                 select kvp.Value).ToArray();

            IEnumerable<string> uniqueDescriptions = descriptions.Distinct();

            Assert.That(uniqueDescriptions.Count(), Is.EqualTo(descriptions.Count()));
        }
    }
}