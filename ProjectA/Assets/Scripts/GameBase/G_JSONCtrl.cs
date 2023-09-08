using UnityEngine;

using System.Text;
using System.IO;
using System;
using SimpleJSON;
using System.Globalization;
using System.Numerics;

public class G_JSONCtrl : MonoBehaviour
{
    public static JSONNode LoadFromTextFile(string strPath)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(strPath);
        if (jsonText != null)
            return JSON.Parse(jsonText.text);
        else
        {
            Debug.LogError("Load From Text File Error ... Path : " + strPath);
            return null;
        }
    }

    public static string Get(JSONNode vData, string strKey)
    {
        return GetString(vData, strKey);
    }

    public static string GetData(JSONNode vData, string strKey)
    {
        return vData[strKey] != null ? Encoding.UTF8.GetString(Convert.FromBase64String(vData[strKey])) : null;
    }

    public static string GetString(JSONNode vData, string strKey)
    {
        return vData[strKey];
    }

    public static long GetLONG(JSONNode vData, string strKey, long lDefault = 0)
    {
        long lResult = 0;

        if (long.TryParse(Get(vData, strKey), out lResult))
            return lResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetLONG");

        return lDefault;
    }

    public static int GetINT(JSONNode vData, string strKey, int iDefault = 0)
    {
        int iResult = 0;

        if (int.TryParse(Get(vData, strKey), out iResult))
            return iResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetINT");

        return iDefault;
    }

    public static BigInteger GetBIGINTEGER(JSONNode vData, string strKey, int iDefault = 0)
    {
        BigInteger iResult = 0;

        if (BigInteger.TryParse(Get(vData, strKey), out iResult))
            return iResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetBIGINTEGER");

        return iDefault;
    }

    public static bool GetBOOL(JSONNode vData, string strKey, bool bDefault = false)
    {
        int iResult;
        if (int.TryParse(Get(vData, strKey), out iResult))
            return iResult > 0 ? true : false;
        Debug.LogWarning("JSON Data Parse Warning ... GetBOOL");

        return bDefault;
    }

    public static float GetFLOAT(JSONNode vData, string strKey, float fDefault = 0.0f)
    {
        float fResult = 0.0f;

        if (float.TryParse(Get(vData, strKey), out fResult))
            return fResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetFLOAT");

        return fDefault;
    }

    public static double GetDOUBLE(JSONNode vData, string strKey, double dDefault = 0)
    {
        double dResult = 0;

        if (double.TryParse(Get(vData, strKey), out dResult))
            return dResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetDOUBLE");

        return dDefault;
    }

    public static byte GetBYTE(JSONNode vData, string strKey, byte btDefault = 0)
    {
        byte btResult = 0;

        if (byte.TryParse(Get(vData, strKey), out btResult))
            return btResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetBYTE");

        return btDefault;
    }

    public static string GetBBCodeToTextColor(JSONNode vData, string strKey)
    {
        string strHex = string.Format("FF{0}", Get(vData, strKey));
        int iHex = int.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
        byte R = (byte)((iHex >> 16) & 0xFF);
        byte G = (byte)((iHex >> 8) & 0xFF);
        byte B = (byte)((iHex) & 0xFF);
        return string.Format("{0:X2}{1:X2}{2:X2}", R, G, B);
    }

    public static DateTime GetDateTime(JSONNode vData, string strKey, bool bDefaultMax = false)
    {
        DateTime dtResult = bDefaultMax ? DateTime.MaxValue : DateTime.MinValue;

        if (DateTime.TryParse(Get(vData, strKey), out dtResult))
            return dtResult;
        Debug.LogWarning("JSON Data Parse Warning ... GetDateTime");

        return bDefaultMax ? DateTime.MaxValue : DateTime.MinValue;
    }
}
