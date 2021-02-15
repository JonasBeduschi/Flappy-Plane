using System;

namespace FlappyPlane
{
    public enum CollisionObject { Rock, Roof, Ground }

    /// <summary>EventArgs for the death of the player</summary>
    public class DeathEventArgs : EventArgs
    {
        /// <summary>The score of the player on this run</summary>
        public int PlayerScore { get; }

        /// <summary>The position the player achieved on the high score board</summary>
        public int Position { get; }

        /// <summary>The reason why the player died</summary>
        public CollisionObject Collision { get; }

        public DeathEventArgs(int score, CollisionObject collision = CollisionObject.Rock)
        {
            PlayerScore = score;
            Position = Highscore.CheckHighscore(score);
            Collision = collision;
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

    /// <summary>EventArgs for selecting plane color/sprite</summary>
    public class PlanePickArgs : EventArgs
    {
        /// <summary>Index of the plane sprite</summary>
        public int Index { get; }

        public PlanePickArgs(int planeIndex)
        {
            Index = planeIndex;
        }
    }
}