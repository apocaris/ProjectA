using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GT_PoolType
{
    Monster,
    DamageFont,
    UIDrop,
}

public enum GT_FieldType
{
    Stage,
    Dungeon,
}

public enum GT_Direction
{
    Left,
    Right,
}

public enum GT_UnitState
{
    Create = 0,
    Idle,
    Move,
    Attack,
    Die,
    Dash_Ready,
    Dash_Move,
}

public enum GT_SpineTrackIndex
{
    None = 0,
    Character,
    Monster,
}

public enum GT_UnitType
{
    None,
    MainCharacter,
    Monster,
}

// 캐릭터 이펙트 소팅 타입
public enum GT_EffectSortType
{
    None,
    Front,
    Back,
}

public static class G_Constant
{
    public const string m_strBaseObject = "Prefabs/BaseObject";

    public const string m_strIngameScene = "GameScene";
    public const string m_strFieldPoints = "FieldPoints";

    public const string m_strMonsterPool = "MonsterPool";

    #region Ingame object names
    public const string m_strCharacterObject = "Prefabs/IngameObject/MainCharacter";
    public const string m_strMonsterObject = "Prefabs/IngameObject/Monster";
    #endregion

    // 기본 모션 이름
    public const string m_strMotion_Idle = "idle";
    public const string m_strMotion_Move = "run";
    public const string m_strMotion_Attack = "atk";
    public const string m_strMotion_Die = "die";
    public const string m_strMotion_Hit = "hit";

    // 캐릭터 모션 이름
    public const string m_strMotion_Atk_1 = "atk_1";
    public const string m_strMotion_Atk_2 = "atk_2";
    public const string m_strMotion_Atk_3 = "atk_3";
    public const string m_strMotion_Atk_4 = "atk_4";
    public const string m_strMotion_Dash = "dash";
    public const string m_strMotion_Skill_1 = "skill_1";
    public const string m_strMotion_Skill_2 = "skill_2";

    // 평타 이벤트 이름
    public const string m_strSpine_Event_Attack_1 = "attack_1";
    public const string m_strSpine_Event_Attack_2 = "attack_2";
    public const string m_strSpine_Event_Attack_3 = "attack_3";
    public const string m_strSpine_Event_Attack_4 = "attack_4";

    // 연타 스킬 이벤트 이름
    public const string m_strSpine_Event_ComoSkill_1 = "skill_combo_1";
    public const string m_strSpine_Event_ComoSkill_2 = "skill_combo_2";
    public const string m_strSpine_Event_ComoSkill_3 = "skill_combo_3";
    public const string m_strSpine_Event_ComoSkill_4 = "skill_combo_4";
    public const string m_strSpine_Event_ComoSkill_5 = "skill_combo_5";
    public const string m_strSpine_Event_ComoSkill_6 = "skill_combo_6";
    public const string m_strSpine_Event_ComoSkill_7 = "skill_combo_7";
    public const string m_strSpine_Event_ComoSkill_8 = "skill_combo_8";
    public const string m_strSpine_Event_ComoSkill_9 = "skill_combo_9";
    public const string m_strSpine_Event_ComoSkill_10 = "skill_combo_10";
    public const string m_strSpine_Event_ComoSkill_11 = "skill_combo_11";
    public const string m_strSpine_Event_ComoSkill_12 = "skill_combo_12";
    public const string m_strSpine_Event_ComoSkill_13 = "skill_combo_13";

    // 메모리 풀
    public const int m_iMonsterPoolSize = 20;

    // 해상도
    public const int m_iScreenResoultion = 1080; //540 : qHD , 720 : HD
    public const int m_iFoldScreenResoultion = 1080; //1080 : FHD

    // 임시
    public const float m_fMonsterSpawnDelay = 1.4f;
    public const int m_iSpawnMonsterMaxCount = 20;
    public const int m_iSpawnOnceCount = 4;
}
