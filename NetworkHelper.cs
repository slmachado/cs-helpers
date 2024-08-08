using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Helpers
{
    public static class NetworkHelper
    {
        /// <summary>
        /// Gets the local IP address.
        /// </summary>
        /// <returns>The local IP address as a string.</returns>
        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// Checks if the specified port is open.
        /// </summary>
        /// <param name="host">The host name or IP address.</param>
        /// <param name="port">The port number.</param>
        /// <returns>True if the port is open; otherwise, false.</returns>
        public static bool IsPortOpen(string host, int port)
        {
            try
            {
                using var client = new TcpClient();
                var result = client.BeginConnect(host, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                return success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Pings the specified host.
        /// </summary>
        /// <param name="host">The host name or IP address.</param>
        /// <returns>True if the ping is successful; otherwise, false.</returns>
        public static bool PingHost(string host)
        {
            try
            {
                using var pinger = new Ping();
                var reply = pinger.Send(host);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the MAC address of the local machine.
        /// </summary>
        /// <returns>The MAC address as a string.</returns>
        public static string GetMacAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var sMacAddress = string.Empty;
            foreach (var adapter in nics)
            {
                if (sMacAddress == string.Empty)
                {
                    var properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
    }
}
