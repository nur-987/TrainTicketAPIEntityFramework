using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;

namespace TrainTicketAPIEntityFrmk
{
    class Program
    {

        static void Main(string[] args)
        {
            UserWorker user = new UserWorker();
            user.Initialize();

            IAppConfiguration configuration = new AppConfiguration();
            configuration.Initialize(300, 250, 150, 3.5, 2.5, 1.5);

            TrainWorker train = new TrainWorker(configuration);
            train.Initialize();

            TicketManager ticketManager = new TicketManager(configuration);
            bool b = true;

            Console.WriteLine("Welcome to Train Ticket Booking Service");
            while (b)
            {
                int input1 = 0;
                Console.WriteLine("Choose from the following menus");
                Console.WriteLine("1) Buy Tickets");
                Console.WriteLine("2) Purchase History");
                Console.WriteLine("3) Exit ");
                try
                {
                    input1 = Int32.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wrong Input. Please choose among the available options.");
                    continue;
                }
                if (input1 == 1) //buy ticket
                {
                    int userId = 0;
                    var trainSelected = new Train();

                    bool userFlag = true;
                    while (userFlag)
                    {
                        Console.WriteLine("Are you an existing user? (Y/N) "); //check if existing user, else create user
                        string input = Console.ReadLine();
                        if (string.Equals(input, "N", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Enter your name: "); //create new user
                            string name = Console.ReadLine();
                            user.AddNewUser(name, out userId);
                            userFlag = false;

                        }
                        else if (string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Enter your user id: ");
                            try
                            {
                                userId = Int32.Parse(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Wrong input. Please enter a valid user id");
                            }
                            if (!user.CheckUserExist(userId))
                            {
                                Console.WriteLine("User does not exist. Please enter a valid user id.");
                            }
                            else
                            {
                                Console.WriteLine("User Validated.");
                                userFlag = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong input. Please choose among the available options.");
                        }
                    }
                SelectTrain:
                    bool sourceStationFlag = true;
                    string sourceStation = string.Empty;
                    while (sourceStationFlag)
                    {
                        Console.WriteLine("Please choose your source station.");
                        List<string> sourceStationList = train.GetAllSourceStations();
                        foreach (string station in sourceStationList)
                        {
                            Console.WriteLine(sourceStationList.IndexOf(station) + 1 + ") " + station);
                        }
                        try
                        {
                            int sourceStationInput = int.Parse(Console.ReadLine());
                            sourceStation = sourceStationList[sourceStationInput - 1];
                            sourceStationFlag = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please enter a valid source station");
                            continue;
                        }
                    }
                    bool destinationStationFlag = true;
                    string destinationStation = string.Empty;
                    while (destinationStationFlag)
                    {
                        Console.WriteLine("Please choose your destination station.");
                        List<string> destinationStationList = train.GetAllDestinationStations();
                        foreach (string station in destinationStationList)
                        {
                            Console.WriteLine(destinationStationList.IndexOf(station) + 1 + ") " + station);
                        }
                        try
                        {
                            int destinationStationInput = int.Parse(Console.ReadLine());
                            destinationStation = destinationStationList[destinationStationInput - 1];
                            destinationStationFlag = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please enter a valid destination station");
                            continue;
                        }
                    }

                    bool userSelectTrainFlag = true;
                    while (userSelectTrainFlag)
                    {
                        List<Train> availableTrainRoutesList = train.GetTrainsBetweenStations(sourceStation, destinationStation);
                        if (availableTrainRoutesList.Count == 0)
                        {
                            Console.WriteLine("No trains available between the source and destination stations");
                            goto SelectTrain;
                        }
                        else
                        {
                            Console.WriteLine("Here are the available train timings: ");
                            foreach (Train item in availableTrainRoutesList)
                            {
                                Console.WriteLine("Train Id: " + item.TrainId);
                                Console.Write("Start Destination: " + item.StartDestination + "   ");
                                Console.WriteLine("End Destination: " + item.EndDestination);
                                Console.Write("Departure Time: " + item.DepartureTime.ToShortTimeString() + "   ");
                                Console.WriteLine("Arrrival Time: " + item.ArrivalTime.ToShortTimeString());
                            }
                        }

                        Console.WriteLine("Which train ticket would u like to purchase? Input Train Id"); //purchase by train ID
                        int temptrainId = 0;
                        try
                        {
                            temptrainId = Int32.Parse(Console.ReadLine());
                            trainSelected = availableTrainRoutesList.First(x => x.TrainId == temptrainId);

                            //display class options and their cost.
                            Console.WriteLine("You have selected a train");
                            Console.WriteLine($"FROM: {trainSelected.StartDestination} TO: {trainSelected.EndDestination}");
                            Console.WriteLine($"Train Departure Time: {trainSelected.DepartureTime.ToShortTimeString()}");
                            Console.WriteLine($"Train Arrival Time: {trainSelected.ArrivalTime.ToShortTimeString()}");

                            userSelectTrainFlag = false;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please choose among the available Train Id.");
                            continue;
                        }

                    }

                    bool travelClassSelectionFlag = true;
                    while (travelClassSelectionFlag)
                    {
                        int tempClass = 0;
                        int tempNumofTicket = 0;
                        try
                        {
                            Console.WriteLine("Please choose a train class: ");
                            Console.WriteLine($"1) {TrainClassEnum.FirstClass} Price:$ {trainSelected.FirstClassFare}");
                            Console.WriteLine($"2) {TrainClassEnum.BusinessClass} Price:$ {trainSelected.BusinessClassFare}");
                            Console.WriteLine($"3) {TrainClassEnum.Economy} Price:$ {trainSelected.EconomyClassFare}");

                            tempClass = Int32.Parse(Console.ReadLine());
                            TrainClassEnum selectedClass = (TrainClassEnum)tempClass - 1;
                            //enum out of range exception not handled

                            Console.WriteLine("Enter the total number of tickets to purchase:");
                            tempNumofTicket = Int32.Parse(Console.ReadLine());

                            ticketManager.BuyTicket(userId, trainSelected, selectedClass, tempNumofTicket);
                            travelClassSelectionFlag = false;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please choose among the available travel class and enter the correct number");
                            continue;
                        }

                    }

                    bool finalConfirmation = true;
                    while (finalConfirmation)
                    {
                        //subscribes to event
                        //ask user to make payment after completed booking
                        ticketManager.TransactionComplete += Calculation_TransactionComplete;


                        Console.WriteLine("Here are your booking details.");
                        user.GetSelectedUserFinalDetail(userId);

                        ticketManager.GrandTotal(userId);
                        finalConfirmation = false;

                    }

                    //after booking complete, exit prog
                    Console.WriteLine("You have completed your booking. Thank you.");
                    b = false;


                }
                else if (input1 == 2)
                {
                    bool userDetailsFlag = true;
                    while (userDetailsFlag)
                    {
                        Console.WriteLine("Enter your userID");
                        int userId = 0;
                        try
                        {
                            userId = Int32.Parse(Console.ReadLine());
                            if (user.CheckUserExist(userId))
                            {
                                user.GetSelectedUserAllDetails(userId);
                                userDetailsFlag = false;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input. Please enter valid user id");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please enter correct user id");
                        }
                    }

                }
                else if (input1 == 3)
                {
                    Console.WriteLine("exiting program. Good Bye");
                    b = false;
                }
                else
                {
                    Console.WriteLine("Wrong Input. Please choose among the available options.");
                }

            }
            Console.ReadLine();
        }
        private static void Calculation_TransactionComplete(double totalCost)
        {
            Console.WriteLine("Please make payment of $" + totalCost + " within 24 hours");
            Console.WriteLine("proceed to payment at: www.trainticketpayment.com");
        }

    }
}
