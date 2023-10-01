﻿using Savidiy.Utils;

namespace LudumDare54
{
    public enum ShipType
    {
        Hero = 0,
        BigAsteroid1 = 10,
        MiddleAsteroid1 = 11,
        MiddleAsteroid2 = 12,
        SmallAsteroid1 = 15,
        SmallAsteroid2 = 16,
        SmallAsteroid3 = 17,
        SmallAsteroid4 = 18,
        Turret = 30,
    }
    
    public static class ShipTypeExtension
    {
        private static readonly EnumToStringCache<ShipType> TypesStrings = new ();
        public static string ToStringCached(this ShipType type) => TypesStrings.ToStringCached(type);
    }
}