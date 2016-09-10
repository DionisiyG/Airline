using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 30);

            IFlight[] flights = new Flight[20];
            FlightManager.FillFlights(flights, new Random().Next(5, 15));
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to flight information service vol.2!");
                Console.WriteLine("1. View the information about  all Flights");
                Console.WriteLine("2. View the information about all flight’s passengers");
                Console.WriteLine("3. Actions with flights");
                Console.WriteLine("4. Actions with passengers");
                Console.WriteLine("5. Search passengers on board");
                Console.WriteLine("6. Your bid - our offer (Enter the amount and we will find all flights with the lower price!)");
        
                byte mode = 0;
                bool tryInput = byte.TryParse(Console.ReadLine(), out mode);
                if (!tryInput)
                {
                    Console.WriteLine("Please enter valid input.");
                }
                else
                    switch (mode)
                    {
                        case 1:
                            Console.Clear();
                            Printer.PrintAllFlights(flights);
                            break;
                        case 2:
                            ConsoleMenu.PrintPassengerMenu(flights);
                            break;
                        case 3:
                            ConsoleMenu.GetFlightMenu(flights);
                            break;
                        case 4:
                            ConsoleMenu.OperatePassengerMenu(flights);
                            break;
                        case 5:
                            ConsoleMenu.SearchMenu(flights);
                            break;
                        case 6:
                            Printer.PrintAllFlights(Searcher.SearchFlightByTicket(flights));
                            break;

                        default:
                            Console.WriteLine("Incorrect input");
                            break;
                    }
                            Console.WriteLine(@"Press Esc to exit.
Enter to go back.");
                    
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
            
        }
    }

