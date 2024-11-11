static class Actions
{
    public const string Damg= "damg";
    public const string Heal= "heal";
    public const string Movx= "movx";
    public const string Movy= "movy";

    // data received with as 4 letter action and then 2 digits for values
    // DAMG20 = damage 20 -> -20 health
    // HEAL20 = heal 20 -> +20 health
    public static void ParseData(Player player, string data)
    {
        int counter = 0;
        while (counter < data.Length - 1)
        {
        string action = data.Substring(counter, 4).ToLower();
        int index = counter+4;
        while (!Char.IsLetter(data[index]) && index<data.Length-1){
            index+=1;
        }
        string value = data.Substring(counter + 4, index-counter-4).ToLower();
        if (action == Actions.Damg)
        {
            player.Hp -= Int32.Parse(value);
        }
        else if (action == Actions.Heal)
        {
            player.Hp += Int32.Parse(value);
        }
        else if (action == Actions.Movx)
        {
            player.intendedX = Int32.Parse(value);
        }
        else if (action == Actions.Movy)
        {
            player.intendedY = Int32.Parse(value);
        }
        counter += index-counter;
        }
    }
    
}

