namespace FoodballGame
{
    public class Game
    {
        public Team TeamA { get; }
        public Team TeamB { get; }
        public Stadium Stadium { get; }
        public Ball Ball { get; set; }

        public Game(Team teamA, Team teamB, Stadium stadium)
        {
            TeamA = teamA;
            teamA.Game = this;
            TeamB = teamB;
            teamB.Game = this;
            Stadium = stadium;
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
            TeamA.StartGame(Stadium.Width, Stadium.Height);
            TeamB.StartGame(Stadium.Width, Stadium.Height);
        }

        private (double, double) GetPositionForTeamB(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y);
        }

        public (double, double) GetPositionForTeam(Team team, double x, double y)
        {
            return team == TeamA ? (x, y) : GetPositionForTeamB(x, y);
        }

        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y);
        }

        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == TeamA)
            {
                Ball.SetSpeed(vx, vy);
            }
            else
            {
                Ball.SetSpeed(-vx, -vy);
            }
        }

        public void Move()
        {
            TeamA.Move();
            TeamB.Move();
            Ball.Move();
            CheckGoal();
        }

        private void CheckGoal()
        {
            int ballX = (int)Ball.X;
            int ballY = (int)Ball.Y;

            if (ballX == 0 && ballY >= (Stadium.Height / 2 - 2) && ballY <= (Stadium.Height / 2 + 2))
            {
                TeamB.Score++;
                ResetBall();
            }

            else if (ballX == Stadium.Width - 1 && ballY >= (Stadium.Height / 2 - 2) && ballY <= (Stadium.Height / 2 + 2))
            {
                TeamA.Score++;
                ResetBall();
            }
        }
        private void ResetBall()
        {
            Ball.SetPosition(Stadium.Width / 2, Stadium.Height / 2);
            Ball.SetSpeed(0, 0);
        }
    }
}
