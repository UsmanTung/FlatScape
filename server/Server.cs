using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Concurrent;


class Server
{
  static void Main()
  {
    ConcurrentDictionary<TcpClient, Player> players = new ConcurrentDictionary<TcpClient, Player>();
    IPAddress ip = IPAddress.Parse("127.0.0.1");
    TcpListener server = new TcpListener(ip, 8080);

    server.Start();
    Console.WriteLine("Server started on " + server.LocalEndpoint);
    GameObject[] gameObjects = [];
    GameState gameState = new GameState(gameObjects);
    GameServer gameServer = new GameServer(gameState);
    gameServer.Start();

    while (true)
    {
      Console.WriteLine("Waiting for a connection...");
      TcpClient client = server.AcceptTcpClient();

      Task.Run(() => HandleClient(client, players, gameState));
    }

    // Stop listening for new clients.
    server.Stop();
  }

  static void HandleClient(TcpClient client, ConcurrentDictionary<TcpClient, Player> players, GameState gameState)
  {
    Console.WriteLine("Connected to client " + ((IPEndPoint)client.Client.RemoteEndPoint).ToString());
    Player john = new Player() { uuid = "1", Name = "JOHNDOE", intendedX = 0, intendedY = 0, Hp = 100 };
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
