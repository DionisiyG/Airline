using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airline
{
    class PassengerManager
    {
        private static Random _rnd = RandomProvider.GetThreadRandom();

        private static int rndValue(int maxValue)
        {
            return _rnd.Next(maxValue);
        }
        private static int rndValue(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public static Passenger GenerateRandomPassenger()
        {
            string firstName;
            string lastName;
            DateTime birthday = new DateTime();
            Passenger passenger;

            string[] firstMaleName = new string[] { "Keiran", "Danny", "Xander", "Jessy", "Tonny" };
            string[] lastMaleName = new string[] { "Lee", "Darko", "Corvus", "James", "Ribas" };

            string[] firstFemaleName = new string[] { "Asa", "Jayden", "Peta", "July", "Summer" };
            string[] lastFemaleName = new string[] { "Akira", "Jones", "Jensen", "May", "Frost" };

            string[] nationality = new string[] { "Japanese", "Espaniol", "Englishman", "Gringo", "Italian" };

            Gender passengerGender = (Gender)rndValue(2);
            if ((byte)passengerGender == 0)
            {
                firstName = firstMaleName[rndValue(firstMaleName.Length)];
                lastName = lastMaleName[rndValue(lastMaleName.Length)];
            }
            else
            {
                firstName = firstFemaleName[rndValue(firstFemaleName.Length)];
                lastName = lastFemaleName[rndValue(lastFemaleName.Length)];
            }

            passenger = new Passenger
            {
                FirstName = firstName,
                LastName = lastName,
                Nationality = nationality[rndValue(nationality.Length)],
                PassportPassenger = RandomPassport.GetRandomPassport(),
                Birthday = birthday,
                PassengerGender = passengerGender
            };

            //Date of birth filling            
            int yearRnd = rndValue(1950, 2010);
            int monthRnd = rndValue(1, 13);
            int dayRnd = 0;

            if (monthRnd == 1 || monthRnd == 3 || monthRnd == 5 ||
                    monthRnd == 7 || monthRnd == 8 || monthRnd == 10 || monthRnd == 12)
            {
                dayRnd = rndValue(1, 32);
            }
            else if (monthRnd == 4 || monthRnd == 6 || monthRnd == 9 || monthRnd == 11)
            {
                dayRnd = rndValue(1, 31);
            }
            else if (monthRnd == 2)
            {
                if (!DateTime.IsLeapYear(yearRnd))
                {
                    dayRnd = rndValue(1, 29);
                }
                else
                {
                    dayRnd = rndValue(1, 30);
                }
            }
            birthday = new DateTime(yearRnd, monthRnd, dayRnd);
          
            return passenger;
        }

        public static void FillPassengerArray(IFlight flight, int amountPassenger)
        {
            for (int i = 0; i < amountPassenger; i++)
            {
                flight.Passengers[i] = GenerateRandomPassenger();
                Ticket ticket = new Ticket();

                //Fill ticket info
                ticket.ClassType = (TicketType)rndValue(0, 2);
                if (ticket.ClassType == TicketType.Econom)
                {
                    ticket.Price = flight.EconomTicketPrice;
                }
                else
                {
                    ticket.Price = flight.BusinessTicketPrice;
                }
                
                flight.Passengers[i].FlightTicket = ticket;
            }
        }

        public static void AddPassenger(IFlight flight)
        {
            Console.Clear();

            string firstName = "";
            string lastName = "";
            string nationality = "";
            string passport = "";
            DateTime birthday = new DateTime();
            Gender passengerGender = new Gender();
            Ticket flightTicket = new Ticket();

            bool isParse = false;
            bool isCheck = true;

            if (flight.Passengers[flight.Passengers.Length - 1] != null)
            {
                Console.WriteLine("Attention! All places are occupied! Some passenger need to be expelled.");
            }
            else
            {
                Console.WriteLine("Please, enter new element");
                //First name
                while (isCheck)
                {
                    Console.Write($"{"Enter first name. !Only letters (A-z) are avaible!",-40}");
                    firstName = Console.ReadLine();
                    if (Regex.IsMatch(firstName, @"[a-zA-Z]"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input. Only letters (A-z) are avaible.");
                        continue;
                    }
                }

                //Last name
                while (isCheck)
                {
                    Console.Write($"{"Enter last name. !Only letters (A-z) are avaible!",-40}");
                    lastName = Console.ReadLine();
                    if (Regex.IsMatch(lastName, @"[a-zA-Z]"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input. Only letters (A-z) are avaible.");
                        continue;
                    }
                }
               
                //Nationality
                while (isCheck)
                {
                    Console.Write($"{"Enter passenger`s nationality. !Only letters (A-z) are avaible!",-40}");
                    nationality = Console.ReadLine();
                    if (Regex.IsMatch(nationality, @"[a-zA-Z]"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input. Only letters (A-z) are avaible.");
                        continue;
                    }
                }
             
                //Passport
                while (isCheck)
                {
                    Console.Write($"{"Enter passenger`s passport data. !Only letters (A-z) and numbers (0-9) are avaible!",-40}");
                    passport = Console.ReadLine();
                    if (Regex.IsMatch(passport, @"[a-zA-Z0-9]"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input. Only letters (A-z) and numbers (0-9) are avaible.");
                        continue;
                    }
                }
               
                //Birthday
                while (isCheck)
                {
                    Console.Write($"{"Enter your day of birth (e.g. 27.08.1990)",-40}");
                    isParse = DateTime.TryParse(Console.ReadLine(), out birthday);
                    if (!isParse)
                    {
                        Console.WriteLine("Incorrect input. Try again. (e.g. 27.08.1990)");
                        continue;
                    }
                    break;
                }

                //Passenger Gender
                while (isCheck)
                {
                    Console.Write($"{"What is passenger`s gender (Male/Female)?",-40}");
                    isParse = Gender.TryParse(Console.ReadLine(), true, out passengerGender);
                    if (!isParse)
                    {
                        Console.WriteLine("Incorrect input. Try again. ");
                        continue;
                    }
                    break;
                }
  
                //Ticket
                while (isCheck)
                {
                    Console.WriteLine("Good news! Ticket price now is sustainable!");
                    Console.WriteLine($"Business ticket costs: {flight.BusinessTicketPrice}");
                    Console.WriteLine($"Econom ticket costs: {flight.EconomTicketPrice}");
                    Console.Write($"{"What type of ticket do you prefer? (enter: Business or Econom)",-40}");
                    isParse = TicketType.TryParse(Console.ReadLine(), true, out flightTicket.ClassType);
                    if (!isParse)
                    {
                        Console.WriteLine("Incorrect input. Try again. Note that for business type ticket enter Business, for econom - Econom");
                        continue;
                    }
                    break;
                }

                if (flightTicket.ClassType == TicketType.Econom)
                {
                    flightTicket.Price = flight.EconomTicketPrice;
                }
                else
                {
                    flightTicket.Price = flight.BusinessTicketPrice;
                }

                // Add inputed data to an array
                for (int i = 0; i < flight.Passengers.Length; i++)
                {
                    if (flight.Passengers[i] == null)
                    {
                        flight.Passengers[i] = new Passenger
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Nationality = nationality,
                            PassportPassenger = passport,
                            Birthday = birthday,
                            PassengerGender = passengerGender,
                            FlightTicket = flightTicket,
                        };
                        Console.WriteLine("Congratulations! Passenger has been added!");
                        break;
                    }
                }
                
            }
        }

        public static void EditPassenger(IFlight flight)
        {
            int i;
            Console.Clear();
            //надо чтобы выводило всех пассажиров

            string firstName = "";
            string lastName = "";
            string nationality = "";
            string passport = "";
            DateTime birthday = new DateTime();
            Gender passengerGender = new Gender();
            Ticket ticket = new Ticket();

            bool isParse = false;
            bool isCheck = true;

            string readLine = "";

            if (flight.Passengers[0] == null)
            {
                Console.WriteLine("There is no passengers! Adding new passenger...");
            }
            else
            {
                Console.WriteLine("Enter passenger index to edit (Look at ID column in Table)");
                isParse = int.TryParse(Console.ReadLine(), out i);
                if (!isParse) 
                {
                    Console.WriteLine("Incorrect input. Be careful and try again.(Data input must be number)");
                }
                else if ((i < 0 && i > flight.Passengers.Length - 1) || (flight.Passengers[i] == null))
                {
                    Console.WriteLine("Incorrect input. Please enter index from 0 to {0}", flight.Passengers.Length - 1);
                }
                else 
                {
                    Console.WriteLine("Your selected item: ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("| {0, -3}{1}", i.ToString(), flight.Passengers[i].ToString()); 
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("If you do not enter anything, the value will not change.");

                    //First name (Change|Save)
                    while (isCheck)
                    {
                        Console.Write($"{"Enter passenger`s first name",-40}");
                        firstName = Console.ReadLine();
                        if (string.Empty.Equals(firstName))
                        {
                            firstName = flight.Passengers[i].FirstName;
                            break;
                        }
                        else if (Regex.IsMatch(firstName, @"[^\w]"))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input. Letters are only availble.");
                            continue;
                        }
                    }
        
                    //Last name (Change|Save)
                    while (isCheck)
                    {
                        Console.Write($"{"Enter passenger`s last name",-40}");
                        lastName = Console.ReadLine();
                        if (string.Empty.Equals(lastName))
                        {
                            lastName = flight.Passengers[i].LastName;
                            break;
                        }
                        else if (Regex.IsMatch(lastName, @"[a-zA-Z]"))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input. Letters are only availble.");
                            continue;
                        }
                    }

                    //Nationality (Change|Save)
                    while (isCheck)
                    {
                        Console.Write($"{"Enter passenger`s nationality",-40}");
                        nationality = Console.ReadLine();
                        if (string.Empty.Equals(nationality))
                        {
                            nationality = flight.Passengers[i].Nationality;
                            break;
                        }
                        else if (Regex.IsMatch(nationality, @"[a-zA-Z]"))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input. Letters are only availble.");
                            continue;
                        }
                    }

                    //Passport (Change|Save)
                    while (isCheck)
                    {
                        Console.Write($"{"Enter passenger`s passport info",-40}");
                        passport = Console.ReadLine();
                        if (string.Empty.Equals(passport))
                        {
                            passport = flight.Passengers[i].PassportPassenger;
                            break;
                        }
                        else if (Regex.IsMatch(passport, @"[a-zA-Z0-9]"))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input. Input the letters or numbers.");
                            continue;
                        }
                    }


                    //Birthday (Edit|Save)
                    while (isCheck)
                    {
                        Console.Write($"{"Enter passenger`s day of birth",-40}");
                        readLine = Console.ReadLine();
                        isParse = DateTime.TryParse(readLine, out birthday);
                        if (!string.Empty.Equals(readLine))
                        {
                            if (!isParse)
                            {
                                Console.WriteLine("Incorrect put. Try again");
                                continue;
                            }
                        }
                        else
                        {
                            birthday = flight.Passengers[i].Birthday;
                        }
                        break;
                    }
                    
                    //Gender (Edit|Save)
                    while (isCheck)
                    {
                        Console.Write($"{"What is your passenger`s gender (Male/Female)?",-40}");
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = Gender.TryParse(readLine, true, out passengerGender);
                            if (!isParse)
                            {
                                Console.WriteLine("Incorrect input. Try again");
                                continue;
                            }
                        }
                        else
                        {
                            passengerGender = flight.Passengers[i].PassengerGender;
                        }
                        break;
                    }

                    //Ticket (Edit|Save)
                    while (isCheck)
                    {
                        Console.WriteLine("Good news! Ticket price now is sustainable!");
                        Console.WriteLine($"Business ticket costs: {flight.BusinessTicketPrice}");
                        Console.WriteLine($"Econom ticket costs: {flight.EconomTicketPrice}");
                        Console.Write($"{"What type of ticket do you prefer? (enter: Business or Econom)",-40}");
                        readLine = Console.ReadLine();
                        if (!string.Empty.Equals(readLine))
                        {
                            isParse = TicketType.TryParse(readLine, true, out ticket.ClassType);
                            if (!isParse)
                            {
                                Console.WriteLine("Incorrect input. Try again");
                                continue;
                            }
                        }
                        else
                        {
                            ticket.ClassType = flight.Passengers[i].FlightTicket.ClassType;
                        }

                        if (ticket.ClassType == TicketType.Econom)
                        {
                            ticket.Price = flight.EconomTicketPrice;
                        }
                        else
                        {
                            ticket.Price = flight.BusinessTicketPrice;
                        }

                        break;
                    }

                    flight.Passengers[i].FirstName = firstName;
                    flight.Passengers[i].LastName = lastName;
                    flight.Passengers[i].Nationality = nationality;
                    flight.Passengers[i].PassportPassenger = passport;
                    flight.Passengers[i].Birthday = birthday;
                    flight.Passengers[i].PassengerGender = passengerGender;
                    flight.Passengers[i].FlightTicket = ticket;
                    Console.WriteLine("Congratulations! Your data has been modified\\saved");
                }
            }
        }

        public static void DeletePassenger(Passenger[] passengers)
        {
            if (passengers[0] == null)
            {
                Console.WriteLine("No passengers - noone to delete.");
            }
            Console.WriteLine("Input ID, to remove passenger element (column ID in table )");
            int i = 0;
            bool tryI = int.TryParse(Console.ReadLine(), out i);
            if (!tryI)
            {
                Console.WriteLine("Incorrect input. Try again");
            }
            else if (i < 0 && i > passengers.Length - 1)
            {
                Console.WriteLine("Incorrect input. Please input index 0 to {0}", passengers.Length - 1);
            }
            else
            {
                if (0 <= i && i < 20)
                {
                    if (passengers[i] == null)
                    {
                        Console.WriteLine("There is no passenger with enered ID. Try again");
                    }
                    else
                    {
                        passengers[i] = null;
                        Console.WriteLine("Element deleting...");
                        RebuildPassengersArray(ref passengers);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Try again");
                }
            }
        }

        private static void RebuildPassengersArray(ref Passenger[] passengers)
        {
            Passenger tmp = passengers[0];

            for (int i = 0; i < passengers.Length; i++)
            {
                if (tmp == null)
                {
                    if (passengers[i] != null)
                    {
                        passengers[i - 1] = passengers[i];
                        passengers[i] = null;
                        tmp = null;
                    }
                    else if (passengers[i] == null && i != 0)
                    {
                        break;
                    }
                }
                else
                {
                    tmp = passengers[i];
                }
            }
        }
    }
}
