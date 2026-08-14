using System.ComponentModel;

namespace ThermostatEventsApp
{
    class Program //20260811 Part 10 - Events - Add/Remove Accessors
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the device...");
            Console.ReadKey();

            IDevice device = new Device();
            device.RunDevice();

            Console.ReadKey();
        }
    }

    public class Device : IDevice
    {
        const double WarningLevel = 27.0;
        const double EmergencyLevel = 75.0;

        public double WarningLevel_Temperature => WarningLevel;

        public double EmergencyLevel_Temperature => EmergencyLevel;

        public void HandleEmergency()
        {
            Console.WriteLine();
            Console.WriteLine("Sending out notification to emergency services personnel...");
            ShutdownDevice();
            Console.WriteLine();
        }
        public void RunDevice()
        {
            Console.WriteLine("Device is running...");
            Console.WriteLine();

            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeatSensor heatSensor = new HeatSensor(WarningLevel, EmergencyLevel);
            IThermostat thermostat = new Thermostat(this, heatSensor, coolingMechanism);
            thermostat.RunThermostat();
        }

        private void ShutdownDevice()
        {
            Console.WriteLine("Shutting down device...");
        }
    }

    public class Thermostat : IThermostat
    {
        private ICoolingMechanism _coolingMechanism = null;
        private IHeatSensor _heatSensor = null;
        private IDevice _device = null;

        public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
        {
            _device = device;
            _coolingMechanism = coolingMechanism;
            _heatSensor = heatSensor;
        }

        private void WireUpEventsToEventHandlers()
        {
            _heatSensor.TemperatureReachesWarningLevelEventHandler += _heatSensor_TemperatureReachesWarningLevelEventHandler;
            //
            // note: pressing tab twice after typing the += will generate (a name and) the event handler method stub
            //
            _heatSensor.TemperatureFallsBelowWarningLevelEventHandler += _heatSensor_TemperatureFallsBelowWarningLevelEventHandler;
            _heatSensor.TemperatureReachesEmergencyLevelEventHandler += _heatSensor_TemperatureReachesEmergencyLevelEventHandler;
        }

        private void _heatSensor_TemperatureReachesEmergencyLevelEventHandler(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Emergency alert!! (Emergency level is between {_device.EmergencyLevel_Temperature}°C and above.)");
            _device.HandleEmergency();
            Console.ResetColor();
        }

        private void _heatSensor_TemperatureFallsBelowWarningLevelEventHandler(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine($"Information alert!! (Temperature falls below warning level ({_device.WarningLevel_Temperature}°C)");
            _coolingMechanism.DeactivateCoolingMechanism();
            Console.ResetColor();
        }

        private void _heatSensor_TemperatureReachesWarningLevelEventHandler(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor=ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($"Warning alert!! (Warning level is between {_device.WarningLevel_Temperature}°C and {_device.EmergencyLevel_Temperature}°C)"); // - Level reached: {e.Temperature}°C
            _coolingMechanism.ActivateCoolingMechanism();
            Console.ResetColor();
        }

        void IThermostat.RunThermostat()
        {
            Console.WriteLine("Thermostat is running...");
            WireUpEventsToEventHandlers();
            _heatSensor.RunHeatSensor();
        }
    }


    public interface IThermostat
    {
        void RunThermostat();
    }

    public interface IDevice
    {
        double WarningLevel_Temperature { get; }
        double EmergencyLevel_Temperature { get; }
        void RunDevice();
        void HandleEmergency();
    }

    public class CoolingMechanism : ICoolingMechanism
    {
        public void ActivateCoolingMechanism()
        {
            Console.WriteLine();
            Console.WriteLine("Cooling mechanism activated.");
            Console.WriteLine();
        }

        public void DeactivateCoolingMechanism()
        {
            Console.WriteLine();
            Console.WriteLine("Cooling mechanism deactivated.");
            Console.WriteLine();
        }
    }

    public interface ICoolingMechanism
    {
        void ActivateCoolingMechanism();
        void DeactivateCoolingMechanism();
    }

    public class HeatSensor : IHeatSensor //note: at inception, hover over IHeatSensor then "Other Fixes" then "Implement all members implicitly"
                                          //the add and remove accessors operate similarly to the get and set accessors of a property.
    {
        double _warningLevel = 0;
        double _emergencyLevel = 0;

        bool _hasReachedWarningTemperature = false;

        protected EventHandlerList _listEventDelegates = new();

        static readonly object _temperatureReachesWarningLevelKey = new();
        static readonly object _temperatureFallsBelowWarningLevelKey = new();
        static readonly object _temperatureReachesEmergencyLevelKey = new();

        private double[] _temperatureData = null;
        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _warningLevel = warningLevel;
            _emergencyLevel = emergencyLevel;

            SeedData();
        }

        public void MonitorTemperature()
        {
            foreach (double temperature in _temperatureData)
            {
                Console.ResetColor();
                Console.WriteLine($"DateTime: {DateTime.Now:yyyy-MM-dd HH:mm:ss}, Temperature: {temperature}°C");

                TemperatureEventArgs e = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now
                };

                if (temperature >= _emergencyLevel)
                {
                    OnTemperatureReachesEmergencyLevel(e);
                }
                else if (temperature >= _warningLevel && !_hasReachedWarningTemperature)
                {
                    _hasReachedWarningTemperature = true;
                    OnTemperatureReachesWarningLevel(e);
                }
                else if (temperature < _warningLevel && _hasReachedWarningTemperature)
                {
                    _hasReachedWarningTemperature = false;
                    OnTemperatureFallsBelowWarningLevel(e);
                }

                System.Threading.Thread.Sleep(5000); // Simulate a delay between temperature readings)
            }
        }

        private void SeedData()
        {
            _temperatureData = new double[50];
            Random random = new Random();
            for (int i = 0; i < _temperatureData.Length; i++)
            {
                _temperatureData[i] = random.Next(0, 100);
            }
        }

        protected void OnTemperatureReachesWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesWarningLevelKey];
            // Handle the event
            handler?.Invoke(this, e);
        }

        protected void OnTemperatureFallsBelowWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureFallsBelowWarningLevelKey];
            // Handle the event
            handler?.Invoke(this, e);
        }

        protected void OnTemperatureReachesEmergencyLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesEmergencyLevelKey];
            // Handle the event
            handler?.Invoke(this, e);
        }

        //The add accessor is called when a subscriber subscribes to the event, and the remove accessor is called when a subscriber unsubscribes from the event.
        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesEmergencyLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesEmergencyLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesEmergencyLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesWarningLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureFallsBelowWarningLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureFallsBelowWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureFallsBelowWarningLevelKey, value  );
            }
        }

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat Sensor is running...");
            MonitorTemperature();
        }
    }

    public interface IHeatSensor
    {
        event EventHandler<TemperatureEventArgs> TemperatureReachesEmergencyLevelEventHandler;
        event EventHandler<TemperatureEventArgs> TemperatureReachesWarningLevelEventHandler;
        event EventHandler<TemperatureEventArgs> TemperatureFallsBelowWarningLevelEventHandler;

        void RunHeatSensor();
    }

    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}
