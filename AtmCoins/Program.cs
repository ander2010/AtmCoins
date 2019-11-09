using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmCoins
{
   public class Program
    {
        /**
         * summary: Mainly the structure with the structure of the bills
         *          
             */
        static Dictionary<string, int> Bills = new Dictionary<string, int>
        {   { "100",10 },
            { "50",10 },
            { "20",10 },
            { "10",10 },
            { "5",10 },
            { "1",10 },
        };

        public static Dictionary<string, int> BillsProperty { get => Bills; set => Bills = value; }

        static void Main(string[] args)
        {
            string outOfProgram = string.Empty;
            while (outOfProgram != "Q")
            {
                /**
                 * Message for showing different options
                 */
                Console.WriteLine("Insert R if you want Restocks the cash machine to the original pre-stock levels\nInsert W <dollar amount> - Withdraws that amount from the cash machine\nInsert I <denominations>for displaying the number of bills in that denomination present in the cash machine\nInsert Q if you want out of the program ");

                string option = Console.ReadLine();//option read the console string

                string auxiliateCommand = option.Substring(1);//auxiliateCommand  take the information after the first character because some  entry need a Letter and another Command
                option = option.Substring(0, 1);

                /**
                 * different options for choosing
                 */
                try
                {
                    switch (option)
                    {
                        case "R":
                            ReloadDrawer();

                            break;
                        case "I":
                            DenominationBills(auxiliateCommand);
                            break;
                        case "W":
                            Withdraw(auxiliateCommand);
                            break;
                        case "Q":
                            Console.WriteLine("thanks for using the ATM goodbye");
                            outOfProgram = "Q";
                            break;

                        default:
                            throw new Exception("Invalid Command");
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }

            Console.ReadLine();
        }

        /**
         * param name="auxiliateCommand" is a command some time is the rest after the option
         * summary you can write I $20 $1 $100 or you can use I 20 1 10 always using space among information
          */
        private static void DenominationBills(string auxiliateCommand)
        {
            string[] billsArray = auxiliateCommand.Trim().Split(' ');
            string removedGarbage;
            foreach (var item in billsArray)
            {
                removedGarbage = item;
                if (item.Contains("$"))
                {
                    removedGarbage = item.Replace("$", "").Trim();
                }
                PrintBills(removedGarbage);
            }

        }

        /**
         * summary: when you press R all Bills is reloading with parameters for default
         *          
         */
        private static void ReloadDrawer()
        {
            BillsProperty = BillsProperty.ToDictionary(kvp => kvp.Key, kvp => kvp.Value * 0 + 10);
            PrintBills(BillsProperty);
        }

        /**
         * summary: when you press W ${amount} this method return the amount and make the operation resting in bills the money
         *  param name="withdraw" amount that you want to obtain        
         */
        static void Withdraw(string withdraw)
        {


            int debitRetire;

            if (withdraw.Trim().Contains("$"))
            {
                debitRetire = int.Parse(withdraw.Trim().Replace("$", ""));
            }
            else
            {
                debitRetire = int.Parse(withdraw.Trim());
            }


            List<int> solution = new List<int>(); //set of solution/ kind of bills returned    


            int x = 0; //selected money to return
            int i = 0; //counter
            int acum = 0; //accumulate the returned money
            bool errors = false;
            int dif = debitRetire; //The difference in each debitRetire with respect to the requested amount

            while (acum != debitRetire)
            { //as long as the money returned so far do not meet the requested value ...
                dif = dif - x; //saving the difference
                x = Selecction(dif); //Choice of the right money and save                
                acum += x; //save the chosen currency
                solution.Add(x); //add to solution set
                i++;

                if (x == -1)//asking about the x because I need to ask if it is -1 because that meaning that doesn't have enough funds
                {
                    errors = true;
                    Console.WriteLine("Failure: insufficient funds");
                    Restauring(solution);
                    break;

                }
            }
            if (!errors)
            {
                Console.WriteLine("Success:Dispensed ${0}", debitRetire);
                PrintBills(BillsProperty);
            }

        }


        /**
         * param name="solution" in this parameter I am saving all values of the currency in case that the operation failed
         summary: always return the money to the Bills
             */
        private static void Restauring(List<int> solution)
        {
            for (int i = 0; i < solution.Count - 1; i++)
            {
                BillsProperty[solution[i].ToString()] = BillsProperty[solution[i].ToString()] + 1;
            }
        }

        /**
         * param name="dif" is the difference  that I need to check
         * summary: this function  choose the ideal coin
         */
        public static int Selecction(int dif)
        {
            int auxVariable = 0;
            foreach (var item in BillsProperty)//foreach of the available coins          
            {
                if (item.Value > 0)//if that kind of coins there is money
                {
                    if (int.Parse(item.Key) <= dif)   //if it is less than the amount to be returned                
                    {
                        auxVariable = int.Parse(item.Key); //saving the ideal coin                      
                        BillsProperty[item.Key] = item.Value - 1;
                        break;
                    }
                }
                else
                {
                    return -1;

                }

            }

            return auxVariable;

        }

        /**
         * summary print the full amount of money
         * param name="BillsATM" data structure for saving the information, in this case, is where I am saving the kind of money and the amount
        */
        static void PrintBills(Dictionary<string, int> BillsATM)
        {
            Console.WriteLine("Machine balance:");
            foreach (var keyValuePair in BillsATM)
            {
                Console.WriteLine("${0}-{1}", keyValuePair.Key, keyValuePair.Value);

            }
            Console.WriteLine("--------------------------------------------");
        }

        /**
         * summary print the  amount of money of the one kind of currency
         * param name="keyOfBills" this parameter is the entrance of the 1 key for knowing how many currencies exist.
        */
        static void PrintBills(string keyOfBills)
        {

            Console.WriteLine("${0}-{1}", keyOfBills, BillsProperty[keyOfBills]);


        }
        /**
         * this method was used for testing mainly because it was important to separate some responsibility in the functionality
         */
        public static string PrintBill(string keyOfBills)
        {
           
              
                return BillsProperty[keyOfBills].ToString();
            
          
          
        }

    }
}

