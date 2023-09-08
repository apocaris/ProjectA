using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_SoundSpeaker : MonoBehaviour
{
    public AudioSource speaker = null;
    public Transform talker = null;
    public bool chase = false;

    public string lastAssetId = null;

    private bool instanceSpeaker = false;
    private IEnumerator playCoroutine = null;
    private IEnumerator volumeCoroutine = null;

    private bool paused = false;

    public bool IsPlaying
    {
        get
        {
            return null != speaker && speaker.isPlaying;
        }
    }

    public void AddSoundClip(G_SoundAsset _asset, G_SoundClip _sound, float _masterVolume, bool _playSmoothly = false)
    {
        instanceSpeaker = !_asset.bgm;
        lastAssetId = _asset.id;
        paused = false;

        if (null == speaker)
        {
            speaker = gameObject.GetComponent<AudioSource>();
            if (null == speaker)
                speaker = gameObject.AddComponent<AudioSource>();
        }
        speaker.clip = _sound.audioClip;
        speaker.pitch = _sound.pitch;
        speaker.volume = _sound.volume * _masterVolume;
        speaker.loop = _asset.bgm || (_sound.loop && 0 == _sound.delay && 0 == _sound.front && _sound.duration == _sound.audioClip.length);
        speaker.Stop();

        if (!_asset.enabled3DSound)
            speaker.spatialBlend = 0f;
        else
        {
            speaker.spatialBlend = _asset.spatialBlend;
            speaker.maxDistance = _asset.distance;
        }

        if (speaker.loop)
        {
            UserStopCoroutine(ref playCoroutine);
            speaker.Play();
            speaker.time = _sound.front;
        }
        else
        {
            UserStartCoroutine(ref playCoroutine, IPlaySound(_sound.delay, _sound.front, _sound.duration, _sound.loop || _asset.bgm));
        }

        UserStopCoroutine(ref volumeCoroutine);
        if (_playSmoothly)
            UserStartCoroutine(ref volumeCoroutine, ILerpVolume(1f));
    }

    public void SetTalker(Transform _talker, bool _chase)
    {
        talker = _talker;
        chase = _chase;

        transform.position = talker.position;

        enabled = null != _talker;
    }

    public void Stop()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            DestroyImmediate(gameObject);
            return;
        }
#endif

        if (null != playCoroutine)
        {
            G_SoundManager.a_instance.StopCoroutine(playCoroutine);
            playCoroutine = null;
        }
        lastAssetId = null;
        talker = null;
        chase = false;
        enabled = false;
        speaker.Stop();

        if (instanceSpeaker)
            G_SoundManager.a_instance.PushInstanceSpeaker(this);
    }

    public void StopSmoothly(System.Action _callback = null)
    {
        enabled = false;

        UserStartCoroutine(ref volumeCoroutine, ILerpVolume(-1, () => { Stop(); _callback?.Invoke(); }));
    }

    private IEnumerator ILerpVolume(float _updown, System.Action _callback = null, float _duration = 0.2f)
    {
        float _lerpA = 0.5f - (0.5f * _updown);
        float _lerpB = 0.5f + (0.5f * _updown);

        float _deltaTime = 0f;
        float _originVolume = speaker.volume;
        while (_deltaTime < _duration)
        {
            _deltaTime += Time.deltaTime;
            speaker.volume = _originVolume * Mathf.Lerp(_lerpA, _lerpB, _deltaTime / _duration);
            yield return null;
        }
        _callback?.Invoke();
    }

    private void Update()
    {
        if (null == talker)
        {
            Stop();
            return;
        }
        if (paused)
        {
            if (talker.gameObject.activeInHierarchy)
            {
                paused = false;
                speaker.UnPause();
            }
        }
        else
        {
            if (!talker.gameObject.activeInHierarchy)
            {
                paused = true;
                speaker.Pause();
            }
        }
        if (chase)
            transform.position = talker.transform.position;
    }

    private IEnumerator IPlaySound(float _delay, float _front, float _duration, bool _loop)
    {
        while (true)
        {
            if (0 != _delay)
                yield return new WaitForSeconds(_delay);
            speaker.time = _front;
            speaker.Play();
            yield return null;
            if (0 != _duration)
            {
                yield return new WaitForSeconds(_duration);
                if (!_loop)
                {
                    Stop();
                    yield break;
                }
            }
        }
    }

    private void UserStartCoroutine(ref IEnumerator _coroutineRef, IEnumerator _coroutine)
    {
        UserStopCoroutine(ref _coroutineRef);

        _coroutineRef = _coroutine;
        G_SoundManager.a_instance.StartCoroutine(_coroutineRef);
    }

    private void UserStopCoroutine(ref IEnumerator _coroutine)
    {
        if (null != _coroutine)
        {
            G_SoundManager.a_instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
