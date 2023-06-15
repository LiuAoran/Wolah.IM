using System.Net;

namespace Wolah.IM.Private;
public static class ServerSource
{
#if true
    public static IPAddress ServerIP { get; set; } = IPAddress.Parse("121.37.111.190");
    public static int ServerPort { get; set; } = 2339;
    public static string ServerKey { get; set; } = "WolahIM";
#else
    public static IPAddress ServerIP { get; } = IPAddress.Parse("127.0.0.1");
    public static int ServerPort { get; } = 8888;
#endif
}