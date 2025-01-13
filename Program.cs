// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

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
