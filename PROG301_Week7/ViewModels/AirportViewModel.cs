
using PROG301_Week7.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using static PROG301_Week7.Utility;

namespace PROG301_Week7.ViewModels
{
    public class AirportViewModel : BaseViewModel
    {
        public ICommand SingleTakeOffCommand { get; set; }
        private bool CanSingleTakeOff(object parameter) => true;

        public ICommand MultiTakeOffCommand { get; set; }
        private bool CanMultiTakeOff(object parameter) => true;

        public ICommand SingleLandCommand { get; set; }
        private bool CanSingleLand(object parameter) => true;

        public ICommand MultiLandCommand { get; set; }
        private bool CanMultiLand(object parameter) => true;


        public AirportViewModel(Airport ap)
        {
            airport = ap;

            SingleTakeOffCommand = new BasicCommand(SingleTakeOff, CanSingleTakeOff);
            MultiTakeOffCommand = new BasicCommand(MultiTakeOff, CanMultiTakeOff);

            SingleLandCommand = new BasicCommand(SingleLand, CanSingleLand);
            MultiLandCommand = new BasicCommand(MultiLand, CanMultiLand);
        }


        public Airport airport;

        public ObservableCollection<AerialVehicle> Vehicles { get { return IEnumToObsCol(airport.Vehicles); } 
            set 
            { 
                airport.Vehicles = ObsColToList(value);
                RaisePropertyChangedEvent("Vehicles"); 
            }}
        public int MaxVehicles { get { return airport.MaxVehicles; } set { airport.MaxVehicles = value; RaisePropertyChangedEvent(); } }
        public string AirportCode { get { return airport.AirportCode; } set { airport.AirportCode = value; RaisePropertyChangedEvent(); } }


        public void LoadVehicles(List<AerialVehicle> _vehicles)
        {
            Vehicles = IEnumToObsCol(_vehicles);
        }


        private void SingleTakeOff(object parameter)
        {
            object[] parameters = (object[])parameter;
            string input = (string)parameters[0];
            ObservableCollection<AerialVehicle> departures = (ObservableCollection<AerialVehicle>)parameters[1];


            int value = Convert.ToInt16(input);

            AerialVehicle index = Vehicles[value];

            departures.Add(index);

            airport.TakeOff(index);

            RaisePropertyChangedEvent("Vehicles");
        }


        private void MultiTakeOff(object parameter)
        {
            ObservableCollection<AerialVehicle> departures = (ObservableCollection<AerialVehicle>)parameter;

            foreach ( var av in Vehicles )
            {
                departures.Add(av);
            }

            airport.AllTakeOff();

            RaisePropertyChangedEvent("Vehicles");
        }

        private void SingleLand(object parameter)
        {
            object[] parameters = (object[])parameter;
            string input = (string)parameters[0];
            ObservableCollection<AerialVehicle> arrivials = (ObservableCollection<AerialVehicle>)parameters[1];


            int value = Convert.ToInt16(input);

            AerialVehicle index = arrivials[value];

            if (Vehicles.Count + 1 > MaxVehicles)
            {
                return;
            }

            arrivials.Remove(index);

            airport.Land(index);

            RaisePropertyChangedEvent("Vehicles");
        }


        private void MultiLand(object parameter)
        {
            object[] parameters = (object[])parameter;
            string smin = (string)parameters[0];
            string smax = (string)parameters[1];
            ObservableCollection<AerialVehicle> arrivials = (ObservableCollection<AerialVehicle>)parameters[2];


            int min = Convert.ToInt16(smin);
            int max = Convert.ToInt16(smax);

            List<AerialVehicle> range = arrivials.Skip(min).Take(max).ToList();

            range.ForEach(av => arrivials.Remove(av));

            airport.Land(range);

            RaisePropertyChangedEvent("Vehicles");
        }
    }
}
