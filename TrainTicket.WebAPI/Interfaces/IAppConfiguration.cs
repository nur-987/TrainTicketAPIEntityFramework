namespace TrainTicket.API.Utility
{
    public interface IAppConfiguration
    {
        int FirstClassBasePrice { get; }
        int BusinessClassBasePrice { get; }
        int EconomyClassBasePrice { get; }
        double FirstClassDistanceMultiplier { get; }
        double EconomyClassDistanceMultiplier { get; }
        double BusinessClassDistanceMultiplier { get; }

        void Initialize(int firstClassBasePrice, int businessClassBasePrice, int economyClassBasePrice,
            double firstClassDistanceMultiplier, double businessClassDistanceMultiplier, double economyClassDistanceMultiplier);
    }
}