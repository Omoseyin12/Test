using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;


class Account
{
    public int id;
    public int customerid;
    public string AccountNumber;
    public decimal Accountbalance;
    public string AccountType;

    public Account(int id, int customerid, string accountNumber, decimal accountbalance, string accountType)
    {
        this.id = id;
        this.customerid = customerid;
        AccountNumber = accountNumber;
        Accountbalance = accountbalance;
        AccountType = accountType;
    }
   
}

public  class Customer
{
    public int Id;
    public string FirstName;
    public string LastName;
    public string Email;
    public string PassWord;
    public string PhoneNumber;

    public  Customer(string FirstName, string LastName, string Email, string PassWord, string PhoneNumber, int id)
    {
        this.Id = id;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.PassWord = PassWord;
        this.PhoneNumber = PhoneNumber;
        
    }
}

namespace omoseyin
{
    public class Program
    {
        static List<Account> NoAccount = new List<Account>();
        static List<Customer> bankCustomers = new List<Customer>();
        static void Main(string[] args)
        {








        start:
            Header();
            Console.WriteLine("welcome to Tomisin bank plc");
            Console.WriteLine("we are more than a bank, we are FAMILY");
            Console.WriteLine("press 1 to sign up");
            Console.WriteLine("press 2 to sign in if you are already part of this wonderful FAMILY");


            int yourChoice = int.Parse(Console.ReadLine());

            Customer customer = null;

            Console.Clear();
            switch (yourChoice)
            {
                case 1:

                    int customerId = CreateCustomer();

                    CreateAccount(customerId);

                     customer=  Login();

                     Dashboard(customer);

                    break;
                case 2:

                    customer = Login();
                    Dashboard(customer);

                    break;
                default:
                    Console.WriteLine("you have picked a wrong option, dont give up yet, TRY AGAIN!");
                    goto start;
            }




        }

        static void CreateAccount(int customerid)
        {
            Header();
            Console.WriteLine("press 1 for savings account and 2 for current");
            int type = int.Parse(Console.ReadLine());

            var accountnumber = AccountNumber(10);

            int Accountid = NoAccount.Count() + 1;

            string accountType = "";

            if (type == 1)
                accountType = "Savings";
            else
                accountType = "Current";

            Account newAccount = new Account(Accountid, customerid, accountnumber, 0, accountType);

            NoAccount.Add(newAccount);
            Console.Clear();

        }

        static int CreateCustomer()
        {
            Header();
            Console.WriteLine("we are you made this great decision of joining this wonderful FAMILY");
            Console.WriteLine("please provide your first name");
            string firstName = Console.ReadLine();

            Console.WriteLine("please provide your last name");
            string lastName = Console.ReadLine();

            Console.WriteLine("please provide a valid email, you are almost there");
            string email = Console.ReadLine();

            Console.WriteLine("please choose a password, should not be more than 6 characters");
            string password = Console.ReadLine();

            Console.WriteLine("confirm password please");
            string confirmPassword = Console.ReadLine();

            Console.WriteLine("provide your mobile number");
            string mobileNumber = Console.ReadLine();

            int customerId = bankCustomers.Count() + 1;

            Customer newCustomer = new Customer(firstName, lastName, email, password, mobileNumber, customerId);

            bankCustomers.Add(newCustomer);
            Console.Clear();

            return customerId;



        }

        static Customer Login()
        {
            Header();
            Console.WriteLine("please enter your valid email");
            string validEmail = Console.ReadLine();
            Console.WriteLine("please enter your password");
            string loginPassword = Console.ReadLine();
            Customer customer = bankCustomers.FirstOrDefault(x => x.Email == validEmail && x.PassWord == loginPassword);
            if (customer == null)
                Login();

            Console.Clear();

            return customer;
        }

        public static void Header()
        {
            Console.WriteLine("");
            Console.WriteLine();
            Console.WriteLine("============================================== WELCOME TO TOMISIN BANK PLC========================================");

            Console.WriteLine();
            Console.WriteLine();
        }

        public static void  Dashboard(Customer customer)
        {
            Header();

            Console.WriteLine($"HELLO {customer.FirstName} {customer.LastName}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("1.  Transfer");
            Console.WriteLine("2.  Open additional account");
            Console.WriteLine("3.  Change password");
            Console.WriteLine("4.  View Accounts");

            Console.WriteLine("ENTER AN OPTION : ");
            int val = int.Parse(Console.ReadLine());

            switch (val)
            {
                case 2 :
                    CreateAccount(customer.Id);
                    ViewAccount(customer);
                    Console.ReadLine();
                    break;
                case 4 :
                    ViewAccount(customer);
                    Console.ReadLine();
                    break;
                default:
                    break;
            }

        }

        public static void ViewAccount(Customer customer)
        {
            List<Account> customerAccounts = NoAccount.Where(x=>x.customerid==customer.Id).ToList();
            Console.WriteLine( "");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("                                         ACCOUNT INFORMATION                                      ");
            Console.WriteLine("|---------------|------------------|----------------|--------------|------------------|");
            Console.WriteLine("|   ID          |    TYPE          |    BALANCE     |  CUSTOMERID  |   ACCOUNT NUMBER |");
            Console.WriteLine("|---------------|------------------|----------------|--------------|------------------|");

            foreach (var account in customerAccounts)
            {
                Console.WriteLine($"|  {account.id}  |  {account.AccountType} |    {account.Accountbalance} |   {account.customerid}|   {account.AccountNumber}   |");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            }

        }


        public static string AccountNumber(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());


            return s;
        }

    }
}

