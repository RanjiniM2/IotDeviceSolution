using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IotHubDevice.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Options;

namespace IotHubDevice.Repository
{
    public class IoTDeviceProperties
    {
        private static string iothubConnectionString = "HostName=Ranjini.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=C+MGp+er+X3DhSnGO0HymqcSiJRtMyVmdVx6M6uvwaU=";
        
        public static RegistryManager registryManager=RegistryManager.CreateFromConnectionString(iothubConnectionString);

        public static DeviceClient client = null;

        public static string myDeviceConnection = "HostName=Ranjini.azure-devices.net;DeviceId=test;SharedAccessKey=hp3IG5qV3T2KNYEicEkGjCVaeP61CcGaHUOLRpzBIRw=";

        public static async Task AddDeviceProperties(string deviceName,DeviceProperties deviceProperties)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Kindly enter a valid device name");
            }

            else
            {
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection twinCollection, connectivity;
                twinCollection= new TwinCollection();
                connectivity= new TwinCollection();
                connectivity["type"] = "cellular";
                twinCollection["connectivity"] = "connectivity";
                twinCollection["temperature"] = deviceProperties.temperature;
                twinCollection["drift"] = deviceProperties.drift;
                twinCollection["fullscale"] = deviceProperties.fullscale;
                twinCollection["pressure"] = deviceProperties.pressure;
                twinCollection["accurarcy"] = deviceProperties.accurarcy;
                twinCollection["sensorType"] = deviceProperties.sensorType;
                twinCollection["resolution"] = deviceProperties.resolution;
                twinCollection["supplyVoltageLevel"] = deviceProperties.supplyVoltageLevel;
                twinCollection["frequency"] = deviceProperties.frequency;
                twinCollection["dateTimeLastAppLaunch"] = deviceProperties.dateTimeLastAppLaunch;
               await client.UpdateReportedPropertiesAsync(twinCollection);
                return;

            }

        }

        public static async Task DesiredProperties(string deviceName)
        {
            client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device = await registryManager.GetTwinAsync(deviceName);
            TwinCollection twinCollection, telemetryconfig;
            twinCollection = new TwinCollection();
            telemetryconfig = new TwinCollection();
            telemetryconfig["frequency"] = "15Hz";
            twinCollection["telemetryconfig"] = telemetryconfig;
            device.Properties.Desired["telemetryconfig"] = telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId,device,device.ETag);
            //return;


        }

        public static async  Task <Twin>GetDeviceProperties(string deviceName)
        {
            var device = await registryManager.GetTwinAsync(deviceName);
            return device;
        }

        public static async Task UpdateDeviceTagProperties(string deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Please give valid device name");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceName);
                var patch =
                    @"{
                       tags:{
                            location:{
                                region:'Canada',
                                plant:'IOTPro'
                                }
                            }
                    }";
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity =  new TwinCollection();
                connectivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);

            }

        }
    }
}
