// This class will represent the tiles that build up the 2D worldmap
using System.Collections.Concurrent;

class Tile
{
    // String can be name to look up game objects
    public ConcurrentDictionary<String, GameObject> gameObjects;


    public Tile(){
        this.gameObjects = new ConcurrentDictionary<String, GameObject>();
    }
    public bool addGameObject(GameObject go) {
        return gameObjects.TryAdd(go.uuid, go);
    }

    public bool removeGameObject(GameObject go){
        return gameObjects.TryRemove(go.uuid, out var go2);
    }


}
