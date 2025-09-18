using UnityEngine;

public class PawnSpaceShip : Pawn
{
    CharacterController controller;
    BoxCollider collider;
    public float speed = 8f;
    private float speedreg;
    public float speedmultiplier = 1.5f;
    public float rotatespeed = 2f;
    private bool isboost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = this.gameObject.AddComponent<CharacterController>();
        collider = this.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        isboost = false;
        speedreg = speed;
    }

    public override void Move(Vector3 movevector)
    {
        if(isboost)
        {
            speed = speedreg * speedmultiplier;
        }
        else
        {
            speed = speedreg;
        }

        //adding Time.deltaTime so it updates in accordance to seconds rather than per frame
        controller.Move((movevector * speed) * Time.deltaTime);  //move the controller	
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0,0,angle * rotatespeed));
    }

    public void ToggleBoost()
    {
        isboost = !isboost;
        print(isboost ? "Boost Activated" : "Boost Deactivated");
    }
}
