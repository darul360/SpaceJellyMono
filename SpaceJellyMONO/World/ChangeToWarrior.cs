﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceJellyMONO.FSM;
using SpaceJellyMONO.FSM.States;
using SpaceJellyMONO.FSM.Trans;
using SpaceJellyMONO.Units;
using SpaceJellyMONO.UnitsFolder;

namespace SpaceJellyMONO.World
{
    public class ChangeToWarrior : GameComponent
    {

        Game1 game;
        GameObject platform;
        bool change = false;
        int i = 0;
        FinateStateMachine animateJelly;
        public ChangeToWarrior(Game1 game,GameObject platform) : base(game)
        {
            this.game = game;
            this.platform = platform;
        }
        public override void Update(GameTime gameTime)
        {

            //State JellyJumping = new Animate("1", 20, true);
            //State JellyMelting = new Animate("2", 20, true);
            //State JellyWaitng = new Animate("3", 20, true);
            //animateJelly = new FinateStateMachineBuilder()
            //    .AddState(JellyJumping)
            //    .AddState(JellyWaitng)
            //    .AddState(JellyMelting)
            //    .AddTransion(JellyWaitng, JellyJumping, new AnimationStateChanger().ChangeState)
            //    .AddTransion(JellyJumping, JellyWaitng, new AnimationStateChanger().ChangeState)
            //    .Build();

            if (platform != null)
            {
                GameObject temp = null;
                foreach (GameObject go in game.gameObjectsRepository.getRepo())
                {
                    if (go.GameTag == "worker")
                        if (platform.collider.Intersect(go.collider)) { temp = go; change = true; }
                }

                if (change)
                {
                    if (game.resourcesStatistics.yellowPowderStats >= 5)
                    {
                        // GameObject gameOBJ = ;
                        GameObject go = new Warrior("jelly_yellow", game, temp.transform.translation, 0, 0f, 0f, 0.008f, true, "warrior", .9f);
                        game.warriorsRepository.AddToRepo(go);
                        game.scene.AddSceneObject("warrior" + i, go);
                        game.scene.SceneObjects["warrior" + i].StartAnimationClip("1", 20, true);
                        string workerKey = game.scene.FindKeyOfObject(temp);     //znajdź klucz obiektu w repo rysowania
                        if (workerKey != null)                                  //overcode
                            game.scene.DeleteSceneObject(workerKey);
                        game.gameObjectsRepository.RemoveFromRepo(temp);
                        Unit selectableUnit = temp as Unit;
                        game.selectedObjectsRepository.RemoveFromRepo(selectableUnit);
                        change = false;
                        go.isObjectSelected = true;
                        i++;
                        game.resourcesStatistics.yellowPowderStats -= 5;
                    }
                }


            }


            base.Update(gameTime);


        }
    }
}
