using Ngin.Cards.Effects;
using Ngin.Characters;

namespace Ngin.Helpers.Calculators;

public class HealCalculator
{
    private readonly Character healedCharacter;
    private readonly Heal heal;

    public HealCalculator(Character healedCharacter, Heal heal)
    {
        this.healedCharacter = healedCharacter;
        this.heal = heal;
    }

    public int CalculateHeal()
    {
        int missingHealth = healedCharacter.Health.Base - healedCharacter.Health.Current;
        
        int calculatedHealPower = heal.Power > missingHealth
            ? missingHealth
            : heal.Power;

        if (calculatedHealPower < 0)
        {
            calculatedHealPower = 0;
        }

        return calculatedHealPower;
    }
}