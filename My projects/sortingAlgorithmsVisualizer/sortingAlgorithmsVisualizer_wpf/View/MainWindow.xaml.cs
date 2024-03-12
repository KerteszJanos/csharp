using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sortingAlgorithmsVisualizer_wpf.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructors
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region private methods
        /// <summary>
        /// Responsible for the placeholder that placed in the arrayInputTextbox.
        /// </summary>
        //(I put it here because the model or viewmodel doesn't need to know about this change)
        private void OnArrayInputTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (arrayInputTextbox.Text != "")
            {
                arrayInputTextboxPlaceholderLabel.Content = "";
            }
            else
            {
                arrayInputTextboxPlaceholderLabel.Content = "Format: 1,2,3,4,5 OR [1-5]";
            }
        }
        #endregion
    }
}