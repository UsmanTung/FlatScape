// represents the base game object; unsure what will need to be in this yet but, maybe coordinates? name? maybe some sort of size thing
class GameObject
{
    public required string uuid {get; set; }

    public int intendedX {get; set; }
    public int intendedY {get; set; }
    public int currentX  {get; set; }
    public int currentY {get; set; }
    public bool touched {get; set; }
}
