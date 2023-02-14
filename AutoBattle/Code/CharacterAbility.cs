using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBattle
{
    public class CharacterAbility
    {
        private float abilityMultplier;
        private int abilityRange;
        private float abilityPower;
        private bool knocksBack;
        private CharacterClass characterClass;
        private Character owner;

        public CharacterAbility(CharacterClass characterClass, Character owner)
        {
            this.characterClass = characterClass;
            this.owner = owner;
            switch(characterClass)
            {
                case CharacterClass.Archer:
                    abilityMultplier = Constants.DamageMult;
                    abilityRange = Constants.ArcherRange;
                    abilityPower = Constants.BaseDamage;
                    knocksBack = false;
                    break;
                case CharacterClass.Cleric:
                    abilityMultplier = 0;
                    abilityRange = 0;
                    abilityPower = 0;
                    knocksBack = false;
                    break;
                case CharacterClass.Paladin:
                    abilityMultplier = Constants.DamageMult;
                    abilityRange = Constants.Range;
                    abilityPower = Constants.BaseDamage;
                    knocksBack = true;
                    break;
                case CharacterClass.Warrior:
                    abilityMultplier = Constants.WarriorDamageMult;
                    abilityRange = Constants.Range;
                    abilityPower = Constants.BaseDamage;
                    knocksBack = false;
                    break;
            }
        }

        private bool CheckTargetsOnRange(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x =>
            (x.xIndex >= owner.currentBox.xIndex - abilityRange) &&
            (x.xIndex <= owner.currentBox.xIndex - 1) &&
            (x.yIndex == owner.currentBox.yIndex) && x.ocupied
            ).ocupied);

            bool right = (battlefield.grids.Find(x =>
            (x.xIndex <= owner.currentBox.xIndex + abilityRange) &&
            (x.xIndex >= owner.currentBox.xIndex + 1) &&
            (x.yIndex == owner.currentBox.yIndex) && x.ocupied
            ).ocupied);

            bool up = (battlefield.grids.Find(x =>
            (x.xIndex == owner.currentBox.xIndex) &&
            (x.yIndex <= owner.currentBox.yIndex + abilityRange) &&
            (x.yIndex >= owner.currentBox.yIndex + 1 && x.ocupied)
            ).ocupied);

            bool down = (battlefield.grids.Find(x =>
            (x.xIndex == owner.currentBox.xIndex) &&
            (x.yIndex >= owner.currentBox.yIndex - abilityRange) &&
            (x.yIndex <= owner.currentBox.yIndex - 1 && x.ocupied)
            ).ocupied);

            if (left || right || up || down)
            {
                return true;
            }
            return false;
        }

        private void PerformKnockBack(Grid battlefield)
        {
            GridBox targetBox = owner.Target.currentBox;
            GridBox ownerBox = owner.currentBox;
            bool isLeft = targetBox.xIndex < ownerBox.xIndex;
            bool isRight = targetBox.xIndex > ownerBox.xIndex;
            bool isUp = targetBox.yIndex < ownerBox.yIndex;
            bool isDown = targetBox.yIndex > ownerBox.yIndex;
            int xIndex = targetBox.xIndex;
            int yIndex = targetBox.yIndex;
            if (isLeft)
                xIndex = Math.Max(0, targetBox.xIndex - 1);
            if (isRight)
                xIndex = Math.Min(battlefield.xLength - 1, targetBox.xIndex + 1);
            if(isUp)
                yIndex = Math.Max(0, targetBox.yIndex - 1);
            if(isDown)
                yIndex = Math.Min(battlefield.yLength - 1, targetBox.yIndex + 1);

            owner.Target.currentBox.ocupied = false;
            battlefield.grids[owner.Target.currentBox.Index] = owner.Target.currentBox;
            owner.Target.currentBox = (battlefield.grids.Find(x => x.xIndex == xIndex && x.yIndex == yIndex));
            owner.Target.currentBox.ocupied = true;
            battlefield.grids[owner.Target.currentBox.Index] = owner.Target.currentBox;
            Console.WriteLine($"Player {owner.Target.PlayerIndex} was knocked back\n");
            battlefield.drawBattlefield();
        }

        private void Attack(Grid battlefield)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            int damage = (rand.Next(0, (int)(abilityPower*abilityMultplier)));
            owner.Target.TakeDamage(damage);
            Console.WriteLine($"Player {owner.PlayerIndex} used ability against player {owner.Target.PlayerIndex} and did {damage} damage\n");
            Console.WriteLine($"Player {owner.Target.PlayerIndex} has {owner.Target.Health} of health\n");
            if(knocksBack)
            {
                PerformKnockBack(battlefield);
            }
        }

        private void Teleport()
        {
            Console.WriteLine("Teleport Test");
        }

        public void AbilityTurn(Grid battlefield)
        {
            switch(characterClass)
            {
                case CharacterClass.Warrior:
                case CharacterClass.Paladin:
                    if (owner.CheckCloseTargets(battlefield))
                        Attack(battlefield);
                    break;
                case CharacterClass.Archer:
                    if (CheckTargetsOnRange(battlefield))
                    {
                        Attack(battlefield);
                    }
                    break;
                case CharacterClass.Cleric:
                    Teleport();
                    break;
            }
        }

    }
}
