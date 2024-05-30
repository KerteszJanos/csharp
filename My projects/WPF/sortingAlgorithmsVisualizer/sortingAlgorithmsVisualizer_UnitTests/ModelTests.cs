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
            Assert.AreEqual(5, SortingTypeChangedInvokes);
        }

        [TestMethod]
        public void StartAlgorithmTest()
        {
            bool ComparisonCounterChangedRegistered = false;
            bool ArrayAccesCounterChangedRegistered = false;
            bool ElapsedSecondsChangedRegistered = false;
            bool ListInitialisedRegistered = false;
            bool AlgorithmIsRunningChangedRegistered = false;
            _model.ComparisonCounterChanged += (object? _, string _) => { ComparisonCounterChangedRegistered = true; };
            _model.ArrayAccesCounterChanged += (object? _, string _) => { ArrayAccesCounterChangedRegistered = true; };
            _model.ElapsedSecondsChanged += (object? _, string _) => { ElapsedSecondsChangedRegistered = true; };
            _model.ListInitialised += (object? _, List<int> _) => { ListInitialisedRegistered = true; };
            _model.AlgorithmIsRunningChanged += (object? _, bool _) => { AlgorithmIsRunningChangedRegistered = true; };

            _model.StartAlgorithm("5,2,4,1,3");
            //Assert.IsTrue(ListisSorted(_model.list));

            Assert.IsTrue(ComparisonCounterChangedRegistered);
            Assert.IsTrue(ArrayAccesCounterChangedRegistered);
            Assert.IsTrue(ElapsedSecondsChangedRegistered);
            Assert.IsTrue(ListInitialisedRegistered);
            Assert.IsTrue(AlgorithmIsRunningChangedRegistered);
        }

        private bool ListisSorted(List<int> inputinputArray)
        {
            int i = 1;
            while (i < inputinputArray.Count && inputinputArray[i - 1] <= inputinputArray[i])
            {
                i++;
            }
            return i == inputinputArray.Count();
        }

        [TestMethod]
        public void StartAlgorithmWithInsertionSortTest()
        {
            //setted back in SetAlgorithmToTest to InsertionSort so no need here

            _model.ComparisonCounterChanged += (object? _, string _) => { }; //to awoid nullreference exceptions
            _model.ArrayAccesCounterChanged += (object? _, string _) => { };
            _model.ElapsedSecondsChanged += (object? _, string _) => { };
            _model.ListInitialised += (object? _, List<int> _) => { };
            _model.AlgorithmIsRunningChanged += (object? _, bool _) => { };

            bool ListItemChangedRegistered = false;
            _model.ListItemChanged += (object? _, ListItemChangedEventArgs _) => { ListItemChangedRegistered = true; };

            _model.sortingSpeed = 0; //set sorting speed 0, so we dont have to wait the Task.Delay-s
            _model.StartAlgorithm("5,2,4,1,3");
            Assert.IsTrue(ListItemChangedRegistered);
            Assert.IsTrue(ListisSorted(_model.list));
        }
        [TestMethod]
        public void StartAlgorithmWithBubbleSortTest()
        {
            _model.sortingType = "BubbleSort";

            _model.ComparisonCounterChanged += (object? _, string _) => { }; //to awoid nullreference exceptions
            _model.ArrayAccesCounterChanged += (object? _, string _) => { };
            _model.ElapsedSecondsChanged += (object? _, string _) => { };
            _model.ListInitialised += (object? _, List<int> _) => { };
            _model.AlgorithmIsRunningChanged += (object? _, bool _) => { };

            bool ListItemChangedRegistered = false;
            _model.ListItemChanged += (object? _, ListItemChangedEventArgs _) => { ListItemChangedRegistered = true; };

            _model.sortingSpeed = 0;
            _model.StartAlgorithm("5,2,4,1,3");
            Assert.IsTrue(ListItemChangedRegistered);
            Assert.IsTrue(ListisSorted(_model.list));
        }

        [TestMethod]
        public void StartAlgorithmWithBogoSortTest()
        {
            _model.sortingType = "BogoSort";

            _model.ComparisonCounterChanged += (object? _, string _) => { }; //to awoid nullreference exceptions
            _model.ArrayAccesCounterChanged += (object? _, string _) => { };
            _model.ElapsedSecondsChanged += (object? _, string _) => { };
            _model.ListInitialised += (object? _, List<int> _) => { };
            _model.AlgorithmIsRunningChanged += (object? _, bool _) => { };

            bool ListItemChangedRegistered = false;
            _model.ListItemChanged += (object? _, ListItemChangedEventArgs _) => { ListItemChangedRegistered = true; };

            _model.sortingSpeed = 0;
            _model.StartAlgorithm("5,2,4,1,3");
            Assert.IsTrue(ListItemChangedRegistered);
            Assert.IsTrue(ListisSorted(_model.list));
        }

        [TestMethod]
        public void StartAlgorithmWithQuickSortTest()
        {
            _model.sortingType = "QuickSort";

            _model.ComparisonCounterChanged += (object? _, string _) => { }; //to awoid nullreference exceptions
            _model.ArrayAccesCounterChanged += (object? _, string _) => { };
            _model.ElapsedSecondsChanged += (object? _, string _) => { };
            _model.ListInitialised += (object? _, List<int> _) => { };
            _model.AlgorithmIsRunningChanged += (object? _, bool _) => { };

            bool ListItemChangedRegistered = false;
            bool PivotChangedRegistered = false;
            _model.ListItemChanged += (object? _, ListItemChangedEventArgs _) => { ListItemChangedRegistered = true; };
            _model.PivotChanged += (object? _, int _) => { PivotChangedRegistered = true; };

            _model.sortingSpeed = 0;
            _model.StartAlgorithm("5,2,4,1,3");
            Assert.IsTrue(ListItemChangedRegistered);
            Assert.IsTrue(PivotChangedRegistered);
            Assert.IsTrue(ListisSorted(_model.list));
        }

        [TestMethod]
        public void SlowDownTest()
        {
            _model.sortingSpeed = 1;
            bool SortingSpeedChangedRegistered = false;
            _model.SortingSpeedChanged += (object? _, double _) => { SortingSpeedChangedRegistered = true; };

            _model.SlowDown();
            Assert.AreEqual(1.1, _model.sortingSpeed);
            Assert.IsTrue(SortingSpeedChangedRegistered);

            for (int i = 0; i < 10; i++)
            {
                _model.SlowDown();
            }
            Assert.AreEqual(1.9, double.Round(_model.sortingSpeed, 1));
        }
        [TestMethod]
        public void SpeedUpTest()
        {
            _model.sortingSpeed = 1;
            bool SortingSpeedChangedRegistered = false;
            _model.SortingSpeedChanged += (object? _, double _) => { SortingSpeedChangedRegistered = true; };

            _model.SpeedUp();
            Assert.AreEqual(0.9, _model.sortingSpeed);
            Assert.IsTrue(SortingSpeedChangedRegistered);

            for (int i = 0; i < 10; i++)
            {
                _model.SpeedUp();
            }
            Assert.AreEqual(0.1, double.Round(_model.sortingSpeed, 1));
        }
        [TestMethod]
        public void timeGoesByTest()
        {
            bool ElapsedSecondsChangedRegistered = false;
            _model.ElapsedSecondsChanged += (object? _, string _) => { ElapsedSecondsChangedRegistered = true; };

            _model.timeGoesBy();
            Assert.AreEqual(1, _model.ElapsedTimeCounter);
            Assert.IsTrue(ElapsedSecondsChangedRegistered);

            for (int i = 0; i < 10; i++)
            {
                _model.timeGoesBy();
            }
            Assert.AreEqual(11, _model.ElapsedTimeCounter);
        }
    }
}