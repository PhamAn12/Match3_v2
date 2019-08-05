public class DebugController : Feature
{
    public DebugController(GameContext gameContext)
    {
        Add(new AddDebugComponentSystem(gameContext));
        Add(new ChangeItemDebugSystem(gameContext));
        Add(new RemoveDebugSystem(gameContext));
    }
}