using Domain;

namespace Game;

partial class Game{
    private Bout bout;
    private List<Player> players;
    private Deck deck;
    private List<Card> discardPile = new List<Card>();
    private int activePlayer = 0;
    private int winner = -1;

    private (bool, string) isValidMove(Card? card){
        if(card == null){
            if(
                bout.isAttackersTurn() &&
                bout.AttackingCards.Count == 0
            ) return (false, "Attack is obligatory");

            return (true, "");
        }
        
        if(bout.isAttackersTurn()){
            if(
                players[(activePlayer+1) % players.Count].GetCards().Count <= 1 && 
                deck.CardsLeft() != 0
            ){
                return (false, "You can't do more attacks!");
            }

            if(bout.AttackingCards.Count == 0){
                return (true, "");
            } else {
                if(
                    (bout.AttackingCards.Find(c => c.Val == card.Val) != null) ||
                    (bout.DefendingCards.Find(c => c.Val == card.Val) != null)
                ){
                    return (true, "");
                } else {
                    return (false, $"card {card} does not match any Value on board");
                }
            }
        } else {
            Card cardtoBeat = bout.AttackingCards.Last();

            if(card.Suit == deck.Trump.Suit){
                if(
                    cardtoBeat.Suit == deck.Trump.Suit &&
                    cardtoBeat.Val > card.Val
                ){
                    return(false, $"{cardtoBeat} is not beatable by {card}");
                } else {
                    return(true, "");
                }
            } else {
                if(cardtoBeat.Suit == deck.Trump.Suit){
                    return(false, $"{cardtoBeat} is not beatable by {card}");
                }

                if(cardtoBeat.Suit != card.Suit){
                    return(false, $"{cardtoBeat} and {card} don't much the suits");
                }

                if(cardtoBeat.Val > card.Val){
                    return(false, $"{cardtoBeat} has higher value than {card}");
                }

                return (true, "");
            }
        }
    }

    private void startNewGame(){
        Card? lowestTrump = null;
        for(int i = 0; i < players.Count; i++){
            players[i].ClearHand();
        }

        for(int i = 0; i < players.Count; i++){
            players[i].AddCards(deck.Draw(6));
            Card? lowestTrumpDealt = players[i].GetCards().Where(card => card.Suit == deck.Trump.Suit)
                                    .OrderBy(card => card.Val)
                                    .FirstOrDefault();

            if(lowestTrumpDealt != null){
                if(
                    lowestTrump != null &&
                    lowestTrumpDealt.Val < lowestTrump.Val
                ){
                    lowestTrump = lowestTrumpDealt;
                    activePlayer = i;
                } else if (lowestTrump == null) {
                    lowestTrump = lowestTrumpDealt;
                    activePlayer = i;
                }
            }
        }

        if(lowestTrump == null){
            activePlayer = Rand.Next(players.Count);
        }
    }

    private void refillHands(){
        for(int i = 0; i < players.Count; i++){
            Player currentPlayer = players[(activePlayer + i) % players.Count];
            int cardsInHand = currentPlayer.CountCards();
            int deckSize = deck.CardsLeft();

            if(cardsInHand < 6){
                if(6 - cardsInHand > deckSize){
                    currentPlayer.AddCards(deck.Draw(deckSize));
                } else {
                    currentPlayer.AddCards(deck.Draw(6 - cardsInHand));
                }
            }
        }
    }

    private void checkWinner(){
        if(deck.CardsLeft() == 0){
            for(int i = 0; i < players.Count; i++){
                Player currentPlayer = players[i];
                int cardsInHand = currentPlayer.CountCards();

                if(cardsInHand == 0){
                    winner = i;
                    return;
                }
            }
        }
    }

    private void nextPlayerClockWise(int n){
        activePlayer = (activePlayer + n) % players.Count;
    }

    private bool makeMove(Card? card){
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count]; 

        if(card == null){
            bool wasAttackersTurn = bout.isAttackersTurn();
            List<Card> cardsCleared = bout.ClearBout();

            if(wasAttackersTurn){
                discardPile.AddRange(cardsCleared);
                refillHands();
                nextPlayerClockWise(1);
            } else {
                currentPlayer.AddCards(cardsCleared);
                refillHands();
                nextPlayerClockWise(2);
            }

            checkWinner();

            return true;
        }

        bool wasRemoved = currentPlayer.RemoveCard(card);
        if(!wasRemoved){
            return false;
        }
        bout.PlayCard(card);

        return true;
    }
}