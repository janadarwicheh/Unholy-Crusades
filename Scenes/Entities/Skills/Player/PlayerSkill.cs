namespace Skull.Scenes.Entities.Skills;

public class PlayerSkill: Skill
{
    public PlayerSkill(int damageAmount, int healAmount, int[] rng, int range, int cooldown) : base(damageAmount, healAmount, rng, range, cooldown)
    {
    }
}