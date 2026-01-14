using System.Diagnostics;

namespace Less.Utils.ResultExtensions.Test
{
    [TestClass]
    public sealed class TestResultLinqClass
    {
        [TestMethod]
        public void TestResultLinq()
        {
            Result<T, string> Ok<T>(T value)
            {
                Debug.WriteLine($"New Ok: {value}");
                return Result<T, string>.NewOk(value);
            }

            var errResult = Result<int, string>.NewError("Error");

            // normal test with select and where
            var linq = from ok in Ok(1)
                       from ok2 in Ok("123")
                       from ok3 in Ok(3)
                       from ok4 in Ok(4.2f)
                       where ok4 == 2 || ok2 == "123"
                       select ok2;
            Assert.IsTrue(linq.IsOk);
            Assert.AreEqual("123", linq.ResultValue);

            // interrupt by errResult
            var linq2 = from ok in Ok(1)
                   from ok2 in Ok(2)
                   from eOk in errResult
                   from ok3 in Ok(3)
                   select ok3;
            Assert.IsTrue(linq2.IsError);
            Assert.AreEqual("Error", linq2.ErrorValue);
        }

        [TestMethod]
        public async Task TestTaskResultLinq()
        {
            Task<Result<int, string>> Ok(int value)
            {
                Debug.WriteLine($"New Ok: {value}");
                return Task.FromResult(Result<int, string>.NewOk(value));
            }

            var errResult = Result<int, string>.NewError("Error");

            // normal test with select and where
            var linq = from ok in Ok(1)
                       from ok2 in Ok(2)
                       from ok3 in Ok(3)
                       from ok4 in Ok(4)
                       where ok4 == 2 || ok2 == 2
                       select ok2;
            var res = await linq;
            Assert.IsTrue(res.IsOk);
            Assert.AreEqual(2, res.ResultValue);

            // interrupt by errResult
            linq = from ok in Ok(1)
                   from ok2 in Ok(2)
                   from eOk in errResult.ToTask()
                   from ok3 in Ok(3)
                   select ok3;
            res = await linq;
            Assert.IsTrue(res.IsError);
            Assert.AreEqual("Error", res.ErrorValue);
        }
    }
}
