﻿using System;
using BEPUphysics.Collidables;
using BEPUphysics.Entities.Prefabs;
using BEPUphysics.MathExtensions;
using Microsoft.Xna.Framework;
using BEPUphysics.Settings;
using BEPUphysics.CollisionTests.CollisionAlgorithms;
using BEPUphysics.CollisionRuleManagement;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using BEPUphysics.CollisionTests.Manifolds;
using System.Diagnostics;
using BEPUphysics.BroadPhaseSystems.SortAndSweep;
using BEPUphysics.CollisionShapes;
using BEPUphysics.CollisionShapes.ConvexShapes;

namespace BEPUphysicsDemos.Demos.Extras.Tests
{
    /// <summary>
    /// Test class for performance analysis.
    /// </summary>
    public class TerrainTestDemo : StandardDemo
    {
        /// <summary>
        /// Constructs a new demo.
        /// </summary>
        /// <param name="game">Game owning this demo.</param>
        public TerrainTestDemo(DemosGame game)
            : base(game)
        {
            //x and y, in terms of heightmaps, refer to their local x and y coordinates.  In world space, they correspond to x and z.
            //Setup the heights of the terrain.
            int xLength = 256;
            int zLength = 256;

            float xSpacing = 8f;
            float zSpacing = 8f;
            var heights = new float[xLength, zLength];
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < zLength; j++)
                {
                    float x = i - xLength / 2;
                    float z = j - zLength / 2;
                    heights[i, j] = (float)(10 * (Math.Sin(x / 8) + Math.Sin(z / 8)));
                }
            }
            //Create the terrain.
            var terrain = new Terrain(heights, new AffineTransform(
                    new Vector3(xSpacing, 1, zSpacing),
                    Quaternion.Identity,
                    new Vector3(-xLength * xSpacing / 2, 0, -zLength * zSpacing / 2)));

            Space.Add(terrain);


            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    //CompoundBody cb = new CompoundBody(new CompoundShapeEntry[] 
                    //    {
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(0, 0,0)),
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(2, 0,0)),
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(4, 0,0)),
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(6, 0,0)),
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(8, 0,0)),
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(10, 0,0)),
                    //        new CompoundShapeEntry(new CapsuleShape(2, 1), new Vector3(12, 0,0)),
                    //    }, 10);
                    //cb.Position = new Vector3(i * 15, 20, j * 4);

                    //CompoundBody cb = new CompoundBody(new CompoundShapeEntry[] 
                    //{
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(0, 0,0)),
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(2, -2,0)),
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(4, 0,0)),
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(6, -2,0)),
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(8, 0,0)),
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(10, -2,0)),
                    //    new CompoundShapeEntry(new SphereShape(1), new Vector3(12, 0,0)),
                    //}, 10);
                    //cb.Position = new Vector3(i * 15, 20, j * 4);

                    CompoundBody cb = new CompoundBody(new CompoundShapeEntry[] 
                    {
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(0, 0,0)),
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(2, -2,0)),
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(4, 0,0)),
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(6, -2,0)),
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(8, 0,0)),
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(10, -2,0)),
                        new CompoundShapeEntry(new CapsuleShape(0, 1), new Vector3(12, 0,0)),
                    }, 10);
                    cb.Position = new Vector3(i * 15, 20, j * 4);

                    cb.ActivityInformation.IsAlwaysActive = true;

                    Space.Add(cb);
                }
            }


            game.ModelDrawer.Add(terrain);

            game.Camera.Position = new Vector3(0, 30, 20);

        }





        /// <summary>
        /// Gets the name of the simulation.
        /// </summary>
        public override string Name
        {
            get { return "Terrain"; }
        }
    }
}