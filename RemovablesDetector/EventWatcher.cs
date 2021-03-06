﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemovablesDetector
{
    public class EventWatcher
    {
        private Thread watcherThread;
        private bool shouldWatch = true;

        public delegate void USBDeviceEventHandler(object Sender, USBEventArgs E);
        public delegate void OpticalDeviceEventHandler(object Sender);
        public event USBDeviceEventHandler DeviceInserted;
        public event USBDeviceEventHandler DeviceRemoved;
        public event OpticalDeviceEventHandler OpticalDiscInserted;
        public event OpticalDeviceEventHandler OpticalDiscRemoved;

        public bool WatchUSB { get; set; } = true;
        public bool WatchCD { get; set; } = false;

        public EventWatcher()
        {
            watcherThread = new Thread(new ThreadStart(() => { }));
        }

        private void threadRun()
        {
            var usbWatcher = new ManagementEventWatcher();
            usbWatcher.Query = new EventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 OR EventType = 3");

            usbWatcher.EventArrived += (s, ea) =>
            {
                var driveName = $"{ea.NewEvent.Properties["DriveName"].Value}";
                var type = (USBEventType)(Enum.Parse(typeof(USBEventType), $"{ea.NewEvent.Properties["EventType"].Value}"));

                //get detailed data on the drive that was inserted
                var availableDrives = DriveInfo.GetDrives();
                var info = DriveInfo.GetDrives().Where(x => driveNameSanitizer(x.Name).Equals(driveNameSanitizer(driveName))).FirstOrDefault();

                Int64? freeSpace = null;
                Int64? totalSpace = null;
                DirectoryInfo root = null;
                string volume = null;
                if(info != null)
                {
                    freeSpace = info.AvailableFreeSpace.BytesToGigabytes();
                    totalSpace = info.TotalFreeSpace.BytesToGigabytes();
                    volume = info.VolumeLabel;
                    root = info.RootDirectory;
                }

                var usbDisk = new USBDisk { Name = driveName, Volume = volume, EntryPoint = root, AvailableSpace = freeSpace, TotalSpace = totalSpace };

                switch(type)
                {
                    case USBEventType.Inserted:
                        DeviceInserted?.Invoke(this, new USBEventArgs(type, usbDisk));
                        break;
                    case USBEventType.Removed:
                        DeviceRemoved?.Invoke(this, new USBEventArgs(type, usbDisk));
                        break;
                    default:
                        throw new NotImplementedException("That type was not implemented");
                }
            };

            var cdWatcher = new ManagementEventWatcher();
            cdWatcher.Query = new EventQuery("SELECT * FROM Win32_LogicalDisk WHERE DriveType = 5");

            cdWatcher.EventArrived += (s, ea) =>
            {

            };

            if (WatchUSB)
            {
                usbWatcher.Start();
            }
            if (WatchCD)
            {
                cdWatcher.Start();
            }

            while (shouldWatch) { Thread.Sleep(250); }

            usbWatcher.Stop();
            cdWatcher.Stop();
        }

        public void Start()
        {
            if(watcherThread == null)
            {
                //should never get here
                throw new InvalidOperationException("Cannot start the watcher, an error has occured. (the watcher object has been unassigned)");
            }

            if(watcherThread.IsAlive)
            {
                throw new InvalidOperationException("Cannot start the watcher, it is already started.");
            }

            shouldWatch = true;
            watcherThread = new Thread(new ThreadStart(threadRun));
            watcherThread.Start();
        }

        public void Stop()
        {
            if(watcherThread == null)
            {
                //should never get here
                throw new InvalidOperationException("Cannot stop the watcher, an error has occured. (the watcher object has been unassigned)");
            }

            if(!watcherThread.IsAlive)
            {
                throw new InvalidOperationException("Cannot stop the watcher, it is already stopped.");
            }

            shouldWatch = false;
        }

        private string driveNameSanitizer(string Input)
        {
            return Input.Replace(":", "").Replace(@"\", "").ToLower();
        }
    }
}
