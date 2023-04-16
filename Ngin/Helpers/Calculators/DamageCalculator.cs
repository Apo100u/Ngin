using Ngin.Cards.Effects;
using Ngin.Characters;

namespace Ngin.Helpers.Calculators;

public class DamageCalculator
{
    private readonly Character damagedCharacter;
    private readonly Damage damage;

    public DamageCalculator(Character damagedCharacter, Damage damage)
    {
        this.damagedCharacter = damagedCharacter;
        this.damage = damage;
    }

    public int CalculateDamagePower()
    {
        int calculatedDamagePower = damage.Power > damagedCharacter.Health.Current
            ? damagedCharacter.Health.Current
            : damage.Power;

        if (calculatedDamagePower < 0)
        {
            calculatedDamagePower = 0;
        }
        
        return calculatedDamagePower;
    }
}