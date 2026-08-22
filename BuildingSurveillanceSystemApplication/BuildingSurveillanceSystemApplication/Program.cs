using System.Runtime.Serialization;
using static BuildingSurveillanceSystemApplication.OutputFormatter;
using static BuildingSurveillanceSystemApplication.SecurityNotify;

namespace BuildingSurveillanceSystemApplication
{
    class Program //Part 12 - Events - The Observer Design Pattern
    {
        static void Main(string[] args)
        {
            Console.Clear();
            OutputFormatter.ChangeOutputTheme(TextOutputTheme.Normal);

            List<IEmployee> employees = new()
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", JobTitle = "Software Engineer" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", JobTitle = "Project Manager" }
            };

            SecuritySurveillanceHub securitySurveillanceHub = new();
            EmployeeNotify employeeNotify1 = new(employees[0]);
            EmployeeNotify employeeNotify2 = new(employees[1]);

            SecurityNotify securityNotify = new(employees);

            employeeNotify1.Subscribe(securitySurveillanceHub);
            employeeNotify2.Subscribe(securitySurveillanceHub);
            securityNotify.Subscribe(securitySurveillanceHub);
            //ENTRY
            securitySurveillanceHub.ConfirmExternalVisitorEntersBuilding(1, "Alice", "Johnson", "TechCorp", "Sales Manager", DateTime.Parse("2026-08-22 09:00:00"), 1);
            securitySurveillanceHub.ConfirmExternalVisitorEntersBuilding(2, "Bob", "Brown", "DataInc", "Data Analyst", DateTime.Parse("2026-08-22 10:00:00"), 2);

            //employeeNotify1.Unsubscribe(); //John Doe isn't notified when Alice Johnson exits the building
            //EXIT
            securitySurveillanceHub.ConfirmExternalVisitorExitsBuilding(1, DateTime.Parse("2026-08-22 11:11:00"));
            securitySurveillanceHub.ConfirmExternalVisitorExitsBuilding(2, DateTime.Parse("2026-08-22 13:13:00"));

            securitySurveillanceHub.BuildingEntryCutoffTimeReached();

