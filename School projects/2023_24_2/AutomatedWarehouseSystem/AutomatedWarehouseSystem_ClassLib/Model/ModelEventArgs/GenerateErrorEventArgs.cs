using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the generated errors
    /// </summary>
    public class GenerateErrorEventArgs : EventArgs
    {
        private int _robot1id;
        private int _robot2id;
        private string _reason;

        /// <summary>
        ///  Robot1 Id getter/setter
        /// </summary>
        public int Robot1Id
        {
            get { return _robot1id; }
            set { _robot1id = value; }
        }
        /// <summary>
        ///  Robot2 Id getter/setter
        /// </summary>
        public int Robot2Id
        {
            get { return _robot2id; }
            set { _robot2id = value; }
        }
        /// <summary>
        ///  Error reason getter/setter
        /// </summary>
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        /// <summary>
        /// Generated error eventargs constructor
        /// </summary>
        public GenerateErrorEventArgs(int robot1id, int robot2id, string reason)
        {
            _robot1id = robot1id;
            _robot2id = robot2id;
            _reason = reason;
        }
    }

}
