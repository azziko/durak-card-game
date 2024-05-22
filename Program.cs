//TODO: Add game initialization and play 
// (or maybe move it to App and just call Game.Begin() here and Console.ReadKey())
using Domain;
using App;
using Game;
using View;
using System.Collections.Generic;

public class Program(){
    static void Main(string[] args){
        ConsoleAgent ca = new ConsoleAgent();
        RandomAgent ra = new RandomAgent();
        List<Player> players = new List<Player>{ca, ra};
        Game.Game game = new Game.Game(players);
        View.View view = new View.View();

        App.App app = new App.App(game, view);

        app.Run();
    }
}