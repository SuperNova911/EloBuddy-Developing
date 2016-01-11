﻿using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using System.Collections.Generic;

namespace DatDarius
{
    public class Debug
    {
        private static AIHeroClient Player = ObjectManager.Player;
        
        static Debug()
        {
            

            
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            foreach (var hero in EntityManager.Heroes.AllHeroes.Where(h => h.VisibleOnScreen && h.IsValidTarget()))
            {
                #region ePosPred
                if (Config.DebugMenu["ePosPred"].Cast<CheckBox>().CurrentValue && hero.IsEnemy)
                {
                    Drawing.DrawLine(Drawing.WorldToScreen(hero.Position), Drawing.WorldToScreen(Darius.PositionPrediction(hero, 0.25f)), 2, Color.Red);
                }
                #endregion

                #region HUD
                if (Config.DebugMenu["hud"].Cast<CheckBox>().CurrentValue)
                {
                    var i = 0;
                    const int step = 20;

                    #region General
                    if (Config.DebugMenu["hudGeneral"].Cast<CheckBox>().CurrentValue)
                    {
                        var data = new Dictionary<string, object>
                            {
                                { "BaseSkinName", hero.BaseSkinName },
                                { "IsValied", hero.IsValid },
                                { "IsValidTarget", hero.IsValidTarget() },
                                { "AttackCastDelay", hero.AttackCastDelay },
                                { "AttackDelay", hero.AttackDelay },
                                { "Attacking", Darius.Attacking }
                            };

                        Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.Orange, "General properties", 10);
                        i += step;
                        foreach (var dataEntry in data)
                        {
                            Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.NavajoWhite, string.Format("{0}: {1}", dataEntry.Key, dataEntry.Value), 10);
                            i += step;
                        }
                    }
                    #endregion

                    #region Health
                    if (Config.DebugMenu["hudHealth"].Cast<CheckBox>().CurrentValue)
                    {
                        var data = new Dictionary<string, object>
                            {
                                { "Health", hero.Health },
                                { "HealthPercent", hero.HealthPercent },
                                { "HPRegenRate", hero.HPRegenRate },
                                { "AllShield", hero.AllShield },
                                { "AttackShield", hero.AttackShield },
                                { "MagicShield", hero.MagicShield }
                            };

                        Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.LimeGreen, "Health properties", 10);
                        i += step;
                        foreach (var dataEntry in data)
                        {
                            Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.NavajoWhite, string.Format("{0}: {1}", dataEntry.Key, dataEntry.Value), 10);
                            i += step;
                        }
                    }
                    #endregion

                    #region Prediction
                    if (Config.DebugMenu["hudPrediction"].Cast<CheckBox>().CurrentValue)
                    {
                        var data = new Dictionary<string, object>
                            {
                                { "MoveSpeed", hero.MoveSpeed },
                                { "BoundingRadius", hero.BoundingRadius },
                                { "Distance", hero.ServerPosition.Distance(Player.ServerPosition) },
                                { "IsMoving", hero.IsMoving },
                            };

                        Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.Blue, "Prediction properties", 10);
                        i += step;
                        foreach (var dataEntry in data)
                        {
                            Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.NavajoWhite, string.Format("{0}: {1}", dataEntry.Key, dataEntry.Value), 10);
                            i += step;
                        }
                    }
                    #endregion

                    #region Damage
                    if (Config.DebugMenu["hudDamage"].Cast<CheckBox>().CurrentValue)
                    {
                        var data = new Dictionary<string, object>
                            {
                                { "UltDamage",DamageHandler.RDamage(hero) },
                                { "PassiveDamage", DamageHandler.PassiveDamage(hero) },
                                { "IgniteDamage", DamageHandler.IgniteDamage(true) }
                            };

                        Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.Red, "Damage properties", 10);
                        i += step;
                        foreach (var dataEntry in data)
                        {
                            Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.NavajoWhite, string.Format("{0}: {1}", dataEntry.Key, dataEntry.Value), 10);
                            i += step;
                        }
                    }
                    #endregion


                    /*
                    #region
                    if (Config.DebugMenu["hudPrediction"].Cast<CheckBox>().CurrentValue)
                    {
                        var data = new Dictionary<string, object>
                            {
                                { "", hero. },
                                { "", hero. },
                                { "", hero. },
                                { "", hero. },
                                { "", hero. },
                                { "", hero. }
                            };

                        Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.Orange, "Prediction properties", 10);
                        i += step;
                        foreach (var dataEntry in data)
                        {
                            Drawing.DrawText(hero.Position.WorldToScreen() + new Vector2(0, i), Color.NavajoWhite, string.Format("{0}: {1}", dataEntry.Key, dataEntry.Value), 10);
                            i += step;
                        }
                    }
                    #endregion
                    */
                }
                #endregion

                /*
                #region
                if (Config.DebugMenu[""].Cast<CheckBox>().CurrentValue)
                {

                }
                #endregion
    */
            }
        }

        public static new void Initialize()
        {

        }
    }
}
