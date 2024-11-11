using System;
using System.Diagnostics;
using System.Threading;

class GameServer
{
    private const int TickRate = 600; // tick rate in milliseconds;
    private bool isRunning = false;


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
    }
}
