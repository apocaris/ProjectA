using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;


public class ChaTest_MotionEffect : MonoBehaviour
{
    [SpineEvent] public string vfx_atk1 = "atk1";
    [SpineEvent] public string vfx_atk2 = "atk2";
    [SpineEvent] public string vfx_skill1 = "skill1";
    [SpineEvent] public string vfx_skill1_1 = "skill1-1";
    [SpineEvent] public string vfx_run = "run";

    public ParticleSystem particle_atk1;
    public ParticleSystem particle_atk2;
    public ParticleSystem particle_skill1;
    public ParticleSystem particle_skill1_1;
    public ParticleSystem particle_run;


    private SkeletonAnimation skeletonAnimation;
    public string idle;
    public string atk1;
    public string atk2;
    public string skill1;
    public string skill1_1;
    public string run;

    private Spine.Animation anim;
    private AnimationClip clip;

    public bool playAura = true; //파티클 제어 bool






    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        playAura = true;

        skeletonAnimation = GetComponent<SkeletonAnimation>();


        if (skeletonAnimation != null)
            skeletonAnimation.state.Event += HandleEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("idle");


            skeletonAnimation.state.AddAnimation(1, idle, true, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("atk1");


            skeletonAnimation.state.AddAnimation(1, atk1, true, 0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("atk2");


            skeletonAnimation.state.AddAnimation(1, atk2, true, 0);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("skill_1");


            skeletonAnimation.state.AddAnimation(1, skill1, true, 0);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("run");


            skeletonAnimation.state.AddAnimation(1, run, true, 0);
        }
    }

    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        
        if (e.Data.Name == atk1)
        {
            Debug.Log("atk1");

            particle_atk1.Play();
        }

        if (e.Data.Name == atk2)
        {
            Debug.Log("atk2");

            particle_atk2.Play();
        }


      if (e.Data.Name == vfx_skill1)
        {
            Debug.Log("atk3");
            particle_skill1.Play();
        }

        if (e.Data.Name == vfx_skill1_1)
            {
           
            Debug.Log("atk3");
                particle_skill1_1.Play();
            }

        if (e.Data.Name == run)
            {
                Debug.Log("run");
                particle_run.Play();
            }



        }

    

    public void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        Debug.Log(trackIndex + "" + state.GetCurrent(trackIndex) + ":event" + e + e.Int);
        if (e.Data.Name == atk1)
        {
            Debug.Log("atk1");

            particle_atk1.Play();
        }

        if (e.Data.Name == atk2)
        {
            Debug.Log("atk2");

            particle_atk2.Play();
        }


        if (e.Data.Name == skill1)
        {
            Debug.Log("atk3");
            particle_skill1.Play();
        }

            if (e.Data.Name == skill1_1)
            {
                Debug.Log("atk3");
                particle_skill1_1.Play();
            }

            if (e.Data.Name == run)
            {
                Debug.Log("run");
                particle_run.Play();
            }



        

    }



}
