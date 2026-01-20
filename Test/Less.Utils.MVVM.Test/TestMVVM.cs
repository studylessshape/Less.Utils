using System.Diagnostics;

namespace Less.Utils.MVVM.Test
{
    [TestClass]
    public sealed class TestMVVM
    {
        [TestMethod]
        public void TestMethodObservableEx()
        {
            var obEX = new ObservableCollectionEx<int>();
            obEX.CollectionChanged += (sender, e) =>
            {
                var newItemsCount = e.NewItems?.Count;
                var oldItemsCount = e.OldItems?.Count;
                Debug.Print($"New: {newItemsCount}, Old: {oldItemsCount}");
            };
            obEX.AddRange([1, 2, 3]);
            obEX.InsertRange(2, [4, 5, 6, 7, 8, 9, 10, 11, 12, 13]);
            obEX.RemoveRange(3, 5);
        }
    }
}
