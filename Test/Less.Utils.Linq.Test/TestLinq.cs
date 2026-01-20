namespace Less.Utils.Linq.Test
{
    [TestClass]
    public sealed class TestLinq
    {
        [TestMethod]
        public void TestMethodFlat()
        {
            int length = 10000;
            IEnumerable<IEnumerable<int>> notFlat = Enumerable.Range(0, length).Select(n => Enumerable.Range(n * length, length));
            IEnumerable<int> flat = Enumerable.Range(0, length * length);

            Assert.IsTrue(notFlat.Flat().SequenceEqual(flat));

            static bool condition(int i) => i % 2 == 1;
            IEnumerable<int> flatWhere = Enumerable.Range(0, length * length).Where(condition);

            Assert.IsTrue(notFlat.Flat(condition).SequenceEqual(flatWhere));
        }
    }
}
