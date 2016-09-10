using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    enum FlightStatus : byte
    {
        CheckIn,
        GateClosed,
        Arrived,
        DepartedAt,
        Unknown,
        Canceled,
        ExpectedAt,
        Delayed,
        InFlight
    }
    enum FlightType : byte
    {
        Arrival,
        Departure
    }

}
