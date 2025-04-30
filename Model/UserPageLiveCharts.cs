using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;

namespace WpfSample // livecharts 折线图数据设置
{
    public class UserPageLiveCharts : INotifyPropertyChanged
    {
        private readonly Random _random = new();
        private readonly List<DateTimePoint> _values = new List<DateTimePoint>();
        private readonly DateTimeAxis _customAxis;

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
        public bool IsReading { get; set; } = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UserPageLiveCharts()
        {
            Series = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _values,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new[] { _customAxis };
            _ = ReadData(); // 启动数据更新
        }

        private async Task ReadData()
        {
            while (IsReading)
            {
                await Task.Delay(100);
                lock (Sync)
                {
                    _values.Add(new DateTimePoint(DateTime.Now, _random.Next(0, 10)));
                    if (_values.Count > 250) _values.RemoveAt(0);
                    _customAxis.CustomSeparators = GetSeparators();
                }
            }
        }

        private static double[] GetSeparators()
        {
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
            var secsAgo = (DateTime.Now - date).TotalSeconds;
            return secsAgo < 1 ? "now" : $"{secsAgo:N0}s ago";
        }
    }
}
