using idtiApiService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//using System.Web;

namespace idtiApiService.Middleware
{
    public class DeviceMiddleware
    {

        private readonly RequestDelegate _next;
        private IApiService _apiService;

        public DeviceMiddleware(RequestDelegate next, IApiService apiService) {
            this._next = next;
            this._apiService = apiService;
        }


        public async Task Invoke(HttpContext context) {

            int deviceId = 0;
            try {
                
                var match = Regex.Match( context.Request.Path.ToString(), "device/\\d+");
                if (match.Success && match.Groups.Any()) {
                    int.TryParse(match.Groups[0].Value.Replace("device/", ""), out deviceId);
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);

            }

            if (deviceId > 0) _apiService.setFlagDevice(deviceId, false); // sincronizacion polling .. 

            await this._next.Invoke(context);

            if (deviceId > 0) _apiService.setFlagDevice(deviceId, true); // sincronizacion polling .. 

        }


    }
}
