﻿using System;
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
            list.Clear();
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
                    algorithmIsRunning = false;
                    break;
                case "BubbleSort":
                    algorithmIsRunning = true;
                    await Task.Delay((int)(500 * sortingSpeed));
                    await BubbleSort(list);
                    algorithmIsRunning = false;
                    break;
                default:
                    break;
            }
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
        private async Task<List<int>> InsertionSort(List<int> inputArray)
        {
            for (int i = 0; i < inputArray.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    OnListItemChanged(new ListItemChangedEventArgs(j - 1, j, (inputArray[j - 1] > inputArray[j])));
                    if (inputArray[j - 1] > inputArray[j])
                    {
                        int temp = inputArray[j - 1];
                        inputArray[j - 1] = inputArray[j];
                        inputArray[j] = temp;
                    }
                    await Task.Delay((int)(1000 * sortingSpeed));
                }
            }
            return inputArray;
        }
        private async Task<List<int>> BubbleSort(List<int> inputArray)
        {
            int n = inputArray.Count;
            for (int i = 0; i < n - 1; i++)
            {

                for (int j = 0; j < n - i - 1; j++)
                {
                    OnListItemChanged(new ListItemChangedEventArgs(j, j + 1, (inputArray[j] > inputArray[j + 1])));
                    if (inputArray[j] > inputArray[j + 1])
                    {
                        int tempVar = inputArray[j];
                        inputArray[j] = inputArray[j + 1];
                        inputArray[j + 1] = tempVar;
                    }
                    await Task.Delay((int)(1000 * sortingSpeed));
                }
            }
            return inputArray;
        }
        #endregion

        #region events/event methods
        public event EventHandler<string>? SortingTypeChanged;
        public event EventHandler<List<int>>? ListInitialised;
        public event EventHandler<ListItemChangedEventArgs>? ListItemChanged;
        public event EventHandler<double>? SortingSpeedChanged; //-1 if lower, 1 if higher

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
        #endregion
    }
}
