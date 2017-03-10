// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TennisGame1.cs" company="Microsoft" >
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Tennis
{
    class Player
    {
        private string name;

        public Player(string name)
        {
            this.name = name;
            this.Points = new PointCount();
        }

        public PointCount Points { get; set; }
    }

    class Score
    {
        public Player Player1 { get; set; }

        public Player Player2 { get; set;  }

        public string GetScore()
        {
            string score = string.Empty;
            if (Equals(Player1.Points, Player2.Points))
            {
                switch (Player1.Points.Point)
                {
                    case 0:
                        score = "Love-All";
                        break;
                    case 1:
                        score = "Fifteen-All";
                        break;
                    case 2:
                        score = "Thirty-All";
                        break;
                    default:
                        score = "Deuce";
                        break;
                }
            }
            else if (Player1.Points.Point >= 4 || Player2.Points.Point >= 4)
            {
                var minusResult = Player1.Points.Point - Player2.Points.Point;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
                else score = "Win for player2";
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    int tempScore;
                    if (i == 1) tempScore = Player1.Points.Point;
                    else
                    {
                        score += "-";
                        tempScore = Player2.Points.Point;
                    }

                    switch (tempScore)
                    {
                        case 0:
                            score += "Love";
                            break;
                        case 1:
                            score += "Fifteen";
                            break;
                        case 2:
                            score += "Thirty";
                            break;
                        case 3:
                            score += "Forty";
                            break;
                    }
                }
            }

            return score;
        }
    }

    class PointCount
    {
        public PointCount()
        {
            this.Point = 0;
        }

        public int Point { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((PointCount)obj);
        }

        public override int GetHashCode()
        {
            return this.Point;
        }

        public void Increment()
        {
            ++this.Point;
        }

        protected bool Equals(PointCount other)
        {
            return this.Point == other.Point;
        }
    }

    class TennisGame1 : ITennisGame
    {
        private readonly Score score;

        public TennisGame1(string player1Name, string player2Name)
        {
            this.score = new Score() { Player1 = new Player(player1Name), Player2 = new Player(player2Name) };
        }

        public string GetScore()
        {
            var score1 = this.score;

            return score1.GetScore();
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1") this.score.Player1.Points.Increment();
            else this.score.Player2.Points.Increment();
        }
    }
}