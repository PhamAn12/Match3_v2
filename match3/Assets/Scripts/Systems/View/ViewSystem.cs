

public class ViewSystem : Feature
{
    public ViewSystem(GameContext gameContext)   
    {
        
        Add(new AddViewSystem(gameContext));
        Add(new SetViewPositionSystem(gameContext));
        Add(new CheckDeleteSystem(gameContext));
        Add(new DebugController(gameContext));
        Add(new DiscoSystem(gameContext));
        Add(new RocketSystem(gameContext));
        Add(new BoomSystem(gameContext));
        
        //Add(new MechanicsSystem(gameContext));
        //Add(new RocketSystem(gameContext));
        //Add(new ReplaceViewRocketSystem(gameContext));
        Add(new AnimatePositionSystem(gameContext));


        Add(new OverlaySystem(gameContext));
        Add(new RemoveViewSystem(gameContext));
        Add(new CheckDieSystem(gameContext));        
        Add(new DestroyEntitySystem(gameContext));
        Add(new DestroyBoardGameSystem(gameContext));
    }
}
