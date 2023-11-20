using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PROG301_Week7.Interfaces;
using PROG301_Week7.Models.Serializable;

namespace PROG301_Week7.Models
{
    public abstract class AerialVehicle : SerializableAerialVehicle
    {
        public AerialVehicle()
        {
            this.ID = Guid.NewGuid();
            this.Engine = new Engine();
            DefaultAltitudeChange = 1000;
            TypeString = this.GetType().Name;
        }

        public new virtual void StartEngine()
        {
            if(Engine == null) { throw new Exception("Null Engine"); }
            Engine.Start();
        }

        public new virtual void StopEngine()
        {
            if (Engine == null) { throw new Exception("Null Engine"); }
            Engine.Stop();
        }

        public void FlyUp()
        {
            CurrentAltitude += DefaultAltitudeChange;

            if(CurrentAltitude >= MaxAltitude) CurrentAltitude = MaxAltitude;
        }

        public new void FlyUp(int HowManyFeet)
        {
            //If HowMany feet is nagtive trow invaid operation exception
            if (HowManyFeet < 0) throw new InvalidOperationException("Can't FlyUp a negative amount");
            
            CurrentAltitude += HowManyFeet;

            if(CurrentAltitude >= MaxAltitude) CurrentAltitude = MaxAltitude;
        }

        public new void FlyDown()
        {
            CurrentAltitude -= DefaultAltitudeChange;

            if(CurrentAltitude <= 0) 
            {
                CurrentAltitude = 0;
                IsFlying = false;
            }
        }

        public new void FlyDown(int HowManyFeet)
        {
            if (HowManyFeet < 0) throw new InvalidOperationException("Can't FlyDown a negative amount");
            
            CurrentAltitude -= HowManyFeet;

            if(CurrentAltitude <= 0) 
            {
                CurrentAltitude = 0;
                IsFlying = false;
            }
        }

        public event Action<string>? TakeOffEvent;

        public virtual string TakeOff()
        {
            if (Engine == null) { throw new Exception("Null Engine"); }
            if (TakeOffEvent == null) { throw new Exception("Null TakeOffEvent"); }
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
        public new string getEngineStartedString()
        {
            if (Engine == null) { throw new Exception("Null Engine"); }
            return this.Engine.About();
        }

        public virtual string About()
        {
            string about = string.Format("This {0} has a max altitude of {1} ft. \nIt's current altitude is {2} ft. \n{3}", 
                this.ToString(), this.MaxAltitude.ToString(), this.CurrentAltitude, this.getEngineStartedString());
            return about;
        }

        public override int GetHashCode() => this.ID.GetHashCode();

        public string GetFileName() => $"{TypeString}_{ID}";
    }
}
