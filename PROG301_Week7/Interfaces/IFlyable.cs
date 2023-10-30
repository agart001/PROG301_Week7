namespace PROG301_Week7.Interfaces
{
    
    public interface IFlyable
    {
        bool IsFlying { get; set;}

        int CurrentAltitude { get; set;}
        int MaxAltitude { get; set;}
        int DefaultAltitudeChange { get; set;}

        void FlyUp();
        void FlyUp(int HowManyFeet);

        void FlyDown();
        void FlyDown(int HowManyFeet);

    }

}