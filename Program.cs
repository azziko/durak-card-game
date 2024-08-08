using Game;

public class Program(){
    static void Main(string[] args){
        ConsoleAgent ca = new ConsoleAgent();
        RandomAgent ra = new RandomAgent();
        SmartAgent sa = new SmartAgent();
        Game.Game game;
        int simulations = 0;

        if(args.Any(arg => arg == "sim")){
            game = new Game.Game([sa, ra]);
        } else {
            game = new Game.Game([ca, sa]);
        }

        args.Any(arg => int.TryParse(arg, out simulations));

        View.View view = new View.View();

        App.App app = new App.App(game, view, simulations);

        app.Run();
    }
}
