namespace FlappyPlane
{
    public class TryAgainScreen : EndingWindow
    {
        protected override void HandlePlayerDeath(object sender, DeathEventArgs e)
        {
            if (e.Position <= 0)
                Show(e);
        }

        protected override void Show(DeathEventArgs e)
        {
            base.Show(e);
        }
    }
}