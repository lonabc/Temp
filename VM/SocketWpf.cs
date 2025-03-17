using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfAppLogin.VM
{
   
   public  class SocketWpf
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 5000;
        private TcpClient _client;
        private NetworkStream _stream;
        public SocketWpf()
        {
            ConnectToServer();
        }

        public  async void ConnectToServer()
        {
            try {
                _client = new TcpClient();
                await _client.ConnectAsync(ServerIp, Port);
                _stream = _client.GetStream();
                MessageBox.Show("连接成功");
            
            } catch (Exception ex)
            {
                MessageBox.Show("连接失败" + ex.Message);
            }
        }

        private async Task ReceiveDataAsync()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                }
            }
        }

        public async void SendDataAsync(String message1)
        {
            string message = "测试消息";
            if (_stream == null)
            {
                MessageBox.Show("未连接到服务器");
                return;
            }
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(data, 0, data.Length);
            MessageBox.Show("Message sent.");
        }
    }
}
