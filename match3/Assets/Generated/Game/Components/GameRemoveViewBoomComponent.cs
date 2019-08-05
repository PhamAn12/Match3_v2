//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly RemoveViewBoomComponent removeViewBoomComponent = new RemoveViewBoomComponent();

    public bool isRemoveViewBoom {
        get { return HasComponent(GameComponentsLookup.RemoveViewBoom); }
        set {
            if (value != isRemoveViewBoom) {
                var index = GameComponentsLookup.RemoveViewBoom;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : removeViewBoomComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherRemoveViewBoom;

    public static Entitas.IMatcher<GameEntity> RemoveViewBoom {
        get {
            if (_matcherRemoveViewBoom == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RemoveViewBoom);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRemoveViewBoom = matcher;
            }

            return _matcherRemoveViewBoom;
        }
    }
}
