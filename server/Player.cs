class Player 
{
    public string Id {get; set; }
    public string Name {get; set; }
    public int X {get; set; }
    public int Y {get; set; }
    
    public override string ToString() {
        return this.Id + " " + this.Name;
    }

}