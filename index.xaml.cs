﻿using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
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
using WpfSample;

namespace WpfAppLogin
{
    /// <summary>
    /// index.xaml 的交互逻辑
    /// </summary>
    /// 
    
    public partial class index 
    {

        public index()
        {
            InitializeComponent();
            this.DataContext = new ViewModels(); //绑定ViewModel数据源
        }
    }
}