            Console.ReadKey();
        }
    }

    public static class Misc
    {
        public static string DateTimeFormat = "dd MMM yyyy HH:mm:ss";
    }

    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
    }

    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string JobTitle { get; set; }
    }


    public abstract class Observer : IObserver<ExternalVisitor>
    {
        private IDisposable _unsubscribe;
        protected List<ExternalVisitor> _externalVisitors = new();

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(ExternalVisitor value);
    
        public void Subscribe(IObservable<ExternalVisitor> provider)
        {
            if (provider != null)
                _unsubscribe = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscribe.Dispose();
            _externalVisitors.Clear();
        }
    }



    public class EmployeeNotify : Observer
    {
        IEmployee _employee;

        public EmployeeNotify(IEmployee employee)
        {
            //https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/may/csharp-best-practices-dangers-of-violating-solid-principles-in-csharp
            _employee = employee;
        }

        public override void OnCompleted()
        {
            string heading = $"{_employee.FirstName} {_employee.LastName} Daily Visitors Report";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            Console.WriteLine();

            foreach (var externalVisitor in _externalVisitors)
            {
                externalVisitor.IsCurrentlyInBuilding = false;
                //columned output
                //Console.WriteLine($"{externalVisitor.Id.ToString().PadRight(6)} ...");
                //or
                Console.WriteLine($"{externalVisitor.Id,-6} {externalVisitor.FirstName,-15} {externalVisitor.LastName,-15} {externalVisitor.EntryDateTime.ToString(Misc.DateTimeFormat),-25} {externalVisitor.ExitDateTime.ToString("dd MMM yyyy HH:mm:ss"),-25}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public override void OnError(Exception error)
        {
            Console.WriteLine($"An error occurred: {error.Message}");
            throw new NotImplementedException();
        }

        public override void OnNext(ExternalVisitor value)
        {
            //if (value.EmployeeContactId == _employee.Id && value.IsCurrentlyInBuilding)
            if (value.EmployeeContactId == _employee.Id)
            {
                var externalVisitorListItem = _externalVisitors.FirstOrDefault(visitor => visitor.Id == value.Id);
                if (externalVisitorListItem == null)
                {
                    _externalVisitors.Add(value);
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Employee);
                    Console.WriteLine($"(To {_employee.FirstName} {_employee.LastName}) Your visitor {value.FirstName} {value.LastName} has entered the building at {value.EntryDateTime.ToString(Misc.DateTimeFormat)}.");
                    Console.WriteLine();
                }
                else
                {
                    if (!value.IsCurrentlyInBuilding)
                    {
                        externalVisitorListItem.IsCurrentlyInBuilding = false;
                        externalVisitorListItem.ExitDateTime = value.ExitDateTime;

                        OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Employee);
                        Console.WriteLine($"(To {_employee.FirstName} {_employee.LastName}) Your visitor {value.FirstName} {value.LastName} has exited the building at {value.ExitDateTime.ToString(Misc.DateTimeFormat)}.");
                        Console.WriteLine();
                    }
                }
                OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Normal);
            }
        }
    }



    public class SecurityNotify : Observer
    {
        private readonly List<IEmployee> _employees;

        public SecurityNotify(List<IEmployee> employees)
        {
            this._employees = employees;
        }

        public override void OnCompleted()
        {
            string heading = $"Security Daily Visitors Report";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            Console.WriteLine();

            foreach (var externalVisitor in _externalVisitors)
            {
                externalVisitor.IsCurrentlyInBuilding = false;
                //columned output
                //Console.WriteLine($"{externalVisitor.Id.ToString().PadRight(6)} ...");
                //or
                Console.WriteLine($"{externalVisitor.Id,-6} {externalVisitor.FirstName,-15} {externalVisitor.LastName,-15} {externalVisitor.EntryDateTime.ToString(Misc.DateTimeFormat),-25} {externalVisitor.ExitDateTime.ToString(Misc.DateTimeFormat),-25}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public override void OnError(Exception error)
        {
            string heading = $"Security Daily Visitors Report";
            //Console.WriteLine(heading);
            Console.WriteLine(error);
            Console.WriteLine(new string('=', heading.Length));
            Console.WriteLine();

            OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.SecurityAlert);
            foreach (var externalVisitor in _externalVisitors)
                if (externalVisitor.IsCurrentlyInBuilding)
                {
                    var visiting = _employees.First(emp => emp.Id == externalVisitor.EmployeeContactId);
                    //var externalVisitor = _externalVisitors.FirstOrDefault(visitor => visitor.Id == externalVisitorId);
                    Console.WriteLine($"{externalVisitor.Id,-6} {externalVisitor.FirstName,-15} {externalVisitor.LastName,-15} {externalVisitor.EntryDateTime.ToString(Misc.DateTimeFormat),-25} visiting {visiting.FirstName} {visiting.LastName}");
                }
            OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Normal);
        }

        public override void OnNext(ExternalVisitor value)
        {
            var externalVisitorListItem = _externalVisitors.FirstOrDefault(visitor => visitor.Id == value.Id);
            if (externalVisitorListItem == null)
            {
                _externalVisitors.Add(value);
                OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Security);
                Console.WriteLine($"Security alert: Visitor {value.FirstName} {value.LastName} has entered the building at {value.EntryDateTime.ToString(Misc.DateTimeFormat)}.");
                Console.WriteLine();
            }
            else
            {
                if (!value.IsCurrentlyInBuilding)
                {
                    externalVisitorListItem.IsCurrentlyInBuilding = false;
                    externalVisitorListItem.ExitDateTime = value.ExitDateTime;
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Security);
                    Console.WriteLine($"Security alert: Visitor {value.FirstName} {value.LastName} has exited the building at {value.ExitDateTime.ToString(Misc.DateTimeFormat)}.");
                    Console.WriteLine();
                }
            }
            OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Normal);
        }



        public class SecuritySurveillanceHub : IObservable<ExternalVisitor>
        {
            private List<ExternalVisitor> _externalVisitors = new();
            private List<IObserver<ExternalVisitor>> _observers = new();


            public class Unsubscriber<ExternalVisitor> : IDisposable
            {
                private List<IObserver<ExternalVisitor>> _observers;
                private IObserver<ExternalVisitor> _observer;
                public Unsubscriber(List<IObserver<ExternalVisitor>> observers, IObserver<ExternalVisitor> observer)
                {
                    _observers = observers;
                    _observer = observer;
                }
                public void Dispose()
                {
                    if (_observer != null && _observers.Contains(_observer))
                        _observers.Remove(_observer);
                }
            }

            public IDisposable Subscribe(IObserver<ExternalVisitor> observer)
            {
                if (!_observers.Contains(observer))
                    _observers.Add(observer);

                foreach (var externalVisitor in _externalVisitors)
                    observer.OnNext(externalVisitor);

                return new Unsubscriber<ExternalVisitor>(_observers, observer);
            }

            public void ConfirmExternalVisitorEntersBuilding(int id, string firstName, string lastName, string companyName, string jobTitle, DateTime entryDateTime, int employeeContactId)
            {
                ExternalVisitor externalVisitor = new()
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    CompanyName = companyName,
                    JobTitle = jobTitle,
                    EntryDateTime = entryDateTime,
                    //IsCurrentlyInBuilding = true,
                    EmployeeContactId = employeeContactId
                };

                _externalVisitors.Add(externalVisitor);

                foreach (var observer in _observers)
                    observer.OnNext(externalVisitor);
            }

            public void ConfirmExternalVisitorExitsBuilding(int externalVisitorId, DateTime exitDateTime)
            {
                var externalVisitor = _externalVisitors.FirstOrDefault(visitor => visitor.Id == externalVisitorId);

                if (externalVisitor != null)
                {
                    externalVisitor.ExitDateTime = exitDateTime;
                    externalVisitor.IsCurrentlyInBuilding = false;

                    foreach (var observer in _observers)
                        observer.OnNext(externalVisitor);
                }
            }


            public void BuildingEntryCutoffTimeReached()
            {
                if (_externalVisitors.Where(visitor => visitor.IsCurrentlyInBuilding).Any())
                {
                    foreach (var observer in _observers)
                    {
                        if (observer.GetType() == typeof(SecurityNotify))
                            //Console.WriteLine($"observer.GetType(): {observer.GetType()}")
                            observer.OnError(new Exception("Building entry cutoff time has been reached. Some visitors are still in the building."));
                    }
                }
                else
                    foreach (var observer in _observers)
                        observer.OnCompleted();
            }
        }
    }



    public static class OutputFormatter
    {
        public enum TextOutputTheme
        {
            Employee,
            Security,
            SecurityAlert,
            Normal

        }

        public static void ChangeOutputTheme(TextOutputTheme textOutputTheme)
        {
            switch (textOutputTheme)
            {
                case TextOutputTheme.Employee:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case TextOutputTheme.Security:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case TextOutputTheme.SecurityAlert:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                default:
                    Console.ResetColor();
                    break;
            }
        }

    }

    public class ExternalVisitor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateTime EntryDateTime { get; set; }
        public DateTime ExitDateTime { get; set; }
        public bool IsCurrentlyInBuilding { get; set; } = true;
        public int EmployeeContactId { get; set; }
    }
}