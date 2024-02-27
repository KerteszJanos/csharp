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

namespace shortingAlgorithmsVizualizer_wpf.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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
                arrayInputTextboxPlaceholderLabel.Content = "Write the integer array here in format: \"1,2,3,4,5\"";
            }
        }
    }
}