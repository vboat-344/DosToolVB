using System.Net.NetworkInformation;

namespace DosToolVB
{
    static class NetworkHelper
    {
        public static bool CheckInternet()
        {
            try
            {
                PingReply reply = new Ping().Send("8.8.8.8", 2000);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}