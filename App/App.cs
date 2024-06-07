using System.Collections.Generic;
using GameController = Game.Game;
using ViewController = View.View;
using Domain.Enums;
using System.ComponentModel;

namespace App;
class App{
    GameController game;
    ViewController view;
    int simulations;

    public App(GameController g, ViewController v, int s){
        game = g;
        view = v;
        simulations = s;
    }

    public void Run(){
        view.PrintMessage(Message.Welcome);

        while(true){
            Console.Write("Enter command: ");
            string input = Console.ReadLine()!;

            switch(input.ToLower()){
                case "start": 
                    Console.WriteLine("New game begins");
                    view.PrintBoard(game);

                    while(true){
                        EPlayerAction action = game.Move();

                        if(action == EPlayerAction.Exit){
                            view.PrintMessage("The game is exited");
                            return;
                        } else if(action == EPlayerAction.Restart){
                            simulations--;

                            if(simulations <= 0){
                                for(int i = 0; i < game.Winners.Count(); i++){
                                    view.PrintMessage("=======================================");
                                    view.PrintMessage($"Player {game.GetPlayerType(i)} won {game.Winners[i]} times");
                                }

                                return;
                            }
                        } else if(action == EPlayerAction.Move){
                            view.PrintBoard(game);
                        }
                    }
                case "exit":
                    view.PrintMessage("The game is exited");
                    return;
                default:
                    Console.WriteLine($"{input} is not a valid command");
                    break;
            }
        }
    }
}