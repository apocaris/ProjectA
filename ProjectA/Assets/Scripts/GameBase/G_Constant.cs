public enum GT_DataTable
{
    Text,

    Skill,
    SkillShape,
    SkillLevel,

    UnitMonster,
    MonsterStatus,

    Field,
    FieldContents,
    FieldReward,
    ExpAccount,
    ExpArenaTier,
    Attendance,
    Currency,
    Item,
    Reward,
    RankReward,
    BaseStat,
    PropertyStat,
    WingStat,
    OverLevel,
    Potential,
    Equipment,
    CharacterEquipEffect,
    Costume,
    Mission,
    Grade,
    CurrencyGroup,
    GlobalVariable,
    StartPossession,
    Probability,
    Ability,
    ExtraGear,
    SupportUnit,
    Stage,
    Dungeon,
    Loot,
    Monster,
    Status,
    Buff,
    Debuff,
    AdBuff,
    Wing,
    TreasureHouseRankReward,
    Product,
    Pass,
    PassProduct,
    Summon,
    OfflineReward,
    Shop,
    Banner,
    ContentsOpen,
    TestHallRankReward,
    Artifact,

    Count,
}

public enum GT_UserData
{
    None,
    Local,
    Base,
    Currency,
    Equipment,
}

public enum GT_GlobalVariable
{
    None = 0,
    SKILL_SLOT_1_OPEN_ACCOUNT_LEVEL,                /*스킬슬롯1 개방조건 계정레벨*/
    SKILL_SLOT_2_OPEN_ACCOUNT_LEVEL,                /*스킬슬롯2 개방조건 계정레벨*/
    SKILL_SLOT_3_OPEN_ACCOUNT_LEVEL,                /*스킬슬롯3 개방조건 계정레벨*/
    SKILL_SLOT_4_OPEN_ACCOUNT_LEVEL,                /*스킬슬롯4 개방조건 계정레벨*/
    SKILL_SLOT_5_OPEN_ACCOUNT_LEVEL,                /*스킬슬롯5 개방조건 계정레벨*/
    SKILL_SLOT_6_OPEN_ACCOUNT_LEVEL,                /*스킬슬롯6 개방조건 계정레벨*/
    SKILL_PRESET_1_OPEN_DUNGEON_STAGE_ID,           /*스킬 프리셋1 개방조건 던전 스테이지 ID*/
    SKILL_PRESET_2_OPEN_DUNGEON_STAGE_ID,           /*스킬 프리셋2 개방조건 던전 스테이지 ID*/
    SKILL_PRESET_3_OPEN_DUNGEON_STAGE_ID,           /*스킬 프리셋3 개방조건 던전 스테이지 ID*/
    SKILL_PRESET_4_OPEN_DUNGEON_STAGE_ID,           /*스킬 프리셋4 개방조건 던전 스테이지 ID*/
    SKILL_PRESET_5_OPEN_DUNGEON_STAGE_ID,           /*스킬 프리셋5 개방조건 던전 스테이지 ID*/
    ABILITY_RESET_COST,                             /*어빌리티 리셋 비용*/
    ABILITY_PROB_GROUP_ID,                          /*어빌리티 확률 그룹 ID*/
    ABILITY_SLOT_COUNT,                             /*어빌리티 슬롯 수*/
    ABILITY_SLOT_1_OPEN_ACCOUNT_LEVEL,              /*어빌리티 슬롯1 개방조건 계정레벨*/
    ABILITY_SLOT_2_OPEN_ACCOUNT_LEVEL,              /*어빌리티 슬롯2 개방조건 계정레벨*/
    ABILITY_SLOT_3_OPEN_ACCOUNT_LEVEL,              /*어빌리티 슬롯3 개방조건 계정레벨*/
    ABILITY_SLOT_4_OPEN_ACCOUNT_LEVEL,              /*어빌리티 슬롯4 개방조건 계정레벨*/
    ABILITY_SLOT_5_OPEN_ACCOUNT_LEVEL,              /*어빌리티 슬롯5 개방조건 계정레벨*/
    ABILITY_SLOT_6_OPEN_ACCOUNT_LEVEL,              /*어빌리티 슬롯6 개방조건 계정레벨*/
    ABILITY_PRESET_1_OPEN_DUNGEON_STAGE_ID,         /*어빌리티 프리셋1 개방조건 던전 스테이지 ID*/
    ABILITY_PRESET_2_OPEN_DUNGEON_STAGE_ID,         /*어빌리티 프리셋2 개방조건 던전 스테이지 ID*/
    ABILITY_PRESET_3_OPEN_DUNGEON_STAGE_ID,         /*어빌리티 프리셋3 개방조건 던전 스테이지 ID*/
    ABILITY_PRESET_4_OPEN_DUNGEON_STAGE_ID,         /*어빌리티 프리셋4 개방조건 던전 스테이지 ID*/
    ABILITY_PRESET_5_OPEN_DUNGEON_STAGE_ID,         /*어빌리티 프리셋5 개방조건 던전 스테이지 ID*/
    EXTRA_GEAR_START_LOAD_VALUE,                    /*엑스트라 장비 초기 적재량*/
    EXTRA_GEAR_INVENTORY_CAPACITY,                  /*엑스트라 장비 인벤토리 칸 수*/
    EXTRA_GEAR_PRESET_1_OPEN_DUNGEON_STAGE_ID,      /*엑스트라 장비 프리셋1 개방조건 던전 스테이지 ID*/
    EXTRA_GEAR_PRESET_2_OPEN_DUNGEON_STAGE_ID,      /*엑스트라 장비 프리셋2 개방조건 던전 스테이지 ID*/
    EXTRA_GEAR_PRESET_3_OPEN_DUNGEON_STAGE_ID,      /*엑스트라 장비 프리셋3 개방조건 던전 스테이지 ID*/
    EXTRA_GEAR_PRESET_4_OPEN_DUNGEON_STAGE_ID,      /*엑스트라 장비 프리셋4 개방조건 던전 스테이지 ID*/
    EXTRA_GEAR_PRESET_5_OPEN_DUNGEON_STAGE_ID,      /*엑스트라 장비 프리셋5 개방조건 던전 스테이지 ID*/
    SUPPORT_UNIT_EQUIP_COUNT,                       /*펫 장착 수*/
    SUPPORT_UNIT_PRESET_1_OPEN_DUNGEON_STAGE_ID,    /*펫 프리셋1 개방조건 던전 스테이지 ID*/
    SUPPORT_UNIT_PRESET_2_OPEN_DUNGEON_STAGE_ID,    /*펫 프리셋2 개방조건 던전 스테이지 ID*/
    SUPPORT_UNIT_PRESET_3_OPEN_DUNGEON_STAGE_ID,    /*펫 프리셋3 개방조건 던전 스테이지 ID*/
    SUPPORT_UNIT_PRESET_4_OPEN_DUNGEON_STAGE_ID,    /*펫 프리셋4 개방조건 던전 스테이지 ID*/
    SUPPORT_UNIT_PRESET_5_OPEN_DUNGEON_STAGE_ID,    /*펫 프리셋5 개방조건 던전 스테이지 ID*/
    SYSTHESIS_COST_EQUIPMENT_COUNT,                 /*장비 아이템 합성 시 소모수량*/
    SUMMON_AD_EQUIPMENT_COUNT,                      /*장비 : 광고소환 수행횟수*/
    SUMMON_RUBY_EQUIPMENT_1_COUNT,                  /*장비 : 루비소환 타입1 수행횟수*/
    SUMMON_RUBY_EQUIPMENT_2_COUNT,                  /*장비 : 루비소환 타입2 수행횟수*/
    SUMMON_RUBY_EQUIPMENT_1_COST,                   /*장비 : 루비소환 타입1 비용*/
    SUMMON_RUBY_EQUIPMENT_2_COST,                   /*장비 : 루비소환 타입2 비용*/
    SUMMON_AD_SKILL_COUNT,                          /*스킬 : 광고소환 수행횟수*/
    SUMMON_RUBY_SKILL_1_COUNT,                      /*스킬 : 루비소환 타입1 수행횟수*/
    SUMMON_RUBY_SKILL_2_COUNT,                      /*스킬 : 루비소환 타입2 수행횟수*/
    SUMMON_RUBY_SKILL_1_COST,                       /*스킬 : 루비소환 타입1 비용*/
    SUMMON_RUBY_SKILL_2_COST,                       /*스킬 : 루비소환 타입2 비용*/
    SUMMON_AD_SUPPORT_UNIT_COUNT,                   /*펫 : 광고소환 수행횟수*/
    SUMMON_RUBY_SUPPORT_UNIT_1_COUNT,               /*펫 : 루비소환 타입1 수행횟수*/
    SUMMON_RUBY_SUPPORT_UNIT_2_COUNT,               /*펫 : 루비소환 타입2 수행횟수*/
    SUMMON_RUBY_SUPPORT_UNIT_1_COST,                /*펫 : 루비소환 타입1 비용*/
    SUMMON_RUBY_SUPPORT_UNIT_2_COST,                /*펫 : 루비소환 타입2 비용*/
    ARENA_BATTLE_TIME_S,                            /*아레나 전투시간 (S)*/
    OFFLINE_REWARD_SAVE_TIME_UNIT_M,                /*오프라인 보상누적 시간단위 (m)*/
    OFFLINE_REWARD_SAVE_MAX_TIME_M,                 /*오프라인 보상누적 최대시간 (m)*/
    PET_POINT_MAX,                                  /*펫 포인트 최대값*/
    MONSTER_KILL_GET_PET_POINT,                     /*몬스터 처치 시 획득 펫 포인트*/
    PET_RIDE_TIME_S,                                /*펫 탑승시간(초)*/
    PET_RIDE_MOVE_SPEED,                            /*펫 탑승 시 이동속도*/
    FINAL_GRADE_TYPE_EQUIPMENT,                     /* 장비 아이템 최고 등급 */
    FINAL_GRADE_TYPE_SKILL,                         /* 스킬 최고 등급 */
    FINAL_GRADE_TYPE_SUPPORT_UNIT,                  /* 펫 최고 등급 */
    SUMMON_AD_COOLTIME_S,                           /* 광고 소환 쿨타임 */
    CRAFT_COUNT_SKILL_TYPE_1,                       /* 제작 : 스킬 타입 1 횟수 */
    CRAFT_COUNT_SKILL_TYPE_2,                       /* 제작 : 스킬 타입 2 횟수 */
    CRAFT_COUNT_PET_TYPE_1,                         /* 제작 : 펫 타입 1 횟수 */
    CRAFT_COUNT_PET_TYPE_2,                         /* 제작 : 펫 타입 2 횟수 */
    CHANCE_PACKAGE_BUY_TIME_LIMIT_M,                /* 찬스 패키지 : 구매 제한 시간 */
    CHANCE_PACKAGE_COOLTIME_M,                      /* 찬스 패키지 : 등장 쿨타임 */
    CHANCE_PACKAGE_MAX_APPEAR_COUNT,                /* 찬스 패키지 : 최대 등장 횟수 */
    RECOMMEND_PACKAGE_DISPLAY_MAX_COUNT,            /* 추천 패키지 최대 표시 수량 */
    CHANCE_PACKAGE_REMOVE_STAGE_ID,                 /* 찬스 패키지 : 제거 조건 클리어 스테이지 아이디 */
    SUMMON_AD_DAILY_LIMIT,                          /* 공통 : 광고 소환 일일 제한 */
    DUNGEON_CEMETERY_STAGE_TIME_LIMIT_S,            /* 타천사의 묘지 스테이지 시간 제한 (초) */
    DUNGEON_TEMPLE_BOSS_CRAZY_TIME_S,               /* 운명의 신전 보스 광폭화 시간 (초) */
    POTENTIAL_PROB_GROUP_ID,                        /* 잠재효과 확률그룹 ID */
    SUMMON_RUBY_ARTIFACT_1_COUNT,                   /* 아티팩트 : 일반 > 루비소환 타입1 수행횟수 */
    SUMMON_RUBY_ARTIFACT_2_COUNT,                   /* 아티팩트 : 일반 > 루비소환 타입2 수행횟수 */
    SUMMON_RUBY_ARTIFACT_1_COST,                    /* 아티팩트 : 일반 > 루비소환 타입1 비용 */
    SUMMON_RUBY_ARTIFACT_2_COST,                    /* 아티팩트 : 일반 > 루비소환 타입2 비용 */
    SUMMON_TALENT_ARTIFACT_1_COUNT,                 /* 아티팩트 : 고급 > 달란트 소환 타입1 수행횟수 */
    SUMMON_TALENT_ARTIFACT_2_COUNT,                 /* 아티팩트 : 고급 > 달란트 소환 타입2 수행횟수 */
    SUMMON_TALENT_ARTIFACT_3_COUNT,                 /* 아티팩트 : 고급 > 달란트 소환 타입3 수행횟수 */
    SUMMON_TALENT_ARTIFACT_1_COST,                  /* 아티팩트 : 고급 > 달란트 소환 타입1 비용 */
    SUMMON_TALENT_ARTIFACT_2_COST,                  /* 아티팩트 : 고급 > 달란트 소환 타입2 비용 */
    SUMMON_TALENT_ARTIFACT_3_COST,                  /* 아티팩트 : 고급 > 달란트 소환 타입3 비용 */
    SUMMON_PIECE_ARTIFACT_COUNT,                    /* 아티팩트 : 고급 > 조각교환 수행횟수 */
    SUMMON_PIECE_ARTIFACT_COST,                     /* 아티팩트 : 고급 > 조각교환 비용 */
}

