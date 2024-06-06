using Domain;
using App;
using Game;
using View;
using System.Collections.Generic;

public class Program(){
    static void Main(string[] args){
        ConsoleAgent ca = new ConsoleAgent();
        RandomAgent ra = new RandomAgent();
        SmartAgent sa = new SmartAgent();

        List<Player> players = new List<Player>{sa, ra};
        Game.Game game = new Game.Game(players);
        View.View view = new View.View();

        App.App app = new App.App(game, view);

        app.Run();
    }
}
