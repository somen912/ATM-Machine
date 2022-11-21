// See https://aka.ms/new-console-template for more information
using System;
using System.Data.SqlClient;

namespace ATM_Software // Note: actual namespace depends on the project name.
{
    internal class Program
    {
       

            static void Main(string[] args)
            {
                

                string connectionString = "Data Source=LAPTOP-2T793RT5\\SQLEXPRESS;Initial Catalog=Bank_Database;Integrated Security=True";
                SqlConnection com = new SqlConnection(connectionString);
                com.Open();
                Console.WriteLine("Connected to Database");
                com.Close();

                string userinput = String.Empty;
                int userPIN = int.MinValue;
                int deposit= 0;
                int withdrawl = 0;
                bool flag = false;


            Console.WriteLine("Welcome to ATM Machine\n");
                Console.WriteLine("Enter your card number");

                while (true)
                {

                    userinput = Console.ReadLine();
                    if (string.IsNullOrEmpty(userinput)) { Console.WriteLine("Please enter your card number"); }
                    else { break; }
                
                }

                Console.WriteLine("Enter your PIN");
                while (true)
                {


                    try { userPIN = int.Parse(Console.ReadLine());
                          break;
                        }
                    catch { Console.WriteLine("Please entere your PIN"); }
                    

                }

                com.Open();
                string stringQuery = $"select * from Debitcard_Database where Card_Number = '{userinput}' and Card_PIN = {userPIN}";

                SqlCommand command = new SqlCommand(stringQuery,com);
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows){ Console.WriteLine("Log in successful\n"); }
                else { Console.WriteLine("\nInvalid Login Application Closed");System.Environment.Exit(0); }

                com.Close();


                com.Open();
                stringQuery = $"select CardHolder_Name from Debitcard_Database where Card_Number = '{userinput}'";
                SqlCommand command2 = new SqlCommand(stringQuery, com);
                SqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read()) { Console.WriteLine($"Welcome Mr. {reader2.GetValue(0)}!!!"); }
                com.Close();


                Console.WriteLine("\nSelect an option\n1. Balance Enquiry\n2. Cash Deposit\n3. Cash Withdrawl\n4. Exit");
                while(true)
                {
                try
                {
                    userPIN = int.Parse(Console.ReadLine());
                    if (userPIN == 1)
                    {
                        com.Open();
                        stringQuery = $"select Balance from Debitcard_Database where Card_Number = '{userinput}'";
                        SqlCommand command3 = new SqlCommand(stringQuery, com);
                        SqlDataReader reader3 = command3.ExecuteReader();

                        while (reader3.Read()) { Console.WriteLine($"Your current balance is INR {reader3.GetValue(0)}"); }
                        com.Close();
                        break;
                    }
                    else if (userPIN == 2)
                    {


                        Console.WriteLine("Enter Amount to Deposit");
                        
                        while(true)
                        {
                            try { deposit = int.Parse(Console.ReadLine()); break; }
                            catch { Console.WriteLine("Enter a Valid Amount"); }
                        }
                        

                        com.Open();
                        stringQuery = $"select Balance from Debitcard_Database where Card_Number = '{userinput}'";
                        SqlCommand command4 = new SqlCommand(stringQuery, com);
                        SqlDataReader reader4 = command4.ExecuteReader();
                        while(reader4.Read())
                        {
                            deposit += Convert.ToInt32(reader4.GetValue(0));
                        }
                        com.Close();

                        com.Open();
                        stringQuery = $"update Debitcard_Database set Balance = {deposit} where Card_Number = '{userinput}'";
                        SqlCommand command5 = new SqlCommand(stringQuery, com);
                        SqlDataReader reader5 = command5.ExecuteReader();

                        Console.WriteLine("Amount Diposited");
                        com.Close();

                        com.Open();
                        stringQuery = $"select Balance from Debitcard_Database where Card_Number = '{userinput}'";
                        SqlCommand command6 = new SqlCommand(stringQuery, com);
                        SqlDataReader reader6 = command6.ExecuteReader();
                        while (reader6.Read())
                        {
                            Console.WriteLine($"Your Current Account Balance is INR: {reader6.GetValue(0)}");
                        }
                        com.Close();
                        break;

                    }
                    else if (userPIN == 3)
                    {
                        Console.WriteLine("Emter amount you want to withdrawl");
                        while(true)
                        {
                            try { withdrawl = int.Parse(Console.ReadLine()); break; }
                            catch { Console.WriteLine("Enter a valid amount"); }
                        }

                       // Console.WriteLine($"Your withdrawl amount is: {withdrawl}");
                        com.Open();
                        stringQuery = $"select Balance from Debitcard_Database where Card_Number = '{userinput}'";
                        SqlCommand command7 = new SqlCommand(stringQuery, com);
                        SqlDataReader reader7 = command7.ExecuteReader();
                        while (reader7.Read())
                        {
                            
                            if(withdrawl< Convert.ToInt32(reader7.GetValue(0)))
                            {
                                withdrawl = Convert.ToInt32(reader7.GetValue(0)) - withdrawl;
                                flag = true;
                                
                            }
                            else
                            {
                                Console.WriteLine("Your don't have sufficient balance");
                            }
                        }
                        com.Close();

                        if(flag == true)
                        {
                            com.Open();
                            stringQuery = $"update Debitcard_Database set Balance = {withdrawl} where Card_Number = '{userinput}'";
                            SqlCommand command8 = new SqlCommand(stringQuery, com);
                            SqlDataReader reader8 = command8.ExecuteReader();
                            com.Close();
                        }

                        com.Open();
                        stringQuery = $"select Balance from Debitcard_Database where Card_Number = '{userinput}'";
                        SqlCommand command9 = new SqlCommand(stringQuery, com);
                        SqlDataReader reader9 = command9.ExecuteReader();
                        while (reader9.Read())
                        {
                            Console.WriteLine($"Your Current Account Balance is INR: {reader9.GetValue(0)}");
                        }
                        com.Close();
                        break;

                    }
                    else { System.Environment.Exit(0); }
                }

                catch { Console.WriteLine("Invalid Input"); }
            
                }









        }
        }
    }

