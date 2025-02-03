using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace PayrollSoftware
{

    class Staff
    {
        //fields
        private float _hourlyRate;
        private int _hWorked;

        //Properties
        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }
        public int HouresWorked
        {
            get { return _hWorked; }
            set
            {
                if (value > 0)
                {
                    _hWorked = value;

                }
                else
                {

                    _hWorked = 0;

                }


            }
        }



        public Staff(string name, float rate)
        {
            NameOfStaff = name;
            _hourlyRate = rate;
        }



        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculateing Pay...");
            BasicPay = _hWorked * _hourlyRate;
            TotalPay = BasicPay;

        }

        public override string ToString()
        {
            return $"Staff name: {NameOfStaff}, \nhRate: {_hourlyRate}, \nhWorked: {_hWorked}, \ntotal pay: {TotalPay} .";


        }

    }

    class Manager : Staff
    {
        private const float managerHourlyRate = 50f;


        public int Allowance {get; private set;}


  


        public Manager(string name) : base(name, managerHourlyRate)
        {
            //NameOfStaff = name;
            //base.NameOfStaff = name;
            //= managerHourlyRate;
        }



        public override void CalculatePay()
        {
            base.CalculatePay();
            Allowance = 1000;

            if (HouresWorked > 160)
            {
                TotalPay = BasicPay + Allowance;

            }

        }

        public override string ToString()
        {
            return $"Manager name: {NameOfStaff}, \nhRate: {managerHourlyRate}, \nhWorked: {HouresWorked}, \ntotal pay: {TotalPay} .";
        }


    }

    class Admin : Staff
    {
        private const float overtimeRate = 15.5f;
        private const float adminHourlyRate = 30f;
        public Admin(string name) : base(name, adminHourlyRate)
        {


        }

        public float Overtime { get; private set; }



        public override void CalculatePay()
        {
            base.CalculatePay();

            if (HouresWorked > 160)
            {
                Overtime = overtimeRate * (HouresWorked - 160);
                TotalPay = TotalPay + Overtime;

            }

        }

        public override string ToString()
        {
            return $"Admin name: {NameOfStaff}, \nhRate: {adminHourlyRate}, \nhWorked: {HouresWorked}, \ntotal pay: {TotalPay} .";
        }

    }

    class FileReader
    {


        public List<Staff> ReadFile()
        {
            List<Staff> myStaff = new List<Staff>();
            string[] result = new string[2];
            string path = "staff.txt";
            string[] separator = { ", " };
            string stringLine;



            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.EndOfStream != true)
                    {
                          
                        stringLine = sr.ReadLine();
                        result = stringLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        if (result[1] == "Manager")
                        {
                            Manager newManager = new Manager(result[0]);
                            myStaff.Add(newManager);
                            Console.WriteLine($"New manager added (Name: {result[0]})");
                        }
                        else if (result[1] == "Admin")
                        {
                            Admin newAdmin = new Admin(result[0]);
                            myStaff.Add(newAdmin);
                            Console.WriteLine($"New admin added (Name: {result[0]})");

                        }



                    }

                    sr.Close();

                }

            }
            else
            {
                Console.WriteLine("File does not exist!");

            }


            return myStaff;





        }




    }

    class PaySlip
    {

        private int month;
        private int year;

        enum MonthsOfYear { JAN = 1, FEB = 2, MAR = 3, APR = 4, MAY = 5, JUN = 6, JUL = 7, AUG = 8, SEP = 9, OCT = 10, NOV = 11, DEC = 12 };



        public PaySlip(int payMonth, int payYear)
        {
            month = payMonth;
            year = payYear;


        }



        public void GeneratePaySlip(List<Staff> myStaff)
        {
            string path;



            foreach (Staff f in myStaff)
            {
                path = f.NameOfStaff + ".txt";

                if (f is Manager)
                {

                    using (StreamWriter sw = new StreamWriter(path, false))

                    {

                        sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                        sw.WriteLine("===============================");
                        sw.WriteLine($"Name of Staff: {0}", f.NameOfStaff);
                        sw.WriteLine($"Hours Worked: {0}", f.HouresWorked);
                        sw.WriteLine();
                        sw.WriteLine($"Basic Pay: {0}", f.BasicPay);
                        sw.WriteLine($"Allowance: {0}", ((Manager)f).Allowance);
                        sw.WriteLine();
                        sw.WriteLine("===============================");
                        sw.WriteLine($"Total Pay: {0}", f.TotalPay);
                        sw.WriteLine("===============================");


                        sw.Close();

                    }

                }
                else if (f is Admin)
                {
                    using (StreamWriter sw = new StreamWriter(path, false))

                    {

                        sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                        sw.WriteLine("===============================");
                        sw.WriteLine($"Name of Staff: {0}", f.NameOfStaff);
                        sw.WriteLine($"Hours Worked: {0}", f.HouresWorked);
                        sw.WriteLine();
                        sw.WriteLine($"Basic Pay: {0}", f.BasicPay);
                        sw.WriteLine($"Overtime Pay: {0}", ((Admin)f).Overtime);
                        sw.WriteLine();
                        sw.WriteLine("===============================");
                        sw.WriteLine($"Total Pay: {0}", f.TotalPay);
                        sw.WriteLine("===============================");





                        sw.Close();
                    }




                }






            }


        }

        public void GenerateSummary(List<Staff> myStaff)

        {
            var employee10houres =
                from employee in myStaff
                where employee.HouresWorked <= 10
                orderby employee.NameOfStaff ascending
                select new { employee.NameOfStaff, employee.HouresWorked };


            string path = "summary.txt";

            using (StreamWriter sw = new StreamWriter(path, false))

            {
                sw.WriteLine("Staff with less than 10 working hours");

                foreach (var s in employee10houres)
                {
                    sw.WriteLine("Name of Staff: {0}, Hours Worked: {1}", s.NameOfStaff, s.HouresWorked);
                }



                sw.Close();

            }


        }

        public override string ToString()
        {
         
            return "month = " + month + "year = " + year;
        }


        class Program
        {




            static void Main(string[] args)
            {

                List<Staff> myStaff = new List<Staff>();

                FileReader fr = new FileReader();
                int month = 0;
                int year = 0;


                while (year == 0)
                {
                    Console.Write("\nPlease enter the year:");

                    try
                    {
                        string input = Console.ReadLine();
                        year = Convert.ToInt32(input);




                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "Wrong input, try again!");

                    }

                }

                while (month == 0)
                {
                    Console.Write("\nPlease enter the month number:");

                    try
                    {
                        string input = Console.ReadLine();

                        int inputConverted = Convert.ToInt32(input);

                        if (inputConverted > 0 && inputConverted <= 12)
                        {
                            month = inputConverted;

                        }
                        else
                        {
                            Console.WriteLine("Month number is incorrect");
                        }





                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong month format");

                    }

                }


                myStaff = fr.ReadFile();

                for (int i = 0; i < myStaff.Count; i++)
                {
                    try
                    {
                        Console.WriteLine($"Enter hours worked for: {myStaff[i].NameOfStaff}");
                        string input = Console.ReadLine();
                        int inputConverted = Convert.ToInt32(input);
                        myStaff[i].HouresWorked = inputConverted;
                        myStaff[i].CalculatePay();
                        Console.WriteLine(myStaff[i].ToString());

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        i--;
                    }

                }

                PaySlip ps = new PaySlip(month, year);
                ps.GeneratePaySlip(myStaff);
                ps.GenerateSummary(myStaff);

                Console.Read(); //wait till exit



            }




        }



    }


}



