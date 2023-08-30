using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }


    void OnEvent (Spine.AnimationState state,int trackIndex,Spine.Event e)
    {
        if (e.Data.Name == "atk1") 
        {
            Debug.Log("atk1 이벤트 호출을 성공!"); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
