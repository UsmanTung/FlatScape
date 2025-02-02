using System.Diagnostics;

class GameServer
{
    private const int TickRate = 5000; // tick rate in milliseconds;
    private bool isRunning = false;
    private GameState gameState;

    public GameServer(GameState gameState){
        this.gameState=gameState;
    }


    public void Start()
    {
        isRunning = true;

        Thread gameLoopThread = new Thread(GameLoop);
        gameLoopThread.Start();
    }


    public void Stop()
    {
        isRunning = false;
    }

    private void GameLoop()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (isRunning)
        {
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            ProcessGameTick();

            long sleepTime = TickRate - (stopwatch.ElapsedMilliseconds - elapsedMilliseconds);
            if (sleepTime > 0)
            {
                Thread.Sleep((int)sleepTime);
            }
        }

        stopwatch.Stop();

    }

    private void ProcessGameTick()
    {
        // TODO: update the game state here
        // update gamestate based on queue of client actions
        gameState.UpdateState();
        Console.WriteLine(Utils.PrettyPrint(gameState.gameObjects));
    }
}
