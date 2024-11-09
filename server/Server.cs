using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;

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
    Player john = new Player() { Id = "1", Name = "JOHNDOE", X = 0, Y = 0, Hp = 100 };
    players.TryAdd(client, john);
    Console.WriteLine(PrettyPrint(players));
    NetworkStream stream = client.GetStream();

    byte[] buffer = new byte[1024];
    int bytesRead;

    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 1)
    {
      string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
      Actions.ParseData(john, data);
      Console.WriteLine(PrettyPrint(players));
      Thread.Sleep(500);
    }

    stream.Close();
    client.Close();

  }
  
  public static string PrettyPrint(Object o)
  {
    var jsonString = JsonConvert.SerializeObject(
         o, Formatting.Indented,
         new JsonConverter[] { new StringEnumConverter() });
    return jsonString;
  }
}
