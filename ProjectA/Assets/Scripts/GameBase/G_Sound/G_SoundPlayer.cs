using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_SoundPlayer : MonoBehaviour
{
    [Serializable]
    public enum Trigger
    {
        OnStart,
        OnPress,
        OnRelease,
        OnClick,
        OnEnable,
        OnDisable,
        OnPlay,
    }

    [Serializable]
    public class G_SoundPlayerInfo
    {
        public Trigger trigger;
        public G_SoundAsset asset;
        public Transform talker;
        public bool chaseTalker;
    }

    public List<G_SoundPlayerInfo> triggerInfo;

    void OnEnable()
    {
        if (null == triggerInfo)
            triggerInfo = new List<G_SoundPlayerInfo>();

        for (int i = 0; i != triggerInfo.Count; ++i)
        {
            if (null == triggerInfo[i])
            {
                triggerInfo.RemoveAt(i--);
                continue;
            }
            if (null == triggerInfo[i].asset)
                Debug.LogWarning(gameObject.name);
        }

        TriggerEvent(Trigger.OnEnable);
    }

    void OnDisable()
    {
        TriggerEvent(Trigger.OnDisable);
    }

    private void Start()
    {
        TriggerEvent(Trigger.OnStart);
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
            TriggerEvent(Trigger.OnPress);
        else
            TriggerEvent(Trigger.OnRelease);
    }

    void OnClick()
    {
        TriggerEvent(Trigger.OnClick);
    }

    public void Play()
    {
        TriggerEvent(Trigger.OnPlay);
    }

    private void TriggerEvent(Trigger _trigger)
    {
        foreach (var _info in triggerInfo.FindAll(_info => { return _info.trigger == _trigger; }))
        {
            G_SoundManager.a_instance.Play(_info.asset, _info.talker, _info.chaseTalker);
        }
    }
}
