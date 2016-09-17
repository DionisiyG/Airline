using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Printer
    {
        private static void PrintTableHeader(byte mode)
        {
            if (mode == 0)
            {
                Console.WriteLine("Flights information:");
            }
            else if (mode == 1)
            {
                Console.WriteLine("Passenger information:");
            }
            string[] headTable = new string[]
            {
                string.Format("| {0, -3}| {1,-12}| {2,-10}| {3,-20}| {4,-25}| {5,-10}| {6,-15}| {7, -10} |", 
                                    "ID", "Flight type", "Flight #", "Date and Time", "City/Port", "Terminal #", "Status", "Passenger"),
                string.Format("| {0, -3}| {1,-20}| {2,-20}| {3,-20}| {4,-13}| {5,-12}| {6,-10}| {7, -22} |", 
                                    "ID", "First name", "Last name", "Nationality", "Passport", "Birthday", "Gender", "Ticket")
            };
            Console.ForegroundColor = ConsoleColor.Green;
            PrintFooter(mode);
            Console.WriteLine(headTable[mode]);
            PrintFooter(mode);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void PrintFooter(byte mode)
        {
            int length = 0;
            if (mode == 0)
            {
                length = 123;
            }
            else if (mode == 1)
            {
                length = 138;
            }
            Console.WriteLine(new string('-', length));
        }

        public static void PrintAllFlights(IFlight[] flights)
        {
            PrintTableHeader(0);

            if (flights[0] == null)
            {
                Console.WriteLine("{0, -122}{1}", "| Sorry, there is no flights.", "|");
            }
            else
            {
                for (int i = 0; i < flights.Length; i++)
                {
                    if (flights[i] != null)
                    {
                        Console.WriteLine("| {0, -3}{1}", i.ToString(), flights[i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            PrintFooter(0);
        }

        public static void PrintFlightPassengers(Passenger[] passengers)
        {
            PrintTableHeader(1);
            if (passengers[0] == null)
            {
                Console.WriteLine("{0, -137}{1}", "| Sorry, there is no passengers", "|");
            }
            else
            {
                for (int i = 0; i < passengers.Length; i++)
                {
                    if (passengers[i] != null)
                    {
                        Console.WriteLine("| {0, -3}{1}", i.ToString(), passengers[i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            PrintFooter(1);
        }

    }
}
