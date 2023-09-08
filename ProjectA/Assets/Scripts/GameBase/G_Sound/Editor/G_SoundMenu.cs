using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public static class G_SoundMenu
{
    [MenuItem("Assets/GSound/Create Simple BGM", false, 0)]
    static public void CreateSimpleBGM()
    {
        if (Selection.objects != null && Selection.objects.Length > 0)
        {
            var _clips = Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
            if (null != _clips && 0 != _clips.Length)
            {
                foreach (AudioClip _clip in _clips)
                {
                    G_SoundAsset _asset = ScriptableObject.CreateInstance<G_SoundAsset>();
                    _asset.name = _clip.name;
                    _asset.id = _clip.name;
                    _asset.bgm = true;
                    _asset.channel = 0;

                    if (null == _asset.sound)
                        _asset.sound = new List<G_SoundClip>();
                    _asset.sound.Clear();
                    G_SoundClip _soundClip = new G_SoundClip()
                    {
                        audioClip = _clip,
                        pitch = 1.0f,
                        volume = 1.0f,
                        loop = true,
                    };
                    _asset.sound.Add(_soundClip);

                    string _path = AssetDatabase.GetAssetPath(_clip.GetInstanceID());
                    if ("" == _path)
                        _path = "Assets";
                    else if ("" != Path.GetExtension(_path))
                        _path = _path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(_clip.GetInstanceID())), "");
                    string _assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_path + $"/{_clip.name}.asset");
                    AssetDatabase.CreateAsset(_asset, _assetPathAndName);
                }
            }
            else
            {
                G_SoundAsset _asset = ScriptableObject.CreateInstance<G_SoundAsset>();
                _asset.bgm = true;
                _asset.channel = 0;

                string _path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
                if ("" == _path)
                    _path = "Assets";
                else if ("" != Path.GetExtension(_path))
                    _path = _path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID())), "");
                string _assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_path + $"/New SoundAsset.asset");
                AssetDatabase.CreateAsset(_asset, _assetPathAndName);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/GSound/Create Simple SFX", false, 1)]
    static public void CreateSimpleSFX()
    {
        if (Selection.objects != null && Selection.objects.Length > 0)
        {
            var _clips = Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
            if (null != _clips && 0 != _clips.Length)
            {
                foreach (AudioClip _clip in _clips)
                {
                    G_SoundAsset _asset = ScriptableObject.CreateInstance<G_SoundAsset>();
                    _asset.name = _clip.name;
                    _asset.id = _clip.name;
                    _asset.bgm = false;

                    if (null == _asset.sound)
                        _asset.sound = new List<G_SoundClip>();
                    _asset.sound.Clear();
                    G_SoundClip _soundClip = new G_SoundClip()
                    {
                        audioClip = _clip,
                        pitch = 1.0f,
                        volume = 1.0f,
                        loop = false,
                        delay = 0.0f,
                        duration = _clip.length,
                    };
                    _asset.sound.Add(_soundClip);

                    string _path = AssetDatabase.GetAssetPath(_clip.GetInstanceID());
                    if ("" == _path)
                        _path = "Assets";
                    else if ("" != Path.GetExtension(_path))
                        _path = _path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(_clip.GetInstanceID())), "");
                    string _assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_path + $"/{_clip.name}.asset");
                    AssetDatabase.CreateAsset(_asset, _assetPathAndName);
                }
            }
            else
            {
                G_SoundAsset _asset = ScriptableObject.CreateInstance<G_SoundAsset>();
                _asset.bgm = false;

                string _path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
                if ("" == _path)
                    _path = "Assets";
                else if ("" != Path.GetExtension(_path))
                    _path = _path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID())), "");
                string _assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_path + $"/New SoundAsset.asset");
                AssetDatabase.CreateAsset(_asset, _assetPathAndName);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
