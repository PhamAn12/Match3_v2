
// Add all together


using Entitas.VisualDebugging.Unity;

public class BoardSystems : Feature
{
    public BoardSystems ( GameContext Game)
    {       
        Add(new CreateBoardSystem(Game));        
        Add(new FallSystem(Game));      
        Add(new FillSystem(Game));
        
        Add(new GameStateSystems(Game));
        Add(new ScoreSystem(Game));
        Add(new MoveSystem(Game));  
        Add(new ViewSystem(Game));

    }
}