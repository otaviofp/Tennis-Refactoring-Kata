// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TennisGame1.cs" company="Microsoft" >
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Tennis
{
    using System.Diagnostics;

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

        public Player Player2 { get; set; }

        public string GetScore()
        {
            string score = string.Empty;
            if (Equals(this.Player1.Points, this.Player2.Points))
            {
                if (this.Player1.Points.Point >= 3)
                {
                    score = "Deuce";
                }
                else
                {
                    score = string.Format("{0}-All", this.Player1.Points.GetTieString());
                }
            }
            else if (this.Player1.Points.Point >= 4 || this.Player2.Points.Point >= 4)
            {
                var minusResult = this.Player1.Points.Point - this.Player2.Points.Point;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
                else score = "Win for player2";
            }
            else
            {
                score = string.Format("{0}-{1}", this.Player1.Points.GetScoreString(), this.Player2.Points.GetScoreString());
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

        public string GetScoreString()
        {
            return this.ConvertScoreString(false);
        }

        public string GetTieString()
        {
            return this.ConvertScoreString(true);
        }

        public void Increment()
        {
            ++this.Point;
        }

        protected bool Equals(PointCount other)
        {
            return this.Point == other.Point;
        }

        private string ConvertScoreString(bool isTie)
        {
            string scoreString;
            switch (this.Point)
            {
                case 0:
                    scoreString = "Love";
                    break;
                case 1:
                    scoreString = "Fifteen";
                    break;
                case 2:
                    scoreString = "Thirty";
                    break;
                case 3:
                    if (isTie)
                    {
                        scoreString = "Deuce";
                    }
                    else
                    {
                        scoreString = "Forty";
                    }

                    break;
                default:
                    Debug.Assert(isTie, "should only be here in case of a tie");
                    scoreString = "Deuce";
                    break;
            }

            return scoreString;
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
            return this.score.GetScore();
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1") this.score.Player1.Points.Increment();
            else this.score.Player2.Points.Increment();
        }
    }
}