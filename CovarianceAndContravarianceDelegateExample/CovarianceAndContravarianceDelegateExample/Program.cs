using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace CovarianceAndContravarianceDelegateExample
{
    class Program //20260710 Part 6 - Delegates - Understanding Covariance and Contravariance
    {
        delegate Car CarFactoryDel(int id, string name);
        delegate void LogICECarDetailsDel(ICECar car);
        delegate void LogEVCarDetailsDel(EVCar car);
        private static void Main(string[] args)
        {
            CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;
            Car iceCar = carFactoryDel(1, "Audi R8");

            //Console.WriteLine($"Object type: {iceCar.GetType()}");
            //Console.WriteLine($"Car details: {iceCar.GetCarDetails()}");

            carFactoryDel = CarFactory.ReturnEVCar;
            Car evCar = carFactoryDel(2, "Tesla Model S");
            //Console.WriteLine();
            //Console.WriteLine($"Object type: {evCar.GetType()}");
            //Console.WriteLine($"Car details: {evCar.GetCarDetails()}");

            LogICECarDetailsDel logICECarDetailsDel = LogCarDetails;
            logICECarDetailsDel(iceCar as ICECar);

            LogEVCarDetailsDel logEVCarDetailsDel = LogCarDetails;
            logEVCarDetailsDel(evCar as EVCar);

            Console.ReadKey();
        }

        static void LogCarDetails(Car car)
     {
            switch (car)
            {
                //case ICECar iceCar:
                case ICECar:
                    using (StreamWriter sw = new StreamWriter("ICECarDetails.txt", true))
                    {
                        sw.WriteLine($"Object type: {car.GetType()}");
                        sw.WriteLine($"Car details: {car.GetCarDetails()}");
                    }
                    //Console.WriteLine($"Car details: {iceCar.GetCarDetails()}");
                    break;
                //case EVCar evCar:
                case EVCar:
                    Console.WriteLine($"Object type: {car.GetType()}");
                    Console.WriteLine($"Car details: {car.GetCarDetails()}");
                    //Console.WriteLine($"Car details: {evCar.GetCarDetails()}");
                    break;
                default:
                    Console.WriteLine("Unknown car type.");
                    break;
            }
        }
    }

    public static class CarFactory
    {
        public static ICECar ReturnICECar(int id, string name)
        {
            return new ICECar { Id = id, Name = name };
        }
        public static EVCar ReturnEVCar(int id, string name)
        {
            return new EVCar { Id = id, Name = name };
        }
    }

    public abstract class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string GetCarDetails()
        {
            return $"{Id} - {Name}";
        }
    }

    public class ICECar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Internal Combustion Engine";
        }
    }
    public class EVCar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Electric Vehicle";
        }
    }
}