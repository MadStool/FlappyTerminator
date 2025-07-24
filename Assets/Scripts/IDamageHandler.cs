using UnityEngine;

public interface IDamageHandler
{
    void HandleDamage(int damage, Transform damageSource);
    void HandleDeath();
}