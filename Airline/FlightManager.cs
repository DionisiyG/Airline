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
                    Console.WriteLine($"{"Enter flight type. 1 - Arrival, 2 - Departure",-30}");
                    isParse = FlightType.TryParse(Console.ReadLine(), true, out typeFlight);
                    if ((int)typeFlight >= 3 || !isParse || (int)typeFlight == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input! Notice: 1 - Arrival, 2 - Departure");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                Console.WriteLine();
                //Flight Number input
                while (isCheck)
                {
                    Console.Write($"{"Enter flight number",-30}");
                    isParse = int.TryParse(Console.ReadLine(), out flightNumberInput);
                    if (!isParse)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input. Try again. Number is only available.");
                        Console.ResetColor();
                        continue;
                    }
                    else if (isParse && flightNumberInput <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input, please enter flight number more than 0");
                        Console.ResetColor();
                        continue;
                    }
                    else if (Searcher.SearchByFlightNumber(flights, flightNumberInput) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This flight number is already exists. Please enter another flight number.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                
                // City input
                while (isCheck)
                {
                    Console.Write($"{"Enter port or city name",-30}");
                    portCityTitleInput = Console.ReadLine();
                    if (Regex.IsMatch(portCityTitleInput, @"[^\d]+$")) //_ detected
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input. Only letters are avaible.");
                        Console.ResetColor();
                        continue;
                    }
                }

                //Terminal number input
                while (isCheck)
                {
                    Console.Write($"{"Enter terminal number",-30}");
                    isParse = byte.TryParse(Console.ReadLine(), out terminalNumberInput); 
                    if (!isParse) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input. Try again. Only numbers (1 - 255) are avaible.");
                        Console.ResetColor();
                        continue;
                    }
                    else if (isParse && terminalNumberInput <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input, please enter terminal number more than 0");
                        Console.ResetColor();
                        continue;
                    }
                    else if (isParse && terminalNumberInput > 255)
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
                    else if (isParse && (byte)flightStatusInput > 8)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Note: available indexes to enter are from 0 to 8. Try again.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }

                //DateTime input
                while (isCheck)
                {
                    Console.WriteLine($"{"Enter the date please (e.g. 27.08.1990) and time (format: 00:00:00)",-30}");
                    isParse = DateTime.TryParse(Console.ReadLine(), out dt);
                    if (!isParse)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, try again");
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Congratilations! Your element has been added");
                        Console.ResetColor();
                        break;
                    }
                }
            }
            Console.WriteLine("Updating...");
            Printer.PrintAllFlights(flights);
        }
        
        public static void EditFlight(IFlight[] flights)
        {
            Console.Clear();
            Printer.PrintAllFlights(flights);
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
                Console.WriteLine("Enter flight index to editing (column 'ID' in table.)"); //TEST HERNIA PROVERKA
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
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("If you do not choose anything, nothing would change. Press Enter to skip or move forward.");
                    Console.ResetColor();
                    bool isCheck = true;

                    //Edit Flight type
                    while (isCheck)
                    {
                        Console.WriteLine($"{"Enter flight type. Notice:  1 - Arrival, 2 - Departure",-30}");
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = FlightType.TryParse(readLine, true, out typeFlight);
                            if (!isParse || (int)typeFlight >= 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. Try again. Notice:  1 - Arrival, 2 - Departure");
                                Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, try again");
                                Console.ResetColor();
                                continue;
                            }
                            else if (isParse && flightNumberInput <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrect input, please input flight number more than 0");
                                Console.ResetColor();
                                continue;
                            }
                            else if (Searcher.SearchByFlightNumber(flights, flightNumberInput) != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("This flight number already exists. Please input other flight number.");
                                Console.ResetColor();
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
                        else if (Regex.IsMatch(portCityTitleInput, @"[^\d]+$"))
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Incorrect input. Only letters are avaible.");
                            Console.ResetColor();
                            continue;
                        }
                    }
                   
                    //Edit terminal number
                    while (isCheck)
                    {
                        Console.Write($"{"Input terminal number",-30}");
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = byte.TryParse(readLine, out terminalNumberInput);
                            if (!isParse)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, try again. Available range from 1 to 254");
                                Console.ResetColor();
                                continue;
                            }
                            else if (isParse && terminalNumberInput <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrect input, please input terminal number > 0");
                                Console.ResetColor();
                                continue;
                            }
                            else if (isParse && terminalNumberInput >= 255)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrect input, please input terminal number < 255");
                                Console.ResetColor();
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
                        Console.Write($"{"Input flight status:",-30}");
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
                        readLine = Console.ReadLine();
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                            break;
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = FlightStatus.TryParse(readLine, true, out flightStatusInput);
                            if (!isParse)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, try again");
                                Console.ResetColor();
                                continue;
                            }
                            else if (isParse && (byte)flightStatusInput > 8)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Note: available indexes to enter are from 0 to 8. Try again.");
                                Console.ResetColor();
                                continue;
                            }
                            else
                            {
                                flightStatusInput = flights[i].StatusFlight;
                            }
                            break;
                        }
                    }
                    
                    //Edit date
                    while (isCheck)
                    {
                        Console.Write($"{"Enter the date please (e.g. 27.08.1990) and time (format: 00:00:00)",-30}");
                        readLine = Console.ReadLine();
                        isParse = DateTime.TryParse(readLine, out dt);
                        if (!string.Empty.Equals(readLine))
                        {
                            if (!isParse)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, try again");
                                Console.ResetColor();
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
                    Console.WriteLine("Updating...");
                    Console.WriteLine("Your item has been modified\\saved");
                    Printer.PrintAllFlights(flights);
                    
                }
            }
        }

        public static void DeleteFlight(IFlight[] flights)
        {
            if (flights[0] == null)
            {
                Console.WriteLine("Nope flights! Add new flights!");
            }
            else
            {
                Console.WriteLine("Input index, to deleting flight element (in table column ID)");
                int i = 0;
                bool TryByteI = int.TryParse(Console.ReadLine(), out i);
                if (!TryByteI)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, try again");
                    Console.ResetColor();
                    DeleteFlight(flights);
                }
                else if (i < 0 && i > flights.Length - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect input. Please enter index from 0 to {0}", flights.Length - 1);
                    Console.ResetColor();
                    DeleteFlight(flights);
                }
                else
                {
                    if (0 <= i && i < flights.Length)
                    {
                        if (flights[i] == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Flight element does not exist, try again");
                            Console.ResetColor();
                            DeleteFlight(flights);
                        }
                        else
                        {
                            flights[i] = null;
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("Flight element deleting..");
                            Console.ResetColor();
                            RebuildFlight(ref flights);
                            Printer.PrintAllFlights(flights);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect input, try again");
                        Console.ResetColor();
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

