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
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChangedEvent([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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

        public bool SelectedFileLocked = false;

        public ObservableCollection<SerializableAirportViewModel> Airports {  get; set; }

        private SerializableAirportViewModel selectedSAPVM;
        public SerializableAirportViewModel SelectedSAPVM
        {
            get { return selectedSAPVM; }
            set
            {
                selectedSAPVM = value;
                RaisePropertyChangedEvent("SelectedSAPVM");
            }
        }

        private SerializableAirport selectedsapvm_ap;
        public SerializableAirport SelectedSAPVM_AP
        {
            get { return selectedsapvm_ap; }
            set
            {
                selectedsapvm_ap = value;
                RaisePropertyChangedEvent("SelectedSAPVM_AP");
            }
        }

        public bool SelectedSAPVMLocked = false;

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

        private void lv_SerializedFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedFileLocked == false)
            {
                ListView lv = sender as ListView ?? throw new ArgumentNullException(nameof(sender));
                SelectedFile = lv.SelectedItem as string ?? throw new ArgumentNullException(nameof(lv));
                tb_SerializedFile.Text = File.ReadAllText(SelectedFile);
            }
        }

        private void btn_SaveFile_Click(object sender, RoutedEventArgs e)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(SelectedFile);
            
            string extension = System.IO.Path.GetExtension(SelectedFile).Substring(1);
            UtilSerializer.SaveFile
                (
                    "Serialized",
                    filename,
                    extension,
                    tb_SerializedFile.Text
                );
        }

        private void btn_LockFile_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedFileLocked == false)
            { 
                SelectedFileLocked = true;
            }
            else
            { SelectedFileLocked = false; }
        }

        private void lv_SAPVMs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedSAPVMLocked == false)
            {
                ListView lv = sender as ListView 
                    ?? throw new ArgumentNullException(nameof(sender));
                SelectedSAPVM = lv.SelectedItem as SerializableAirportViewModel 
                    ?? throw new ArgumentNullException(nameof(lv));
                SelectedSAPVM_AP = (SerializableAirport)SelectedSAPVM.airport 
                    ?? throw new InvalidCastException(nameof(SelectedSAPVM.airport));
            }
        }

        private void btn_LockAirport_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedSAPVMLocked == false)
            {
                SelectedSAPVMLocked = true;
            }
            else 
            { SelectedSAPVMLocked = false; }
        }
    }
}
