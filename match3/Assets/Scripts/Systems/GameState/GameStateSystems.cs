public sealed class GameStateSystems : Feature {

    public GameStateSystems(GameContext Game) {
        Add(new ScoreSystem(Game));
    }
}