using UnityEngine;

public class ControllerPlayer : Controller
{
    public Pawn pawnobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pawnobject.Move(Vector3.right); //moving right along x axis
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pawnobject.Move(Vector3.left); //moving left along x axis
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pawnobject.Move(Vector3.up); //moving up along y axis
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pawnobject.Move(Vector3.down); //moving down along y axis
        }


        //pawnobject.Move(Vector3.forward); //moving forward along z axis
        //pawnobject.Move(Vector3.back); //moving back along z axis
    }
}
