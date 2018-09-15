using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using telegen.Actors;

namespace telegen.Agents
{
    public class NetworkAgent : INetworkAgent
    {

        public WebResp Execute(WebReq req)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];
            string response = null;
            var utcTimeStamp = DateTime.MinValue;
            int clientPort = 0;

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                //IPHostEntry ipClientInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPHostEntry ipHostInfo = Dns.GetHostEntry(req.Uri.Host);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, req.Port);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);
                    clientPort = ((IPEndPoint)sender.LocalEndPoint).Port;

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes(req.ToString());

                    // Send the data through the socket.  
                    utcTimeStamp = DateTime.UtcNow;
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = sender.Receive(bytes);
                    response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    // Release the socket.  
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return new WebResp(req, response, utcTimeStamp, Dns.GetHostName(), clientPort);
        }



    }
}
