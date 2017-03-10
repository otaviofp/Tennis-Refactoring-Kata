namespace Tennis
{
    internal class ScoreRuleChain
    {
        private readonly IScoreRule[] _scoreRules;

        public ScoreRuleChain(IScoreRule[] scoreRules)
        {
            _scoreRules = scoreRules;
        }

        public string Evaluate(GameState gameState)
        {
            foreach (IScoreRule rule in _scoreRules)
            {
                string score = rule.Evaluate(gameState);
                if (score != null)
                {
                    return score;
                }
            }

            return "Score Error";
        }
    }
}