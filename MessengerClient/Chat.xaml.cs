using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessengerClient
{
    public partial class Chat : Window
    {
        string URL = "http://localhost:5000";
        int pos = 0;
        string Username;
        string msg;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public Chat()
        {
            InitializeComponent();
            Username = Microsoft.VisualBasic.Interaction.InputBox("Введите имя пользователя", "Введите имя пользователя", "");
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e)
        {
            var client = new RestClient(URL);
            var request = new RestRequest("api/getMessage/"+pos, Method.GET);
            try
            {
                var getresult = client.Execute(request);
                msg = getresult.Content.Trim('\"');
                if (msg != "Not found")
                {
                    ChatWindow.AppendText(msg + '\n');
                    pos++;
                }
            }
            catch
            {
                MessageBox.Show("Соединение с сервером прервано!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(URL);
            var request = new RestRequest("/api/sendmessage", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            MessageClass sendmsg = new MessageClass { userName = Username, messageText = SendMsgBox.Text, timeStamp = DateTime.Now.ToString() };
            request.AddBody(sendmsg);
            client.Execute(request);
            SendMsgBox.Text = "Введите сюда сообщение...";
        }
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChatWindow.ScrollToEnd();
        }
    }
}
