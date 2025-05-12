using UnityEngine;

public class AnimIntro : MonoBehaviour
{
    public Animator animatorMan;

    public void StopWalk()
    {
        animatorMan.SetBool("Move",true);
    }

    public void Walk()
    {
        animatorMan.SetBool("Move", false);
    }


}
