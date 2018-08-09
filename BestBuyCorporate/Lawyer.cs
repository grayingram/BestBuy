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
        public int NotNeg(int num, string query)
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
        public int GetMonth(string question)
        {
            int userInput = GetInt(question);
            while(!(userInput >=1 && userInput <= 12))
            {
                Console.WriteLine("That is not a valid month.");
                userInput = GetInt(question);
            }
            userInput = NotNeg(userInput, question);
            return userInput;
            

        }
        public int GetDay(string question, int month, int year)
        {
            int userInput = GetInt(question);
            bool fact = false;
            while (!(fact))
            {
                if(month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                {
                    while(!(userInput >= 1 && userInput <= 31))
                    {
                        Console.WriteLine("Not a valid day of the month, enter a valid day?");
                        userInput = GetInt(question);
                    }
                    fact = true;
                }
                else if(month == 4 || month == 6 || month == 9 || month == 11)
                {
                    while (!(userInput >= 1 && userInput <= 30))
                    {
                        Console.WriteLine("Not a vaild day of the month, enter a valid day?");
                        userInput = GetInt(question);
                    }
                    fact = true;
                }
                else
                {
                    if(year % 4 == 0)
                    {
                        while(!(userInput >= 1 && userInput <= 29))
                        {
                            Console.WriteLine("Not a valid day of the month, enter a valid day?");
                            userInput = GetInt(question);
                        }
                    }
                    else
                    {
                        while (!(userInput >= 1 && userInput <= 28))
                        {
                            Console.WriteLine("Not a valid day of the month, enter a valid day?");
                            userInput = GetInt(question);
                        }
                    }
                }
            }
            return userInput;
        }
        public int GetYear(string question)
        {
            int userInput = GetInt(question);
            userInput = NotNeg(userInput, question);
            return userInput;
        }
    }
}
