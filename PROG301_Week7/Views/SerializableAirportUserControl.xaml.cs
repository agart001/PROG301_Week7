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

        private ObservableCollection<string>? serializedfiles;
        public ObservableCollection<string> SerializedFiles
        {
            get { return serializedfiles ?? throw new ArgumentNullException(nameof(serializedfiles)); }
            set
            {
                serializedfiles = value;
                RaisePropertyChangedEvent("SerializedFiles");
            }
        }

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

        private ObservableCollection<SerializableAirportViewModel>? airports;
        public ObservableCollection<SerializableAirportViewModel> Airports 
        {
            get { return airports ?? throw new ArgumentNullException(nameof(airports)); } 
            set
            {
                airports = value;
                RaisePropertyChangedEvent("Airports");
            }
        }

        private SerializableAirportViewModel? selectedSAPVM;
        public SerializableAirportViewModel SelectedSAPVM
        {
            get { return selectedSAPVM ?? throw new ArgumentNullException(nameof(selectedSAPVM)); }
            set
            {
                selectedSAPVM = value;
                RaisePropertyChangedEvent("SelectedSAPVM");
            }
        }

        public bool SelectedSAPVMLocked = false;

        private ObservableCollection<SerializableAirportViewModel> CreateAirports()
        {
            List<IAirport> aps = new List<IAirport>
            {
                new Airport("1a", 5, new List<AerialVehicle>{ new Airplane(), new Airplane() }),
                new Airport("2a", 5),
                new Airport("1b", 8, new List<AerialVehicle>{ new Airplane(), new Airplane() }),
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
            SerializedFiles = IEnumToObsCol(
                Directory.GetFiles(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Serialized"))
                );

            Airports = CreateAirports();

            selectedSAPVM = Airports.FirstOrDefault();

            DeserializeJSON = new BasicCommand(D_JSON, CanDeserializeJSON);
            SerializeJSON = new BasicCommand(S_JSON, CanSerializeJSON);

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


        public ICommand DeserializeJSON { get; set; }
        private bool CanDeserializeJSON(object parameter) => true;

        private void D_JSON(object parameter)
        {
            string path = SelectedFile;

            string file = System.IO.Path.GetFileNameWithoutExtension(path);

            SerializedFile sf = UtilSerializer.GetFile
                (
                    "Serialized",
                    file,
                    "json"
                );

            Airport ap =
                UtilSerializer.JsonDeserialize<Airport>
                (sf.Contents ?? throw new ArgumentNullException(nameof(sf)));

            UtilSerializer.DeleteFile("Serialized", sf);

            tb_SerializedFile.Text = "";

            SerializedFiles.Remove(UtilSerializer.GetPath("Serialized", sf));

            SelectedFile = SerializedFiles.FirstOrDefault();
            SelectedFileLocked = false;

            Airports.Add(new SerializableAirportViewModel(ap));
        }


        public ICommand SerializeJSON { get; set; }
        private bool CanSerializeJSON(object parameter) => true;

        private void S_JSON(object parameter)
        {

            SerializedFile sf = new SerializedFile
                (
                    SelectedSAPVM.airport.GetFileName(),
                    "json",
                    UtilSerializer.JsonSerialize(SelectedSAPVM.airport),
                    typeof(SerializableAirport)
                );

            UtilSerializer.SaveFile
                (
                    "Serialized",
                    sf.Name ?? "default",
                    sf.Extension ?? "txt",
                    sf.Contents ?? ""
                );

            SerializedFiles.Add(UtilSerializer.GetPath("Serialized", sf));

            Airports.Remove(SelectedSAPVM);

            SelectedSAPVM = airports.FirstOrDefault();
            SelectedSAPVMLocked = false;

            tb_SerializedFile.Text = "";
        }
    }
}
