using Domain;

class Bout{
    public List<Card> AttackingCards;
    public List<Card> DefendingCards;
    private bool isAttacking = true;

    public Bout(){
        AttackingCards = new List<Card>();
        DefendingCards = new List<Card>();
    }

    public bool isAttackersTurn(){
        return isAttacking;
    }

    public void PlayCard(Card card){
        if(isAttacking){
            AttackingCards.Add(card);
        } else {
            DefendingCards.Add(card);
        }

        isAttacking = !isAttacking;
    }

    public List<Card> ClearBout(){      
        List<Card> cardsCleared = new List<Card>();
        cardsCleared.AddRange(AttackingCards);
        cardsCleared.AddRange(DefendingCards);
        
        AttackingCards = new List<Card>();
        DefendingCards = new List<Card>();
        isAttacking = true;

        return cardsCleared;
    }
}