public enum GT_BaseStat
{
    // 일반
    None = 0,
    Base_Attack = 101,                                  // 공격력
    Base_HP,                                            // 체력
    Base_Defense,                                       // 방어력
    Base_Recovery,                                      // 회복력
    Base_Increase_Attack,                               // 공격력 증가
    Base_Increase_HP,                                   // 체력 증가
    Base_Increase_Defense,                              // 방어력 증가
    Base_Increase_Recovery,                             // 회복력 증가
    Base_Decrease_HitDamage,                            // 피격 대미지 감소
    Base_AttackSpeed,                                   // 공격 속도
    Base_MoveSpeed,                                     // 이동 속도
    Base_Increase_SkillDamage,                          // 스킬 공격 대미지 증가
    Base_CriticalRate,                                  // 크리티컬 확률
    Base_CriticalDamage,                                // 크리티컬 대미지
    Base_SuperCriticalRate,                             // 슈퍼 크리티컬 확률
    Base_SuperCriticalDamage,                           // 슈퍼 크리티컬 대미지
    Base_HyperCriticalRate,                             // 하이퍼 크리티컬 확률
    Base_HyperCriticalDamage,                           // 하이퍼 크리티컬 대미지
    Base_Increase_Attack_BaseDamage,                    // 기본 공격 대미지 증가
    Base_Increase_Attack_BaseMonsterDamage,             // 일반 몬스터 공격 대미지 증가
    Base_Increase_Attack_BossMonsterDamage,             // 보스 몬스터 공격 대미지 증가
    Base_Decrease_Hit_BaseMonsterDamage,                // 일반 몬스터 피격 대미지 감소
    Base_Decrease_Hit_BossMonsterDamage,                // 보스 몬스터 피격 대미지 감소
    Base_Increase_Gain_EquipmentItem,                   // 장비 아이템 획득확률 증가
    Base_Increase_Gain_EquipmentEnchantItem,            // 장비 강화재료 획득확률 증가
    Base_Increase_Gain_SkillEnchantItem,                // 스킬 강화재료 획득확률 증가
    Base_Increase_Gain_GoldAmount,                      // 골드 획득량 증가
    Base_Increase_Gain_ExpAmount,                       // 경험치 획득량 증가
    Base_DefensePenetration,                            // 방어관통 (아레나에서만 제한적으로 사용)
    Base_Increase_BaseDamage_InArena,                   // 아레나에서 기본공격 대미지 증가
    Base_Increase_SkillDamage_InArena,                  // 아레나에서 스킬공격 대미지 증가
    Base_Increase_CriticalDamage_InArena,               // 아레나에서 크리티컬 대미지 증가
    Base_Increase_SuperCriticalDamage_InArena,          // 아레나에서 슈퍼 크리티컬 대미지 증가
    Base_Increase_HyperCriticalDamage_InArena,          // 아레나에서 하이퍼 크리티컬 대미지 증가
    Base_ExtraGear_Payload,                             // 엑스트라 장비 유효 탑재량
    // v1.0.015 추가
    Base_Decrease_BaseMonster_HP,                       // 일반 몬스터 체력 감소
    Base_Decrease_BossMonster_HP,                       // 보스 몬스터 체력 감소
    Base_Decrease_SkillCooltime,                        // 스킬 쿨타임 감소

