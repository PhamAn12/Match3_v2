public class DiscoSystem : Feature
{
    public DiscoSystem(GameContext gameContext)
    {
        Add(new ReplaceViewDiscoSystem(gameContext));
        Add(new MechanicDiscoSystem(gameContext));
        Add(new RemoveDiscoSystem(gameContext));
        Add(new DestroyDiscoSystem(gameContext));
    }
}