using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

class Server
{
    static void Main()
    {
        ConcurrentDictionary<TcpClient, Player> players = new ConcurrentDictionary<TcpClient, Player>();
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        TcpListener server = new TcpListener(ip, 8080);

        server.Start();
        Console.WriteLine("Server started on " + server.LocalEndpoint);

        while (true)
        {
            Console.WriteLine("Waiting for a connection...");
            TcpClient client = server.AcceptTcpClient();

            Task.Run(() => HandleClient(client, players));
        }

        // Stop listening for new clients.
        server.Stop();
    }

    static void HandleClient(TcpClient client, ConcurrentDictionary<TcpClient, Player> players)
    {
        Console.WriteLine("Connected to client " + ((IPEndPoint)client.Client.RemoteEndPoint).ToString());
        players.TryAdd(client, new Player()
        {
            Id = "1",
            Name = "JOHNDOE",
            X = 0,
            Y = 0
        });
        Console.WriteLine(PrettyPrint(players));
        NetworkStream stream = client.GetStream();
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received message: " + data);

                string response = "Server response: " + data;
                byte[] responseBuffer = Encoding.ASCII.GetBytes(response);

                stream.Write(responseBuffer, 0, responseBuffer.Length);
                Console.WriteLine("Response sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                stream.Close();
                client.Close();
            }

        }

    }

    public static string PrettyPrint(Object o)
    {
        var jsonString = JsonConvert.SerializeObject(
             o, Formatting.Indented,
             new JsonConverter[] { new StringEnumConverter() });
        return jsonString;
    }
}
