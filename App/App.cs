using System.Collections.Generic;
using GameController = Game.Game;
using ViewController = View.View;
using Domain.Enums;
using System.ComponentModel;

namespace App;
//TODO: add bot game simulation command

class App{
    GameController game;
    ViewController view;

    public App(GameController g, ViewController v){
        game = g;
        view = v;
    }

    public void Run(){
        view.PrintMessage(Message.Welcome);
        int count = 499;

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
                            view.PrintMessage("The game is restarting");
                            count--;

                            if(count < 0){
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