using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;

public class AssetBundleCreator : MonoBehaviour
{
    static string m_strVersion = "1.0.0";
    static int m_iBundleVersionCode = 1; //Android Only
    static string m_strCompany = "SuperPixel. Inc";

    [MenuItem("Assets/Build Asset Bundle")]
    static void BuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("Custom Menu/Build/Android-Google Build Setting")]
    private static void BuildSetting_Android_Google()
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.idle.july.third.aos");
        PlayerSettings.Android.bundleVersionCode = m_iBundleVersionCode;
        PlayerSettings.bundleVersion = m_strVersion;
        PlayerSettings.companyName = m_strCompany;

        //구글은 AAB
        EditorUserBuildSettings.buildAppBundle = true;
        EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Public;
        QualitySettings.softParticles = true;

        List<string> arrayAllDefines = AddBasicBuildDefineForAndroid();
        /* Types */
        arrayAllDefines.Add("Build_Type_AND_GooglePlay");

        PlayerSettings.Android.useAPKExpansionFiles = true;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", arrayAllDefines.ToArray()));
    }

    [MenuItem("Custom Menu/Build/Android-OneStore Build Setting")]
    private static void BuildSetting_Android_OneStore()
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.idle.july.third.aos");
        PlayerSettings.Android.bundleVersionCode = m_iBundleVersionCode;
        PlayerSettings.bundleVersion = m_strVersion;
        QualitySettings.softParticles = true;
        PlayerSettings.companyName = m_strCompany;

        //원스토어는 APK
        EditorUserBuildSettings.buildAppBundle = false;
        EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Disabled;

        List<string> arrayAllDefines = AddBasicBuildDefineForAndroid();
        /* Types */
        arrayAllDefines.Add("Build_Type_AND_OneStore");


        PlayerSettings.Android.useAPKExpansionFiles = false;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", arrayAllDefines.ToArray()));
    }

    [MenuItem("Custom Menu/Build/*----------------------")]
    private static void Line_1() { }
    [MenuItem("Custom Menu/Build/*-------ATTENTION-------")]
    private static void Line_2() { }
    [MenuItem("Custom Menu/Build/-------CLICK WARNING-------")]
    private static void Line_3() { }
    [MenuItem("Custom Menu/Build/-------ATTENTION-------*")]
    private static void Line_4() { }
    [MenuItem("Custom Menu/Build/----------------------*")]
    private static void Line_5() { }


    [MenuItem("Custom Menu/Build/IOS-Build Setting")]
    private static void BuildSetting_IOS()
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.idle.beltscroll.hero.ios");
        PlayerSettings.bundleVersion = m_strVersion;
        PlayerSettings.companyName = m_strCompany;

        QualitySettings.softParticles = false; //IOS는 soft particle 쓰면 깨진다. 빼자(정확히는 Legacy Shader의 Particle/Alhpa Bleneded)

        List<string> arrayAllDefines = AddBasicBuildDefineForAndroid();
        /* Types */
        arrayAllDefines.Add("Build_Type_IOS");


        PlayerSettings.Android.useAPKExpansionFiles = false;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", arrayAllDefines.ToArray()));
    }


    [MenuItem("Custom Menu/Tools/ClearPlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        CodeStage.AntiCheat.Storage.ObscuredPrefs.DeleteAll();
        // Backend.BMember.DeleteGuestInfo();
    }


private static List<string> AddBasicBuildDefineForAndroid()
    {
        List<string> arrayAllDefines = new List<string>();
        /* Basic */
        arrayAllDefines.Add("UNITY_POST_PROCESSING_STACK_V2");
        arrayAllDefines.Add("PRODUCTION_AD");
        arrayAllDefines.Add("DEV_SERVER");
        //arrayAllDefines.Add("ATTACK_BASE_TIMING"); // 기본 캐릭터의 공격 인식 방식 (타이밍), 추가가 안될 경우 기본이 이벤트 발생 방식으로
        arrayAllDefines.Add("MONSTER_SPAWN_WAVE");  // 몬스터 스폰 방식 : 웨이브 (데몬스쿼드 방식)
        arrayAllDefines.Add("CAM_PERSPECTIVE");     // 카메라 모드 : 주석 처리시 Orthographic 카메라

        return arrayAllDefines;
    }
}