    // 날개
    Wing_Increase_Attack = 201,                         // 날개의 공격력 증가
    Wing_Increase_HP,                                   // 날개의 체력 증가
    Wing_Increase_Defense,                              // 날개의 방어력 증가
    Wing_Decrease_HitDamage,                            // 날개의 피격 대미지 감소
    Wing_Increase_AttackSpeed,                          // 날개의 공격 속도 증가 (안쓸거임)
    Wing_Increase_MoveSpeed,                            // 날개의 이동 속도 증가 ()
    Wing_Increase_SkillDamage,                          // 날개의 스킬 공격 대미지 증가
    Wing_Increase_CriticalDamage,                       // 날개의 크리티컬 대미지 증가
    Wing_Increase_SuperCriticalDamage,                  // 날개의 슈퍼 크리티컬 대미지 증가
    Wing_Increase_HyperCriticalDamage,                  // 날개의 하이퍼 크리티컬 대미지 증가
    Wing_Increase_Attack_BaseDamage,                    // 날개의 기본공격 대미지 증가
    Wing_Increase_Attack_BaseMonsterDamage,             // 날개의 일반 몬스터 공격 대미지 증가
    Wing_Increase_Attack_BossMonsterDamage,             // 날개의 보스 몬스터 공격 대미지 증가
    Wing_Decrease_Hit_BaseMonsterDamage,                // 날개의 일반 몬스터 피격 대미지 감소
    Wing_Decrease_Hit_BossMonsterDamage,                // 날개의 보스 몬스터 피격 대미지 감소
}

