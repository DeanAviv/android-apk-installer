using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ApkInstaller.GUI
{
    public class AdbDeviceManager
    {
        private string? ResolveAdbPath()
        {
            // 1. Try using "adb" from PATH
            if (IsAdbAvailable("adb"))
                return "adb";

            // 2. Check known fallback locations
            string[] fallbackPaths = new[]
            {
                @"C:\Android\platform-tools\adb.exe",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Android\Sdk\platform-tools\adb.exe")
            };

            foreach (var path in fallbackPaths)
            {
                if (IsAdbAvailable(path))
                    return path;
            }

            // 3. Not found
            return null;
        }

        private bool IsAdbAvailable(string adbPath)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = adbPath,
                    Arguments = "version",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                if (process == null)
                    return false;

                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        public List<AdbDeviceInfo> GetConnectedDevices()
        {
            var devices = new List<AdbDeviceInfo>();
            var adbPath = ResolveAdbPath();

            if (adbPath == null)
            {
                Console.WriteLine("ADB not found!");
                return devices;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = adbPath,
                    Arguments = "devices",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                if (process == null) return devices;

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines.Skip(1))
                {
                    if (line.Contains("\tdevice"))
                    {
                        var deviceId = line.Split('\t')[0];
                        var model = GetDeviceModel(adbPath, deviceId);
                        devices.Add(new AdbDeviceInfo { DeviceId = deviceId, Model = model });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ADB error: {ex.Message}");
            }

            return devices;
        }

        private string GetDeviceModel(string adbPath, string deviceId)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = adbPath,
                    Arguments = $"-s {deviceId} shell getprop ro.product.model",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                if (process == null) return "(Unknown Model)";

                string output = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                return string.IsNullOrWhiteSpace(output) ? "(Unknown Model)" : output;
            }
            catch
            {
                return "(Error fetching model)";
            }
        }


    }
}
