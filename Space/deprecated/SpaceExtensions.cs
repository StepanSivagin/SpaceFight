﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SF.Space
{
    public static class SpaceExtensions
    {
        /// <summary>
        /// Opened board part.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>absolute cosie of roll angle</returns>
        public static double Board(this IShip ship)
        {
            return Math.Abs(Math.Cos(ship.Roll));
        }

        public static double MissileRange(this IShip ship)
        {
            if (string.IsNullOrEmpty(ship.MissileName))
                return 0;
            if (ship.Missile == null)
                return Catalog.Instance.MaximumMissileRange;
            var t = ship.Missile.FlyTime;
            return ship.Missile.Acceleration * t * t / 2;
        }

        public static T ById<T>(this IEnumerable<T> particles, int id) where T : IParticle
        {
            if (id == 0)
                return default(T);
            return particles.FirstOrDefault(i => i.Id == id);
        }

        public static bool IsLight(this IShip ship)
        {
            return ship.Class.Superclass == ShipSuperclass.LAC;
        }

        public static bool InSpace(this IHelm ship)
        {
            return ship.State != ShipState.Annihilated && ship.State != ShipState.Hyperspace && string.IsNullOrEmpty(ship.Carrier);
        }

        public static bool IsDead(this IShip ship)
        {
            return ship.State == ShipState.Annihilated;
        }

        public static bool IsLeft(this IShip helm, IParticle target)
        {
            return Math.Sin((target.Position - helm.Position).Argument - helm.Heading) < 0;
        }

        public static bool IsLeftBoard(this IShip helm, IParticle target)
        {
            var left = helm.IsLeft(target);
            if (Math.Cos(helm.Roll) < 0)
                left = !left;
            return left;
        }

    }
}