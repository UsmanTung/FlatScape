using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Concurrent;
using System.Collections;


class Server
{
  static void Main()
  {
    int port = 8080;
    string localIp = "127.0.0.1";

    ConcurrentDictionary<TcpClient, Player> players = new ConcurrentDictionary<TcpClient, Player>();
    IPAddress ip = IPAddress.Parse(localIp);
    TcpListener server = new TcpListener(ip, port);


    server.Start();
    Console.WriteLine("Server started on " + server.LocalEndpoint);
    ArrayList gameObjects = new ArrayList();
    GameState gameState = new GameState(gameObjects);
    GameServer gameServer = new GameServer(gameState);
    gameServer.Start();

    while (true)
    {
      Console.WriteLine($"Waiting for a connection on port: {port}");
      TcpClient client = server.AcceptTcpClient();

      Task.Run(() => HandleClient(client, players, gameState));
    }

    // Stop listening for new clients.
    server.Stop();
  }

  static void HandleClient(TcpClient client, ConcurrentDictionary<TcpClient, Player> players, GameState gameState)
  {
    Console.WriteLine("Connected to client " + ((IPEndPoint)client.Client.RemoteEndPoint).ToString());
    Player john = new Player() { uuid = "1", Name = "JOHNDOE", intendedX = 0, intendedY = 0,currentX = 0, currentY=0, Hp = 100 };
    //players.TryAdd(client, john);
    gameState.AddGameObject(john);
    Console.WriteLine(Utils.PrettyPrint(players));
    NetworkStream stream = client.GetStream();

    byte[] buffer = new byte[1024];
    int bytesRead;

    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 1)
    {
      string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
      Actions.ParseData(john, data);
    }

    stream.Close();
    client.Close();

  }

}
