// The Game State will hold all the data for the lay of the world including:
// The tiles, NPCs, players, etc


class GameState
{
    const int gameWidth = 100;
    const int gameHeight = 100;
    Tile[,] tiles;

    public GameState(GameObject[] allObjects){
        this.tiles = new Tile[gameWidth, gameHeight];
        for (int col = 0; col<gameWidth; col++){
            for(int row=0; row<gameHeight;  row++){
                tiles[row, col] = new Tile();
            }
        }
        foreach (GameObject obj in allObjects){
            tiles[obj.intendedY,obj.intendedX].addGameObject(obj);
        }
    }

    public void AddGameObject(GameObject obj){
        tiles[obj.intendedY,obj.intendedX].addGameObject(obj);
    }

    public void UpdateState(){
        for (int col = 0; col<gameWidth; col++){
            for(int row=0; row<gameHeight;  row++){
                foreach (GameObject obj in this.tiles[row,col].gameObjects.Values) {
                    if(obj.touched){
                        continue;
                    }
                    int Xmove = 0;
                    int Ymove =0;
                    if (obj.intendedX != col){
                        if( col > obj.intendedX){
                            Xmove=-1;
                        }
                        else {
                            Xmove=1;
                        }
                    }
                    if (obj.intendedY != row){
                        if( row > obj.intendedY){
                            Ymove=-1;
                        }
                        else {
                            Ymove=1;
                        }
                    }
                    if(Xmove !=0 || Ymove !=0 ){
                        tiles[row,col].removeGameObject(obj);
                        tiles[row+Ymove, col+Xmove].addGameObject(obj);
                    }
                    obj.touched = true;
                }
            }
        }
        for (int col = 0; col<gameWidth; col++){
            for(int row=0; row<gameHeight;  row++){
                foreach (GameObject obj in this.tiles[row,col].gameObjects.Values) {
                   obj.touched = false;
                }
            }
        }
    }
}
