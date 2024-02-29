using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmsVizualizer_classLib.Model
{
    public class MainModel
    {

        #region properties/fields
        public List<int> list { get; set; }
        private string sortingType; //set QuickSort default in ctor
        #endregion

        #region constructors
        public MainModel()
        {
            list = new List<int>();
            sortingType = String.Empty;
        }
        #endregion

        #region public methods
        public bool InputListInAGoodFormat(string inputList) //if the string is "" returns false
        {
            bool isComma = true;

            int i = 0;
            while(i < inputList.Length && (Char.IsNumber(inputList[i]) || inputList[i] == ','))
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
            return (i <= inputList.Length) && !isComma ;
        }
        public void SetAlgorithmTo(string sortingType)
        {
            this.sortingType = sortingType;
            onSortingTypeChanged(sortingType);
        }
        public void StartAlgorithm(string inputList)
        {
            list.Clear();
            string[] inputLists = inputList.Split(',');
            for (int i = 0; i < inputLists.Length; i++)
            {
                list.Add(int.Parse(inputList[i])); //some reaaly weird behaviour
            }
            list.Sort();
        }
        #endregion

        #region events/event methods
        public event EventHandler<string>? SortingTypeChanged;

        private void onSortingTypeChanged(string sortingType)
        {
            SortingTypeChanged!.Invoke(this, sortingType);
        }
        #endregion
    }
}
