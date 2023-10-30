using PROG301_Week7.Models;
using PROG301_Week7.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG301_Week7.Views
{
    /// <summary>
    /// Interaction logic for AirportUserControl.xaml
    /// </summary>
    public partial class AirportUserControl : UserControl
    {
        public AirportViewModel ap_vm { get; set; }
        public ObservableCollection<AerialVehicle> Arrivials { get; set; }
        public ObservableCollection<AerialVehicle> Departures { get; set; }

        public AirportUserControl()
        {
            InitializeComponent();
        }

        private void uc_airport_Loaded(object sender, RoutedEventArgs e)
        {
            Arrivials = new ObservableCollection<AerialVehicle>
            {
                new Airplane(),
                new Airplane(),
                new Airplane(),
            };

            Departures = new ObservableCollection<AerialVehicle>();

            ap_vm = new AirportViewModel
                (
                    new Airport
                    (
                        "AF8B95",
                        10,
                        new List<AerialVehicle>()
                    )
                );

            ap_vm.LoadVehicles
                (
                new List<AerialVehicle> { new Airplane(), new Airplane(), new Airplane() }
                );

            UpdateDataContext();
        }

        private void UpdateDataContext()
        {
            dtgrd_arrivials.ItemsSource = Arrivials;
            dtgrd_departures.ItemsSource = Departures;

            grd_AP.DataContext = ap_vm;
            grd_Buttons.DataContext = ap_vm;
        }
    }
}
