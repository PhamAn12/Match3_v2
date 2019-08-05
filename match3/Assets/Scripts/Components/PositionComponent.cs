using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;
[Game]
public class PositionComponent : IComponent
{
    [EntityIndex]
    public Vector2 value;
}