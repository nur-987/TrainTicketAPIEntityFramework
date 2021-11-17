using System;
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
            _trainticketClient.BaseAddress = new Uri("https://localhost:44355");

            //no more initialise => SEEDING

        }

        public List<User> GetAllUsers()
        {
            var responseTask = _trainticketClient.GetAsync("api/user");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<User>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;

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

        public Ticket GetSelectedUserDetail(int userId)
        {
            //latest train history
            var responseTask = _trainticketClient.GetAsync("api/user/getdetails/" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Ticket>();
                readTask.Wait();
                return readTask.Result;
            }

            return null;

        }

        public IQueryable<Ticket> GetSelectedUserAllDetail(int userId)
        {
            //all the train history
            var responseTask = _trainticketClient.GetAsync("api/user/getalldetails/" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IQueryable<Ticket>>();
                readTask.Wait();
                return readTask.Result;
            }

            return null;

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
                if (string.Equals(response, "true"))
                {
                    return true;
                }

            }
            return false;
        }

        
        public Ticket GetSelectedUserDetailSpecific(int userId)
        {
            //only current train history (no user)
            var responseTask = _trainticketClient.GetAsync("api/user/getdetails/" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<User>();
                readTask.Wait();
                Ticket specificTrainDetail = readTask.Result.TicketHistory.LastOrDefault();
                return specificTrainDetail;
            }

            return null;

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
            var responseTask = _trainticketClient.GetAsync("api/train/getbetween/" + start + "/" + end);
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

        public IQueryable<Ticket> DisplayAllTicket()
        {
            var responseTask = _trainticketClient.GetAsync("api/ticket");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IQueryable<Ticket>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        public User BuyTicket(int userId, int numofTickets, TrainClassEnum selectedClass, Train selectedTrain)
        {
            var responseTask = _trainticketClient.GetAsync("api/ticket/buy/" + userId + "/" + numofTickets + "/" + selectedClass);      //train class from body??
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<User>();
                readTask.Wait();
                return readTask.Result;
            }

            return null;

        }

        public string GrandTotal(int userId)
        {   //different return from API. Possible?
            var responseTask = _trainticketClient.GetAsync("api/ticket/finalcost/"+ userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                return readTask.Result;
            }

            return null;
        }
    }
}
