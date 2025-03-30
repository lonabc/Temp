using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows;
using HandyControl.Data;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Extensions;

namespace WpfAppLogin.Model
{

    public class ViewModelDev : INotifyPropertyChanged
    {
        //饼状图数据
        private ObservableValue _valueTemp; // 动态温度值
        private ObservableValue _valueHumidity; // 动态湿度值
        private ObservableValue _valueNoise; // 动态噪音值

        public ObservableCollection<ISeries> SeriesYuan { get; set; }


        // 数据系列
        public ISeries[] Series { get; set; }


        public static int valueTemp;

        private  string _tempView="25°C";
        public string tempView
        {
            get { return _tempView; }
            set
            {
                if (_tempView != value)
                {
                    _tempView = value;
                    RaisePropertyChanged(nameof(tempView)); // 通知UI UserName 属性已更改
                }
            }
        }

      


        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // 横坐标轴
        public ObservableCollection<Axis> XAxes { get; set; }

        // 纵坐标轴
        public ObservableCollection<Axis> YAxes { get; set; }

        // 定时器，用于模拟实时更新
        private DispatcherTimer _timer;

        // 随机数生成器，用于生成随机数据点
        private Random _random;

        public ICommand MyCommand { get; set; }

        public ICommand DefaultCommand { get; set; }
        public ICommand Option1Command { get; set; }

        //分割按钮命令绑定
        private void ExecuteDefaultCommand(String mess)
        {
            MessageBox.Show("默认操作被执行！");
        }

        private void ExecuteOption1Command(String mess)
        {
            MessageBox.Show("操作 1 被执行！");
        }

        private void ExecuteMyCommand(String parameter)
        {
            MessageBox.Show($"命令绑定测试{parameter}");
        }
        private bool CanExecuteMyCommand()
        { 
            return true;
        }

        public ViewModelDev()
        {

            MyCommand = new RelayCommand<string>(ExecuteMyCommand);
            DefaultCommand = new RelayCommand<string>(ExecuteDefaultCommand);
            Option1Command = new RelayCommand<string>(ExecuteOption1Command);

            // 初始化饼状图动态值
            _valueTemp = new ObservableValue(50); // 初始温度值
            _valueHumidity = new ObservableValue(50); // 初始湿度值
            _valueNoise = new ObservableValue(95); // 初始噪音值

                                                   // 创建仪表盘
            SeriesYuan = new ObservableCollection<ISeries>(
                GaugeGenerator.BuildSolidGauge(
                    new GaugeItem(_valueHumidity, series => SetStyle("Environmental Humidity", series)),
                    new GaugeItem(_valueTemp, series => SetStyle("Environmental Temperature", series)),
                    new GaugeItem(_valueNoise, series => SetStyle("Environmental Noise", series)),
                    new GaugeItem(GaugeItem.Background, series =>
                    {
                        series.Fill = null;
                    })));




            // 初始化数据
            var values = new ObservableCollection<double>();
            var xLabels = new ObservableCollection<string>();

            // 创建系列
            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = values, // 绑定数据点
                    Fill = null // 不填充区域
                }
            };

            // 初始化横坐标轴
            XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Labels = xLabels, // 绑定横坐标标签
                    Labeler = value => value.ToString(), // 自定义标签显示格式
                    TextSize = 12, // 标签字体大小
                    NameTextSize = 14, // 坐标轴名称字体大小
                    NamePadding = new LiveChartsCore.Drawing.Padding(0, 10) // 坐标轴名称的内边距
                }
            };

            // 初始化纵坐标轴
            YAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = "Temperature", // 坐标轴名称
                    TextSize = 12, // 标签字体大小
                    NameTextSize = 14, // 坐标轴名称字体大小
                    NamePadding = new LiveChartsCore.Drawing.Padding(0, 10), // 坐标轴名称的内边距
                    MinLimit = 5, // Y轴最小值
                    MaxLimit = 30 // Y轴最大值
                }
            };

            // 初始化随机数生成器
            _random = new Random();

            // 设置定时器，实时更新
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 每秒更新一次
            };
            _timer.Tick += (sender, e) => OnTimerTick(values, xLabels);
            _timer.Start();
        }
        private void OnTimerTick(ObservableCollection<double> values, ObservableCollection<string> xLabels) //定时回调方法
        {
            
            // 添加新的横坐标标签（当前时间）
            string newX = DateTime.Now.ToString("HH:mm:ss"); // 当前时间
            xLabels.Add(newX);
          
             tempView = valueTemp.ToString()+ "°C";
            _valueTemp.Value = valueTemp; // 温度值
            // 添加新的数据点
            double smallTmep = _random.Next(10, 500); // 随机生成一个值
            smallTmep = smallTmep / 1000;
           
            double newValue = (double)valueTemp+smallTmep;
            values.Add(newValue);

            // 如果数据点太多，移除最旧的数据点
            if (xLabels.Count > 5)
            {
                xLabels.RemoveAt(0);
                values.RemoveAt(0);
            }
        }





        public static void SetStyle(string name, PieSeries<ObservableValue> series)
        {
            series.Name = name;
            series.DataLabelsSize = 10;
            series.DataLabelsPosition = PolarLabelsPosition.End;
            series.DataLabelsFormatter =
                    point => point.Coordinate.PrimaryValue.ToString();
            series.InnerRadius = 20;
            series.MaxRadialColumnWidth = 5;
        }

    }
}
