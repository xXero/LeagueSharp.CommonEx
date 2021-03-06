﻿using System;
using System.Reflection;

namespace LeagueSharp.CommonEx.Core.Events
{
    /// <summary>
    ///     Contains events involving turrets.
    /// </summary>
    public class Turret
    {
        /// <summary>
        ///     OnTurretShot Delegate.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">OnTurretShot Arguments Container</param>
        public delegate void OnTurretShotDelegate(object sender, TurretShotArgs e);

        /// <summary>
        ///     Static Constructor
        /// </summary>
        static Turret()
        {
            Obj_AI_Base.OnProcessSpellCast += ObjAiBaseOnOnProcessSpellCast;
        }

        /// <summary>
        ///     This event gets called when any unit gets shot by a tower.
        /// </summary>
        public static event OnTurretShotDelegate OnTurretShot;

        #region OnTurretShot

        private static void ObjAiBaseOnOnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var turret = sender as Obj_AI_Turret;

            if (turret != null)
            {
                FireOnTurretShot(args.Target, turret);
            }
        }

        private static void FireOnTurretShot(GameObject unit, Obj_AI_Turret turret)
        {
            if (OnTurretShot != null)
            {
                OnTurretShot(MethodBase.GetCurrentMethod().DeclaringType, new TurretShotArgs(unit, turret));
            }
        }

        #endregion
    }

    /// <summary>
    ///     Contains the event arguements for the OnTurretShot event.
    /// </summary>
    public class TurretShotArgs : EventArgs
    {
        /// <summary>
        ///     The turret that has attacked the unit.
        /// </summary>
        public Obj_AI_Turret Turret;

        /// <summary>
        ///     The unit that has been shot by a turret..
        /// </summary>
        public GameObject Unit;

        internal TurretShotArgs(GameObject unit, Obj_AI_Turret turret)
        {
            Unit = unit;
            Turret = turret;
        }
    }
}