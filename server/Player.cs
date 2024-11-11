class Player:GameObject
{ 
    public string Name {get; set; }
    public int Hp {get; set; }
    
    public override string ToString() {
        return this.uuid + " " + this.Name;
    }

}