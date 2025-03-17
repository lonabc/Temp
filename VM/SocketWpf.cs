using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfAppLogin.Model;

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
                ReceiveDataAsync();
            } catch (Exception ex)
            {
                MessageBox.Show("连接失败" + ex.Message);
            }
        }

        private async Task ReceiveDataAsync()
        {
            byte[] buffer = new byte[4096];

            while (true)
            {
                try
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; //服务器断开连接
                                               // 检查 bytesRead 是否是 sizeof(int) 的整数倍
                    if (bytesRead % sizeof(int) != 0)
                    {
                        MessageBox.Show("接收到的数据长度不是 int 的整数倍，无法转换");
                        continue;
                    }
                    // 将 byte[] 转换为 int[]
                    //int[] receivedArray = new int[bytesRead / sizeof(int)];
                    //Buffer.BlockCopy(buffer, 0, receivedArray, 0, bytesRead);

                    //Console.WriteLine("Received array from server: " + string.Join(", ", receivedArray));
                    ViewModelDev.valueTemp = BitConverter.ToInt32(buffer, 0); 

                    //string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);//将字节流转换为字符数据
                  
                } catch (Exception ex)
                {
                    MessageBox.Show("消息接受出错"+ex.Message);
                    break;
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
