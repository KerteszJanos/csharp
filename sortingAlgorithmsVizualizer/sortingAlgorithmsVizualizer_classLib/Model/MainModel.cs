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
            return true; //need to add the logic
        }
        public void SetAlgorithmTo(string sortingType)
        {
            this.sortingType = sortingType;
        }
        #endregion
    }
}
