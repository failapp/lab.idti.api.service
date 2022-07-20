using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using idtiApiService.Model;
using idtiApiService.Service;


namespace idtiApiService.Controllers
{
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UserDevController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<UserDevController> _logger;
        private IApiService _apiService;

        public UserDevController(IConfiguration configuration, ILogger<UserDevController> logger, IApiService apiService) {
            _configuration = configuration;
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet("/api/sync/device/{deviceId}/users")]
        public ActionResult<List<UserDev>> getUsersDevice(int deviceId)
        {

            List<UserDev> userDevList = _apiService.fetchUsersDevice(deviceId);
            return userDevList;
        }


        [HttpGet("/api/sync/device/{deviceId}/user/{userId}")]
        public ActionResult<UserDev> getUserDevice(int deviceId, string userId)
        {

            UserDev user = _apiService.fetchUserDevice(deviceId, userId);
            if (user == null) return BadRequest();

            return user;
        }


        [HttpPost("/api/sync/device/{deviceId}/users")]
        public IActionResult sendUsersDev([FromBody] List<UserDev> userDevList, int deviceId)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            List<UserDev> userList = _apiService.sendUsersDevice(deviceId, userDevList);

            return Ok(userList);

        }




        [HttpDelete("/api/sync/device/{deviceId}/user/{userId}")]
        public IActionResult deleteUserDevice(int deviceId, string userId)
        {

            int ack = _apiService.deleteUserDevice(deviceId, userId);
            if (ack <= 0) return BadRequest();

            return Ok(ack);

        }




        ///////////////////////////////////////////////////////////////////////////////////////////////////////////// ..
    }
}
