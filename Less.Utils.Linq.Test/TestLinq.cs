namespace Less.Utils.Linq.Test
{
    [TestClass]
    public sealed class TestLinq
    {
        [TestMethod]
        public void TestMethodFlat()
        {
            IEnumerable<IEnumerable<int>> notFlat = [[1, 2, 3], [], [4, 5, 6], null, [7, 8, 9]];
            IEnumerable<int> flat = [1, 2, 3, 4, 5, 6, 7, 8, 9];

            Assert.IsTrue(notFlat.Flat().SequenceEqual(flat));
        }
    }
}
