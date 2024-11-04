using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Server
{
    static void Main()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        TcpListener server = new TcpListener(ip, 8080);

        server.Start();
        Console.WriteLine("Server started on " + server.LocalEndpoint);

        while (true)
        {
            Console.WriteLine("Waiting for a connection...");
            TcpClient client = server.AcceptTcpClient();

            Task.Run(() => HandleClient(client));
        }

        // Stop listening for new clients.
        server.Stop();
    }

    static void HandleClient(TcpClient client)
    {
        Console.WriteLine("Connected to client " + ((IPEndPoint)client.Client.RemoteEndPoint).ToString());

        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        
        string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Received message: " + data);

        string response = "Server response: " + data;
        byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
        
        stream.Write(responseBuffer, 0, responseBuffer.Length);
        Console.WriteLine("Response sent.");

        stream.Close();
        client.Close();
    }
}