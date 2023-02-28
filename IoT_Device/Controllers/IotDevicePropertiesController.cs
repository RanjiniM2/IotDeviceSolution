using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IotHubDevice.Models;
using IotHubDevice.Repository;
using Microsoft.Azure.Devices.Shared;


namespace IoT_Device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IotDevicePropertiesController : ControllerBase
    {

        

        [HttpPut("UpdateDeviceReportProperties")]
        public async Task<string> UpdateReportProperties(string deviceName,DeviceProperties reportedProperties)
        {
            await IoTDeviceProperties.AddDeviceProperties(deviceName, reportedProperties);
            return null;

        }

        [HttpPut("UpdateDeviceDesiredProperties")]
        public async Task<string> UpdateDesiredProperties(string deviceName)
        {
            await IoTDeviceProperties.DesiredProperties(deviceName);
            return null;

        }

        [HttpPut("UpdateDeviceTagProperties")]
        public async Task<string> UpdateTagProperties(string deviceName)
        {
            await IoTDeviceProperties.UpdateDeviceTagProperties(deviceName);
            return null;

        }

        [HttpGet("GetIoTDeviceProperties")]
        public async Task<Twin> GetIoTDevice(string deviceName)
        {
            var device=await IoTDeviceProperties.GetDeviceProperties(deviceName);
            return device;

        }
    }
}
