using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IotHubDevice.Repository;
using Microsoft.Azure.Devices;

namespace IoT_Device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IotDeviceController : ControllerBase
    {
        

        [HttpPost("AddIoTDevice")]
        public async Task<string> Post(string deviceName)
        {
            await IotDevice.AddIotDevice(deviceName);
                return null;
        }

        [HttpGet("GetDevice")]
        public async Task<Device> Get(string deviceName)
        {
            Device device = await IotDevice.GetIotDevice(deviceName);
            return device;
        }

        [HttpPut("UpdateIoTDevice")]
        public async Task<Device> Put(string deviceName)
        {
            Device device;
            device = await IotDevice.UpdateIotDevice(deviceName);
            return device;
        }

        [HttpDelete("DeleteDevice")]
        public async Task<string> Delete(string deviceName)
        {
            await IotDevice.DeleteDevice(deviceName);
            return null;
        }
    }
}
