using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using idtiApiService.Model;

namespace idtiApiService.Service
{
    public interface IApiService {

        void loadDevices();

        List<Terminal> findAllDevices();

        void pollingDevice();

        void setFlagDevice(int deviceId, bool flag);

        List<UserDev> fetchUsersDevice(int deviceId);

        UserDev fetchUserDevice(int deviceId, string userId);

        List<UserDev> sendUsersDevice(int deviceId, List<UserDev> userDev);

        int deleteUserDevice(int deviceId, string userId);

        int resetUsersDevice(int deviceId);

        int resetEventsDevice(int deviceId);

        string sendDateTimeDevice(int deviceId, string dateTime);

        string fetchDateTimeDevice(int deviceId);

    }
}
