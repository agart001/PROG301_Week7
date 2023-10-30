using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PROG301_Week7.Interfaces;

namespace PROG301_Week7.Models
{
    public abstract class AerialVehicle : IFlyable, IEngine
    {
        // Private backing fields
        private Engine engine;
        private bool isFlying;
        private int maxAltitude;
        private int currentAltitude;
        private int defaultAltitudeChange;
        private string typeString;

        // Public properties with getters and setters using the backing fields
        public Engine Engine { get => engine; set => engine = value; }
        public bool IsFlying { get => isFlying; set => isFlying = value; }
        public int MaxAltitude { get => maxAltitude; set => maxAltitude = value; }
        public int CurrentAltitude { get => currentAltitude; set => currentAltitude = value; }
        public int DefaultAltitudeChange { get => defaultAltitudeChange; set => defaultAltitudeChange = value; }
        public string TypeString { get => typeString; set => typeString = value; }



        public AerialVehicle()
        {
            this.Engine = new Engine();
            DefaultAltitudeChange = 1000;
            TypeString = this.GetType().Name;
        }

        public virtual void StartEngine()
        {
            Engine.Start();
        }

        public virtual void StopEngine()
        {
            Engine.Stop();
        }

        public void FlyUp()
        {
            CurrentAltitude += DefaultAltitudeChange;

            if(CurrentAltitude >= MaxAltitude) CurrentAltitude = MaxAltitude;
        }

        public void FlyUp(int HowManyFeet)
        {
            //If HowMany feet is nagtive trow invaid operation exception
            if (HowManyFeet < 0) throw new InvalidOperationException("Can't FlyUp a negative amount");
            
            CurrentAltitude += HowManyFeet;

            if(CurrentAltitude >= MaxAltitude) CurrentAltitude = MaxAltitude;
        }

        public void FlyDown()
        {
            CurrentAltitude -= DefaultAltitudeChange;

            if(CurrentAltitude <= 0) 
            {
                CurrentAltitude = 0;
                IsFlying = false;
            }
        }

        public void FlyDown(int HowManyFeet)
        {
            if (HowManyFeet < 0) throw new InvalidOperationException("Can't FlyDown a negative amount");
            
            CurrentAltitude -= HowManyFeet;

            if(CurrentAltitude <= 0) 
            {
                CurrentAltitude = 0;
                IsFlying = false;
            }
        }

        public event Action<string> TakeOffEvent;

        public virtual string TakeOff()
        {
            if (Engine.IsStarted)
            {
                IsFlying = true;
                string result = $"{this} is flying";
                OnTakeOffEvent(result);
                return result;
            }

            string cannotFlyMessage = $"{this} can't fly it's engine is not started.";
            OnTakeOffEvent(cannotFlyMessage);
            return cannotFlyMessage;
        }

        protected virtual void OnTakeOffEvent(string result)
        {
            TakeOffEvent?.Invoke(result);
        }

        /// <summary>
        /// Returns a string that describes if an engine is started
        /// </summary>
        /// <returns></returns>
        public string getEngineStartedString()
        {
            return this.Engine.About();
        }

        public virtual string About()
        {
            string about = string.Format("This {0} has a max altitude of {1} ft. \nIt's current altitude is {2} ft. \n{3}", 
                this.ToString(), this.MaxAltitude.ToString(), this.CurrentAltitude, this.getEngineStartedString());
            return about;
        }
    }
}
