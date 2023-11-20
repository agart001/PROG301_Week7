using PROG301_Week7.Interfaces;
using PROG301_Week7.Models;
using PROG301_Week7.Models.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using static PROG301_Week7.Utility;

namespace PROG301_Week7.ViewModels.Serializable
{
    public class SerializableAirportViewModel : AirportViewModel
    {
        public SerializableAirportViewModel(IAirport iap) : base(iap)
        {
            SerializeXML = new BasicCommand(S_XML, CanSerializeXML);
            SerializeJSON = new BasicCommand(S_JSON, CanSerializeJSON);
        }

        public ICommand SerializeXML { get; set; }
        private bool CanSerializeXML(object parameter) => true;

        public ICommand SerializeJSON { get; set; }
        private bool CanSerializeJSON(object parameter) => true;


        private void S_XML(object parameter)
        {
            object[] parameters = (object[])parameter ?? throw new ArgumentNullException(nameof(parameter));

            ObservableCollection<SerializableAirportViewModel> viewmodels = 
                parameters[0] as ObservableCollection<SerializableAirportViewModel> ??
                throw new ArgumentNullException(nameof(parameters));

            ObservableCollection<string> files = parameters[1] as ObservableCollection<string> ?? throw new ArgumentNullException(nameof(parameters));

            SerializableAirportViewModel viewmodel = parameters[2] as SerializableAirportViewModel ?? throw new ArgumentNullException(nameof(parameters));

            string selected_txt = parameters[3] as string ?? throw new ArgumentNullException(nameof(parameters));

            SerializedFile sf = new SerializedFile
                (
                    airport.GetFileName(),
                    "xml",
                    UtilSerializer.XmlSerialize(airport),
                    typeof(SerializableAirport)
                );

            UtilSerializer.SaveFile
                (
                    "Serialized", 
                    sf.Name ?? "default", 
                    sf.Extension ?? "txt", 
                    sf.Contents ?? ""
                );

            files.Add(UtilSerializer.GetPath("Serialized", sf));

            viewmodels.Remove(viewmodel);

            selected_txt = string.Empty;
        }

        private void S_JSON(object parameter) 
        {
            object[] parameters = (object[])parameter ?? throw new ArgumentNullException(nameof(parameter));

            ObservableCollection<SerializableAirportViewModel> viewmodels =
                parameters[0] as ObservableCollection<SerializableAirportViewModel> ??
                throw new ArgumentNullException(nameof(parameters));

            ObservableCollection<string> files = parameters[1] as ObservableCollection<string> ?? throw new ArgumentNullException(nameof(parameters));

            SerializableAirportViewModel viewmodel = parameters[2] as SerializableAirportViewModel ?? throw new ArgumentNullException(nameof(parameters));

            string selected_txt = parameters[3] as string ?? throw new ArgumentNullException(nameof(parameters));

            SerializedFile sf = new SerializedFile
                (
                    airport.GetFileName(),
                    "json",
                    UtilSerializer.JsonSerialize(airport),
                    typeof(SerializableAirport)
                );

            UtilSerializer.SaveFile
                (
                    "Serialized",
                    sf.Name ?? "default",
                    sf.Extension ?? "txt",
                    sf.Contents ?? ""
                );

            files.Add(UtilSerializer.GetPath("Serialized", sf));

            viewmodels.Remove(viewmodel);

            selected_txt = string.Empty;
        }
    }
}
