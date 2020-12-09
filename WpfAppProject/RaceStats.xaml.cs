using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppProject
{
    /// <summary>
    /// Interaction logic for RaceStatsWPF.xaml
    /// </summary>
    public partial class RaceStats : Window
    {
        public RaceStats()
        {
            InitializeComponent();
            Data.CurrentRace.DriversChanged += UpdateList;
        }

        public void UpdateList(DriversChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Laps.Items.Refresh();
                Timebroken.Items.Refresh();
            });
        }
    }
    }

