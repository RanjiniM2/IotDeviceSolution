using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using IotHubDevice.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;


namespace IotHubDevice.Repository
{
    public class SendTelemeteryMessages
    {
        private static string iothubConnectionString = "HostName=iothubranjini.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=b2L9WR5tVRmwQ50qV4qp1Of9k9E/DaiDESTe7QQNWnI=";
        public static RegistryManager registryManager;

        public static DeviceClient client = null;

        public static string myDeviceConnection = "HostName=iothubranjini.azure-devices.net;DeviceId=testdevice;SharedAccessKey=dMNFqtLLdh+akQbegtdnHtl2SF8p1l8PjzwB2HEnUL0=";
        public static async Task SendMessage(string deviceName)
        {
            try
            {
                registryManager=RegistryManager.CreateFromConnectionString(iothubConnectionString);
                var device = await registryManager.GetTwinAsync(deviceName);
                DeviceProperties reportedProperties = new DeviceProperties();
                TwinCollection twinCollection;
                twinCollection = device.Properties.Reported;
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                while(true)
                {
                    var telemetry = new
                    {
                        temperature = twinCollection["temperature"],
                        //drift = twinCollection["drift"],
                        accurarcy = twinCollection["accurarcy"],
                        //fullscale = twinCollection["fullscale"],
                        pressure = twinCollection["pressure"],
                        //supplyVoltageLevel = twinCollection["supplyVoltageLevel"],
                        frequency = twinCollection["frequency"],
                        //resolution = twinCollection["resolution"],
                        dateTimeLastAppLaunch = twinCollection["dateTimeLastAppLaunch"],
                        //sensorType = twinCollection["sensorType"],


                    };

                    var telemetrys = JsonConvert.SerializeObject(telemetry);
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telemetrys));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0}>Sending message:{1}",DateTime.Now,telemetrys);
                    await Task.Delay(1000);

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
