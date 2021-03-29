using System;

namespace FlappyPlane
{
    public enum CollisionObject { Rock, Roof, Ground }

    public enum PlayerResult { None, Highscore, Medal }

    /// <summary>EventArgs for the death of the player</summary>
    public class DeathEventArgs : EventArgs
    {
        /// <summary>The score of the player on this run</summary>
        public int PlayerScore { get; }

        /// <summary>The position the player achieved on the high score board</summary>
        public int Position { get; }

        /// <summary>The reason why the player died</summary>
        public CollisionObject Collision { get; }

        /// <summary>The result based on the position</summary>
        public PlayerResult Result { get; }

        public DeathEventArgs(int score, CollisionObject collision = CollisionObject.Rock)
        {
            PlayerScore = score;
            Collision = collision;

            Position = Highscore.CheckHighscore(score);

            Result = PlayerResult.None;
            if (Position >= 1) {
                if (Position <= 3)
                    Result = PlayerResult.Medal;
                else if (Position <= 10)
                    Result = PlayerResult.Highscore;
            }
        }
    }

    /// <summary>EventArgs for player collisions</summary>
    public class CollisionArgs : EventArgs
    {
        /// <summary>What the player hit</summary>
        public CollisionObject Collision { get; }

        /// <summary>How strong was the collision</summary>
        public float Strength { get; }

        public CollisionArgs(CollisionObject collision, float strength)
        {
            Collision = collision;
            Strength = strength;
        }
    }
}