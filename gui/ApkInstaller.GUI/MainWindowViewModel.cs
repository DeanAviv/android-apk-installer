using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ApkInstaller.GUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AdbDeviceManager _adbManager = new();

        public ObservableCollection<AdbDeviceInfo> Devices { get; } = new();

        public ICommand RefreshDevicesCommand { get; }

        private bool _isAdbAvailable = true;
        public bool IsAdbAvailable
        {
            get => _isAdbAvailable;
            set
            {
                _isAdbAvailable = value;
                OnPropertyChanged();
            }
        }

        private string _statusMessage = "Welcome to the Android APK Installer!";
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }


        public MainWindowViewModel()
        {
            RefreshDevicesCommand = new RelayCommand(RefreshDevices);
            RefreshDevices(); // Automatically scan devices on startup
        }

        private void RefreshDevices()
        {
            Console.WriteLine("RefreshDevices() called"); 

            Devices.Clear();
            var found = _adbManager.GetConnectedDevices();
            if (found.Count == 0)
            {
                IsAdbAvailable = false;
                StatusMessage = "⚠️ ADB not found. Please install ADB and ensure it's added to PATH.";
            }
            else
            {
                IsAdbAvailable = true;
                StatusMessage = "Welcome to the Android APK Installer!";
                foreach (var device in found)
                {
                    Console.WriteLine($"Found device: {device}");
                    Devices.Add(device);
                }
            }      
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
