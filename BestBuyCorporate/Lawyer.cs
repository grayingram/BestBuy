using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCorporate
{
    class Lawyer
    {
        public string GetResponse(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("Please enter an actual value for the question\n" + question);
                response = Console.ReadLine();
            }

            return response;
        }
        public int GetInt(string question)
        {
            string response = GetResponse(question);
            int userInput;

            while (int.TryParse(response, out userInput) == false)
            {
                Console.WriteLine("Unable to determine number. ");
                response = GetResponse(question);
            }
            userInput = NotNeg(userInput, response, question);

            return userInput;
        }
        public int NotNeg(int num, string answer, string query)
        {

            int userInput = 0;

            while ((num < 0) || (num == 0))
            {
                Console.WriteLine("Must be a nonnegative nonzero number");
                num = GetInt(query);
            }
            userInput = num;
            return userInput;
        }
        public decimal NotNeg(decimal num, string answer, string query)
        {
            decimal userInput = 0;

            while ((num < 0) || (num == 0))
            {
                Console.WriteLine("Must be a nonnegative nonzero number");
                num = GetDecimal(query);
            }
            userInput = Math.Round(num, 2);
            return userInput;
        }
        public decimal GetDecimal(string question)
        {
            string response = GetResponse(question);
            decimal userInput = 0;

            while (decimal.TryParse(response, out userInput) == false)
            {
                Console.WriteLine("Unable to determine number. ");
                response = GetResponse(question);
            }
            userInput = NotNeg(userInput, response, question);

            return userInput;
        }
        public bool GetYesNo(string question)
        {
            string response = GetResponse(question);
            response = response.ToUpper();
            while (!(response.Equals("YES")) && !(response.Equals("NO")))
            {
                Console.WriteLine("Please enter yes or no.");
                response = GetResponse(question);
                response = response.ToUpper();
            }
            if (response == "YES")
            {
                return true;
            }
            return false;

        }
    }
}
