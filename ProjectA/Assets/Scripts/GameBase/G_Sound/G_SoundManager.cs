using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;

public class G_SoundManager : G_SimpleMGR<G_SoundManager>
{

    #region SingletonMember
    private static G_SoundManager m_vInstance = null;
    public static G_SoundManager a_instance { get { if (m_vInstance == null) { EditorLog("MGR Instance is null !", 1); m_vInstance = ResetMGR(); } return m_vInstance; } }
    #endregion
    public static float masterVolume = 1f;

    public static bool muteBGM
    {
        set
        {
            if (_muteBGM != !value)
            {
                _muteBGM = !value;
                _muteBGMLoaded = true;
            }
        }
        get
        {
            if (!_muteBGMLoaded)
            {
                _muteBGMLoaded = true;
                if(G_UserDataMGR.a_instance.a_vLocalData != null)
                    _muteBGM = !G_UserDataMGR.a_instance.a_vLocalData.a_bBGM;
            }
            return _muteBGM;
        }
    }
    private static bool _muteBGM = false;
    private static bool _muteBGMLoaded = false;

    public static bool muteSFX
    {
        set
        {
            if (_muteSFX != !value)
            {
                _muteSFX = !value;

                _muteSFXLoaded = true;
            }
        }
        get
        {
            if (!_muteSFXLoaded)
            {
                _muteSFXLoaded = true;
                if(G_UserDataMGR.a_instance.a_vLocalData != null)
                    _muteSFX = !G_UserDataMGR.a_instance.a_vLocalData.a_bSE;
            }
            return _muteSFX;
        }
    }
    private static bool _muteSFX = false;
    private static bool _muteSFXLoaded = false;
    public static string SOUND_ASSET_PATH = "Sound/";

    public float volume = 1f;
    public float assetLoadProgress = 0f;
    public bool isAssetLoaded = false;

    public AudioSource simpleSpeaker = null;
    public G_SoundSpeaker channelSpeakerSample = null;
    public G_SoundSpeaker instanceSpeakerSample = null;

    private Dictionary<string, G_SoundAsset> soundAssets = new Dictionary<string, G_SoundAsset>();
    private Dictionary<G_SoundAsset, float> soundCooltimes = new Dictionary<G_SoundAsset, float>();
    private Dictionary<int, G_SoundSpeaker> channelSpeaker = new Dictionary<int, G_SoundSpeaker>();
    private List<G_SoundSpeaker> instanceSpeakerPool = new List<G_SoundSpeaker>();

    private const float SOUND_COOLTIME = 0.12f;

    protected override void Awake()
    {
        base.Awake();

        ResetMGR();

        if (null == simpleSpeaker)
        {
            GameObject _speakerObject = new GameObject("Simple Speaker");
            _speakerObject.transform.parent = transform;
            _speakerObject.transform.localPosition = Vector3.zero;
            simpleSpeaker = _speakerObject.AddComponent<AudioSource>();
        }

        //LoadAllSoundAssets();
    }

    public void LoadAllSoundAssets()
    {
        isAssetLoaded = true;
        assetLoadProgress = 1f;
    }

    private IEnumerator LoadAsync(string[] _assets)
    {
        if (isAssetLoaded)
            yield break;
        ResourceRequest _request = null;
        for (int i = 0; i != _assets.Length; ++i)
        {
            _request = Resources.LoadAsync<G_SoundAsset>(_assets[i]);
            yield return new WaitUntil(() => _request.isDone);
            assetLoadProgress = (float)(i + 1) / _assets.Length;
            if (null == _request.asset)
            {
                Debug.LogError($"Sound asset {_assets[i]} not found");
                continue;
            }
            LoadSoundAsset((G_SoundAsset)_request.asset);
        }
        isAssetLoaded = true;
    }

    public void LoadSoundAsset(G_SoundAsset _asset)
    {
        if (soundAssets.ContainsKey(_asset.id))
            return;
        soundAssets.Add(_asset.id, _asset);
    }

    public void Play(string _id, Transform _talker = null, bool _chase = false, bool _cooltime = false, bool _smoothly = false)
    {
        if (_id == null)
            return;

        if (_id == string.Empty || _id == "none")
            return;

        G_SoundAsset _asset = null;
        if (!soundAssets.ContainsKey(_id))
        {
            Debug.Log($"BGM Path {SOUND_ASSET_PATH}{_id}");
            _asset = Resources.Load<G_SoundAsset>($"{SOUND_ASSET_PATH}{_id}");
            soundAssets.Add(_id, _asset);
            if (null == _asset)
            {
                Debug.Log($"LOAD SOUND {_id} is NULL");
            }
            else
            {
                Debug.Log($"LOAD SOUND {_id} - {_asset.sound.Count}");
                foreach (var _sound in _asset.sound)
                {
                    if (_sound.audioClip == null)
                    {
                        Debug.LogError($"Audio Source Is Null - {_id}");
                        return;
                    }
                    else
                        Debug.Log($"SOUND LIST - {_sound.audioClip.name} {_sound.audioClip}");
                }
            }
        }
        else
            _asset = soundAssets[_id];
        if (null == _asset)
            return;
        Play(_asset, _talker, _chase, _cooltime, _smoothly);
    }

    public void PlayOnGame(string strID, bool bCoolTime)
    {
        //3D 뷰 안켜져있으면 효과음 재생 X
        //if (G_GameMGR.a_instance.a_vGameSceneCtrl.a_vBatterySaveModeSystem.a_bBatterySaveModeState || !G_GameFieldMGR.a_instance.a_vCharUnit.a_vMainCameraAnchor.activeSelf)
        //    return;

        Play(strID, null, false, bCoolTime, false);
    }

