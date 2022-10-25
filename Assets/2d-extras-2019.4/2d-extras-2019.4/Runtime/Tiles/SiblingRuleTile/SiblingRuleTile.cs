using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace UnityEngine
{
    [CreateAssetMenu(fileName = "New Sibling Rule Tile", menuName = "Tiles/Sibling Rule Tile")]
    public class SiblingRuleTile : UnityEngine.RuleTile
    {
        public List<TileBase> siblings;

        public override bool RuleMatch(int neighbor, TileBase other)
        {
            switch(neighbor)
            {
                case UnityEngine.RuleTile.TilingRule.Neighbor.This:
                    return (siblings.Contains(other) 
                        || base.RuleMatch(neighbor, other));
                case UnityEngine.RuleTile.TilingRule.Neighbor.NotThis:
                    return (!siblings.Contains(other)
                        && base.RuleMatch(neighbor, other));
            }
            return base.RuleMatch(neighbor, other);
        }
    }
}