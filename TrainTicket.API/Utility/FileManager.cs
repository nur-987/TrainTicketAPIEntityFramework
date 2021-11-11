using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TrainTicket.API.Utility
{
    public class FileManager : IFileReadWrite
    {
        public FileManager()
        {

        }

        public string ReadAllText(string FileName)
        {
            string content = null;
            try
            {
                content = File.ReadAllText(FileName);
                //return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error in file! {ex.Message}");
            }

            return content;

        }

        public bool WriteAllText(string FileName, string InputDetails)
        {
            try
            {
                File.WriteAllText(FileName, InputDetails);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"error in file! {ex.Message}");
                return false;
            }

        }
    }
}