using System.IO;
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
        public string NameOfStaff { get; set; }
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
            return $"Staff name: {NameOfStaff}, hRate: {_hourlyRate}, hWorked: {_hWorked}, total pay: {TotalPay} .";


        }

    }

    class Manager : Staff
    {
        private const float managerHourlyRate = 50;


        public int Allowance
        {
            get;
            private set;
        }


        public float TotalPay
        {
            get { return base.TotalPay; }
            set
            {
                if (HouresWorked > 160)
                {
                    TotalPay = BasicPay + Allowance;

                }
                else
                {
                    TotalPay = BasicPay;

                }
            }
        }


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


        }

        public override string ToString()
        {
            return $"Manager name: {NameOfStaff}, hRate: {managerHourlyRate}, hWorked: {HouresWorked}, total pay: {TotalPay} .";
        }


    }

    class Admin : Staff
    {
        private const float overtimeRate = 15.5F;
        private const float adminHourlyRate = 30;
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
            return $"Admin name: {NameOfStaff}, hRate: {adminHourlyRate}, hWorked: {HouresWorked}, total pay: {TotalPay} .";
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
                        Console.WriteLine(sr.ReadLine()); //display txt file in console    
                        stringLine = sr.ReadLine();
                        result = stringLine.Split(separator, StringSplitOptions.None);

                        if (result[1] == "Manager")
                        {
                            Manager newManager = new Manager(result[0]);
                            myStaff.Add(newManager);
                            Console.WriteLine($"New manager added (Name: {result[0]}");
                        }
                        else if (result[1] == "Admin")
                        {
                            Admin newAdmin = new Admin(result[0]);
                            myStaff.Add(newAdmin);
                            Console.WriteLine($"New admin added (Name: {result[0]}");

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



        public PaySlip( int payMonth, int payYear )
        {
        month = payMonth;   
        year = payYear; 
        
        
        }



        public void GeneratePaySlip(List<Staff> myStuff)
        {
            string path;

            foreach(Staff f in myStuff)
            {
                
                path = Path.Join(f.NameOfStaff ,".txt");

                using (StreamWriter sw = new StreamWriter(path,false))





                {
             

                    sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                    sw.WriteLine("===============================");
                    sw.WriteLine($"Name of Staff: {0}", f.NameOfStaff);
                    sw.WriteLine($"Hours Worked: {0}", f.HouresWorked);
                    sw.WriteLine();
                    sw.WriteLine($"Basic Pay: {0}", f.BasicPay);
                    sw.WriteLine($"Allowance: $1000");
                    sw.WriteLine();
                    sw.WriteLine("===============================");
                    sw.WriteLine($"Total Pay: {0}", f.TotalPay);
                    sw.WriteLine("===============================");


                    Console.WriteLine("----");

                }


            }


        }



    }




    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Start..!");

            Console.WriteLine("----");
        }


    }



}