namespace Skull.Scenes.Entities.Skills;

public class MattSkill :  PlayerSkill
{
    public MattSkill(int damageAmount, int healAmount, int[] rng, int range, int cooldown) : base(damageAmount, healAmount, rng, range, cooldown)
    {
    }
}