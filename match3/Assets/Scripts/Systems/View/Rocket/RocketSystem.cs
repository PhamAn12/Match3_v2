public class RocketSystem : Feature
{
    public RocketSystem(GameContext gameContext)
    {

        Add(new ReplaceViewRocketSystem(gameContext));
        Add(new RocketCombineDiscoSystem(gameContext));
        Add(new MechanicRocketSystem(gameContext));
        Add(new RemoveRocketSystem(gameContext));
        Add(new DestroyBlockRocketSystem(gameContext));
    }
}