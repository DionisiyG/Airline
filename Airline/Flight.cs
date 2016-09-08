using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Flight : IFlight
    {
        public FlightType TypeFlight { get; set; }
        public int FlightNumber { get; set; }
        public DateTime FlightDT { get; set; }
        public string PortCityTitle { get; set; }
        public byte TerminalNumber { get; set; }
        public FlightStatus StatusFlight { get; set; }
        public decimal BusinesTicketPrice { get; }
        public decimal EconomTicketPrice { get; }

        public Passenger[] Passengers { get; set; } = new Passenger[20];

        public Flight()
        {
            Random rnd = RandomProvider.GetThreadRandom();
            double economyPrice = 0;
            double businesPrice = 0;

            economyPrice = rnd.Next(10, 100) + rnd.NextDouble();
            economyPrice = Math.Round(economyPrice, 2);
            EconomTicketPrice = (decimal)economyPrice;

            businesPrice = rnd.Next(100, 500) + rnd.NextDouble();
            businesPrice = Math.Round(businesPrice, 2);
            BusinesTicketPrice = (decimal)businesPrice;

        }

        public override string ToString()
        {
            int count = 0;
            for (int i = 0; i < Passengers.Length; i++)
            {
                if (Passengers[i] != null)
                {
                    count++;
                }
            }
            return string.Format("| {0,-12}| {1,-10}| {2,-20}| {3,-25}| {4,-10}| {5,-15}| {6, -10} |", TypeFlight, FlightNumber, FlightDT, PortCityTitle, TerminalNumber, StatusFlight, count);
        }
    }
}
