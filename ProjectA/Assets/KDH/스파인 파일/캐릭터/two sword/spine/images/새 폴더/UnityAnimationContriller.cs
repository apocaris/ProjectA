using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;



public class UnityAnimationContriller : MonoBehaviour
{
    private SkeletonAnimation SkeletonAnimation;
    private Spine.Animation anim;
    private AnimationClip clip;

    // Start is called before the first frame update
    void Start()
    {
        SkeletonAnimation = GetComponent<SkeletonAnimation>();
    
        anim= this.GetComponent<Spine.Animation>();

     

    }

    public void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        Debug.Log(trackIndex + "" + state.GetCurrent(trackIndex) + ":evemt" + e + e.Int);
        
    
    
    
    }

    // Update is called once per frame
    void Update()
    {

    }
}
