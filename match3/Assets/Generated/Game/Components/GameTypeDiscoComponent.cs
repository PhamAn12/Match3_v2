//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TypeDiscoComponent typeDisco { get { return (TypeDiscoComponent)GetComponent(GameComponentsLookup.TypeDisco); } }
    public bool hasTypeDisco { get { return HasComponent(GameComponentsLookup.TypeDisco); } }

    public void AddTypeDisco(string newTypeDisco) {
        var index = GameComponentsLookup.TypeDisco;
        var component = (TypeDiscoComponent)CreateComponent(index, typeof(TypeDiscoComponent));
        component.typeDisco = newTypeDisco;
        AddComponent(index, component);
    }

    public void ReplaceTypeDisco(string newTypeDisco) {
        var index = GameComponentsLookup.TypeDisco;
        var component = (TypeDiscoComponent)CreateComponent(index, typeof(TypeDiscoComponent));
        component.typeDisco = newTypeDisco;
        ReplaceComponent(index, component);
    }

    public void RemoveTypeDisco() {
        RemoveComponent(GameComponentsLookup.TypeDisco);
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

    static Entitas.IMatcher<GameEntity> _matcherTypeDisco;

    public static Entitas.IMatcher<GameEntity> TypeDisco {
        get {
            if (_matcherTypeDisco == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TypeDisco);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTypeDisco = matcher;
            }

            return _matcherTypeDisco;
        }
    }
}
