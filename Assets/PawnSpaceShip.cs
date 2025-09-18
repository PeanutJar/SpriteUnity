using UnityEngine;

public class PawnSpaceShip : Pawn
{
    CharacterController controller;
    BoxCollider collider;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = this.gameObject.AddComponent<CharacterController>();
        collider = this.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    public override void Move(Vector3 movevector)
    {
        //adding Time.deltaTime so it updates in accordance to seconds rather than per frame
        controller.Move((movevector * speed) * Time.deltaTime);  //move the controller	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
