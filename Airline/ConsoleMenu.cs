using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    internal static class ConsoleMenu
    {
        internal static void GetFlightMenu(IFlight[] flights)
        {
            Console.Clear();
            Console.WriteLine("Flight menu.");
            Console.WriteLine("1. Create new flight");
            Console.WriteLine("2. Edit existed flight");
            Console.WriteLine("3. Delete flight");
            byte mode = 0;
            bool TryByte = byte.TryParse(Console.ReadLine(), out mode);
            if (!TryByte)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                switch (mode)
                {
                    case 1:
                        FlightManager.AddFlight(flights);
                        break;
                    case 2:
                        FlightManager.EditFlight(flights);
                        break;
                    case 3:
                        FlightManager.DeleteFlight(flights);
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
        }

        internal static void GetPassengerMenu(IFlight flight)
        {
            Console.WriteLine("Passenger menu");
            Console.WriteLine("1. Add new passenger");
            Console.WriteLine("2. Edit existed passenger");
            Console.WriteLine("3. Delete passenger");

            byte mode = 0; ;
            bool TryByte = byte.TryParse(Console.ReadLine(), out mode);

            if (!TryByte)
            {
                Console.WriteLine("Incorrect input");
            }
            else
                switch (mode)
                {
                    case 1:
                        PassengerManager.AddPassenger(flight);
                        break;
                    case 2:
                        PassengerManager.EditPassenger(flight);
                        break;
                    case 3:
                        PassengerManager.DeletePassenger(flight.Passengers);
                        break;

                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
        }

        internal static void OperatePassengerMenu(IFlight[] flights)
        {
            Console.Clear();
            Console.WriteLine("Available flights:");
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] == null)
                    break;
                Console.Write($"{flights[i].FlightNumber.ToString()}\t");
            }

            Console.WriteLine();

            Console.WriteLine("Enter the flight number to edit passenger`s information.");
            int flightNumber = 0;
            bool tryByte = int.TryParse(Console.ReadLine(), out flightNumber);
            if (!tryByte)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                IFlight flight = Searcher.SearchByFlightNumber(flights, flightNumber);
                if (flight != null)
                {
                    for (int i = 0; i < flights.Length; i++)
                    {
                        if (flight == flights[i])
                        {
                            GetPassengerMenu(flights[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Entered flight does not exist!");
                }
            }
        }

        internal static void PrintPassengerMenu(IFlight[] flights)
        {
            Console.Clear();
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    Console.WriteLine($"Flight number = {flights[i].FlightNumber}");
                    Printer.PrintFlightPassengers(flights[i].Passengers);
                }
                else
                {
                    break;
                }
            }
        }

        internal static void SearchMenu(IFlight[] flights)
        {
            Console.Clear();
            Console.WriteLine("Search for passengers:");
            Console.WriteLine("1. Search passenger by name or last name");
            Console.WriteLine("2. Search by flight number");
            Console.WriteLine("3. Search passenger by passport");
            byte mode = 0; ;
            bool TryByte = byte.TryParse(Console.ReadLine(), out mode);
            if (!TryByte)
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                switch (mode)
                {
                    case 1:
                        Printer.PrintFlightPassengers(Searcher.SearchPassengerByName(flights));
                        break;
                    case 2:
                        Printer.PrintFlightPassengers(Searcher.SearchPassengerByFlightNumber(flights));
                        break;
                    case 3:
                        Printer.PrintFlightPassengers(Searcher.SearchPassengerByPassport(flights));
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
        }
    }
}
