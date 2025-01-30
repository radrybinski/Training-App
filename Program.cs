// See https://aka.ms/new-console-template for more information

using System;
using System.Drawing;

namespace Training
{

    class Program
    {

        enum WeekDays
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday

        }


        //test Struct
        struct myPoint
        {
            private double x;
            private double y;
            private double z;

            private string structName;


            public myPoint(double xDim, double yDim, double zDim, string name)
            {

                x = xDim;
                y = yDim;
                z = zDim;
                structName = name;



            }


            public void Print()
            {

                Console.WriteLine("Point details: X = {0}, Y = {1}, Z = {2}, name = {3}", x, y, z, structName);

            }


        }


        class Worker
        {
            private int hourlyRate = 30;
            private string name;
            private int hoursWorked;

            public int HoursWorked
            {
                get
                {

                    return hoursWorked;
                }

                set
                {

                    if (value >= 160)
                    {

                        hoursWorked = 160;

                    }
                    else
                    {
                        hoursWorked = value;


                    }



                }
            }

            public void Payroll()
            {

                int hRate = hourlyRate;
                int hWorked = hoursWorked;
                int payroll = hWorked * hRate;

                Console.WriteLine($"Payroll = {payroll} ");


            }

        }

        //Delegate
        public delegate int Operacja(int x, int y);




        static void Main(string[] args)
        {

            //Delagate

            //static int Dodaj(int a, int b) => a + b;
            //static int Odejmij(int a, int b) => a - b;

            Operacja op1 = (x, y) => x + y;
            Operacja op2 = (x, y) => x - y;
            Operacja op3 = (x, y) => x * y;
            Operacja op4 = (x, y) => x / y;

            //Operacja op5 = (x, y, z) => x+y+z;




            Console.WriteLine(op1(5,5));
            Console.WriteLine(op2(5, 5));
            Console.WriteLine(op3(5, 5));
            Console.WriteLine(op4(5, 5));

            /*
            //File read/write

            string path = "d:\\test.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {

                    Console.WriteLine(sr.ReadLine());


                }


                sr.Close();


            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Add text1");
                sw.WriteLine("Add text2");
                sw.WriteLine("Add text3");
                sw.WriteLine("The end");

            }




            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {

                    Console.WriteLine(sr.ReadLine());

                }

                sr.Close();

            }


            */



            /*
            //LINQ test
            int[] myArray = { 0, 1, 2, 3, 4, 5, 6, 7 };

            var testQuery =
                from num in myArray
                where (num % 2) == 0
                select num;


            foreach (int i in testQuery)
            {
                Console.WriteLine("{0} is even number ", i);


            }


            */





            /*

            // worker class
            Worker worker1 = new Worker();
            Console.WriteLine("Input number of hours:");
            int numberOfHoures = Convert.ToInt32(Console.ReadLine());
            worker1.HoursWorked = numberOfHoures;
            worker1.Payroll();

            */


            /* struct
            WeekDays today = WeekDays.Friday;

            Console.WriteLine((WeekDays)4);

            WeekDays startDay = WeekDays.Monday;

            myPoint point1 = new myPoint (10,20, 30,"Point-1");

           point1.Print();

            */


            /* class
            Console.WriteLine("Hello, World!");


            Console.WriteLine("My C# Training App");

            Car car1 = new Car();
            Car car2 = new Car();

            Console.WriteLine(car1.color);
            car1.color = "blue";
            Console.WriteLine(car1.color);
            car1.maxSpeed = 123;

            car1.DriveFullSpeed();

            Console.WriteLine(car1.model);
            Console.WriteLine(car2.model);
            */

        }




    }


    class Car
    {
        //default it is set to  private
        public string color = "red";
        public string model;
        public int maxSpeed;
        public int year;

        public Car()
        {
            model = "Mustang";


        }



        public void DriveFullSpeed()
        {
            Console.WriteLine($"Car speed is {maxSpeed}");


        }




    }

}




















