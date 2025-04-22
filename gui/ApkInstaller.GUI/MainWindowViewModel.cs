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

        public MainWindowViewModel()
        {
            RefreshDevicesCommand = new RelayCommand(RefreshDevices);
        }

        private void RefreshDevices()
        {
            Console.WriteLine("RefreshDevices() called"); 

            Devices.Clear();
            var found = _adbManager.GetConnectedDevices();

            foreach (var device in found)
            {
                Console.WriteLine($"Found device: {device}");
                Devices.Add(device);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
