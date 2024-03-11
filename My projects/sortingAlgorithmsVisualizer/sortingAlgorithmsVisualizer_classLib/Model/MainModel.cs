using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmsVisualizer_classLib.Model
{
    public class MainModel
    {
        #region properties/fields
        public List<int> list { get; set; }
        private string sortingType; //set InsertionSort default in ctor
        public bool algorithmIsRunning { get; set; }

        private double sortingSpeed { get; set; }

        private int ComparisonCounter;
        private int ArrayAccesCounter;
        #endregion

        #region constructors
        public MainModel()
        {
            list = new List<int>();
            sortingType = "InsertionSort";
            algorithmIsRunning = false;
            sortingSpeed = 1;
        }
        #endregion

        #region public methods
        public bool InputListInAGoodFormat(string inputList) //if the string is "" returns false
        {
            bool isComma = true;

            int i = 0;
            while (i < inputList.Length && (Char.IsNumber(inputList[i]) || inputList[i] == ','))
            {
                if (inputList[i] == ',')
                {
                    if (isComma)
                    {
                        return false;
                    }
                    else
                    {
                        isComma = true;
                    }
                }
                else
                {
                    isComma = false;
                }
                i++;
            }
            return (i <= inputList.Length) && !isComma;
        }
        public void SetAlgorithmTo(string sortingType)
        {
            this.sortingType = sortingType;
            onSortingTypeChanged(sortingType);
        }

        public async void StartAlgorithm(string inputList)
        {
            //set default values
            list.Clear();
            ComparisonCounter = 0;
            OnComparisonCounterChanged(ComparisonCounter);
            ArrayAccesCounter = 0;
            OnArrayAccesCounterChanged(ArrayAccesCounter);
            algorithmIsRunning = true;

            //initialize list
            string[] inputLists = inputList.Split(',');
            for (int i = 0; i < inputLists.Length; i++)
            {
                list.Add(int.Parse(inputLists[i]));
            }
            OnListInitialised(list);
            switch (sortingType)
            {
                case "InsertionSort":
                    OnSortingSpeedChanged(sortingSpeed);
                    await Task.Delay((int)(500 * sortingSpeed));
                    await InsertionSort(list);
                    break;
                case "BubbleSort":
                    await Task.Delay((int)(500 * sortingSpeed));
                    await BubbleSort(list);
                    break;
                case "BogoSort":
                    await Task.Delay((int)(500 * sortingSpeed));
                    await BogoSort(list);
                    break;
                case "QuickSort":
                    await Task.Delay((int)(500 * sortingSpeed));
                    QuickSort(list);
                    break;
                default:
                    break;
            }
            algorithmIsRunning = false;
        }

        public void SlowDown()
        {
            if (sortingSpeed + 0.1 <= 2)
            {
                sortingSpeed += 0.1;
                OnSortingSpeedChanged(sortingSpeed);
            }
        }
        public void SpeedUp()
        {
            if (sortingSpeed - 0.1 > 0.01)
            {
                sortingSpeed -= 0.1;
                OnSortingSpeedChanged(sortingSpeed);
            }
        }
        #endregion

        #region private methods
        private async Task<List<int>> InsertionSort(List<int> inputinputArray)
        {
            for (int i = 0; i < inputinputArray.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    OnListItemChanged(new ListItemChangedEventArgs(j - 1, j, (inputinputArray[j - 1] > inputinputArray[j])));
                    if (inputinputArray[j - 1] > inputinputArray[j])
                    {
                        ComparisonCounter++;
                        OnComparisonCounterChanged(ComparisonCounter);
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);

                        int temp = inputinputArray[j - 1];
                        inputinputArray[j - 1] = inputinputArray[j];
                        inputinputArray[j] = temp;
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                    }
                    await Task.Delay((int)(1000 * sortingSpeed));
                }
            }
            return inputinputArray;
        }
        private async Task<List<int>> BubbleSort(List<int> inputinputArray)
        {
            int n = inputinputArray.Count;
            for (int i = 0; i < n - 1; i++)
            {

                for (int j = 0; j < n - i - 1; j++)
                {
                    OnListItemChanged(new ListItemChangedEventArgs(j, j + 1, (inputinputArray[j] > inputinputArray[j + 1])));
                    if (inputinputArray[j] > inputinputArray[j + 1])
                    {
                        ComparisonCounter++;
                        OnComparisonCounterChanged(ComparisonCounter);
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);

                        int tempVar = inputinputArray[j];
                        inputinputArray[j] = inputinputArray[j + 1];
                        inputinputArray[j + 1] = tempVar;
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                    }
                    await Task.Delay((int)(1000 * sortingSpeed));
                }
            }
            return inputinputArray;
        }

        private async Task<List<int>> BogoSort(List<int> inputinputArray)
        {
            bool ListisSorted(List<int> inputinputArray)
            {
                int i = 1;
                while (i < inputinputArray.Count && inputinputArray[i - 1] < inputinputArray[i])
                {
                    ComparisonCounter += 2;
                    OnComparisonCounterChanged(ComparisonCounter);
                    ArrayAccesCounter += 2;
                    OnArrayAccesCounterChanged(ArrayAccesCounter);
                    i++;
                }
                return i == inputinputArray.Count();
            }
            Random rnd = new Random();
            while (!ListisSorted(inputinputArray))
            {
                //Fisher–Yates shuffle
                int n = list.Count;
                int k;
                int temp;
                while (n > 1)
                {
                    ComparisonCounter++;
                    OnComparisonCounterChanged(ComparisonCounter);
                    n--;
                    k = rnd.Next(n + 1);
                    temp = list[k];
                    list[k] = list[n];
                    list[n] = temp;
                    ArrayAccesCounter += 2;
                    OnArrayAccesCounterChanged(ArrayAccesCounter);

                    OnListItemChanged(new ListItemChangedEventArgs(k, n, true));
                    await Task.Delay((int)(1000 * sortingSpeed));
                }
            }
            return inputinputArray;
        }

        private async void QuickSort(List<int> inputArray)
        {
            async Task<List<int>> SortArray(List<int> array, int leftIndex, int rightIndex)
            {
                var i = leftIndex;
                var j = rightIndex;
                var pivot = array[leftIndex];
                ArrayAccesCounter += 1;
                OnArrayAccesCounterChanged(ArrayAccesCounter);
                OnPivotChanged(leftIndex);

                while (i <= j)
                {
                    while (array[i] < pivot)
                    {
                        ComparisonCounter++;
                        OnComparisonCounterChanged(ComparisonCounter);
                        ArrayAccesCounter += 1;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                        i++;
                    }

                    while (array[j] > pivot)
                    {
                        ComparisonCounter++;
                        OnComparisonCounterChanged(ComparisonCounter);
                        ArrayAccesCounter += 1;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                        j--;
                    }

                    if (i <= j)
                    {
                        ComparisonCounter++;
                        OnComparisonCounterChanged(ComparisonCounter);
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                        OnListItemChanged(new ListItemChangedEventArgs(i, j, true));
                        await Task.Delay((int)(1000 * sortingSpeed));
                        i++;
                        j--;
                    }
                }

                if (leftIndex < j)
                {
                    ComparisonCounter++;
                    OnComparisonCounterChanged(ComparisonCounter);
                    await SortArray(array, leftIndex, j);
                }

                if (i < rightIndex)
                {
                    ComparisonCounter++;
                    OnComparisonCounterChanged(ComparisonCounter);
                    await SortArray(array, i, rightIndex);
                }

                return array;
            }
            var result = await SortArray(inputArray, 0, inputArray.Count - 1);
            OnPivotChanged(-1);
        }

        #endregion

        #region events/event methods
        public event EventHandler<string>? SortingTypeChanged;
        public event EventHandler<List<int>>? ListInitialised;
        public event EventHandler<ListItemChangedEventArgs>? ListItemChanged;
        public event EventHandler<double>? SortingSpeedChanged; //-1 if lower, 1 if higher
        public event EventHandler<int>? PivotChanged;
        public event EventHandler<string>? ComparisonCounterChanged;
        public event EventHandler<string>? ArrayAccesCounterChanged;
        private void onSortingTypeChanged(string sortingType)
        {
            SortingTypeChanged!.Invoke(this, sortingType);
        }
        private void OnListInitialised(List<int> list)
        {
            ListInitialised!.Invoke(this, list);
        }
        private void OnListItemChanged(ListItemChangedEventArgs e)
        {
            ListItemChanged!.Invoke(this, e);
        }
        private void OnSortingSpeedChanged(double sortingSpeed)
        {
            SortingSpeedChanged!.Invoke(this, sortingSpeed);
        }
        private void OnPivotChanged(int newPiv)
        {
            PivotChanged!.Invoke(this, newPiv);
        }
        private void OnComparisonCounterChanged(int value)
        {
            ComparisonCounterChanged!.Invoke(this, value.ToString());
        }
        private void OnArrayAccesCounterChanged(int value)
        {
            ArrayAccesCounterChanged!.Invoke(this, value.ToString());
        }
        #endregion
    }
}
