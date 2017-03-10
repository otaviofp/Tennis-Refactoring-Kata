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

        public int Score { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.Score = 0;
        }
    }

    class TennisGame1 : ITennisGame
    {
        private Player player1;

        private Player player2;

        public TennisGame1(string player1Name, string player2Name)
        {
            this.player1 = new Player(player1Name);
            this.player2 = new Player(player2Name);
        }

        public string GetScore()
        {
            string score = string.Empty;
            var tempScore = 0;
            if (this.player1.Score == this.player2.Score)
            {
                switch (this.player1.Score)
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
            else if (this.player1.Score >= 4 || this.player2.Score >= 4)
            {
                var minusResult = this.player1.Score - this.player2.Score;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
                else score = "Win for player2";
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    if (i == 1) tempScore = this.player1.Score;
                    else
                    {
                        score += "-";
                        tempScore = this.player2.Score;
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

        public void WonPoint(string playerName)
        {
            if (playerName == "player1") this.player1.Score += 1;
            else this.player2.Score += 1;
        }
    }
}