using Filtering;
using Filtering.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestSearch
{
    [TestClass]
    public class UnitTestFilters
    {
        private static Item _positive;
        private static Item _negative;

        private static FilterByText<Item> _filterByText;
        private static FilterByInequality<Item, int> _filter;
        private static FilterByRange<Item, int> _rangeFilter;

        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            _positive = new Item(2, "имя первой зависимости обрабатываемого правила", new DateTime(2022, 01, 01));
            _negative = new Item(0, "all work and no play makes Jack a dull boy", new DateTime(1900, 01, 01));

            _filterByText = new FilterByText<Item>("имя", x => x.Text);
            _filter = new FilterByInequality<Item, int>(
                2,
                InequalityRelation.Equal,
                x => x.Count);
            var left = new FilterByInequality<Item, int>(
                1,
                InequalityRelation.GreaterOrEqual,
                x => x.Count);
            var right = new FilterByInequality<Item, int>(
                2,
                InequalityRelation.LessOrEqual,
                x => x.Count);

            _rangeFilter = new FilterByRange<Item, int>(left, right, x => x.Count);
        }

        [TestMethod]
        public void TestMethodFilterByTextPositive()
        {
            Assert.IsTrue(_filterByText.ApplyTo(_positive));
        }

        [TestMethod]
        public void TestMethodFilterByTextNegative()
        {
            Assert.IsFalse(_filterByText.ApplyTo(_negative));
        }

        [TestMethod]
        public void TestFilterPositive()
        {
            Assert.IsTrue(_filter.ApplyTo(_positive));
        }

        [TestMethod]
        public void TestFilterNegative()
        {
            Assert.IsFalse(_filter.ApplyTo(_negative));
        }

        [TestMethod]
        public void TestRangePositive()
        {
            Assert.IsTrue(_rangeFilter.ApplyTo(_positive));
        }
    }
}
