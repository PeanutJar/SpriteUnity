using UnityEngine;

public abstract class ProjectileMover : MonoBehaviour //creating an abstract/parent class for projectile movers in the event that we want variation of movement techniques with same projectiles.
    //for example, same bullet object, but instead of moving in a straight line, it moves all zig zag like. (could technically achieve the same thing in the projectile pawn's scripting with if statements
    //but whatever. This helps cut down on some repetitivness like if in addition to using a linear movement to a bullet, we also needed to switch a mortar projectile's movement to also be linear.
    //AKA: cuts down on net movement related if statements/logic.
    //also I think it mkes pointin to them easier, since we can just apply a "ProjectileMover" type variable isntead of a bunch of different projectle mover types.
{
    //public abstract void Move();
}
