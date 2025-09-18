using UnityEngine;

//this class is meant handle switching which pawn the player will control
//this will include generic pawn controls (every pawn should be able do [this])

public abstract class Pawn : MonoBehaviour
{
    public abstract void Move(Vector3 movevector);
    public abstract void Rotate(float angle);
}
