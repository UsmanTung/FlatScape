class Player:GameObject
{ 
    public required string Name {get; set; }
    public int Hp {get; set; }
    
    public override string ToString() {
        return this.uuid + " " + this.Name;
    }

}