using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using App;
using Domain;
using Game;

namespace View;

class View{
    public void PrintBoard(Game.Game g){
        int deckSize = g.GetDeckSize();
        Card tramp = g.Tramp;
        Bout bout = g.GetBout();
        List<Card> activePlayerHand = g.GetHandActivePlayer(); 
        int cardsOpponentCount = g.CountCardsOpponent();

        Console.WriteLine("=========================================");
        Console.WriteLine("Trump Card: [{0}]", tramp.ToString());
        Console.WriteLine("");
        Console.Write("Your Hand: ");
        PrintCardsList(activePlayerHand);
        Console.WriteLine();
        Console.WriteLine("Deck size: {0} cards", deckSize);
        Console.WriteLine("");
        Console.WriteLine("Bout:");
        Console.WriteLine("-----------------------------------------");
        Console.Write("Attacking Cards: ");
        PrintCardsList(bout.AttackingCards);
        Console.WriteLine("");
        Console.Write("Defending Cards: ");
        PrintCardsList(bout.DefendingCards);
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("");
        Console.WriteLine("Opponent's Cards: {0}", cardsOpponentCount);
        Console.WriteLine("");
    }

    public void PrintCardsList(List<Card> cards){
        if(cards.Count == 0){
            Console.Write("-");
            return;
        }

        foreach(Card card in cards){
            Console.Write("[{0}] ", card.ToString());
        }
    }

    public void PrintMessage(string message){
        Console.WriteLine(message);
    }
}