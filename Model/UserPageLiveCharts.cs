using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
using WpfAppLogin.Model;

namespace WpfSample // livecharts 折线图数据设置
{
    public class UserPageLiveCharts : INotifyPropertyChanged
    {
        private readonly Random _random = new();
        private readonly List<DateTimePoint> _values = new List<DateTimePoint>();
        private readonly DateTimeAxis _customAxis;
        public static float[] _sensorData=new float[3]; // 传感器数据
       

        private ObservableCollection<ISeries> _series;
        public ObservableCollection<ISeries> Series
        {
            get => _series;
            set
            {
                _series = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Series)));
            }
        }

        public Axis[] XAxes { get; set; }
        public object Sync { get; } = new object();
        private readonly object _dataLock = new object(); // 添加数据锁
        public bool IsReading { get; set; } = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UserPageLiveCharts()
        {
            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _values,  // 绑定数据源
                    Fill = null, //无填充颜色
                    GeometryFill = null,// 无数据点标记
                    GeometryStroke = null
                }
            };

            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),// 自定义刻度线
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new[] { _customAxis };

         
            _ =  ReadData(); // 启动数据更新
        }

        private async Task ReadData()
        {

          
            while (IsReading)
            {
                await Task.Delay(2000);
              
                float currentValue;
                lock (_dataLock) // 加锁确保数据一致性
                {
                    // 在循环内部获取最新数据，并进行四舍五入
                   currentValue = (float)((int)(_sensorData[0] * 100 + 0.5) / 100.0);
                 
                }

                lock (Sync)
                {
                  
                    _values.Add(new DateTimePoint(DateTime.Now,currentValue)); // 添加当前时间点
                    if (_values.Count > 10) _values.RemoveAt(0);
                    _customAxis.CustomSeparators = GetSeparators();  // 更新刻度
                }
            }
        }

        private static double[] GetSeparators() // 每5秒一个刻度
        {
            //刻度线固定在当前时间的 0, -5, -10, -15, -20, -25 秒位置。
            var now = DateTime.Now;
            return new double[]
            {
            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
            };
        }

        private static string Formatter(DateTime date)
        {
            //小于 1 秒显示 "now"，否则显示 "Xs ago"（如 "5s ago"）。
            var secsAgo = (DateTime.Now - date).TotalSeconds;
            return secsAgo < 1 ? "now" : $"{secsAgo:N0}s ago";
        }
    }
}
