using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public ProjectileMover movercomponent;
    public Health healthcomponent;
    public abstract float GetSpeed();
}
