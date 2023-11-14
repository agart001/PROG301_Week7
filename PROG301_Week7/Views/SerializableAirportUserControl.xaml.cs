using PROG301_Week7.Interfaces;
using PROG301_Week7.Models;
using PROG301_Week7.Models.Serializable;
using PROG301_Week7.ViewModels.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
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
using static PROG301_Week7.Utility;


namespace PROG301_Week7.Views
{
    /// <summary>
    /// Interaction logic for SerializableAirportUserControl.xaml
    /// </summary>
    public partial class SerializableAirportUserControl : UserControl
    {
        public string[] SerializedFiles { get; set; }

        public ObservableCollection<SerializableAirportViewModel> Airports {  get; set; }

        private ObservableCollection<SerializableAirportViewModel> CreateAirports()
        {
            List<IAirport> aps = new List<IAirport>
            {
                new Airport("1a", 5),
                new Airport("2a", 5),
                new Airport("1b", 8),
                new Airport("2b", 8)
            };

            List<SerializableAirportViewModel> sapvms = new List<SerializableAirportViewModel>();

            foreach(Airport ap in aps)
            {
                sapvms.Add(new SerializableAirportViewModel(ap));
            }

            return IEnumToObsCol(sapvms);
        }

        public SerializableAirportUserControl()
        {
            SerializedFiles = Directory.GetFiles($"{Directory.GetCurrentDirectory}\\Serialized");

            Airports = CreateAirports();

            InitializeComponent();
        }
    }
}
