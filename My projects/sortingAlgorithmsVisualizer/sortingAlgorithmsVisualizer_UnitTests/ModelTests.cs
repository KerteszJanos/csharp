using Microsoft.VisualStudio.TestTools.UnitTesting;
using sortingAlgorithmsVisualizer_classLib.Model;

namespace sortingAlgorithmsVisualizer_UnitTests
{
    [TestClass]
    public class ModelTests //White-box testing
    {
        MainModel _model = new MainModel();

        [TestMethod]
        public void ModelInitialization()
        {
            Assert.AreEqual("InsertionSort", _model.sortingType);
            Assert.IsFalse(_model.algorithmIsRunning);
            Assert.AreEqual(1, _model.sortingSpeed);
        }

        [TestMethod]
        public void InputListInAGoodFormatTest()
        {
            Assert.IsFalse(_model.InputListInAGoodFormat(""));
            Assert.IsFalse(_model.InputListInAGoodFormat(","));
            Assert.IsFalse(_model.InputListInAGoodFormat(",1,1"));
            Assert.IsFalse(_model.InputListInAGoodFormat("1,1,"));
            Assert.IsFalse(_model.InputListInAGoodFormat("[]"));
            Assert.IsFalse(_model.InputListInAGoodFormat("1-2"));
            Assert.IsFalse(_model.InputListInAGoodFormat("[12]"));
            Assert.IsFalse(_model.InputListInAGoodFormat("[1,2]"));
            Assert.IsFalse(_model.InputListInAGoodFormat("[1-2-3]"));
            Assert.IsFalse(_model.InputListInAGoodFormat("[10-5]"));

            Assert.IsTrue(_model.InputListInAGoodFormat("1"));
            Assert.IsTrue(_model.InputListInAGoodFormat("123456"));
            Assert.IsTrue(_model.InputListInAGoodFormat("12,34,56"));
            Assert.IsTrue(_model.InputListInAGoodFormat("1,23,456,789"));
            Assert.IsTrue(_model.InputListInAGoodFormat("1,2,3           "));
            Assert.IsTrue(_model.InputListInAGoodFormat("   1,2,3"));
            Assert.IsTrue(_model.InputListInAGoodFormat("[10-20]"));
            Assert.IsTrue(_model.InputListInAGoodFormat("[1-1]"));
            Assert.IsTrue(_model.InputListInAGoodFormat("[1-5]         "));
            Assert.IsTrue(_model.InputListInAGoodFormat("   [1-5]"));
        }

        [TestMethod]
        public void SetAlgorithmToTest()
        {
            int SortingTypeChangedInvokes = 0;
            _model.SortingTypeChanged += ((object? _, string _) => SortingTypeChangedInvokes++);
            _model.SetAlgorithmTo("BubbleSort");
            Assert.AreEqual("BubbleSort", _model.sortingType);
            _model.SetAlgorithmTo("MergeSort");
            Assert.AreEqual("MergeSort", _model.sortingType);
            _model.SetAlgorithmTo("BogoSort");
            Assert.AreEqual("BogoSort", _model.sortingType);
            _model.SetAlgorithmTo("QuickSort");
            Assert.AreEqual("QuickSort", _model.sortingType);
            _model.SetAlgorithmTo("InsertionSort");
            Assert.AreEqual("InsertionSort", _model.sortingType);
            Assert.AreEqual(5,SortingTypeChangedInvokes);
        }

        //we can test the array after startsorting
    }
}