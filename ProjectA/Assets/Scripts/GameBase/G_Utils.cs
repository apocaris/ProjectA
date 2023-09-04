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
        만, 억, 조, 경, 해, 자, 양, 구, 간, 정, 재, 극,
        가만, 가억, 가조, 가경, 가해, 가자, 가양, 가구, 가간, 가정, 가재, 가극,
        나만, 나억, 나조, 나경, 나해, 나자, 나양, 나구, 나간, 나정, 나재, 나극,
        다만, 다억, 다조, 다경, 다해, 다자, 다양, 다구, 다간, 다정, 다재, 다극,
        라만, 라억, 라조, 라경, 라해, 라자, 라양, 라구, 라간, 라정, 라재, 라극,
        마만, 마억, 마조, 마경, 마해, 마자, 마양, 마구, 마간, 마정, 마재, 마극,
        바만, 바억, 바조, 바경, 바해, 바자, 바양, 바구, 바간, 바정, 바재, 바극,
        아만, 아억, 아조, 아경, 아해, 아자, 아양, 아구, 아간, 아정, 아재, 아극,
        자만, 자억, 자조, 자경, 자해, 자자, 자양, 자구, 자간, 자정, 자재, 자극,
        차만, 차억, 차조, 차경, 차해, 차자, 차양, 차구, 차간, 차정, 차재, 차극,
        카만, 카억, 카조, 카경, 카해, 카자, 카양, 카구, 카간, 카정, 카재, 카극,
        타만, 타억, 타조, 타경, 타해, 타자, 타양, 타구, 타간, 타정, 타재, 타극,
        파만, 파억, 파조, 파경, 파해, 파자, 파양, 파구, 파간, 파정, 파재, 파극,
        하만, 하억, 하조, 하경, 하해, 하자, 하양, 하구, 하간, 하정, 하재, 하극,
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

            //글로벌
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

            //국내
            if (iValue < m_iKORAnchorDigitValue)
                strResult = iValue.ToString();
            else
            {
                if (iDigitAnchor < (int)GT_DigitKorean.Count)
                {
                    GT_DigitKorean eFirst = (GT_DigitKorean)(iDigitAnchor - 1);
                    GT_DigitKorean eSecond = (GT_DigitKorean)(iDigitAnchor - 2);

                    if (eFirst > GT_DigitKorean.만)
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
                    else if (eFirst == GT_DigitKorean.만)
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
                    strResult = string.Format("{0}{1}", iValue / (BigInteger.Pow(m_iKORAnchorDigitValue, (int)GT_DigitKorean.Count)), GT_DigitKorean.하극);
            }
        }

        return strResult;
    }

    private const int m_iAnchorDigitValue = 1000; //표기 단위 기준 수
    private const int m_iAnchorDigit = 3; //표기 단위 기준 수 길이
    private const int m_iAnchorDecimalCount = 2; //소수점 표기 자리 수

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
