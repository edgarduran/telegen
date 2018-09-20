using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents
{
    public class NetworkAgent : Agent
    {
        public override Result Execute(Operation oper)
        {
            Guard(oper, "Get");
            return Get(oper);
        }

        protected NetResult Get(Operation oper )
        {
            var url = oper.Require<string>("url");
            var port = (int) oper.Optional<long>("port", 80);

            var req = new WebReq(url, port);

            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];
            string response = null;
            var utcTimeStamp = DateTime.MinValue;
            int clientPort = 0;

            // Connect to a remote device.  

            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            //IPHostEntry ipClientInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPHostEntry ipHostInfo = Dns.GetHostEntry(req.Uri.Host);
            IPAddress ipAddress = ipHostInfo.AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, req.Port);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  

            sender.Connect(remoteEP);
            clientPort = ((IPEndPoint)sender.LocalEndPoint).Port;

            //Console.WriteLine("Socket connected to {0}",
            //    sender.RemoteEndPoint.ToString());

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

            return new NetResult(new WebResp(req, response, utcTimeStamp, Dns.GetHostName(), clientPort));
        }

    }
}
