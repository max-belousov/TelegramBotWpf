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

namespace TelegramBotWpfSB2.View
{
    /// <summary>
    /// Логика взаимодействия для LogsWindow.xaml
    /// </summary>
    public partial class LogsWindow : Window
    {
        private TelegramClient _client;
        public LogsWindow()
        {
            InitializeComponent();
            _client = new TelegramClient();
            logList.ItemsSource = _client.SentMessageLog();
        }
    }
}
