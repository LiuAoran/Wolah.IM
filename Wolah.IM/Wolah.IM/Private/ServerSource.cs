﻿using System.Net;

namespace Wolah.IM.Private;
public static class ServerSource
{
#if true
    public static IPAddress ServerIP { get; } = IPAddress.Parse("xxx.xxx.xxx.xxx");
    public static int ServerPort { get; } = 8888;
#else
    public static IPAddress ServerIP { get; } = IPAddress.Parse("127.0.0.1");
    public static int ServerPort { get; } = 8888;
#endif
}