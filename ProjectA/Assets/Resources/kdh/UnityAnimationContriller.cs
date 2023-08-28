using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;



public class UnityAnimationContriller : MonoBehaviour
{
    [SpineEvent] public string atk1 = "atk1";
    [SpineEvent] public string atk2 = "atk2";
    [SpineEvent] public string atk3 = "atk3";
    [SpineEvent] public string run = "run";
    private SkeletonAnimation SkeletonAnimation;
    private Spine.Animation anim;
    private AnimationClip clip;
    public ParticleSystem vfx_atk1;
    public ParticleSystem vfx_atk2;
    public ParticleSystem vfx_atk3;
    public ParticleSystem vfx_run;

    public bool playAura = true; //파티클 제어 bool



    // Start is called before the first frame update
    void Start()
    {
        playAura = true;
        SkeletonAnimation = GetComponent<SkeletonAnimation>();
        if (SkeletonAnimation != null)
            SkeletonAnimation.state.Event += HandleEvent;
    }

    public void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        Debug.Log(trackIndex + "" + state.GetCurrent(trackIndex) + ":event" + e + e.Int);
        if (e.Data.Name == atk1)
        {
            Debug.Log("atk1");

            vfx_atk1.Play();
        }

        if (e.Data.Name == atk2)
        {
            Debug.Log("atk2");
            vfx_atk2.Play();
        }

        if (e.Data.Name == atk3)
        {
            Debug.Log("atk3");
            vfx_atk3.Play();
        }

        if (e.Data.Name == run)
        {   
            Debug.Log("run");
            vfx_run.Play();
        }



    }
    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        // Play some sound if the event named "footstep" fired.
        if (e.Data.Name == atk1)
        {
            vfx_atk1.Play();
            Debug.Log("atk1");
        }

        if (e.Data.Name == atk2)
        {
            vfx_atk2.Play();
            Debug.Log("atk2");
        }

        if (e.Data.Name == atk3)
        {
            vfx_atk3.Play();
            Debug.Log("atk3");
            Debug.Log("atk3");
        }

        if (e.Data.Name == run)
        {
            vfx_run.Play();
            Debug.Log("run");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
