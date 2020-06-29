using System;

namespace AM_BackendForFrontend_Data.Generic
{
    public class NotificationsServiceBaseAddress : INotificationsServiceBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public NotificationsServiceBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
