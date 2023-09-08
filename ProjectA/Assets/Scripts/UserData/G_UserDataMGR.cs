using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class G_UserDataMGR : G_SimpleMGR<G_UserDataMGR>
{
    #region SingletonMember
    private static G_UserDataMGR m_vInstance = null;
    public static G_UserDataMGR a_instance { get { if (m_vInstance == null) { EditorLog("MGR Instance is null !", 1); m_vInstance = ResetMGR(); } return m_vInstance; } }
    #endregion

    public void Initialize()
    {
        LoadNetworkFlagData(ref m_vNetworkFlagData);
        LoadLocalData(ref m_vLocalData);
        LoadBaseData(ref m_vBaseData);
        LoadAccountData(ref m_vAccountData);
        LoadStatusData(ref m_vStatusData);
        LoadCurrencyData(ref m_vCurrencyData);
        LoadLoadoutData(ref m_vLoadoutData);
        LoadEquipmentData(ref m_vEquipmentData);
        LoadSkillLoadData(ref m_vSkillLoadData);
        LoadSkillSetData(ref m_vSkillSetData);
        LoadPotentialData(ref m_vPotentialData);
        LoadQuestData(ref m_vQuestData);
        LoadPlayData(ref m_vPlayData);

        if (m_vLocalData != null && m_vLocalData.a_iLastStage == 0)
        {

            SaveLocalData();
        }
    }

    #region Network Flag Data
    public void SaveNetworkFlagData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vNetworkFlagData == null)
            {
                LoadNetworkFlagData(ref m_vNetworkFlagData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vNetworkFlagData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_NetworkFlag, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadNetworkFlagData(ref G_UserNetworkFlagData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_NetworkFlag))
        {
            vRetData = new G_UserNetworkFlagData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_NetworkFlag));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserNetworkFlagData)vData;
            }
            else
                vRetData = new G_UserNetworkFlagData();
        }
    }
    #endregion

    #region Local Data
    public void SaveLocalData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vLocalData == null)
            {
                LoadLocalData(ref m_vLocalData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vLocalData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Local, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadLocalData(ref G_UserLocalData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Local))
        {
            vRetData = new G_UserLocalData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Local));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserLocalData)vData;
            }
            else
                vRetData = new G_UserLocalData();
        }
    }
    #endregion

    #region Account Data
    public void SaveAccountData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vAccountData == null)
            {
                LoadAccountData(ref m_vAccountData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vAccountData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Account, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadAccountData(ref G_UserAccountData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Account))
        {
            vRetData = new G_UserAccountData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Account));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserAccountData)vData;
            }
            else
                vRetData = new G_UserAccountData();
        }
    }
    #endregion

    #region Base Data
    public void SaveBaseData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vBaseData == null)
            {
                LoadBaseData(ref m_vBaseData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vBaseData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Base, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadBaseData(ref G_UserBaseData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Base))
        {
            vRetData = new G_UserBaseData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Base));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserBaseData)vData;
            }
            else
                vRetData = new G_UserBaseData();
        }
    }
    #endregion

    #region Currency Data
    public void SaveCurrencyData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vCurrencyData == null)
            {
                LoadCurrencyData(ref m_vCurrencyData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vCurrencyData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Currency, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadCurrencyData(ref G_UserCurrencyData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Currency))
        {
            vRetData = new G_UserCurrencyData();
            return;
        }
            

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Currency));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserCurrencyData)vData;
            }
            else
                vRetData = new G_UserCurrencyData();
        }
    }
    #endregion

    #region Status Data
    public void SaveStatusData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vStatusData == null)
            {
                LoadStatusData(ref m_vStatusData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vStatusData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Status, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadStatusData(ref G_UserStatusData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Status))
        {
            vRetData = new G_UserStatusData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Status));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserStatusData)vData;
            }
            else
                vRetData = new G_UserStatusData();
        }
    }
    #endregion

    #region Loadout Data
    public void SaveLoadoutData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vLoadoutData == null)
            {
                LoadLoadoutData(ref m_vLoadoutData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vLoadoutData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Loadout, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadLoadoutData(ref G_UserLoadoutData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Loadout))
        {
            vRetData = new G_UserLoadoutData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Loadout));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserLoadoutData)vData;
            }
            else
                vRetData = new G_UserLoadoutData();
        }
    }
    #endregion

    #region Equipment Data
    public void SaveEquipmentData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vEquipmentData == null)
            {
                LoadEquipmentData(ref m_vEquipmentData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vEquipmentData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Equipment, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadEquipmentData(ref G_UserEquipmentData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Equipment))
        {
            vRetData = new G_UserEquipmentData();
            return; 
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Equipment));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);

                vRetData = (G_UserEquipmentData)vData;
            }
            else
                vRetData = new G_UserEquipmentData();
        }
    }
    #endregion

    #region Skill Load Data
    public void SaveSkillLoadData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vSkillLoadData == null)
            {
                LoadSkillLoadData(ref m_vSkillLoadData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vSkillLoadData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_SkillLoad, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadSkillLoadData(ref G_UserSkillLoadData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_SkillLoad))
        {
            vRetData = new G_UserSkillLoadData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_SkillLoad));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserSkillLoadData)vData;
            }
            else
                vRetData = new G_UserSkillLoadData();
        }
    }
    #endregion

    #region Skill Set Data
    public void SaveSkillSetData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vSkillSetData == null)
            {
                LoadSkillSetData(ref m_vSkillSetData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vSkillSetData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_SkillSet, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadSkillSetData(ref G_UserSkillSetData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_SkillSet))
        {
            vRetData = new G_UserSkillSetData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_SkillSet));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserSkillSetData)vData;
            }
            else
                vRetData = new G_UserSkillSetData();
        }
    }
    #endregion

    #region Potential Data
    public void SavePotentialData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vPotentialData == null)
            {
                LoadPotentialData(ref m_vPotentialData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vPotentialData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Potential, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadPotentialData(ref G_UserPotentialData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Potential))
        {
            vRetData = new G_UserPotentialData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Potential));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserPotentialData)vData;
            }
            else
                vRetData = new G_UserPotentialData();
        }
    }
    #endregion

    #region Quest Data
    public void SaveQuestData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vQuestData == null)
            {
                LoadQuestData(ref m_vQuestData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vQuestData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Play, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadQuestData(ref G_UserQuestData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Quest))
        {
            vRetData = new G_UserQuestData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Quest));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserQuestData)vData;
            }
            else
                vRetData = new G_UserQuestData();
        }
    }
    #endregion

    #region Play Data
    public void SavePlayData()
    {
        if (a_bNullReferenceExceptionOccurred)
            return;

        try
        {
            if (m_vPlayData == null)
            {
                LoadPlayData(ref m_vPlayData);
                a_bNullReferenceExceptionOccurred = true;
                return;
            }

            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m_vPlayData);
                ObscuredPrefs.SetString(G_Constant.m_strSaveKey_Play, Convert.ToBase64String(ms.ToArray()));
            }
        }
        catch (NullReferenceException e)
        {
            a_bNullReferenceExceptionOccurred = true;
        }
    }

    public void LoadPlayData(ref G_UserPlayData vRetData)
    {
        if (!ObscuredPrefs.HasKey(G_Constant.m_strSaveKey_Play))
        {
            vRetData = new G_UserPlayData();
            return;
        }

        byte[] bytes = Convert.FromBase64String(ObscuredPrefs.GetString(G_Constant.m_strSaveKey_Play));
        using (var ms = new MemoryStream(bytes))
        {
            if (ms.Length > 0)
            {
                object vData = new BinaryFormatter().Deserialize(ms);
                vRetData = (G_UserPlayData)vData;
            }
            else
                vRetData = new G_UserPlayData();
        }
    }
    #endregion


    #region Classes

    public G_UserAccountData a_vAccountData { get { return m_vAccountData; } }
    private G_UserAccountData m_vAccountData = null;

    public G_UserBaseData a_vBaseData { get { return m_vBaseData; } }
    private G_UserBaseData m_vBaseData = null;

    public G_UserCurrencyData a_vCurrencyData { get { return m_vCurrencyData; } }
    private G_UserCurrencyData m_vCurrencyData = null;

    public G_UserStatusData a_vStatusData { get { return m_vStatusData; } }
    private G_UserStatusData m_vStatusData = null;

    public G_UserLoadoutData a_vLoadoutData { get { return m_vLoadoutData; } }
    private G_UserLoadoutData m_vLoadoutData = null;

    public G_UserEquipmentData a_vEquipmentData { get { return m_vEquipmentData; } }
    private G_UserEquipmentData m_vEquipmentData = null;

    public G_UserSkillLoadData a_vSkillLoadData { get { return m_vSkillLoadData; } }
    private G_UserSkillLoadData m_vSkillLoadData = null;

    public G_UserSkillSetData a_vSkillSetData { get { return m_vSkillSetData; } }
    private G_UserSkillSetData m_vSkillSetData = null;

    public G_UserPotentialData a_vPotentialData { get { return m_vPotentialData; } }
    private G_UserPotentialData m_vPotentialData = null;

    public G_UserQuestData a_vQuestData { get { return m_vQuestData; } }
    private G_UserQuestData m_vQuestData = null;

    public G_UserLocalData a_vLocalData { get { return m_vLocalData; } }
    private G_UserLocalData m_vLocalData = null;

    public G_UserPlayData a_vPlayData { get { return m_vPlayData; } }
    private G_UserPlayData m_vPlayData = null;

    public G_UserNetworkFlagData a_vNetworkFlagData { get { return m_vNetworkFlagData; } }
    private G_UserNetworkFlagData m_vNetworkFlagData = null;

    #endregion

    #region Variables

    private static bool m_bNullReferenceExceptionOccurred = false;
    public static bool a_bNullReferenceExceptionOccurred
    {
        set
        {
            if (value && !m_bNullReferenceExceptionOccurred)
            {
                //G_GameMGR.a_instance.a_vGameScene.CreateMessageBox(true, "Exception Error", "Exception error has occurred.\nThe game will be terminated.", () => Application.Quit(), null);
                Time.timeScale = 0f;
            }
            m_bNullReferenceExceptionOccurred = value;
        }
        get => m_bNullReferenceExceptionOccurred;
    }

    #endregion
}