public enum GT_PotentialCondition
{
    None = 0,
    StageClear,
    Level,
}

public enum GT_Grade
{
    None = 0,
    C,
    B,
    A,
    A_Plus,
    S,
    S_Plus,
    R,
    R_Plus,
    HR,
    HR_Plus,
    SR,
    SR_Plus,
    UR,
    UR_Plus,
    LR,
    LR_Plus,
}

public enum GT_Possession
{
    None = 0,
    Character,
}

public enum GT_Skill
{
    None,
    Range,
    Shoot,
    Summon,
    Buff,
}

public enum GT_SkillApply
{
    None = 0,
    Active,
    Passive,
}

public enum GT_CalcValueBasis
{
    None = 0,
    Initial,
    Level,
}

public enum GT_Attendance
{
    None = 0,
    First,
    Normal,
}

public enum GT_Refill
{
    None = 0,
    Second,
    Daily,
    Weekly,
}

public enum GT_NumberFormat
{
    None = 0,
    Normal,
    Cross,
    Plus,
    Minus,
    Max,
    Count,
}

public enum GT_Equipment
{
    None = 0,

    Weapon = 101,
    Helmet,
    Armor,
    Ring,
    Neckless,

    Wing = 300,
    Pet = 400,
    Costume = 500,
}

public enum GT_Rarity
{
    None = 0,
    Low,
    Middle,
    High,
    Highest,
}

