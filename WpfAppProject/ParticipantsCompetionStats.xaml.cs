using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ParticipantsCompetionStats.xaml
    /// </summary>
    public partial class ParticipantsCompetionStats : Window 
    {
        
        public ParticipantsCompetionStats()
        {
            InitializeComponent();
            Data.CurrentRace.DriversChanged += UpdateList;     
        }

        public void UpdateList(DriversChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                myListView.Items.Refresh();
});
        }
    }
}
