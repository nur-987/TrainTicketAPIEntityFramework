﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Common;
using TrainTicketAPIEntityFrmk.ViewModels;

namespace TrainTicketAPIEntityFrmk
{
    class Program
    {

        static void Main(string[] args)
        {
            TrainTicketViewModel trainvm = new TrainTicketViewModel();

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
                            int userID = trainvm.AddNewUser(name);
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
                            if (!trainvm.CheckUserExist(userId))
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
                    bool startStationFlag = true;
                    string startStation = string.Empty;
                    while (startStationFlag)
                    {
                        Console.WriteLine("Please choose your start station.");
                        List<string> startStationList = trainvm.GetAllStartStations();
                        foreach (string station in startStationList)
                        {
                            Console.WriteLine(startStationList.IndexOf(station) + 1 + ") " + station);
                        }
                        try
                        {
                            int startStationInput = int.Parse(Console.ReadLine());
                            startStation = startStationList[startStationInput - 1];
                            startStationFlag = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please enter a valid source station");
                            continue;
                        }
                    }
                    bool endStationFlag = true;
                    string endStation = string.Empty;
                    while (endStationFlag)
                    {
                        Console.WriteLine("Please choose your end station.");
                        List<string> endStationList = trainvm.GetAllEndStations();
                        foreach (string station in endStationList)
                        {
                            Console.WriteLine(endStationList.IndexOf(station) + 1 + ") " + station);
                        }
                        try
                        {
                            int endStationInput = int.Parse(Console.ReadLine());
                            endStation = endStationList[endStationInput - 1];
                            endStationFlag = false;
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
                        List<Train> availableTrainRoutesList = trainvm.GetTrainsBetweenStations(startStation, endStation);
                        if (availableTrainRoutesList.Count == 0)
                        {
                            Console.WriteLine("No trains available between the start and end stations");
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

                            trainvm.BuyTicket(userId, trainSelected, selectedClass, tempNumofTicket);
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
                            if (trainvm.CheckUserExist(userId))
                            {
                                trainvm.GetSelectedUserAllDetails(userId);
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