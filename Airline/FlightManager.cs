using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airline
{
    class FlightManager
    {
        private static Random _rnd = RandomProvider.GetThreadRandom();

        private static int _randomValue(int maxValue)
        {
            return _rnd.Next(maxValue);
        }
        private static int _randomValue(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public static IFlight GenerateRandomFlight(IFlight[] flights)
        {
            DateTime dt = new DateTime();
            IFlight flightsObject;
            string[] portCityTitle = new string[] { "Odessa", "Helsinki", "Munich", "Yuma", "Gdansk", "Oklahoma", "Dubai" };

            int monthRandom = 0;
            int yearRandom = 0;
            int dayRandom = 0;
            int hourRandom = 0;
            int minuteRandom = 0;
            int secondRandom = 0;

            //Date and Time
            yearRandom = _randomValue(2015, 2017);
            monthRandom = _randomValue(1, 13);
            hourRandom = _randomValue(24);
            minuteRandom = _randomValue(60);
            secondRandom = _randomValue(60);


            if (monthRandom == 1 || monthRandom == 3 || monthRandom == 5 || monthRandom == 7 || monthRandom == 8 ||
                    monthRandom == 10 || monthRandom == 12)
            {
                dayRandom = _randomValue(1, 32);
            }
            else if (monthRandom == 4 || monthRandom == 6 || monthRandom == 9 || monthRandom == 11)
            {
                dayRandom = _randomValue(1, 31);
            }
            else if (monthRandom == 2)
            {
                if (!DateTime.IsLeapYear(yearRandom))
                {
                    dayRandom = _randomValue(1, 29);
                }
                else
                {
                    dayRandom = _randomValue(1, 30);
                }
            }
            dt = new DateTime(yearRandom, monthRandom, dayRandom, hourRandom, minuteRandom, secondRandom);

            //Flight number
            bool check = true;
            int flightNumber = _randomValue(1, 1000);
            while (check)
            {

                if (Searcher.SearchByFlightNumber(flights, flightNumber) == null)
                {
                    break;
                }
                else
                {
                    flightNumber = _randomValue(100, 900);
                }
            }

            flightsObject = new Flight
            {
                TypeFlight = (FlightType)_randomValue(2),
                FlightNumber = flightNumber,
                FlightDT = dt,
                PortCityTitle = portCityTitle[_randomValue(7)],
                TerminalNumber = (byte)_randomValue(1, 15),
                StatusFlight = (FlightStatus)_randomValue(9),
            };

            PassengerManager.FillPassengerArray(flightsObject, _randomValue(15));
            return flightsObject;
        }

        public static void FillFlights(IFlight[] flights, int numberToFill)
        {
            for (int i = 0; i < numberToFill; i++)
            {
                flights[i] = GenerateRandomFlight(flights);
            }
        }

        public static void AddFlight(IFlight[] flights)
        {
            Console.Clear();

            FlightType typeFlight = new FlightType();
            int flightNumberInput = 0;
            string portCityTitleInput = "";
            byte terminalNumberInput = 0;
            FlightStatus flightStatusInput = new FlightStatus();
            DateTime dt = new DateTime();
            bool isParse = false;

            if (flights[flights.Length - 1] != null)
            {
                Console.WriteLine("Sorry, there are no free seats! Please, delete flight element.");
            }
            else
            {
                Console.WriteLine("Enter new element...");
                bool isCheck = true;

                //Flight Type input
                while (isCheck)
                {
                    Console.Write($"{"Enter flight type. 1 - Arrival, 2 - Departure",-30}");
                    isParse = FlightType.TryParse(Console.ReadLine(), true, out typeFlight);
                    if ((int)typeFlight >= 3 || !isParse) //need to test
                    {
                        Console.WriteLine("Incorrect input! Notice: 1 - Arrival, 2 - Departure");
                        continue;
                    }
                    break;
                }
                Console.WriteLine();
                //Flight Number input
                while (isCheck)
                {
                    Console.Write($"{"Enter flight number",-30}"); // input flight number > 0
                    isParse = int.TryParse(Console.ReadLine(), out flightNumberInput);
                    if (!isParse)
                    {
                        Console.WriteLine("Incorrect input. Try again. Number is only available.");
                        continue;
                    }
                    else if (isParse && flightNumberInput <= 0)
                    {
                        Console.WriteLine("Incorrect input, please enter flight number more than 0");
                        continue;
                    }
                    else if (Searcher.SearchByFlightNumber(flights, flightNumberInput) != null)
                    {
                        Console.WriteLine("This flight number is already exists. Please enter another flight number.");
                        continue;
                    }
                    break;
                }
                
                // City input
                while (isCheck)
                {
                    Console.Write($"{"Enter port or city name",-30}");
                    portCityTitleInput = Console.ReadLine();
                    if (Regex.IsMatch(portCityTitleInput, @"[^\d]+$"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input. Only letters are avaible.");
                        continue;
                    }
                }

                //Terminal number input
                while (isCheck)
                {
                    Console.Write($"{"Enter terminal number",-30}");
                    isParse = byte.TryParse(Console.ReadLine(), out terminalNumberInput);
                    if (!isParse) //NADO TESTIT HEROVAIA LOGIKA
                    {
                        Console.WriteLine("Incorrect input. Try again. Only numbers are avaible.");
                        continue;
                    }
                    else if (isParse && terminalNumberInput <= 0)
                    {
                        Console.WriteLine("Incorrect input, please enter terminal number more than 0");
                        continue;
                    }
                    else if (isParse && terminalNumberInput >= 255)
                    {
                        Console.WriteLine("Incorrect input, please input terminal number < 255");
                        continue;
                    }
                    break;
                }

                //Flight Status input
                while (isCheck)
                {
                    Console.Write($"{"Enter flight status",-30}");
                    Console.WriteLine(@"
                CheckIn - 0,
                GateClosed - 1,
                Arrived - 2,
                DepartedAt - 3,
                Unknown - 4,
                Canceled - 5,
                ExpectedAt - 6,
                Delayed - 7,
                InFlight - 8");
                    isParse = FlightStatus.TryParse(Console.ReadLine(), true, out flightStatusInput);
                    if (!isParse)
                    {
                        Console.WriteLine("Invalid input, try again");
                        continue;
                    }
                    else if (isParse && (byte)flightStatusInput > 9)
                    {
                        Console.WriteLine("Note: available indexes to enter are from 1 to 9. Try again.");
                        continue;
                    }
                    break;
                }

                //DateTime input
                while (isCheck)
                {
                    Console.Write($"{"Enter the date please (e.g. 27.08.1990",-30}");
                    isParse = DateTime.TryParse(Console.ReadLine(), out dt);
                    if (!isParse)
                    {
                        Console.WriteLine("Invalid input, try again");
                        continue;
                    }
                    break;
                }

                // Add entered element

                for (int i = 0; i < flights.Length; i++)
                {
                    if (flights[i] == null)
                    {
                        flights[i] = new Flight
                        {
                            TypeFlight = typeFlight,
                            FlightNumber = flightNumberInput,
                            FlightDT = dt,
                            PortCityTitle = portCityTitleInput,
                            TerminalNumber = terminalNumberInput,
                            StatusFlight = flightStatusInput,
                        };
                        Console.WriteLine("Congratilations! Your element has been added");
                        break;
                    }
                }
            }

        }

        public static void EditFlight(IFlight[] flights)
        {
            Console.Clear();
            int i;

            bool isParse = false;
            FlightType typeFlight = new FlightType();
            FlightStatus flightStatusInput = new FlightStatus();
            int flightNumberInput = 0;
            string portCityTitleInput = "";
            byte terminalNumberInput = 0;
            string readLine = "";
            DateTime dt = new DateTime();

            if (flights[0] == null)
            {
                Console.WriteLine("Nope flights! Adding new flights.");
            }
            else
            {
                Console.WriteLine("Enter flight index to editing (column 'ID' in table.");
                isParse = int.TryParse(Console.ReadLine(), out i);
                if (!isParse | i > 3) // change validation data
                {
                    Console.WriteLine("Invalid input, try again");
                }
                else if (isParse && i > flights.Length - 1 && i < 0)
                {
                    Console.WriteLine("This element does not exist. Please input index >= 0 and < {0}", flights.Length);
                }
                else
                {
                    Console.WriteLine("Your selected element: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("| {0, -3}{1}", i.ToString(), flights[i].ToString());
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("If you do not enter anything, nothing would change.");
                    bool isCheck = true;

                    //Edit Flight type
                    while (isCheck)
                    {
                        Console.Write($"{"Enter flight type. Notice:  1 - Arrival, 2 - Departure",-30}");
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = FlightType.TryParse(readLine, true, out typeFlight);
                            if (!isParse || (int)typeFlight >= 3)
                            {
                                Console.WriteLine("Invalid input. Try again. Notice:  1 - Arrival, 2 - Departure");
                                continue;
                            }
                        }
                        else
                        {
                            typeFlight = flights[i].TypeFlight;
                        }
                        break;
                    }
                    
                    // Edit Flight number
                    while (isCheck)
                    {
                        Console.Write($"{"Enter flight number",-30}"); // input flight number > 0 
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = int.TryParse(readLine, out flightNumberInput);
                            if (!isParse)
                            {
                                Console.WriteLine("Invalid input, try again");
                                continue;
                            }
                            else if (isParse && flightNumberInput <= 0)
                            {
                                Console.WriteLine("Incorrect input, please input flight number > 0");
                                continue;
                            }
                            else if (Searcher.SearchByFlightNumber(flights, flightNumberInput) != null)
                            {
                                Console.WriteLine("This flight number already exists. Please input other flight number.");
                                continue;
                            }
                        }
                        else
                        {
                            flightNumberInput = flights[i].FlightNumber;
                        }
                        break;
                    }
                    
                    // Edit City
                    while (isCheck)
                    {
                        Console.Write($"{"Input port or city title",-30}");
                        portCityTitleInput = Console.ReadLine();
                        if (string.Empty.Equals(portCityTitleInput))
                        {
                            portCityTitleInput = flights[i].PortCityTitle;
                            break;
                        }
                        else if (Regex.IsMatch(portCityTitleInput, @"[a-zA-Z]"))
                        {
                            break;
                        }
                        else// if (Regex.IsMatch(portCityTitleInput, @"[^\w]"))
                        {
                            Console.WriteLine("Incorrect input. Input the letters or numbers.");
                            continue;
                        }
                    }
                   
                    //Edit terminal number
                    while (isCheck)
                    {
                        Console.Write($"{"Input terminal number",-30}"); // > 0 and < 255
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = byte.TryParse(readLine, out terminalNumberInput);
                            if (!isParse)
                            {
                                Console.WriteLine("Invalid input, try again");
                                continue;
                            }
                            else if (isParse && terminalNumberInput <= 0)
                            {
                                Console.WriteLine("Incorrect input, please input terminal number > 0");
                                continue;
                            }
                            else if (isParse && terminalNumberInput >= 255)
                            {
                                Console.WriteLine("Incorrect input, please input terminal number < 255");
                                continue;
                            }
                        }
                        else
                        {
                            terminalNumberInput = flights[i].TerminalNumber;
                        }
                        break;
                    }

                    
                    // Edit flight status
                    while (isCheck)
                    {
                        Console.Write($"{"Input flight status",-30}");
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = FlightStatus.TryParse(readLine, true, out flightStatusInput);
                            if (!isParse)
                            {
                                Console.WriteLine("Invalid input, try again");
                                continue;
                            }
                        }
                        else
                        {
                            flightStatusInput = flights[i].StatusFlight;
                        }
                        break;
                    }
                    
                    //Edit date
                    while (isCheck)
                    {
                        Console.Write($"{"Input date and time",-30}");
                        readLine = Console.ReadLine();
                        isParse = DateTime.TryParse(readLine, out dt);
                        if (!string.Empty.Equals(readLine))
                        {
                            if (!isParse)
                            {
                                Console.WriteLine("Invalid input, try again");
                                continue;
                            }
                        }
                        else
                        {
                            dt = flights[i].FlightDT;
                        }
                        break;
                    }
                   

                    flights[i].TypeFlight = typeFlight;
                    flights[i].FlightNumber = flightNumberInput;
                    flights[i].FlightDT = dt;
                    flights[i].PortCityTitle = portCityTitleInput;
                    flights[i].TerminalNumber = terminalNumberInput;
                    flights[i].StatusFlight = flightStatusInput;
                    Console.WriteLine("Your item has been modified\\saved");
                }
            }
        }

        public static void DeleteFlight(IFlight[] flights)
        {
            if (flights[0] == null)
            {
                Console.WriteLine("Nope flights! Adding new flights.");
            }
            else
            {
                Console.WriteLine("Input index, to deleting flight element (in table column Index)");
                int i = 0;
                bool TryByteI = int.TryParse(Console.ReadLine(), out i);
                if (!TryByteI)
                {
                    Console.WriteLine("Invalid input, try again");
                }
                else if (i < 0 && i > flights.Length - 1)
                {
                    Console.WriteLine("Incorrect input. Please input index 0 to { 0}", flights.Length - 1);
                }
                else
                {
                    if (0 <= i && i < flights.Length)
                    {
                        if (flights[i] == null)
                        {
                            Console.WriteLine("Flight element does not exist, try again");
                        }
                        else
                        {
                            flights[i] = null;
                            Console.WriteLine("Flight element deleting");
                            RebuildFlight(ref flights);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input, try again");
                    }
                }
            }
        }

        public static void RebuildFlight(ref IFlight[] flights)
        {
            IFlight tmp = flights[0];

            for (int i = 0; i < flights.Length; i++)
            {
                if (tmp == null)
                {
                    if (flights[i] != null)
                    {
                        flights[i - 1] = flights[i];
                        flights[i] = null;
                        tmp = null;
                    }
                    else if (flights[i] == null && i != 0)
                    {
                        break;
                    }
                }
                else
                {
                    tmp = flights[i];
                }

            }
        }

    }
}

