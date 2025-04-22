namespace ApkInstaller.GUI
{
    public class AdbDeviceInfo
    {
        public string DeviceId { get; set; } = "";
        public string Model { get; set; } = "";

        public override string ToString() => $"{Model} ({DeviceId})";
    }
}
