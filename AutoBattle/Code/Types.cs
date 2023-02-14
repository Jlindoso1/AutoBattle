using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Constants
    {
        public const float Health = 100;
        public const float BaseDamage = 20;
        public const float DamageMult = 1;
        public const float WarriorDamageMult = 2;
        public const int Range = 1;
        public const int ArcherRange = 4;
        public const float KnockBackTilesAmount = 1;
        public const double AbilityChance = 0.5;
    }
    public struct GridBox
    {
        public int xIndex;
        public int yIndex;
        public bool ocupied;
        public int Index;

        public GridBox(int x, int y, bool ocupied, int index)
        {
            xIndex = x;
            yIndex = y;
            this.ocupied = ocupied;
            this.Index = index;
        }

    }

    public enum CharacterClass : uint
    {
        Paladin = 1,
        Warrior = 2,
        Cleric = 3,
        Archer = 4
    }
}
