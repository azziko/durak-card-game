using System.Collections.Generic;
using GameController = Game.Game;
using ViewController = View.View;

namespace App;

class App{
    GameController game;
    ViewController view;
    public App(GameController g, ViewController v){
        game = g;
        view = v;
    }

    public void Run(){
        return;
    }
}