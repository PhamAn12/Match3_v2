//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RemoveViewRocketComponent removeViewRocket { get { return (RemoveViewRocketComponent)GetComponent(GameComponentsLookup.RemoveViewRocket); } }
    public bool hasRemoveViewRocket { get { return HasComponent(GameComponentsLookup.RemoveViewRocket); } }

    public void AddRemoveViewRocket(string newTypeRocket) {
        var index = GameComponentsLookup.RemoveViewRocket;
        var component = (RemoveViewRocketComponent)CreateComponent(index, typeof(RemoveViewRocketComponent));
        component.typeRocket = newTypeRocket;
        AddComponent(index, component);
    }

    public void ReplaceRemoveViewRocket(string newTypeRocket) {
        var index = GameComponentsLookup.RemoveViewRocket;
        var component = (RemoveViewRocketComponent)CreateComponent(index, typeof(RemoveViewRocketComponent));
        component.typeRocket = newTypeRocket;
        ReplaceComponent(index, component);
    }

    public void RemoveRemoveViewRocket() {
        RemoveComponent(GameComponentsLookup.RemoveViewRocket);
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

    static Entitas.IMatcher<GameEntity> _matcherRemoveViewRocket;

    public static Entitas.IMatcher<GameEntity> RemoveViewRocket {
        get {
            if (_matcherRemoveViewRocket == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RemoveViewRocket);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRemoveViewRocket = matcher;
            }

            return _matcherRemoveViewRocket;
        }
    }
}
