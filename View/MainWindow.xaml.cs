using System.Windows;
using System.Windows.Input;

namespace TelegramBotWpfSB2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TelegramClient _client;
        private LogsWindow _logsWindow;
        public MainWindow()
        {
            InitializeComponent();

            _client = new TelegramClient(this);

            logList.ItemsSource = _client.BotMessageLog;
        }

        private void ButtonSentClick(object sender, RoutedEventArgs e)
        {
            if (textMessageSent.Text != "")
            {
                _client.SendMessage(textMessageSent.Text, TargetSend.Text);
                textMessageSent.Text = null;
            }
            else
            {
                MessageBox.Show("Нельзя отправлять пустое сообщение", "Пустое сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextMessageSentKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (textMessageSent.Text != "")
                {
                    _client.SendMessage(textMessageSent.Text, TargetSend.Text);
                    textMessageSent.Text = null;
                }
                else
                {
                    MessageBox.Show("Нельзя отправлять пустое сообщение", "Пустое сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void MessageLogButtonClick(object sender, RoutedEventArgs e)
        {
            _logsWindow = new LogsWindow();
            _logsWindow.Show();
        }
    }
}
