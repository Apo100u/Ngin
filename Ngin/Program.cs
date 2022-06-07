using Ngin.Characters;
using Ngin.Gameplay;

namespace Ngin;

internal class Program
{
    public static void Main(string[] args)
    {
        // TEST ===========================================================================
        System.Random random = new();
        Character character1 = new Character("Ziom1", 10, random.Next(-3, 3));
        Character character2 = new Character("Ziom2", 10, random.Next(-3, 3));
        Character character3 = new Character("Ziom3", 10, random.Next(-3, 3));
        Character character4 = new Character("Ziom4", 10, random.Next(-3, 3));
        Character character5 = new Character("Ziom5", 10, random.Next(-3, 3));
        Character character6 = new Character("Ziom6", 10, random.Next(-3, 3));
        Character character7 = new Character("Ziom7", 10, random.Next(-3, 3));
        Character character8 = new Character("Ziom8", 10, random.Next(-3, 3));
        Character character9 = new Character("Ziom9", 10, random.Next(-3, 3));
        Character character10 = new Character("Ziom10", 10, random.Next(-3, 3));

        Team team1 = new(character1, character2, character3, character4, character5);
        Team team2 = new(character6, character7, character8, character9, character10);
        
        GameSettings settings = new(4, 1, 10);
        
        Game game = new(settings, team1, team2);
        game.Start();
        // TEST ===========================================================================
    }
}