using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TrainTicket.Common;
using TrainTicket.Common.DTO;

namespace TrainTicketAPIEntityFrmk.ViewModels
{
    class TrainTicketViewModel
    {
        //subscribe to service
        private readonly HttpClient _trainticketClient;

        internal TrainTicketViewModel()
        {
            _trainticketClient = new HttpClient();
            _trainticketClient.BaseAddress = new Uri("https://localhost:44375/");

            //no more initialise => SEEDING

        }

        public int AddNewUser(string name)
        {
            int userId = 0;
            var responseTask = _trainticketClient.PostAsJsonAsync("api/user/adduser/" + name, name );
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                userId = int.Parse(readTask.Result);
                return userId;
            }

            return userId;
        }

        public TicketDTO GetSelectedUserDetail(int userId)
        {
            //latest train history
            var responseTask = _trainticketClient.GetAsync("api/user/getdetails/" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TicketDTO>();
                readTask.Wait();
                return readTask.Result;
            }

            return null;

        }

        public IList<TicketDTO> GetSelectedUserAllDetail(int userId)
        {
            //all the train history
            var responseTask = _trainticketClient.GetAsync("api/user/getalldetails/" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                var stringResult = readTask.Result; //giving me a json string
                IList<TicketDTO> convert = JsonConvert.DeserializeObject<IList<TicketDTO>>(stringResult);

                return convert;
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

        public IList<string> GetAllStartStations()
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

        public IList<string> GetAllEndStations()
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

        public IList<TrainDTO> GetTrainsBetweenStations(string start, string end)
        {
            var responseTask = _trainticketClient.GetAsync("api/train/getbetween/" + start + "/" + end);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<TrainDTO>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        public void BuyTicket(int userId, int numofTickets, TrainClassEnum selectedClass, TrainDTO selectedTrain)
        {
            var responseTask = _trainticketClient.PostAsJsonAsync("api/ticket/buy/" + userId + "/" + numofTickets + "/" + selectedClass, selectedTrain);      //train class from body??
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UserDTO>();
                readTask.Wait();
            }
        }

        public string GrandTotal(int userId)
        {   

            double content = 0;
            var responseTask = _trainticketClient.PutAsJsonAsync("api/ticket/finalcost/"+ userId, content);
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
