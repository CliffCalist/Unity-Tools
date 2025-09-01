using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace WhiteArrow.Tests
{
    public class InterfacesListTests
    {
        private IMyInterface _a;
        private IMyInterface _b;



        [SetUp]
        public void SetUp()
        {
            _a = new MyImplementationA();
            _b = new MyImplementationB();
        }



        [Test]
        public void Count_ReturnsCorrectValue()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void IsReadOnly_ReturnsFalse()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            Assert.IsFalse(list.IsReadOnly);
        }

        [Test]
        public void Indexer_GetAndSet_WorksCorrectly()
        {
            IList<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            list[0] = _b;
            Assert.AreEqual(_b, list[0]);
        }

        [Test]
        public void Contains_ReturnsTrueForExistingItem()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            Assert.IsTrue(list.Contains(_a));
        }

        [Test]
        public void IndexOf_ReturnsCorrectIndex()
        {
            IList<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            list.Add(_b);
            Assert.AreEqual(1, list.IndexOf(_b));
        }

        [Test]
        public void Add_WorksCorrectly()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            Assert.AreEqual(_a, ((IList<IMyInterface>)list)[0]);
        }

        [Test]
        public void AddRange_AddsAllItems()
        {
            var list = new InterfacesList<IMyInterface>();
            var range = new[] { _a, _b };
            list.AddRange(range);

            IReadOnlyList<IMyInterface> roList = list;
            Assert.AreEqual(2, roList.Count);
            Assert.AreEqual(_a, roList[0]);
            Assert.AreEqual(_b, roList[1]);
        }

        [Test]
        public void Insert_InsertsAtCorrectIndex()
        {
            IList<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_b);
            list.Insert(0, _a);
            Assert.AreEqual(_a, list[0]);
        }

        [Test]
        public void Remove_RemovesSpecifiedItem()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            Assert.IsTrue(list.Remove(_a));
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveAt_RemovesAtSpecifiedIndex()
        {
            IList<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            list.RemoveAt(0);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveRange_RemovesExpectedItems()
        {
            var list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            list.Add(_b);
            list.RemoveRange(0, 2);

            IReadOnlyCollection<IMyInterface> ro = list;
            Assert.AreEqual(0, ro.Count);
        }

        [Test]
        public void Clear_RemovesAllItems()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CopyTo_GenericArray_WorksCorrectly()
        {
            ICollection<IMyInterface> list = new InterfacesList<IMyInterface>();
            list.Add(_a);
            list.Add(_b);

            var array = new IMyInterface[2];
            list.CopyTo(array, 0);

            Assert.AreEqual(_a, array[0]);
            Assert.AreEqual(_b, array[1]);
        }

        [Test]
        public void CopyTo_NonGenericArray_WorksCorrectly()
        {
            ICollection list = new InterfacesList<IMyInterface>() { _a, _b };

            var array = new InterfaceField<IMyInterface>[2];
            list.CopyTo(array, 0);

            Assert.AreEqual(_a, array[0].Value);
            Assert.AreEqual(_b, array[1].Value);
        }

        [Test]
        public void GetEnumerator_EnumeratesAllItems()
        {
            IEnumerable<IMyInterface> list = new InterfacesList<IMyInterface>();
            ((ICollection<IMyInterface>)list).Add(_a);
            ((ICollection<IMyInterface>)list).Add(_b);

            var result = new List<IMyInterface>();
            foreach (var item in list)
                result.Add(item);

            CollectionAssert.AreEqual(new[] { _a, _b }, result);
        }

        [Test]
        public void GetEnumerator_NonGeneric_Works()
        {
            IEnumerable list = new InterfacesList<IMyInterface>();
            ((ICollection<IMyInterface>)list).Add(_a);

            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(_a, enumerator.Current);
        }
    }
}
