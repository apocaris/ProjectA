using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEngine;

public static class G_Utils
{
    #region Digit
    public enum GT_DigitGlobal
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        AA, AB, AC, AD, AE, AF, AG, AH, AI, AJ, AK, AL, AM, AN, AO, AP, AQ, AR, AS, AT, AU, AV, AW, AX, AY, AZ,
        BA, BB, BC, BD, BE, BF, BG, BH, BI, BJ, BK, BL, BM, BN, BO, BP, BQ, BR, BS, BT, BU, BV, BW, BX, BY, BZ,
        CA, CB, CC, CD, CE, CF, CG, CH, CI, CJ, CK, CL, CM, CN, CO, CP, CQ, CR, CS, CT, CU, CV, CW, CX, CY, CZ,
        DA, DB, DC, DD, DE, DF, DG, DH, DI, DJ, DK, DL, DM, DN, DO, DP, DQ, DR, DS, DT, DU, DV, DW, DX, DY, DZ,
        EA, EB, EC, ED, EE, EF, EG, EH, EI, EJ, EK, EL, EM, EN, EO, EP, EQ, ER, ES, ET, EU, EV, EW, EX, EY, EZ,
        Count,
    }

    public enum GT_DigitKorean
    {
        ��, ��, ��, ��, ��, ��, ��, ��, ��, ��, ��, ��,
        ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����,
        ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����,
        �ٸ�, �پ�, ����, �ٰ�, ����, ����, �پ�, �ٱ�, �ٰ�, ����, ����, �ٱ�,
        ��, ���, ����, ���, ����, ����, ���, ��, ��, ����, ����, ���,
        ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����,
        �ٸ�, �پ�, ����, �ٰ�, ����, ����, �پ�, �ٱ�, �ٰ�, ����, ����, �ٱ�,
        �Ƹ�, �ƾ�, ����, �ư�, ����, ����, �ƾ�, �Ʊ�, �ư�, ����, ����, �Ʊ�,
        �ڸ�, �ھ�, ����, �ڰ�, ����, ����, �ھ�, �ڱ�, �ڰ�, ����, ����, �ڱ�,
        ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����, ����,
        ī��, ī��, ī��, ī��, ī��, ī��, ī��, ī��, ī��, ī��, ī��, ī��,
        Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��, Ÿ��,
        �ĸ�, �ľ�, ����, �İ�, ����, ����, �ľ�, �ı�, �İ�, ����, ����, �ı�,
        �ϸ�, �Ͼ�, ����, �ϰ�, ����, ����, �Ͼ�, �ϱ�, �ϰ�, ����, ����, �ϱ�,
        Count,
    }

    public static string GetLargeValue(BigInteger iValue)
    {
        SystemLanguage eLang = SystemLanguage.English;

        string strResult;
        string strValue = iValue.ToString();
        int iDigit = strValue.Length;

        if (eLang != SystemLanguage.Korean)
        {
            int iDigitAnchor = (iDigit - 1) / m_iAnchorDigit;
            int iDigitDecimal = (iDigit - 1) % m_iAnchorDigit;

            //�۷ι�
            if (iValue < m_iAnchorDigitValue)
                strResult = iValue.ToString();
            else
            {
                if (iDigitAnchor < (int)GT_DigitGlobal.Count)
                {
                    strResult = string.Format("{0}.{1}{2}", iValue / (BigInteger.Pow(m_iAnchorDigitValue, iDigitAnchor)), strValue.Substring(iDigitDecimal + 1, m_iAnchorDecimalCount), (GT_DigitGlobal)(iDigitAnchor - 1));
                }
                else
                    strResult = string.Format("{0}{1}", iValue / (BigInteger.Pow(m_iAnchorDigitValue, (int)GT_DigitGlobal.Count)), GT_DigitGlobal.EZ);
            }
        }
        else
        {
            int iDigitAnchor = (iDigit - 1) / m_iKORAnchorDigit;
            int iDigitDecimal = (iDigit - 1) % m_iKORAnchorDigit;

            //����
            if (iValue < m_iKORAnchorDigitValue)
                strResult = iValue.ToString();
            else
            {
                if (iDigitAnchor < (int)GT_DigitKorean.Count)
                {
                    GT_DigitKorean eFirst = (GT_DigitKorean)(iDigitAnchor - 1);
                    GT_DigitKorean eSecond = (GT_DigitKorean)(iDigitAnchor - 2);

                    if (eFirst > GT_DigitKorean.��)
                    {
                        int iSceondDamage;
                        int.TryParse(strValue.Substring(iDigitDecimal + 1, m_iKORAnchorDecimalCount), out iSceondDamage);
                        if (iSceondDamage == 0)
                        {
                            strResult = string.Format("{0}{1}",
                                iValue / (BigInteger.Pow(m_iKORAnchorDigitValue, iDigitAnchor)),
                                eFirst);
                        }
                        else
                        {
                            strResult = string.Format("{0}{1} {2}{3}",
                                iValue / (BigInteger.Pow(m_iKORAnchorDigitValue, iDigitAnchor)),
                                eFirst,
                                iSceondDamage,
                                eSecond);
                        }
                    }
                    else if (eFirst == GT_DigitKorean.��)
                    {
                        int iSceondDamage;
                        int.TryParse(strValue.Substring(iDigitDecimal + 1, m_iKORAnchorDecimalCount), out iSceondDamage);
                        if (iSceondDamage == 0)
                        {
                            strResult = string.Format("{0}{1}",
                                iValue / (BigInteger.Pow(m_iKORAnchorDigitValue, iDigitAnchor)),
                                eFirst);
                        }
                        else
                        {
                            strResult = string.Format("{0}{1} {2}",
                                iValue / (BigInteger.Pow(m_iKORAnchorDigitValue, iDigitAnchor)),
                                eFirst,
                                iSceondDamage);
                        }
                    }
                    else
                    {
                        strResult = strValue;
                    }
                }
                else
                    strResult = string.Format("{0}{1}", iValue / (BigInteger.Pow(m_iKORAnchorDigitValue, (int)GT_DigitKorean.Count)), GT_DigitKorean.�ϱ�);
            }
        }

        return strResult;
    }

    private const int m_iAnchorDigitValue = 1000; //ǥ�� ���� ���� ��
    private const int m_iAnchorDigit = 3; //ǥ�� ���� ���� �� ����
    private const int m_iAnchorDecimalCount = 2; //�Ҽ��� ǥ�� �ڸ� ��

    private const int m_iKORAnchorDigitValue = 10000;
    private const int m_iKORAnchorDigit = 4;
    private const int m_iKORAnchorDecimalCount = 4;

    public static string GetCommaNumberText(BigInteger iNumber)
    {
        if (iNumber == 0)
            return "0";

        return string.Format("{0:#,###}", iNumber);
    }

    #endregion

    #region ETC

    public static int GetNumbersInString(string strSource)
    {
        if (string.IsNullOrEmpty(strSource))
            return -1;

        int iValue = -1;
        MatchCollection vMatches = Regex.Matches(strSource, G_Constant.m_strRegexPattern);
        if (vMatches.Count > 0)
        {
            iValue = int.Parse(vMatches[0].Value);
        }

        return iValue;
    }

    public static void RemoveListAll<T>(ref List<T> vTargetList)
    {
        if (vTargetList == null)
            return;
        if (vTargetList.Count == 0)
            return;

        for (int i = vTargetList.Count - 1; i >= 0; i--)
        {
            if (vTargetList[i] == null)
                return;

            vTargetList.Remove(vTargetList[i]);
        }
    }

    public static void RemoveListAll<T>(List<T> vTargetList)
    {
        if (vTargetList == null)
            return;
        if (vTargetList.Count == 0)
            return;

        for (int i = vTargetList.Count - 1; i >= 0; i--)
        {
            if (vTargetList[i] == null)
                return;

            vTargetList.Remove(vTargetList[i]);
        }
    }

    #endregion
}
