using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Wolah.IM.Helper;
using Wolah.IM.ViewModel;

namespace Wolah.IM.Network
{
    // A class that represents a WPF client connection
    public class TCPClient
    {
        // The underlying TcpClient object
        private TcpClient client;

        // The network stream for sending and receiving data
        private NetworkStream stream;

        // The event handler for data received event
        public event EventHandler<JObject> CallLoginWindow;

        // The constructor that takes a host name and a port number
        public TCPClient(string host, int port)
        {
            // Create a new TcpClient object and connect to the host and port
            client = new TcpClient();
            client.Connect(host, port);

            // Get the network stream from the TcpClient object
            stream = client.GetStream();
        }
        
        public TCPClient(IPAddress host, int port)
        {
            // Create a new TcpClient object and connect to the host and port
            client = new TcpClient();
            client.Connect(host, port);
            //check if the connection is established
            if (!client.Connected)
            {
                Console.WriteLine("Connection failed");
            }
            else
            {
                Console.WriteLine("Connection established");
            }
            // Get the network stream from the TcpClient object
            stream = client.GetStream();
        }

        // A method that starts receiving data asynchronously
        public async Task StartReceivingAsync()
        {
            // Create a buffer to store the received data
            byte[] buffer = new byte[0];

            // Read data from the network stream asynchronously in a while loop
            while (true)
            {
                // Create a temporary buffer to store the data read in each iteration
                byte[] tempBuffer = new byte[1_024];

                // Store the number of bytes read in each iteration
                int bytesRead = await stream.ReadAsync(tempBuffer, 0, tempBuffer.Length);

                // If no bytes were read, it means the connection was closed by the remote host
                if (bytesRead == 0)
                {
                    StopReceiving();
                    return;
                }

                // Append the data read to the buffer
                buffer = buffer.Concat(tempBuffer.Take(bytesRead)).ToArray();

                // Check if the buffer contains at least 4 bytes of message length
                while (buffer.Length >= 4)
                {
                    // Get the message length from the first 4 bytes of the buffer
                    int len = BitConverter.ToInt32(buffer, 0);

                    // Check if the buffer contains a complete message
                    if (buffer.Length >= len + 4)
                    {
                        // Extract the valid data (excluding the length prefix)
                        byte[] data = buffer.Skip(4).Take(len).ToArray();

                        // Remove the processed data from the buffer
                        buffer = buffer.Skip(len + 4).ToArray();

                        // Convert the data to a string using UTF8 encoding
                        string msg = Encoding.UTF8.GetString(data);

                        // Parse the string to a JObject using JSON.NET library and check if it is valid
                        try
                        {
                            JObject j = JObject.Parse(msg);
                            Int32.TryParse(j["cmd"].ToString(), out int cmd);
                            if (cmd == Commands.CmdLogin.ToInt() || cmd == Commands.CmdRegister.ToInt())
                            {
                                CallLoginWindow?.Invoke(this, j);
                            }
                        }
                        catch (ArgumentException)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break; // If the buffer does not contain a complete message, wait for more data to arrive
                    }
                }
            }
        }


        // A method that stops receiving data and closes the connection
        public void StopReceiving()
        {
            // Close the network stream and the TcpClient object
            stream.Close();
            client.Close();
        }

        // A method that sends data asynchronously
        public async Task SendDataAsync(JObject j)
        {
            // Convert the JObject to a string using JSON.NET library
            string msg = j.ToString();

            // Convert the string to a byte array using ASCII encoding
            byte[] bytes = Encoding.ASCII.GetBytes(msg);

            // Get the length of the byte array and convert it to a 4-byte array using BitConverter class
            int len = bytes.Length;
            byte[] buffer = BitConverter.GetBytes(len);

            // Create a new byte array that combines the length and the message bytes
            byte[] message = new byte[4 + len];
            Buffer.BlockCopy(buffer, 0, message, 0, 4);
            Buffer.BlockCopy(bytes, 0, message, 4, len);

            // Write both the length and the message bytes to the network stream asynchronously using Task.WhenAll to wait for both tasks to complete
            await Task.WhenAll(stream.WriteAsync(buffer, 0, buffer.Length), stream.WriteAsync(bytes, 0, bytes.Length));
        }
        
        public bool IsConnected()
        {
            return client.Connected;
        }
    }
}
