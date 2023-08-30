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
    Hit,
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

public enum GT_Unit
{
    None,
    MainCharacter,
    Monster,
}

public enum GT_UnitClass
{
    None,
    Axe,
    Spear,
    TwoSword,
}

// 캐릭터 이펙트 소팅 타입
public enum GT_EffectSort
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

    #region Character spine class folder
    public const string m_strClassAxe = "axe";
    public const string m_strClassSpear = "spear";
    public const string m_strClassTwoSword = "two_sword";
    #endregion

    #region Motion names
    // 기본 모션 이름
    public const string m_strMotion_Idle = "idle";
    public const string m_strMotion_Move = "run";
    public const string m_strMotion_Attack = "atk";
    public const string m_strMotion_Die = "die";
    public const string m_strMotion_Hit = "hit";

    // 캐릭터 모션 이름
    public const string m_strMotion_Atk_1 = "atk1";
    public const string m_strMotion_Atk_2 = "atk2";
    public const string m_strMotion_Atk_3 = "atk3";
    public const string m_strMotion_Atk_4 = "atk4";
    public const string m_strMotion_Dash = "dash";
    public const string m_strMotion_Skill_1 = "skill_1";
    public const string m_strMotion_Skill_2 = "skill_2";

    // 평타 이벤트 이름
    public const string m_strSpine_Event_Attack_1 = "atk1";
    public const string m_strSpine_Event_Attack_2 = "atk2";
    public const string m_strSpine_Event_Attack_3 = "atk3";
    public const string m_strSpine_Event_Attack_4 = "atk4";
    #endregion

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
