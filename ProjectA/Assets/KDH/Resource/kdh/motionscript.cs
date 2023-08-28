using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class motionscript : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    // Start is called before the first frame update
   
 
    public string idle;
    public string atk2;
    public string atk3;
    public string atk1;
    public string run;



    void Start()
    {
 


        skeletonAnimation = GetComponent<SkeletonAnimation>();
        
     
    }

// Update is called once per frame
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {


            skeletonAnimation.state.AddAnimation(1, idle, true, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            skeletonAnimation.state.AddAnimation(1, run, true, 0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            skeletonAnimation.state.AddAnimation(1, atk1, true, 0);
            
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            skeletonAnimation.state.AddAnimation(1, atk2, true, 0);

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            skeletonAnimation.state.AddAnimation(1, atk3, true, 0);
        
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            

            skeletonAnimation.state.AddAnimation(1, "intro", true, 0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {


            skeletonAnimation.state.AddAnimation(1, "1", true, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {


            skeletonAnimation.state.AddAnimation(1, "2", true, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {


            skeletonAnimation.state.AddAnimation(1, "2", true, 0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {


            skeletonAnimation.state.AddAnimation(1, "3", true, 0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {


            skeletonAnimation.state.AddAnimation(1, "4", true, 0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {


            skeletonAnimation.state.AddAnimation(1, "5", true, 0);
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {


            skeletonAnimation.state.AddAnimation(1, "6", true, 0);


        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {


            skeletonAnimation.state.AddAnimation(1, "7", true, 0);
        }
    }
}
