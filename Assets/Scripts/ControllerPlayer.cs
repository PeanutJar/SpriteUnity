using UnityEngine;
using System;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using System.Collections.Generic;

public class ControllerPlayer : Controller
{
    public Pawn pawnobject;
    private Vector3 moveDirection = Vector3.up;
    public int randomteleportXmax = 9;
    public int randomteleportYmax = 4;
    [SerializeField] private Image healthbar;
    [SerializeField] private int lives;
    [SerializeField] private GameObject heartsobj;
    private List<Image> hearts;
    private Vector3 defaulthealthbarscale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaulthealthbarscale = healthbar.transform.localScale;
        hearts = new List<Image>();
        heartsobj.transform.position = new Vector3(210, 32.5f, 0); // just to make sure (even though this is already set in preset could jstu get rid of this)
        Image _heart;
        for(int i = 0; i < lives; i++)
        {
            _heart = Instantiate(Camera.main.GetComponent<GneralScript>().heartimage, new Vector3(heartsobj.transform.position.x + (30*i), heartsobj.transform.position.y, 0), Quaternion.identity, heartsobj.transform) as Image;
            hearts.Add(_heart);
        }
    }
    public void IstantiatePawnPlayerConnection()
    {
        pawnobject.GetComponentInChildren<PawnSpaceShip>().IstantiatePawnPlayerConnection(this);
    }

    // Update is called once per frame
    void Update()
    {
        if( pawnobject == null ) //doing this just so I don't have to deal with error messages for now
        {
            return;
        }

        //for example, if our object were rotated to the right (in a 2d setting), then its directional "up" within its local space would actually translate to a directional "right" within the worldspace
        //it's like trying to get "what is the equivilant of [X] direction in the real worldspace when compared to [X] object's perspective direction (its direction relative its own localspace)
        Vector3 forward = pawnobject.transform.TransformDirection(Vector3.up);   // forward vector (really it is the up vector) relative to the pawn object along the y plane (in the worldspace) -> forward/back
        forward.z = 0;                                                                  // "forward/back" (movement on the z axis) is set to 0
        forward = forward.normalized;                                                   // set forward between 0-1	

        Vector3 right = new Vector3(forward.z, 0, -forward.x);                      // right vector relative to the camera, always orthogonal to the forward vector -> right/left

        Vector3 targetDirection = right + forward;      // target direction relative to the camera
        moveDirection = Vector3.Lerp(moveDirection, targetDirection, 4f * Time.deltaTime); // smooth camera follow player direction

        if (Input.GetKey(KeyCode.W))
        {
            pawnobject.Move(moveDirection); //the above code related to "moveDirection" is a lot more complicated than the simple tranform solution down below; however, I wanted to demonstrate it because
            //make a normalized move direction like this can be useful if you would prefer to have a camera-relative movement (mooving where the camera is looking rather than where the game object is
            //looking). To be more specific, this is more useful when you have a camera that is either frequently or angularly changing positions. If you were to use it this way, you woud rather want to
            //replace the "pawnObject" with the camera object (and then properly set it up to fir the according axis views)
            //pawnobject.Move(pawnobject.transform.up); this also works
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pawnobject.Move(-pawnobject.transform.up);
        }

        if (Input.GetKey(KeyCode.D))
        {
            pawnobject.Rotate(-1.0f); //rotates left along the z rotational axis
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pawnobject.Rotate(1.0f); //rotates right along the z rotational axis
        }

        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            Vector3 pos = pawnobject.transform.position;
            pawnobject.GetComponent<CharacterController>().enabled = false;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pawnobject.transform.position = new Vector3(pos.x, pos.y + 1.5f, pos.z);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                pawnobject.transform.position = new Vector3(pos.x, pos.y - 1.5f, pos.z);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pawnobject.transform.position = new Vector3(pos.x - 1.5f, pos.y, pos.z);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pawnobject.transform.position = new Vector3(pos.x + 1.5f, pos.y, pos.z);
            }


            if (Input.GetKeyDown(KeyCode.T))
            {
                System.Random random = new System.Random();
                int r1 = random.Next(-randomteleportXmax, randomteleportXmax+1); //-9-9
                int r2 = random.Next(-randomteleportYmax, randomteleportYmax+1); //-4-4
                pawnobject.transform.position = new Vector3(r1, r2, 0);
            }
            pawnobject.GetComponent<CharacterController>().enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            pawnobject.GetComponentInChildren<PawnSpaceShip>().ToggleBoost();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            pawnobject.gameObject.GetComponent<AudioPlayer>().PlayAudio(pawnobject.gameObject.GetComponent<PawnSpaceShip>().getAudio("firingsound"), 0.7f);
            pawnobject.GetComponent<Shooter>().Shoot();
        }
    }

    public Image gethealthbar()
    {
        return (healthbar);
    }

    public Vector3 returnHealthScale()
    {
        return (defaulthealthbarscale);
    }

    public int getLives()
    {
        return (lives);
    }
    public void setLives(int _lives)
    {
        lives += _lives;
    }
    public List<Image> getHeartsList()
    {
        return (hearts);
    }
    public void setHeartsList(List<Image> list)
    {
        hearts = list;
    }
    public void ResetHeartsList()
    {
        if (hearts.Count > 0)
        {
            for(int i = 0; i < hearts.Count; i++)
            {
                Destroy(hearts[i].gameObject);
                hearts.Remove(hearts[i]);
            }
            Image _heart;
            for (int i = 0; i < lives; i++)
            {
                _heart = Instantiate(Camera.main.GetComponent<GneralScript>().heartimage, new Vector3(heartsobj.transform.position.x + (30 * i), heartsobj.transform.position.y, 0), Quaternion.identity, heartsobj.transform) as Image;
                hearts.Add(_heart);
            }
        }
    }
}
