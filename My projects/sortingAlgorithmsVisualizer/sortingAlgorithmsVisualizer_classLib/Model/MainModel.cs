using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

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
        private int ElapsedTimeCounter;
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
            inputList = inputList.Trim();

            if (inputList == null || inputList.Length == 0)
            {
                return false;
            }
            if (inputList[0] == '[')
            {
                if (inputList[inputList.Length - 1] == ']' && inputList.Length >= 5) // 5 because [x-y] is minimum 5 character
                {
                    string[] splittedList = inputList.Split('-');
                    if (splittedList.Length != 2)
                    {
                        return false;
                    }
                    return int.TryParse(splittedList[0].Substring(1), out int fst) && int.TryParse(splittedList[1].Substring(0, splittedList[1].Length - 1), out int snd) && fst < snd;
                    //                                /\                                                /\                                                                       /\
                    //              returns true if the first string is an int       returns true if the first string is an int                      returns true if the first string is smaller than the second
                }
                return false;
            }

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
            inputList = inputList.Trim();

            //set values to default
            list.Clear();
            ComparisonCounter = 0;
            OnComparisonCounterChanged(ComparisonCounter);
            ArrayAccesCounter = 0;
            OnArrayAccesCounterChanged(ArrayAccesCounter);
            ElapsedTimeCounter = 0;
            OnElapsedSecondsChanged(ElapsedTimeCounter);

            //initialize list
            if (inputList[0] == '[')
            {
                string[] splittedList = inputList.Split('-');
                int.TryParse(splittedList[0].Substring(1), out int startValue);
                int.TryParse(splittedList[1].Substring(0, splittedList[1].Length - 1), out int endValue);

                for (int i = startValue; i <= endValue; i++)
                {
                    list.Add(i);
                }

                //Fisher–Yates shuffle
                Random rnd = new Random();
                int n = list.Count;
                int k;
                int temp;
                while (n > 1)
                {
                    n--;
                    k = rnd.Next(n + 1);
                    temp = list[k];
                    list[k] = list[n];
                    list[n] = temp;
                }
            }
            else
            {
                string[] inputLists = inputList.Split(',');
                for (int i = 0; i < inputLists.Length; i++)
                {
                    list.Add(int.Parse(inputLists[i]));
                }
            }
            OnListInitialised(list); // algorithmIsRunning = true; needs to be after this, bc we Investigate if an algorithm is running when check input format
            algorithmIsRunning = true;
            OnAlgorithmIsRunningChanged(algorithmIsRunning);
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
                    await QuickSort(list);
                    break;
                default:
                    break;
            }
            algorithmIsRunning = false;
            OnAlgorithmIsRunningChanged(algorithmIsRunning);
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

        public void timeGoesBy()
        {
            ElapsedTimeCounter++;
            OnElapsedSecondsChanged(ElapsedTimeCounter);
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
                        int temp = inputinputArray[j - 1];
                        inputinputArray[j - 1] = inputinputArray[j];
                        inputinputArray[j] = temp;
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                    }
                    ComparisonCounter++;
                    OnComparisonCounterChanged(ComparisonCounter);
                    ArrayAccesCounter += 2;
                    OnArrayAccesCounterChanged(ArrayAccesCounter);
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
                        int tempVar = inputinputArray[j];
                        inputinputArray[j] = inputinputArray[j + 1];
                        inputinputArray[j + 1] = tempVar;
                        ArrayAccesCounter += 2;
                        OnArrayAccesCounterChanged(ArrayAccesCounter);
                    }
                    ComparisonCounter++;
                    OnComparisonCounterChanged(ComparisonCounter);
                    ArrayAccesCounter += 2;
                    OnArrayAccesCounterChanged(ArrayAccesCounter);
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
                while (i < inputinputArray.Count && inputinputArray[i - 1] <= inputinputArray[i])
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

        private async Task<int> QuickSort(List<int> inputArray)
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
                    ComparisonCounter++;
                    OnComparisonCounterChanged(ComparisonCounter);
                }

                if (leftIndex < j)
                {
                    await SortArray(array, leftIndex, j);
                }
                ComparisonCounter++;
                OnComparisonCounterChanged(ComparisonCounter);

                if (i < rightIndex)
                {
                    await SortArray(array, i, rightIndex);
                }
                ComparisonCounter++;
                OnComparisonCounterChanged(ComparisonCounter);

                return array;
            }
            var result = await SortArray(inputArray, 0, inputArray.Count - 1);
            OnPivotChanged(-1);
            return 0;
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
        public event EventHandler<string>? ElapsedSecondsChanged;

        //events for the App.xaml.cs
        public event EventHandler<bool>? AlgorithmIsRunningChanged;

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
        private void OnElapsedSecondsChanged(int value)
        {
            ElapsedSecondsChanged!.Invoke(this, value.ToString());
        }
        private void OnAlgorithmIsRunningChanged(bool value)
        {
            AlgorithmIsRunningChanged!.Invoke(this, value);
        }
        #endregion
    }
}
