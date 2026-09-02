using WarehouseManagementSystemAPI;

namespace HardwareWarehouseManagementSystem
{
    public class Program //Part 15 - Generics - Generic Delegates and Events
    {
        const int Batch_Size = 5; // Number of items to process in each batch

        static void Main(string[] args)
        {
            CustomQueue<HardwareItem> hardwareItemQueue = new();
            hardwareItemQueue.CustomQueueEvent += CustomQueue_CustomQueueEvent;
            //----------------------------------------------------
            //https://github.com/GavinLonDigital/HardwareWarehouseManagementSystem/blob/master/HardwareWarehouseManagementSystem/Program.cs
            System.Threading.Thread.Sleep(2000);

            //comes into stock - device scans a bar code or QR code
            hardwareItemQueue.AddItem(new Drill { Id = 1, Name = "Drill 1", Type = "Drill", UnitValue = 20.00m, Quantity = 10 });

            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new Drill { Id = 2, Name = "Drill 2", Type = "Drill", UnitValue = 30.00m, Quantity = 20 });

            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new Ladder { Id = 3, Name = "Ladder 1", Type = "Ladder", UnitValue = 100.00m, Quantity = 5 });

            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new Hammer { Id = 4, Name = "Hammer 1", Type = "Hammer", UnitValue = 10.00m, Quantity = 80 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new PaintBrush { Id = 5, Name = "Paint Brush 1", Type = "PaintBrush", UnitValue = 5.00m, Quantity = 100 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new PaintBrush { Id = 6, Name = "Paint Brush 2", Type = "PaintBrush", UnitValue = 5.00m, Quantity = 100 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new PaintBrush { Id = 7, Name = "Paint Brush 3", Type = "PaintBrush", UnitValue = 5.00m, Quantity = 100 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new Hammer { Id = 8, Name = "Hammer 2", Type = "Hammer", UnitValue = 11.00m, Quantity = 80 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new Hammer { Id = 9, Name = "Hammer 3", Type = "Hammer", UnitValue = 13.00m, Quantity = 80 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());

            hardwareItemQueue.AddItem(new Hammer { Id = 10, Name = "Hammer 4", Type = "Hammer", UnitValue = 14.00m, Quantity = 80 });
            System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());
            //----------------------------------------------------
            //Console.WriteLine("Finished.");
            //Console.ReadKey();
        }

        private static int GetRandomTimeDelayInMilliseconds()
        {
            Random random = new Random();
            return random.Next(1000, 5000); // Random delay between 1 and 5 seconds
        }

        private static void ProcessItems(CustomQueue<HardwareItem> customQueue)
        {
            //int itemsToProcess = Math.Min(Batch_Size, customQueue.QueueLength);
            //for (int i = 0; i < itemsToProcess; i++)
            //{
            //    HardwareItem item = customQueue.GetItem();
            //    Console.WriteLine($"Processing item: Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Quantity: {item.Quantity}");
            //    System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());
            //}

            while(customQueue.QueueLength > 0)
            {
                System.Threading.Thread.Sleep(GetRandomTimeDelayInMilliseconds());
                HardwareItem hardwareItem = customQueue.GetItem();
            }
        }

        private static void CustomQueue_CustomQueueEvent(CustomQueue<HardwareItem> sender, QueueEventArgs eventArgs)
        {
            Console.Clear();

            Console.WriteLine(MainHeading());
            Console.WriteLine();
            Console.WriteLine(RealTimeUpdateHeading());

            if (sender.QueueLength > 0)
            {
                Console.WriteLine(eventArgs.Message);
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine(ItemsInQueueHeading());
                Console.WriteLine(FieldHeadings());

                WriteValuesInQueueToScreen(sender);
                if (sender.QueueLength == Batch_Size)
                {
                    ProcessItems(sender);
                }
            }
            else
            {
                Console.WriteLine("The queue is empty.");
            }
        }

        private static void WriteValuesInQueueToScreen(CustomQueue<HardwareItem> hardwareItems)
        {
            foreach (var hardwareItem in hardwareItems)
            {
                Console.WriteLine($"{hardwareItem.Id,-6}{hardwareItem.Name,-15}{hardwareItem.Type,-20}{hardwareItem.Quantity,10}{hardwareItem.UnitValue,10}");
            }
        }

        //Headings
        private static string FieldHeadings()
        {
            return UnderLine($"{"Id",-6}{"Name",-15}{"Type",-20}{"Quantity",10}{"Value",10}");
        }

        private static string RealTimeUpdateHeading()
        {
            return UnderLine("Real-time Update");
        }

        private static string ItemsInQueueHeading()
        {
            return UnderLine("Items Queued for Processing");
        }

        private static string MainHeading()
        {
            return UnderLine("Warehouse Management System");
        }

        private static string UnderLine(string heading)
        {
            return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
        }
        //Headings
    }

    public abstract class HardwareItem : IEntityPrimaryProperties, IEntityAdditionalProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        //public HardwareItem(int id, string name, string type, int quantity, decimal unitValue)
        //{
        //    Id = id;
        //    Name = name;
        //    Type = type;
        //    Quantity = quantity;
        //    UnitValue = unitValue;
        //}
        //public abstract void DisplayInfo();
    }

    public interface IDrill
    {
        string DrillBrandName { get; set; }
    }

    public class Drill : HardwareItem, IDrill
    {
        public string DrillBrandName { get; set; }

        //public Drill(int id, string name, string type, int quantity, decimal unitValue)
        //    : base(id, name, type, quantity, unitValue)
        //{
        //}
        //public override void DisplayInfo()
        //{
        //    Console.WriteLine($"Drill - Id: {Id}, Name: {Name}, Type: {Type}, Quantity: {Quantity}, Unit Value: {UnitValue}, Brand: {DrillBrandName}");
        //}
    }

    public interface ILadder
    {
        string LadderBrandName { get; set; }
    }

    public class Ladder : HardwareItem, ILadder
    {
        public string LadderBrandName { get; set; }

        //public Ladder(int id, string name, string type, int quantity, decimal unitValue)
        //    : base(id, name, type, quantity, unitValue)
        //{
        //}
        //public override void DisplayInfo()
        //{
        //    Console.WriteLine($"Ladder - Id: {Id}, Name: {Name}, Type: {Type}, Quantity: {Quantity}, Unit Value: {UnitValue}, Brand: {LadderBrandName}");
        //}
    }

    public interface IPaintBrush
    {
        string PaintBrushBrandName { get; set; }
    }

    public class PaintBrush : HardwareItem, IPaintBrush
    {
        public string PaintBrushBrandName { get; set; }

        //public PaintBrush(int id, string name, string type, int quantity, decimal unitValue)
        //    : base(id, name, type, quantity, unitValue)
        //{
        //}
        //public override void DisplayInfo()
        //{
        //    Console.WriteLine($"PaintBrush - Id: {Id}, Name: {Name}, Type: {Type}, Quantity: {Quantity}, Unit Value: {UnitValue}, Brand: {PaintBrushBrandName}");
        //}
    }

    public interface IHammer
    {
        string HammerBrandName { get; set; }
    }

    public class Hammer : HardwareItem, IHammer
    {
        public string HammerBrandName { get; set; }

        //public Hammer(int id, string name, string type, int quantity, decimal unitValue)
        //    : base(id, name, type, quantity, unitValue)
        //{
        //}
        //public override void DisplayInfo()
        //{
        //    Console.WriteLine($"Hammer - Id: {Id}, Name: {Name}, Type: {Type}, Quantity: {Quantity}, Unit Value: {UnitValue}, Brand: {HammerBrandName}");
        //}
    }
}
