using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.WPF;

namespace WpfSample // livecharts 折线图数据设置
{
    public class ViewModels
    {
        
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };
        public ObservableCollection<Axis> XAxes { get; set; }
        public ObservableCollection<Axis> YAxes { get; set; }
        public ObservableCollection<double> XLables { get; set; }
        public ViewModels()
        {
             XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = "X轴",
                 //   Labels = new List<string> { "A", "B", "C", "D", "E" },
                    TextSize = 12,// 标签字体大小
                    NameTextSize = 14,// 标签坐标轴字体大小
                    NamePadding = new LiveChartsCore.Drawing.Padding(0, 10),  // 坐标轴名称的内边距
                    LabelsRotation = 45 //标签斜45度
                }
            };
            XLables = new ObservableCollection<double> { 1, 2, 3, 4, 5, 6, 7 };// 绑定横坐标标签

            YAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Name = "Y轴",
                    TextSize = 12,
                    NameTextSize = 14,
                    NamePadding = new LiveChartsCore.Drawing.Padding(0, 10),
                    MinLimit = 0,
                    MaxLimit = 10
                }
            };
        }
    }
}
