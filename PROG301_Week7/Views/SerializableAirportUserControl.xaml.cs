using PROG301_Week7.Interfaces;
using PROG301_Week7.Models;
using PROG301_Week7.Models.Serializable;
using PROG301_Week7.ViewModels.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class SerializableAirportUserControl : UserControl, INotifyPropertyChanged
    {
        public string[] SerializedFiles { get; set; }

        private string selfi = "";
        public string SelectedFile
        {
            get
            {
                return selfi;
            }
            set
            {
                selfi = value;
                RaisePropertyChangedEvent("SelectedFile");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChangedEvent([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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
            SerializedFiles = Directory.GetFiles(System.IO.Path.Combine(Directory.GetCurrentDirectory(),"Serialized"));

            Airports = CreateAirports();

            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView ?? throw new ArgumentNullException(nameof(sender));
            SelectedFile = lv.SelectedItem as string ?? throw new ArgumentNullException(nameof(lv));
        }
    }
}
