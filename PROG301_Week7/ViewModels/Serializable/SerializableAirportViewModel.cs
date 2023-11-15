using PROG301_Week7.Interfaces;
using PROG301_Week7.Models;
using PROG301_Week7.Models.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PROG301_Week7.ViewModels.Serializable
{
    public class SerializableAirportViewModel : AirportViewModel
    {
        public SerializableAirportViewModel(IAirport iap) : base(iap)
        {
            SerializeXML = new BasicCommand(S_XML, CanSerializeXML);
            DeserializeXML = new BasicCommand(D_XML, CanDeserializeXML);

            SerializeJSON = new BasicCommand(S_JSON, CanSerializeJSON);
            DeserializeJSON = new BasicCommand(D_JSON, CanDeserializeJSON);
        }

        public ICommand SerializeXML { get; set; }
        private bool CanSerializeXML(object parameter) => true;

        public ICommand DeserializeXML { get; set; }
        private bool CanDeserializeXML(object parameter) => true;

        public ICommand SerializeJSON { get; set; }
        private bool CanSerializeJSON(object parameter) => true;

        public ICommand DeserializeJSON { get; set;}
        private bool CanDeserializeJSON(object parameter) => true;


        private void S_XML(object parameter)
        {
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
        }

        private void D_XML(object parameter) 
        {
            SerializedFile sf = UtilSerializer.GetFile
                (
                    "Serialized",
                    airport.GetFileName(),
                    "xml"
                );

            SerializableAirport ap = 
                UtilSerializer.XmlDeserialize<SerializableAirport>
                (sf.Contents ?? throw new ArgumentNullException(nameof(sf)));
        }

        private void S_JSON(object parameter) 
        {
            SerializedFile sf = new SerializedFile
                (
                    airport.GetFileName(),
                    "json",
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
        }

        private void D_JSON(object parameter)
        {
            SerializedFile sf = UtilSerializer.GetFile
                (
                    "Serialized",
                    airport.GetFileName(),
                    "json"
                );

            SerializableAirport ap = 
                UtilSerializer.JsonDeserialize<SerializableAirport> 
                (sf.Contents ?? throw new ArgumentNullException(nameof(sf)));
        }
    }
}
