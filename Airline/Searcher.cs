using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airline
{
    static class Searcher
    {
        public static IFlight SearchByFlightNumber(IFlight[] flights, int flightNumber)
        {
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    if (flightNumber == flights[i].FlightNumber)
                    {
                        return flights[i];
                    }
                }
                else
                {
                    break;
                }
            }
            return null;
        }

        public static Passenger[] SearchPassengerByName(IFlight[] flights)
        {
            Passenger[] passengers = new Passenger[flights.Length * flights[0].Passengers.Length];
            bool isCheck = true;
            string readName = "";

            while (isCheck)
            {
                Console.WriteLine("For searching enter first or last name");
                readName = Console.ReadLine();
                if (Regex.IsMatch(readName, @"[^\w]"))
                {
                    Console.WriteLine("Incorrect input. Letters are onle available");
                    continue;
                }
                isCheck = false;
            }
            //Logic
            string firstName = "";
            string lastName = "";

            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    for (int j = 0; j < flights[i].Passengers.Length; j++)
                    {
                        if (flights[i].Passengers[j] == null)
                        {
                            break;
                        }
                        else if ((firstName = flights[i].Passengers[j].FirstName).ToUpper().Contains(readName.ToUpper())
                                     || (lastName = flights[i].Passengers[j].LastName).ToUpper().Contains(readName.ToUpper()))
                        {
                            for (int k = 0; k < passengers.Length; k++)
                            {
                                if (passengers[k] == null)
                                {
                                    passengers[k] = flights[i].Passengers[j];
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return passengers;

        }

        public static Passenger[] SearchPassengerByFlightNumber(IFlight[] flights)
        {
            IFlight flight = new Flight();
            int flightNumberInput = 0;
            bool check = true;
            Console.WriteLine("Available flights:");
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] == null)
                    break;
                Console.WriteLine($"{flights[i].FlightNumber.ToString()}");
            }
            Console.WriteLine();
            while (check)
            {
                Console.WriteLine("To see passengers on board, please enter the flight number from the list above.");
                bool isParse = int.TryParse(Console.ReadLine(), out flightNumberInput);
                if (!isParse)
                {
                    Console.WriteLine("Incorrect input. Try again");
                    continue;
                }
                else if (isParse && flightNumberInput <= 0)
                {
                    Console.WriteLine("Incorrect input. Please input flight number which is more then  0");
                    continue;
                }
                flight = Searcher.SearchByFlightNumber(flights, flightNumberInput);
                if (flight == null)
                {
                    Console.WriteLine("This flight doesn`t exists.");
                }
                else
                {
                    break;
                }
            }
            return flight.Passengers;
        }

        public static Passenger[] SearchPassengerByPassport(IFlight[] flights)
        {
            Passenger[] passengers = new Passenger[flights.Length * flights[0].Passengers.Length];
            string readLine = "";

            while (true)
            {
                Console.WriteLine("For searching the passenger, please, enter passport data.");
                readLine = Console.ReadLine();
                if (Regex.IsMatch(readLine, @"[a-zA-Z0-9]"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Only letters or numbers are available.");
                    continue;
                }
            }
            //Logic
            string passport = "";

            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    for (int j = 0; j < flights[i].Passengers.Length; j++)
                    {
                        if (flights[i].Passengers[j] == null)
                        {
                            break;
                        }
                        else if ((passport = flights[i].Passengers[j].PassportPassenger).ToUpper().Contains(readLine.ToUpper()))
                        {
                            for (int k = 0; k < passengers.Length; k++)
                            {
                                if (passengers[k] == null)
                                {
                                    passengers[k] = flights[i].Passengers[j];
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return passengers;
        }

        public static IFlight[] SearchFlightByTicket(IFlight[] flights)
        {
            Console.Clear();
            IFlight[] flight = new Flight[flights.Length];
            decimal inputPrice = 0m;
            bool check = true;

            Console.WriteLine("Don`t pay more! Searching for ticket in econom class which price is lower than your offer.");
            while (check)
            {
                Console.WriteLine("Input ticket priceEnter the desirable ticket price (we will found the best proposals!)");
                bool isParse = decimal.TryParse(Console.ReadLine(), out inputPrice);
                if (!isParse)
                {
                    Console.WriteLine("Incorrec input. Try again");
                    continue;
                }
                else if (isParse && inputPrice < 0)
                {
                    Console.WriteLine("Incorrect input. Please enter the ticket price which is more than 0");
                    continue;
                }

                for (int i = 0; i < flights.Length; i++)
                {
                    if (flights[i] != null && flights[i].EconomTicketPrice < inputPrice)
                    {
                        for (int j = 0; j < flight.Length; j++)
                        {
                            if (flight[j] == null)
                            {
                                flight[j] = flights[i];
                                break;
                            }
                        }
                    }
                    else if (flights[i] == null)
                    {
                        break;
                    }
                }
                break;
            }
            return flight;
        }
    }
}
