using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfAppLogin.Model;
using WpfSample;

namespace WpfAppLogin.VM
{

    public class SocketWpf
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 5000;
        private TcpClient _client;
        private NetworkStream _stream;
        private bool _disposed = true;

        public SocketWpf()
        {
            ConnectToServer();
        }

        public async Task  ConnectToServer()
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ServerIp, Port);
                _stream = _client.GetStream();
                MessageBox.Show("连接成功");
                ReceiveDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败:" + ex.Message);
            }
            
        }

        private async Task ReceiveDataAsync()
        {
            byte[] buffer = new byte[4096];

            while (true)
            {
                try
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);//获取字节流
                    if (bytesRead == 0) break; //服务器断开连接
                                               // 检查 bytesRead 是否是 sizeof(int) 的整数倍
                    if (bytesRead % sizeof(float) != 0)
                    {
                        MessageBox.Show("接收到的数据长度不是 float 的整数倍，无法转换");
                        continue;
                    }

                    float[] receivedArray = new float[bytesRead / sizeof(float)];
                    Buffer.BlockCopy(buffer, 0, receivedArray, 0, bytesRead);

                    ParseArrayFrame(buffer, out bool isValid);
                    if (_disposed)
                    {
                        _disposed = false;
                        MessageBox.Show("接收到数据" + string.Join(", ", receivedArray));
                        await SendDataAsync("接收到数据" + string.Join(", ", receivedArray));
                    }
                    ViewModelDev.sensorData = receivedArray; //数据传递给ViewModel，每隔10秒更新一次
                    UserPageLiveCharts._sensorData = receivedArray; //数据传递给ViewModel，每隔10秒更新一次
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("消息接受出错" + ex.Message);
                    break;
                }

            }
        }

        public async Task SendDataAsync(String message)
        {
            if (_stream == null)
            {
                MessageBox.Show("未连接到服务器");
                return;
            }
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(data, 0, data.Length);
            MessageBox.Show("Message sent.");
        }

        public float[] ParseArrayFrame(byte[] frameData, out bool isValid)
        {
            isValid = false;
            if (frameData == null || frameData.Length < 10)
            {
                return null;
            }
            try
            {
                using (MemoryStream ms = new MemoryStream(frameData))
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    byte[] header = reader.ReadBytes(4);
                    if (!header.SequenceEqual(new byte[] { 0x7E, 0x7E, 0x7E, 0x7E }))
                    {
                        return null;
                    }
                    byte[] identifier = reader.ReadBytes(2);
                    if (!identifier.SequenceEqual(new byte[] { 0x01, 0x00 }))
                    {
                        return null;
                    }
                    ushort payloadLength = reader.ReadUInt16();
                    // 检查剩余数据是否足够
                    if (reader.BaseStream.Length - reader.BaseStream.Position < payloadLength + 2)
                    {
                        return null;
                    }
                    //读取有效载荷
                    byte[] payload = reader.ReadBytes(payloadLength);
                    //读取并校验帧尾
                    byte[] footer = reader.ReadBytes(2);
                    if (!footer.SequenceEqual(new byte[] { 0x0D, 0x0A }))
                    {
                        return null;
                    }
                    int floatCount = payloadLength / sizeof(float);
                    float[] floatArray = new float[floatCount];
                    Buffer.BlockCopy(payload, 0, floatArray, 0, payloadLength);
                    isValid = true;
                    return floatArray;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("解析数据出错" + ex.Message);
                return null;
            }
        }
    }
}