    public void Play(G_SoundAsset _asset, Transform _talker = null, bool _chase = false, bool _cooltime = false, bool _smoothly = false)
    {
        if (_asset == null)
            return;

        if (_cooltime)
        {
            // 소리 겹쳐서 재생되는거 방지
            float time = Time.realtimeSinceStartup;
            if (soundCooltimes.ContainsKey(_asset))
            {
                if (soundCooltimes[_asset] > time)
                    return;
                soundCooltimes[_asset] = time + SOUND_COOLTIME;
            }
            else
                soundCooltimes.Add(_asset, time + SOUND_COOLTIME);
        }

        if (_asset.bgm)
            PlayBGM(_asset, null != _talker ? _talker : transform, _chase, _smoothly);
        else
            PlaySFX(_asset, null != _talker ? _talker : transform, _chase, _smoothly);
    }

    private void PlayBGM(G_SoundAsset _asset, Transform _talker, bool _chase, bool _smoothly)
    {
        if (muteBGM)
        {
            return;
        }
        G_SoundSpeaker _speaker = GetChannelSpeaker(_asset.channel, _talker, _chase);
        if (null == _speaker)
        {
            return;
        }
        if (_asset.id == _speaker.lastAssetId)
        {
            return;
        }
        if (0 == _asset.sound.Count)
        {
            return;
        }
        G_SoundClip _sound = _asset.sound[0];
        if (null == _sound)
        {
            return;
        }
            
        if (null == _sound.audioClip)
        {
            return;
        }

        Debug.Log("PlayBGM : " + _asset.id);

        if (_smoothly && _speaker.IsPlaying)
        {
            _speaker.StopSmoothly(() => _speaker.AddSoundClip(_asset, _sound, volume * masterVolume, _smoothly));
        }
        else
        {
            _speaker.AddSoundClip(_asset, _sound, volume * masterVolume, _smoothly);
        }
        _speaker.SetTalker(_talker, _chase);
    }

    private void PlaySFX(G_SoundAsset _asset, Transform _talker, bool _chase, bool _smoothly)
    {
        if (muteSFX)
            return;
        foreach (G_SoundClip _sound in _asset.sound)
        {
            if (null == _sound)
                continue;
            if (null == _sound.audioClip)
                continue;
            G_SoundSpeaker _speaker = PopInstanceSpeaker(_asset, _talker, _chase);
            if (null == _speaker)
                continue;
            _speaker.AddSoundClip(_asset, _sound, volume * masterVolume, _smoothly);
            _speaker.SetTalker(_talker, _chase);
        }
    }

    public void StopBGM()
    {
        foreach (G_SoundSpeaker _speaker in channelSpeaker.Values)
            _speaker.Stop();
    }

    public void StopBGM(int _channel, bool _smoothly = false)
    {
        if (channelSpeaker.ContainsKey(_channel))
        {
            G_SoundSpeaker _speaker = channelSpeaker[_channel];
            if (_speaker != null)
            {
                if (_smoothly)
                    _speaker.StopSmoothly();
                else
                    _speaker.Stop();
            }
        }
    }

    public void StopSFX()
    {
    }

    private G_SoundSpeaker GetChannelSpeaker(int _channel, Transform _talker, bool _chase)
    {
        G_SoundSpeaker _speaker = null;
        if (channelSpeaker.ContainsKey(_channel))
        {
            _speaker = channelSpeaker[_channel];
            if (null == _speaker)
                channelSpeaker.Remove(_channel);
        }
        if (!channelSpeaker.ContainsKey(_channel))
        {
            if (null == channelSpeakerSample)
                _speaker = CreateSpeaker($"Audio_Channel_{_channel}");
            else
                _speaker = Instantiate(channelSpeakerSample.gameObject, transform).GetComponent<G_SoundSpeaker>();
            channelSpeaker.Add(_channel, _speaker);
        }
        _speaker.SetTalker(_talker, _chase);
        return _speaker;
    }

    private G_SoundSpeaker PopInstanceSpeaker(G_SoundAsset _asset, Transform _talker, bool _chase)
    {
        G_SoundSpeaker _speaker = null;
        while (null == _speaker && 0 != instanceSpeakerPool.Count)
        {
            _speaker = instanceSpeakerPool[0];
            instanceSpeakerPool.RemoveAt(0);
        }
        if (null == _speaker)
        {
            if (null == instanceSpeakerSample)
                _speaker = CreateSpeaker("Instance_Audio");
            else
                _speaker = Instantiate(instanceSpeakerSample.gameObject, _talker).GetComponent<G_SoundSpeaker>();
        }
        _speaker.gameObject.SetActive(true);
        return _speaker;
    }

    public void PushInstanceSpeaker(G_SoundSpeaker _speaker)
    {
        _speaker.gameObject.SetActive(false);
        instanceSpeakerPool.Add(_speaker);
    }

    private G_SoundSpeaker CreateSpeaker(string _name)
    {
        GameObject _object = new GameObject(_name);
        _object.transform.parent = transform;
        _object.transform.localPosition = Vector3.zero;
        G_SoundSpeaker _speaker = _object.AddComponent<G_SoundSpeaker>();
        return _speaker;
    }

    private IEnumerator ExecuteAfter(float _delay, Action _callback)
    {
        yield return new WaitForSeconds(_delay);
        _callback.Invoke();
    }
}
