using System.Text.RegularExpressions;
using UnityEditor.MemoryProfiler;

public static class G_Utils
{
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
    
}
