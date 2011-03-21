﻿using BEPUphysics.BroadPhaseSystems;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using BEPUphysics.ResourceManagement;

namespace BEPUphysics.NarrowPhaseSystems.Factories
{
    ///<summary>
    /// Manufactures terrain-convex pairs.
    ///</summary>
    public class TerrainConvexPairFactory : NarrowPhasePairFactory
    {
        LockingResourcePool<TerrainConvexPairHandler> pool = new LockingResourcePool<TerrainConvexPairHandler>();

        ///<summary>
        /// Manufactures and returns a narrow phase pair for the given overlap.
        ///</summary>
        ///<param name="overlap">Overlap used to create a pair.</param>
        ///<returns>Narrow phase pair.</returns>
        public override INarrowPhasePair GetNarrowPhasePair(BroadPhaseOverlap overlap)
        {
            return pool.Take();
        }

        /// <summary>
        /// Returns a pair to the factory for re-use.
        /// </summary>
        /// <param name="pair">Pair to return.</param>
        public override void GiveBack(INarrowPhasePair pair)
        {
            pool.GiveBack(pair as TerrainConvexPairHandler);
        }
    }
}