public enum GT_Class
{
    None = 0,
    Normal,
    Rare,
    Hero,
    Legend,
    Ancient,
    Mystic,
}

public enum GT_Shortcut
{
    None = 0,
    Status,
    Property,
    Ability,
    Rune,
    Equipment,
    Skill,                          // 스킬메뉴 > 스킬
    Pet,                            // 펫메뉴 (지원유닛) > 펫  (지원유닛)
    Spawn_Weapon,                   // 소환메뉴 > 무기
    Spawn_Skill,                    // 소환메뉴 > 스킬
    Spawn_Pet,                      // 소환메뉴 > 펫 (지원유닛)
    Challenge_Normal_Dungeon,       // 도전메뉴 > 던전 > 일반
    Challenge_Special_Dungeon,      // 도전메뉴 > 던전 > 특수
    Normal_Attendance,              // 메인메뉴 > 출석 (일반)
    Shop_AD,                        // 상점메뉴 > 재화
    Wing_Wing,                      // 날개메뉴 > 날개
    Wing_Status,                    // 날개메뉴 > 능력치
    Contents_Event,                 // 콘텐츠 > 이벤트
    Pass,                           // 패스
    Package_Shop_Limit,             // 상점 > 패키지 > 일반 > 한정
    Package_Shop_Monthly,           // 상점 > 패키지 > 일반 > 월간
    Package_Shop_Costume,           // 상점 > 패키지 > 일반 > 코스튬
    Package_Shop_Stepup,            // 상점 > 패키지 > 스텝업
    Shop,                           // 상점
    Event_Exchange_Shop,            // 이벤트 > 교환 상점
    Event_Stage_Clear,              // 이벤트 > 스테이지 돌파
}

public enum GT_QuestCategory
{
    None = 0,
    Guide,
    Pass = 5,
}

public enum GT_QuestSubCategory
{
    None = 0,
    Daily = 111,
    Weekly,
    Repeat,
    Sub_Event_StageClear = 311,
}

