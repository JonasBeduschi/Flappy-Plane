namespace FlappyPlane
{
    public class TryAgainScreen : EndingWindow
    {
        protected override void HandlePlayerDeath(DeathEventArgs e)
        {
            if (e.Result == PlayerResult.None)
                Show(e);
        }

        protected override void Show(DeathEventArgs e)
        {
            base.Show(e);
        }
    }
}