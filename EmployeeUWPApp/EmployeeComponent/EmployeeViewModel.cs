using System.ComponentModel;

/*
Missing Package References for EmployeeUWPApp:
Microsoft.WindowsAppSDK - NuGet.PackageManagement.VisualStudio.Exceptions.ProjectNotNominatedException: The operation failed as details for project EmployeeComponent could not be loaded.
Microsoft.Windows.SDK.BuildTools - NuGet.PackageManagement.VisualStudio.Exceptions.ProjectNotNominatedException: The operation failed as details for project EmployeeComponent could not be loaded.

Please manually add package references before building.

i changed the project file to match EmployeeUWPApp: <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
*/
namespace EmployeeComponent
{
    public class EmployeeViewModel : INotifyPropertyChanged 
    {
        private string _firstName = string.Empty;

        public int Id { get; set; }
        public string FirstName {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        public string LastName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public char Gender { get; set; }
        public bool IsManager { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //// Example helper for property setters; use in your properties:
        //protected bool SetProperty<T>(ref T backingField, T value, string propertyName)
        //{
        //    if (EqualityComparer<T>.Default.Equals(backingField, value))
        //        return false;

        //    backingField = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}
    }
}