public enum GT_Quest
{
    None = 0,
    Complete_Level_Account,                                 // 계정 : 레벨 달성 o
    WatchAd,                                                // 광고 : 시청 x
    Playtime,                                               // 플레이타임 달성 (분) o
    BattlePower,                                            // 전투력 수치 달성 o
    Accumulated_Damage,                                     // 누적 대미지 달성 x
    Reward_Attend,                                          // 출석 보상 받기 o
    Kill_Monster,                                           // 몬스터 처치 o
    Clear_FieldStage,                                       // 필드 스테이지 클리어 o
    Clear_NormalDungeon_1,                                  // 던전 : 왕의 무기고 클리어 o
    Clear_NormalDungeon_2,                                  // 던전 : 현자의 숲 클리어 o
    Clear_NormalDungeon_3,                                  // 던전 : 타천사의 묘지 클리어 o
    Clear_SpecialDungeon_2,                                 // 던전 : 운명의 신전 클리어 o
    Clear_Dungeon,                                          // 던전 클리어 o
    Clear_FieldStage_First,                                 // 필드 스테이지 최초 클리어 o
    Clear_Dungeon_Wing_First,                               // 날개의 시련 스테이지 최초 클리어 o
    Clear_NormalDungeon_1_First,                            // 던전 : 왕의 무기고 최초 클리어 o
    Clear_NormalDungeon_2_First,                            // 던전 : 현자의 숲 최초 클리어 o
    Clear_NormalDungeon_3_First,                            // 던전 : 타천사의 묘지 최초 클리어 o
    Clear_SpecialDungeon_2_First,                           // 던전 : 운명의 신천 최초 클리어 o
    Play_SpecialDungeon_1,                                  // 던전 : 수호자의 보물창고 플레이 o
    Play_Arena,                                             // 아레나 플레이 x
    Perfect_Clear_DailyMission,                             // 일일미션 : 모두 완료 o
    Perfect_Clear_WeeklyMission,                            // 주간미션 : 모두 완료 o
    Complete_Level_Base_Attack,                             // 능력치 : 공격력 레벨 달성 o
    Complete_Level_Base_HP,                                 // 능력치 : 체력 레벨 달성 o
    Complete_Level_Base_Defense,                            // 능력치 : 방어력 레벨 달성 o
    Complete_Level_Base_Recovery,                           // 능력치 : 회복력 레벨 달성 o
    Complete_Level_Base_CriticalRate,                       // 능력치 : 크리티컬 확률 레벨 달성 o
    Complete_Level_Base_CriticalDamage,                     // 능력치 : 크리티컬 대미지 레벨 달성 o
    Levelup_BaseStatus,                                     // 능력치 : 레벨업 횟수 o
    Complete_Level_BaseStatus,                              // 능력치 : 레벨 달성 o
    Complete_Level_Property_Increase_Attack,                // 특성 : 공격력 증가 레벨 달성 o
    Complete_Level_Property_Increase_HP,                    // 특성 : 체력 증가 레벨 달성 o
    Complete_Level_Property_Increase_Defense,               // 특성 : 방어력 증가 레벨 달성 o
    Complete_Level_Property_AttackSpeed,                    // 특성 : 공격 속도 증가 레벨 달성 o
    Complete_Level_Property_MoveSpeed,                      // 특성 : 이동 속도 증가 레벨 달성 o
    Complete_Level_Property_Increase_Gain_GoldAmount,       // 특성 : 골드 획득량 증가 레벨 달성 o
    Complete_Level_Property_Increase_Gain_ExpAMount,        // 특성 : 경험치 획득량 증가 레벨 달성 o
    Levelup_PropertyStatus,                                 // 특성 : 레벨업 횟수 o
    Complete_Level_PropertyStatus,                          // 특성 : 레벨 달성 o
    Complete_Level_Wing_Increase_Attack,                    // 날개 능력치 : 날개의 공격력 증가 레벨 달성 o
    Complete_Level_Wing_Increase_HP,                        // 날개 능력치 : 날개의 체력 증가 레벨 달성 o
    Complete_Level_Wing_Increase_Defense,                   // 날개 능력치 : 날개의 방어력 증가 레벨 달성 o
    Levelup_WingStatus,                                     // 날개 능력치 : 레벨업 횟수 o
    Complete_Level_WingStatus,                              // 날개 능력치 : 레벨 달성 o
    Levelup_Gear,                                           // 장비 아이템 (장착류) 레벨업 횟수 o
    Levelup_Wing,                                           // 날개 레벨업 횟수 o
    Levelup_SupportUnit,                                    // 펫 레벨업 횟수 o
    Levelup_Extragear,                                      // 룬 레벨업 횟수 o
    Levelup_Gear_or_Extragear,                              // 장비 아이템 (장착류) or 룬 레벨업 횟수 o
    Complete_Level_wing,                                    // 날개 레벨 달성 o
    Complete_Level_SupportUnit,                             // 펫 레벨 달성 o
    Complete_Level_Extragear,                               // 룬 레벨 달성 o
    Levelup_Skill,                                          // 스킬 레벨업 o
    Complete_Level_Skill,                                   // 스킬 레벨 달성 o
    Equipped_Mount_Item,                                    // 장비 아이템 (장착류) 장착 횟수 o
    Equipped_Wing,                                          // 날개 장착 횟수 o
    Equip_Leader_Pet,                                       // 리더펫 장착 o
    Ride_SupportUnit,                                       // 펫 탑승 횟수 o
    Equipped_Extragear,                                     // 룬 장착 o
    Equipped_Skill,                                         // 스킬 장착 o
    Overlevelup_Gear,                                       // 장비 아이템 (장착류) 초월 o
    Overlevelup_Wing,                                       // 날개 초월 o
    Overlevelup_SupportUnit,                                // 지원유닛 초월 o
    Overlevelup_Rune,                                       // 룬 초월 o
    Overlevelup_Skill,                                      // 스킬 초월 o
    Compose_Gear,                                           // 장비 아이템 (장착류) 합성 o
    SetPotential_Weapon,                                    // 무기 잠재효과 재설정 x
    GetBestValue_Potential_Weapon,                          // 무기 잠재효과 최대값 획득 x
    SetPotential_Costume,                                   // 코스튬 잠재효과 재설정 o
    GetBestValue_Potential_Costume,                         // 코스튬 잠재효과 최대값 획득 o
    Enchant_Wing,                                           // 날개 인챈트 o
    GetClass_Enchant_Grade_Rare_Wing,                       // 날개: 희귀 등급 이상의 인챈트 옵션 획득 o
    GetClass_Enchant_Grade_Hero_Wing,                       // 날개: 영웅 등급 이상의 인챈트 옵션 획득 o
    GetClass_Enchant_Grade_Legend_Wing,                     // 날개: 전설 등급 이상의 인챈트 옵션 획득 o
    GetClass_Enchant_Grade_Ancient_Wing,                    // 날개: 고대 등급 이상의 인챈트 옵션 획득 o
    Reset_Ability,                                          // 어빌리티 재설정 o
    Open_AbilitySlot,                                       // 어빌리티 슬롯 개방 o
    GetClass_Ability_Grade_Rare,                            // 희귀 등급 이상의 어빌리티 획득 o
    GetClass_Ability_Grade_Hero,                            // 영웅 등급 이상의 어빌리티 획득 o
    GetClass_Ability_Grade_Legend,                          // 전설 등급 이상의 어빌리티 획득 o
    GetClass_Ability_Grade_Ancient,                         // 고대 등급 이상의 어빌리티 획득 o
    Spawn_Gears,                                            // 장비 아이템 소환 o
    Spawn_Skill,                                            // 스킬 소환 o
    Spawn_Pet,                                              // 펫 소환 o
    Spawn,                                                  // 소환 o
    Decompose_Skill,                                        // 스킬 분해 o
    Decompose_Rune,                                         // 엑스트라 분해 o
    Decompose_Pet,                                          // 지원유닛 분해 o
    Wakeup_Wing,                                            // 날개 각성
}

