//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly TapOnDiscoComponent tapOnDiscoComponent = new TapOnDiscoComponent();

    public bool isTapOnDisco {
        get { return HasComponent(GameComponentsLookup.TapOnDisco); }
        set {
            if (value != isTapOnDisco) {
                var index = GameComponentsLookup.TapOnDisco;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : tapOnDiscoComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTapOnDisco;

    public static Entitas.IMatcher<GameEntity> TapOnDisco {
        get {
            if (_matcherTapOnDisco == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TapOnDisco);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTapOnDisco = matcher;
            }

            return _matcherTapOnDisco;
        }
    }
}
