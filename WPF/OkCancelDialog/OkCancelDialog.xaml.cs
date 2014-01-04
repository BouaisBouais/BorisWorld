using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF.OkCancelDialog
{
    /// <summary>
    /// Interaction logic for OKCancelDialog.xaml
    /// </summary>
    public partial class OKCancelDialog : Window
    {
        public string MessageInfo
        {
            get { return LabelInfo.Text; }
            set { LabelInfo.Text = value; }
        }

        public OKCancelDialog(String label)
        {
            InitializeComponent();
            MessageInfo = label;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}