﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Common;

namespace TrainTicketAPIEntityFrmk.ViewModels
{
    class TrainTicketViewModel
    {
        //subscribe to service

        private readonly HttpClient _trainticketClient;

        internal TrainTicketViewModel()
        {
            _trainticketClient = new HttpClient();
            _trainticketClient.BaseAddress = new Uri("https://trainticket.booking");

            //initialise the user
            var responseTask = _trainticketClient.GetAsync("api/user");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {

            }

            //initialise the train
            var responseTask2 = _trainticketClient.GetAsync("api/train");
            responseTask2.Wait();
            var result2 = responseTask.Result;
            if (result2.IsSuccessStatusCode)
            {

            }

        }

        public int AddNewUser(string name)
        {
            int userId = 0;
            var responseTask = _trainticketClient.GetAsync("api/user/adduser/" + name);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                userId = Int32.Parse(readTask.Result);  
            }

            return userId;
        }

        public bool CheckUserExist(int userId)
        {
            var responseTask = _trainticketClient.GetAsync("api/user/checkexist/" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                string response = readTask.Result;
                if (string.Equals(response,"true"))
                {
                    return true;
                }

            }
            return false;
        }

        public List<string> GetAllStartStations()
        {
            var responseTask = _trainticketClient.GetAsync("api/train/getstart/");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<string>>(); 
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        public List<string> GetAllEndStations()
        {
            var responseTask = _trainticketClient.GetAsync("api/train/getend/");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<string>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        public List<Train> GetTrainsBetweenStations(string start, string end)
        {
            var responseTask = _trainticketClient.GetAsync("api/train/getbetween?start=" +start+"&end=" +end);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<Train>>();         
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        public void BuyTicket(int userId, Train selectedTrain, TrainClassEnum selectedClass, int numofTickets)
        {

        }


    }
}