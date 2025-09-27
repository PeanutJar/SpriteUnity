using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public ProjectileMover movercomponent;
    public Health healthcomponent;
    public int lifetimeduration;
    public abstract float GetSpeed();
}