public enum GT_QuestState
{
    Ongoing,
    Completed,
    Finished,
}

public enum GT_Reward
{
    None = 0,
    Item,
    Currency,
    Skill,
    RemoveAD,
}

// Content type of level-related open conditions
public enum GT_OpenCondition
{
    None,
    SkillSlot,
    AbilitySlot,
}

public enum GT_StartPossession
{
    None = 0,
    Item,
    Currency,
    Skill,
}

public enum GT_ProbTarget
{
    None = 0,
    Status,
    Exp,
    Currency,
    Gear,
    Rune,
    Wing,
    SupportUnit,
    Costume,
    Skill,
    Artifact,
}

public enum GT_RuneType
{
    None = 0,
    Core,
    Parts
}

public enum GT_BuffType
{
    None = 0,
    Increase_Attack,
    Increase_Defense,
    Increase_AttackSpeed,
    Increase_MoveSpeed,
    Increase_AttackDamage,
    Increase_SkillDamage,
    Increase_CriticalDamage,
    Increase_SuperCriticalDamage,
    Increase_HyperCriticalDamage,
}

public enum GT_Debuff
{
    None = 0,
    Decrease_Attack,
    Decrease_Defense,
    Decrease_AttackSpeed,
    Decrease_MoveSpeed,
}

public enum GT_Field
{
    None = 0,
    Stage = 101,
    Wing_Trial = 200,
    Dungeon_Normal = 201,
    Dungeon_Special = 301,
    Arena = 401,
}

public enum GT_FieldSub
{
    None = 0,
    Stage_Normal = 101,
    Stage_Conquest = 102,
    TrialOfWins = 200,
    Dungeon_Normal_1 = 201,
    Dungeon_Normal_2 = 202,
    Dungeon_Normal_3 = 203,
    Dungeon_Special_1 = 301,
    Dungeon_Special_2 = 302,
    Dungeon_Special_3 = 303,
    Arena_1 = 401,
}

public enum GT_FieldLoot
{
    None = 0,
    Item,
    Currency,
    Exp,
}

public enum GT_Monster
{
    None = 0,
    Normal,
    Boss,
    Count,
}

public enum GT_WakeupResource
{
    None = 0,
    Weapon,
    Helmet,
    Armor,
    Ring,
    Neckless,
    SupportUnit,
    Skill,
}

public enum GT_Product
{
    None = 0,

