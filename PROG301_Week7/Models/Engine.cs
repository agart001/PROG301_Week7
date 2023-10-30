using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROG301_Week7.Models
{
    public class Engine
    {
        public bool IsStarted;

        public Engine()
        {
            IsStarted = false;
        }

        public virtual void Start()
        {
            IsStarted =  true;
        }

        public virtual void Stop()
        {
            IsStarted = false;
        }

        public virtual string About()
        {
            string engineString = ToString() + " is not started.";
            if (IsStarted)
            {
                engineString = engineString.Replace("not ", "");
            }
            return engineString;
        }
    }
}
