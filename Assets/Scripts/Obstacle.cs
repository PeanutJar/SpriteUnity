using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class Obstacle : MonoBehaviour
{
    public bool isinstantdeath = false;
    public Health healthcomponent;
    public int scoreincreaser;
    public float speed;
    public bool isfinalenemy;

    public abstract void setDirection(Vector3 pos);

    public abstract bool GetInstDeath();

    public abstract UnityEngine.UI.Image gethealthbar();

    public abstract Vector3 returnHealthScale();

    public abstract AudioClip getAudio(string audiovariablename);
}