    Guild = 101,                                            // 콘텐츠 상점 : 길드
    Prize,                                                  // 콘텐츠 상점 : 현상금
    Exchange,                                               // 콘텐츠 상점 : 교환소 (이벤트)

    Pass = 201,                                             // 패스
    Mileage = 301,                                          // 마일리지

    Ruby = 401,                                             // 재화 : 루비
    Normal,                                                 // 재화 : 일반
    AD,                                                     // 재화 : 광고

    First_Purchase = 501,                                   // 특수 패키지 : 첫 구매 이벤트
    Chance,                                                 // 특수 패키지 : 찬스

    Limit = 601,                                            // 일반 패키지 : 한정
    Daily,                                                  // 일반 패키지 : 일일
    Weekly,                                                 // 일반 패키지 : 주간
    Monthly,                                                // 일반 패키지 : 월간
    Costume,                                                // 일반 패키지 : 코스튬

    Stepup_Package = 701,                                   // 스템업 패키지

    Exchange_Currency = 801,                                // 이벤트 : 교환소 재화
}

public enum GT_ProductLimit
{
    None = 0,
    Account,
    Daily,
    Weekly,
    Monthly,
}

public enum GT_Pass
{
    None = 0,
    Monsterkill,
    Growth,
    Stage,
    Attendence,
}

public enum GT_PassCategory
{
    None = 0,
    Season,
    Normal,
    Event,
}

public enum GT_PassReward
{
    Free = 0,
    Pay,
}

public enum GT_Summon
{
    None = 0,
    Weapon,
    Helmet,
    Armor,
    Ring,
    Neckless,
    Skill,
    SupportUnit,
    Artifact_Normal,
    Artifact_Advanced,
    Artifact_Exchange,
}

public enum GT_OfflineReward
{
    None = 0,
    Item,
    Currency,
    Exp,
}

public enum GT_Banner
{
    None = 0,
    Shop_Recommend_Product,
    Popup_Package,
    Event,
}

public enum GT_RankReward
{
    None = 0,
    Arena,
    TreasureHouse,
}

public enum GT_ContentsOpen
{
    None = 0,
    AccountLevel,
    Stage,
    GuideQuest,
}

public enum GT_Artifact
{
    None = 0,
    Normal,
    Advanced,
}

public enum GT_Pool
{
    Monster,
    DamageFont,
    UIDrop,
}

public enum GT_Direction
{
    Left,
    Right,
}

public enum GT_VerticalPos
{
    Top,
    Bottom,
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

public enum GT_Damage
{
    None = 0,
    Normal,
    Critical,
    SuperCritical,
    HyperCritical,
}

public enum GT_Currency
{
    None,
    Gold,
    Ruby,
}

public enum GT_UI
{
    None,
    Growth,
    Equipment,
    Skill,
    Pet,
}

public enum GT_CurrencyDisplay
{
    None = 0,
    Number,
    Alphabet,
}

public enum GT_UISizeTarget
{
    None,
    Widget,
    BoxCollider,
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

    #region UI object names
    public const string m_strUIDamageFont = "Prefabs/UI/Component/ui_ingame_damage_font";
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

    // 대미지 이벤트 이름
    public const string m_strSpine_Event_Damage = "dmg";
    #endregion

    #region SaveKey

    public const string m_strSaveKey_Account = "account";
    public const string m_strSaveKey_Base = "base";
    public const string m_strSaveKey_Currency = "currency";
    public const string m_strSaveKey_Status = "status";
    public const string m_strSaveKey_Loadout = "loadout";
    public const string m_strSaveKey_Equipment = "equipment";
    public const string m_strSaveKey_SkillLoad = "skillload";
    public const string m_strSaveKey_SkillSet = "skillset";
    public const string m_strSaveKey_Potential = "potential";
    public const string m_strSaveKey_Quest = "quest";
    public const string m_strSaveKey_Local = "local";
    public const string m_strSaveKey_Play = "play";
    public const string m_strSaveKey_NetworkFlag = "networkflg";

    #endregion

    // 메모리 풀
    public const int m_iMonsterPoolSize = 40;
    public const int m_iDamageFontPoolSize = 25;

    // 해상도
    public const int m_iScreenResoultion = 1080; //540 : qHD , 720 : HD
    public const int m_iFoldScreenResoultion = 1080; //1080 : FHD

    public const string m_strRegexPattern = @"\d+";

    // Last guide quest id
    public const int m_iLastGuideQuestID = 10065800;

    // 임시
    //public const float m_fMonsterSpawnDelay = 0.3f;
    public const float m_fMonsterSpawnDelay = 3.0f;
    public const int m_iSpawnMonsterMaxCount = 40;
    public const int m_iSpawnOnceCount = 4;
}
