

public class InputSystem : Feature
{
    public InputSystem(InputContext Input, GameContext gameContext)
    {
        Add(new InputMouseSystem(Input));
        Add(new ProcessInputSystem(Input, gameContext));
        Add(new ProcessTapOnRocketSystem(Input, gameContext));
        Add(new ProcessTapOnBoomSystem(Input, gameContext));
        Add(new ProcessTapOnDiscoSystem(Input, gameContext));
        //Add(new CheckDeleteSystem(gameContext));
    }
}
    