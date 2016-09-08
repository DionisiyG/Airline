using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    interface IFlight
    {
        FlightType TypeFlight { get; set; }
        int FlightNumber { get; set; }
        DateTime FlightDT { get; set; }
        string PortCityTitle { get; set; }
        byte TerminalNumber { get; set; }
        FlightStatus StatusFlight { get; set; }
        decimal BusinessTicketPrice { get; }
        decimal EconomTicketPrice { get; }

        Passenger[] Passengers { get; set; }
        decimal EconomyTicketPrice { get; }
    }
}
