using System;

namespace FlappyPlane
{
    public static class EventSystem
    {
        public static event EventHandler<DeathEventArgs> OnPlayerDeath;

        public static event EventHandler<CollisionArgs> OnPlayerHitSomething;

        public static event EventHandler<PlanePickArgs> OnPlanePick;

        public static void FireEvent(object sender, EventArgs e)
        {
            if (e is DeathEventArgs)
                OnPlayerDeath?.Invoke(sender, e as DeathEventArgs);
            else if (e is CollisionArgs)
                OnPlayerHitSomething?.Invoke(sender, e as CollisionArgs);
            else if (e is PlanePickArgs)
                OnPlanePick?.Invoke(sender, e as PlanePickArgs);
        }
    }
}