﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using funkylib;

namespace Stupid {
  public readonly struct Player {
    public readonly PlayerNumber number;
    public readonly ImmutableList<Card> cards;

    public Player(ImmutableList<Card> cards, PlayerNumber number) {
      this.cards = cards;
      this.number = number;
    }

    public Player withCards(Func<ImmutableList<Card>, ImmutableList<Card>> f) => new Player(f(cards), number);
    public Player withCards (ImmutableList<Card> cards) => new Player(cards, number);

    public static Option<Player> parse(IEnumerable<string> playerCards, PlayerNumber number) {
      var parsedCards = new List<Card>();

      foreach (var card in playerCards) {
        var parsedCardOpt = Card.parse(card);
        if (!parsedCardOpt.isSome) return Option.None;
        parsedCards.Add(parsedCardOpt._unsafe);
      }
      return Option.Some(new Player(parsedCards.ToImmutableList(), number));
    }
  }

  public readonly struct PlayerNumber {
    public readonly int number;
    public PlayerNumber(int number) { this.number = number; }
  }
}