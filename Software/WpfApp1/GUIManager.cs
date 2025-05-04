using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1;

namespace Presentation_Layer
{
    public class GUIManager
    {
        public static MainWindow MainWindow { get; set; }

        private static UserControl oldContent;

        public static void Open(UserControl newContent)
        {
            oldContent = MainWindow.contentPanel.Content as UserControl;
            MainWindow.contentPanel.Content = newContent;
        }

        public static void Close() 
        { 
            Open(oldContent);
        }
    }
}
