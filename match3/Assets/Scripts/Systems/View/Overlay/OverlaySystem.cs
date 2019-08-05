public class OverlaySystem : Feature
{
    public OverlaySystem(GameContext gameContext)
    {
        Add(new ClassifyItemOverlay(gameContext));
    }
}