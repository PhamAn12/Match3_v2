using UnityEngine;

namespace DefaultNamespace
{
    // get the new position for old blocks after move down
    public class CheckEmptyPosition
    {
        public static float GetNextEmptyRow(GameContext context, Vector2 position)
        {
            position.y -= 1.5f;

            while (position.y >= 0 && context.GetEntitiesWithPosition(position).Count == 0)
            {
                position.y -= 1.5f;
            }

            return position.y + 1.5f;
        }
    }
}