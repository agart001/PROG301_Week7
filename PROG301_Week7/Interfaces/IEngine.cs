using PROG301_Week7.Models;

namespace PROG301_Week7.Interfaces
{
    
    public interface IEngine
    {

        Engine? Engine { get; set;}

        void StartEngine();
        void StopEngine();

        string getEngineStartedString();

    }

}