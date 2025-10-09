using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class Obstacle : MonoBehaviour
{
    public Health healthcomponent;
    public int scoreincreaser;

    public abstract void setDirection(Vector3 pos);

    public abstract UnityEngine.UI.Image gethealthbar();

    public abstract Vector3 returnHealthScale();

    public abstract AudioClip getAudio(string audiovariablename);
}
