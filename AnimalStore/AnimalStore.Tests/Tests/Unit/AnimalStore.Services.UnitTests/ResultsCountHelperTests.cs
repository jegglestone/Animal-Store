using AnimalStore.Web.API.Helpers;
using NUnit.Framework;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture] 
    public class ResultsCountHelperTests
    {
        [TestFixture] 
        public class GetResultsFromTests
        {
            [Test]
            public void GetResultsFrom_Returns_The_Ordinal_Position_Of_The_First_Record_In_The_First_Page()
            {
                const int currentPage = 1;
                const int pageSizeLimit = 10;

                //act
                var result = ResultsCountHelper.GetResultsFrom(currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(1));
            }

            [Test]
            public void GetResultsFrom_Returns_The_Ordinal_Position_Of_The_First_Record_In_The_Second_Page()
            {
                const int currentPage = 2;
                const int pageSizeLimit = 10;

                //act
                var result = ResultsCountHelper.GetResultsFrom(currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(11));
            }

            [TestCase(3, 10, 21, Description = "Third page of 4 with page size 0f 10 returns 21")]
            [TestCase(4, 10, 31, Description = "Final page of 4 with page size 0f 10 returns 31")]
            public void GetResultsFrom_Returns_The_Ordinal_Position_Of_The_First_Record_In_The_nth_Page(int currentPage, int pageSizeLimit, int expected)
            {
                //act
                var result = ResultsCountHelper.GetResultsFrom(currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase(3, 10, 21, Description = "Third page of 4 with page size 0f 10 returns 21")]
            [TestCase(3, 10, 21, Description = "Third page of 4 with page size 0f 10 returns 21")]
            [TestCase(4, 10, 31, Description = "Final page of 4 with page size 0f 10 returns 31")]
            public void GetResultsFrom_Returns_The_Ordinal_Position_Of_The_First_Record_In_The_nth_Page_With_Odd_Number_Of_Records(int currentPage, int pageSizeLimit, int expected)
            {
                //act
                var result = ResultsCountHelper.GetResultsFrom(currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetResultsToTests
        {
            [Test]
            public void GetResultsTo_Returns_The_Ordinal_Position_Of_The_Last_Record_In_The_First_Page()
            {
                const int totalRecords = 30;
                const int numberOfPages = 3;
                const int currentPage = 1;
                const int pageSizeLimit = 10;

                //act
                var result = ResultsCountHelper.GetResultsTo(totalRecords, numberOfPages, currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(10));
            }

            [Test]
            public void GetResultsTo_Returns_The_Ordinal_Position_Of_The_Last_Record_In_The_First_Page_When_The_Result_Size_Is_Smaller_Than_The_Results_Count()
            {
                const int totalRecords = 8;
                const int numberOfPages = 1;
                const int currentPage = 1;
                const int pageSizeLimit = 10;

                //act
                var result = ResultsCountHelper.GetResultsTo(totalRecords, numberOfPages, currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(8));
            }

            [TestCase(38, 4, 2, 10, 20, Description = "Gets the position of the final record on page 2 of 4")]
            [TestCase(38, 4, 3, 10, 30, Description = "Gets the position of the final record on page 3 of 4")]
            public void GetResultsTo_Returns_The_Ordinal_Position_Of_The_Last_Record_In_The_Nth_Page(int totalRecords, int numberOfPages, int currentPage, int pageSizeLimit, int expected)
            {
                //act
                var result = ResultsCountHelper.GetResultsTo(totalRecords, numberOfPages, currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void GetResultsTo_Returns_The_Ordinal_Position_Of_The_Last_Record_In_The_Final_Page_When_There_Are_Fewer_Records_On_The_Page_Than_In_The_Page_Size()
            {
                const int totalRecords = 38;
                const int numberOfPages = 4;
                const int currentPage = 4;
                const int pageSizeLimit = 10;

                //act
                var result = ResultsCountHelper.GetResultsTo(totalRecords, numberOfPages, currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(38));
            }

            [Test]
            public void GetResultsTo_Returns_The_Ordinal_Position_Of_The_Last_Record_In_The_Final_Page_When_It_Is_Divisible_By_Page_Size()
            {
                const int totalRecords = 40;
                const int numberOfPages = 4;
                const int currentPage = 4;
                const int pageSizeLimit = 10;

                //act
                var result = ResultsCountHelper.GetResultsTo(totalRecords, numberOfPages, currentPage, pageSizeLimit);

                Assert.That(result, Is.EqualTo(40));
            }
        }
    }
}
