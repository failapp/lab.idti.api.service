using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using idtiApiService.Model;
using idtiApiService.Service;
using idtiApiService.Utils;
using Microsoft.Extensions.Logging;

namespace idtiApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase {

        private IApiService _apiService;
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(ILogger<DeviceController> logger, IApiService apiService) {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet("/api/sync/devices")]
        public IActionResult getAllDevices(int deviceId) {

            List<Terminal> deviceList = _apiService.findAllDevices();
            return Ok(deviceList);

        }

        [HttpGet("/api/sync/device/{deviceId}/datetime")]
        public IActionResult getDateTime(int deviceId) {

            string dateTime = _apiService.fetchDateTimeDevice(deviceId);
            return Ok(dateTime);

        }


        [HttpPost("/api/sync/device/{deviceId}/datetime")]
        public IActionResult sendDateTime([FromBody] DateTimeDevice dateTimeDevice, int deviceId ) {

            string date = _apiService.sendDateTimeDevice(deviceId, dateTimeDevice.dateTime);
            if (date == string.Empty) return NotFound();

            return Ok(date);

        }


        [HttpDelete("/api/reset/device/{deviceId}/users")]
        public IActionResult resetUsers(int deviceId) {

            int ack = _apiService.resetUsersDevice(deviceId);
            if (ack <= 0) return NotFound();
            return Ok(ack);

        }

        [HttpDelete("/api/reset/device/{deviceId}/eventdata")]
        public IActionResult resetEventData(int deviceId) {

            int ack = _apiService.resetEventsDevice(deviceId);
            if (ack <= 0) return NotFound();
            return Ok(ack);

        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
