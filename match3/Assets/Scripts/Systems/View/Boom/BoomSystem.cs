using Entitas;

public class BoomSystem : Feature
{
    public BoomSystem(GameContext gameContext)
    {
        Add(new ReplaceViewBoomSystem(gameContext));
        Add(new BoomCombineDiscoSystem(gameContext));
        Add(new MechanicBoomSystem(gameContext));
        Add(new RemoveBoomSystem(gameContext));
        Add(new DestroyBlockBoomSystem(gameContext));
    }
}
