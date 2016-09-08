using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string PassportPassenger { get; set; }
        public DateTime Birthday { get; set; }
        public Gender PassengerGender { get; set; }
        public Ticket FlightTicket { get; set; }

        public override string ToString()
        {
            return string.Format("| {0,-20}| {1,-20}| {2,-20}| {3,-13}| {4,-12: dd-mm-yyyy}| {5,-10}| {6, -22} |",
                FirstName, LastName, Nationality, PassportPassenger, Birthday.ToString("dd:MM:yyyy"), PassengerGender, FlightTicket);
        }

    }
}
