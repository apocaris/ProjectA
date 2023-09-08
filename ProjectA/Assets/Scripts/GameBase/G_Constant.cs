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
    SKILL_SLOT_1_OPEN_ACCOUNT_LEVEL,                /*��ų����1 �������� ��������*/
    SKILL_SLOT_2_OPEN_ACCOUNT_LEVEL,                /*��ų����2 �������� ��������*/
    SKILL_SLOT_3_OPEN_ACCOUNT_LEVEL,                /*��ų����3 �������� ��������*/
    SKILL_SLOT_4_OPEN_ACCOUNT_LEVEL,                /*��ų����4 �������� ��������*/
    SKILL_SLOT_5_OPEN_ACCOUNT_LEVEL,                /*��ų����5 �������� ��������*/
    SKILL_SLOT_6_OPEN_ACCOUNT_LEVEL,                /*��ų����6 �������� ��������*/
    SKILL_PRESET_1_OPEN_DUNGEON_STAGE_ID,           /*��ų ������1 �������� ���� �������� ID*/
    SKILL_PRESET_2_OPEN_DUNGEON_STAGE_ID,           /*��ų ������2 �������� ���� �������� ID*/
    SKILL_PRESET_3_OPEN_DUNGEON_STAGE_ID,           /*��ų ������3 �������� ���� �������� ID*/
    SKILL_PRESET_4_OPEN_DUNGEON_STAGE_ID,           /*��ų ������4 �������� ���� �������� ID*/
    SKILL_PRESET_5_OPEN_DUNGEON_STAGE_ID,           /*��ų ������5 �������� ���� �������� ID*/
    ABILITY_RESET_COST,                             /*�����Ƽ ���� ���*/
    ABILITY_PROB_GROUP_ID,                          /*�����Ƽ Ȯ�� �׷� ID*/
    ABILITY_SLOT_COUNT,                             /*�����Ƽ ���� ��*/
    ABILITY_SLOT_1_OPEN_ACCOUNT_LEVEL,              /*�����Ƽ ����1 �������� ��������*/
    ABILITY_SLOT_2_OPEN_ACCOUNT_LEVEL,              /*�����Ƽ ����2 �������� ��������*/
    ABILITY_SLOT_3_OPEN_ACCOUNT_LEVEL,              /*�����Ƽ ����3 �������� ��������*/
    ABILITY_SLOT_4_OPEN_ACCOUNT_LEVEL,              /*�����Ƽ ����4 �������� ��������*/
    ABILITY_SLOT_5_OPEN_ACCOUNT_LEVEL,              /*�����Ƽ ����5 �������� ��������*/
    ABILITY_SLOT_6_OPEN_ACCOUNT_LEVEL,              /*�����Ƽ ����6 �������� ��������*/
    ABILITY_PRESET_1_OPEN_DUNGEON_STAGE_ID,         /*�����Ƽ ������1 �������� ���� �������� ID*/
    ABILITY_PRESET_2_OPEN_DUNGEON_STAGE_ID,         /*�����Ƽ ������2 �������� ���� �������� ID*/
    ABILITY_PRESET_3_OPEN_DUNGEON_STAGE_ID,         /*�����Ƽ ������3 �������� ���� �������� ID*/
    ABILITY_PRESET_4_OPEN_DUNGEON_STAGE_ID,         /*�����Ƽ ������4 �������� ���� �������� ID*/
    ABILITY_PRESET_5_OPEN_DUNGEON_STAGE_ID,         /*�����Ƽ ������5 �������� ���� �������� ID*/
    EXTRA_GEAR_START_LOAD_VALUE,                    /*����Ʈ�� ��� �ʱ� ���緮*/
    EXTRA_GEAR_INVENTORY_CAPACITY,                  /*����Ʈ�� ��� �κ��丮 ĭ ��*/
    EXTRA_GEAR_PRESET_1_OPEN_DUNGEON_STAGE_ID,      /*����Ʈ�� ��� ������1 �������� ���� �������� ID*/
    EXTRA_GEAR_PRESET_2_OPEN_DUNGEON_STAGE_ID,      /*����Ʈ�� ��� ������2 �������� ���� �������� ID*/
    EXTRA_GEAR_PRESET_3_OPEN_DUNGEON_STAGE_ID,      /*����Ʈ�� ��� ������3 �������� ���� �������� ID*/
    EXTRA_GEAR_PRESET_4_OPEN_DUNGEON_STAGE_ID,      /*����Ʈ�� ��� ������4 �������� ���� �������� ID*/
    EXTRA_GEAR_PRESET_5_OPEN_DUNGEON_STAGE_ID,      /*����Ʈ�� ��� ������5 �������� ���� �������� ID*/
    SUPPORT_UNIT_EQUIP_COUNT,                       /*�� ���� ��*/
    SUPPORT_UNIT_PRESET_1_OPEN_DUNGEON_STAGE_ID,    /*�� ������1 �������� ���� �������� ID*/
    SUPPORT_UNIT_PRESET_2_OPEN_DUNGEON_STAGE_ID,    /*�� ������2 �������� ���� �������� ID*/
    SUPPORT_UNIT_PRESET_3_OPEN_DUNGEON_STAGE_ID,    /*�� ������3 �������� ���� �������� ID*/
    SUPPORT_UNIT_PRESET_4_OPEN_DUNGEON_STAGE_ID,    /*�� ������4 �������� ���� �������� ID*/
    SUPPORT_UNIT_PRESET_5_OPEN_DUNGEON_STAGE_ID,    /*�� ������5 �������� ���� �������� ID*/
    SYSTHESIS_COST_EQUIPMENT_COUNT,                 /*��� ������ �ռ� �� �Ҹ����*/
    SUMMON_AD_EQUIPMENT_COUNT,                      /*��� : �����ȯ ����Ƚ��*/
    SUMMON_RUBY_EQUIPMENT_1_COUNT,                  /*��� : ����ȯ Ÿ��1 ����Ƚ��*/
    SUMMON_RUBY_EQUIPMENT_2_COUNT,                  /*��� : ����ȯ Ÿ��2 ����Ƚ��*/
    SUMMON_RUBY_EQUIPMENT_1_COST,                   /*��� : ����ȯ Ÿ��1 ���*/
    SUMMON_RUBY_EQUIPMENT_2_COST,                   /*��� : ����ȯ Ÿ��2 ���*/
    SUMMON_AD_SKILL_COUNT,                          /*��ų : �����ȯ ����Ƚ��*/
    SUMMON_RUBY_SKILL_1_COUNT,                      /*��ų : ����ȯ Ÿ��1 ����Ƚ��*/
    SUMMON_RUBY_SKILL_2_COUNT,                      /*��ų : ����ȯ Ÿ��2 ����Ƚ��*/
    SUMMON_RUBY_SKILL_1_COST,                       /*��ų : ����ȯ Ÿ��1 ���*/
    SUMMON_RUBY_SKILL_2_COST,                       /*��ų : ����ȯ Ÿ��2 ���*/
    SUMMON_AD_SUPPORT_UNIT_COUNT,                   /*�� : �����ȯ ����Ƚ��*/
    SUMMON_RUBY_SUPPORT_UNIT_1_COUNT,               /*�� : ����ȯ Ÿ��1 ����Ƚ��*/
    SUMMON_RUBY_SUPPORT_UNIT_2_COUNT,               /*�� : ����ȯ Ÿ��2 ����Ƚ��*/
    SUMMON_RUBY_SUPPORT_UNIT_1_COST,                /*�� : ����ȯ Ÿ��1 ���*/
    SUMMON_RUBY_SUPPORT_UNIT_2_COST,                /*�� : ����ȯ Ÿ��2 ���*/
    ARENA_BATTLE_TIME_S,                            /*�Ʒ��� �����ð� (S)*/
    OFFLINE_REWARD_SAVE_TIME_UNIT_M,                /*�������� ������ �ð����� (m)*/
    OFFLINE_REWARD_SAVE_MAX_TIME_M,                 /*�������� ������ �ִ�ð� (m)*/
    PET_POINT_MAX,                                  /*�� ����Ʈ �ִ밪*/
    MONSTER_KILL_GET_PET_POINT,                     /*���� óġ �� ȹ�� �� ����Ʈ*/
    PET_RIDE_TIME_S,                                /*�� ž�½ð�(��)*/
    PET_RIDE_MOVE_SPEED,                            /*�� ž�� �� �̵��ӵ�*/
    FINAL_GRADE_TYPE_EQUIPMENT,                     /* ��� ������ �ְ� ��� */
    FINAL_GRADE_TYPE_SKILL,                         /* ��ų �ְ� ��� */
    FINAL_GRADE_TYPE_SUPPORT_UNIT,                  /* �� �ְ� ��� */
    SUMMON_AD_COOLTIME_S,                           /* ���� ��ȯ ��Ÿ�� */
    CRAFT_COUNT_SKILL_TYPE_1,                       /* ���� : ��ų Ÿ�� 1 Ƚ�� */
    CRAFT_COUNT_SKILL_TYPE_2,                       /* ���� : ��ų Ÿ�� 2 Ƚ�� */
    CRAFT_COUNT_PET_TYPE_1,                         /* ���� : �� Ÿ�� 1 Ƚ�� */
    CRAFT_COUNT_PET_TYPE_2,                         /* ���� : �� Ÿ�� 2 Ƚ�� */
    CHANCE_PACKAGE_BUY_TIME_LIMIT_M,                /* ���� ��Ű�� : ���� ���� �ð� */
    CHANCE_PACKAGE_COOLTIME_M,                      /* ���� ��Ű�� : ���� ��Ÿ�� */
    CHANCE_PACKAGE_MAX_APPEAR_COUNT,                /* ���� ��Ű�� : �ִ� ���� Ƚ�� */
    RECOMMEND_PACKAGE_DISPLAY_MAX_COUNT,            /* ��õ ��Ű�� �ִ� ǥ�� ���� */
    CHANCE_PACKAGE_REMOVE_STAGE_ID,                 /* ���� ��Ű�� : ���� ���� Ŭ���� �������� ���̵� */
    SUMMON_AD_DAILY_LIMIT,                          /* ���� : ���� ��ȯ ���� ���� */
    DUNGEON_CEMETERY_STAGE_TIME_LIMIT_S,            /* Ÿõ���� ���� �������� �ð� ���� (��) */
    DUNGEON_TEMPLE_BOSS_CRAZY_TIME_S,               /* ����� ���� ���� ����ȭ �ð� (��) */
    POTENTIAL_PROB_GROUP_ID,                        /* ����ȿ�� Ȯ���׷� ID */
    SUMMON_RUBY_ARTIFACT_1_COUNT,                   /* ��Ƽ��Ʈ : �Ϲ� > ����ȯ Ÿ��1 ����Ƚ�� */
    SUMMON_RUBY_ARTIFACT_2_COUNT,                   /* ��Ƽ��Ʈ : �Ϲ� > ����ȯ Ÿ��2 ����Ƚ�� */
    SUMMON_RUBY_ARTIFACT_1_COST,                    /* ��Ƽ��Ʈ : �Ϲ� > ����ȯ Ÿ��1 ��� */
    SUMMON_RUBY_ARTIFACT_2_COST,                    /* ��Ƽ��Ʈ : �Ϲ� > ����ȯ Ÿ��2 ��� */
    SUMMON_TALENT_ARTIFACT_1_COUNT,                 /* ��Ƽ��Ʈ : ��� > �޶�Ʈ ��ȯ Ÿ��1 ����Ƚ�� */
    SUMMON_TALENT_ARTIFACT_2_COUNT,                 /* ��Ƽ��Ʈ : ��� > �޶�Ʈ ��ȯ Ÿ��2 ����Ƚ�� */
    SUMMON_TALENT_ARTIFACT_3_COUNT,                 /* ��Ƽ��Ʈ : ��� > �޶�Ʈ ��ȯ Ÿ��3 ����Ƚ�� */
    SUMMON_TALENT_ARTIFACT_1_COST,                  /* ��Ƽ��Ʈ : ��� > �޶�Ʈ ��ȯ Ÿ��1 ��� */
    SUMMON_TALENT_ARTIFACT_2_COST,                  /* ��Ƽ��Ʈ : ��� > �޶�Ʈ ��ȯ Ÿ��2 ��� */
    SUMMON_TALENT_ARTIFACT_3_COST,                  /* ��Ƽ��Ʈ : ��� > �޶�Ʈ ��ȯ Ÿ��3 ��� */
    SUMMON_PIECE_ARTIFACT_COUNT,                    /* ��Ƽ��Ʈ : ��� > ������ȯ ����Ƚ�� */
    SUMMON_PIECE_ARTIFACT_COST,                     /* ��Ƽ��Ʈ : ��� > ������ȯ ��� */
}

public enum GT_BaseStat
{
    // �Ϲ�
    None = 0,
    Base_Attack = 101,                                  // ���ݷ�
    Base_HP,                                            // ü��
    Base_Defense,                                       // ����
    Base_Recovery,                                      // ȸ����
    Base_Increase_Attack,                               // ���ݷ� ����
    Base_Increase_HP,                                   // ü�� ����
    Base_Increase_Defense,                              // ���� ����
    Base_Increase_Recovery,                             // ȸ���� ����
    Base_Decrease_HitDamage,                            // �ǰ� ����� ����
    Base_AttackSpeed,                                   // ���� �ӵ�
    Base_MoveSpeed,                                     // �̵� �ӵ�
    Base_Increase_SkillDamage,                          // ��ų ���� ����� ����
    Base_CriticalRate,                                  // ũ��Ƽ�� Ȯ��
    Base_CriticalDamage,                                // ũ��Ƽ�� �����
    Base_SuperCriticalRate,                             // ���� ũ��Ƽ�� Ȯ��
    Base_SuperCriticalDamage,                           // ���� ũ��Ƽ�� �����
    Base_HyperCriticalRate,                             // ������ ũ��Ƽ�� Ȯ��
    Base_HyperCriticalDamage,                           // ������ ũ��Ƽ�� �����
    Base_Increase_Attack_BaseDamage,                    // �⺻ ���� ����� ����
    Base_Increase_Attack_BaseMonsterDamage,             // �Ϲ� ���� ���� ����� ����
    Base_Increase_Attack_BossMonsterDamage,             // ���� ���� ���� ����� ����
    Base_Decrease_Hit_BaseMonsterDamage,                // �Ϲ� ���� �ǰ� ����� ����
    Base_Decrease_Hit_BossMonsterDamage,                // ���� ���� �ǰ� ����� ����
    Base_Increase_Gain_EquipmentItem,                   // ��� ������ ȹ��Ȯ�� ����
    Base_Increase_Gain_EquipmentEnchantItem,            // ��� ��ȭ��� ȹ��Ȯ�� ����
    Base_Increase_Gain_SkillEnchantItem,                // ��ų ��ȭ��� ȹ��Ȯ�� ����
    Base_Increase_Gain_GoldAmount,                      // ��� ȹ�淮 ����
    Base_Increase_Gain_ExpAmount,                       // ����ġ ȹ�淮 ����
    Base_DefensePenetration,                            // ������ (�Ʒ��������� ���������� ���)
    Base_Increase_BaseDamage_InArena,                   // �Ʒ������� �⺻���� ����� ����
    Base_Increase_SkillDamage_InArena,                  // �Ʒ������� ��ų���� ����� ����
    Base_Increase_CriticalDamage_InArena,               // �Ʒ������� ũ��Ƽ�� ����� ����
    Base_Increase_SuperCriticalDamage_InArena,          // �Ʒ������� ���� ũ��Ƽ�� ����� ����
    Base_Increase_HyperCriticalDamage_InArena,          // �Ʒ������� ������ ũ��Ƽ�� ����� ����
    Base_ExtraGear_Payload,                             // ����Ʈ�� ��� ��ȿ ž�緮
    // v1.0.015 �߰�
    Base_Decrease_BaseMonster_HP,                       // �Ϲ� ���� ü�� ����
    Base_Decrease_BossMonster_HP,                       // ���� ���� ü�� ����
    Base_Decrease_SkillCooltime,                        // ��ų ��Ÿ�� ����

    // ����
    Wing_Increase_Attack = 201,                         // ������ ���ݷ� ����
    Wing_Increase_HP,                                   // ������ ü�� ����
    Wing_Increase_Defense,                              // ������ ���� ����
    Wing_Decrease_HitDamage,                            // ������ �ǰ� ����� ����
    Wing_Increase_AttackSpeed,                          // ������ ���� �ӵ� ���� (�Ⱦ�����)
    Wing_Increase_MoveSpeed,                            // ������ �̵� �ӵ� ���� ()
    Wing_Increase_SkillDamage,                          // ������ ��ų ���� ����� ����
    Wing_Increase_CriticalDamage,                       // ������ ũ��Ƽ�� ����� ����
    Wing_Increase_SuperCriticalDamage,                  // ������ ���� ũ��Ƽ�� ����� ����
    Wing_Increase_HyperCriticalDamage,                  // ������ ������ ũ��Ƽ�� ����� ����
    Wing_Increase_Attack_BaseDamage,                    // ������ �⺻���� ����� ����
    Wing_Increase_Attack_BaseMonsterDamage,             // ������ �Ϲ� ���� ���� ����� ����
    Wing_Increase_Attack_BossMonsterDamage,             // ������ ���� ���� ���� ����� ����
    Wing_Decrease_Hit_BaseMonsterDamage,                // ������ �Ϲ� ���� �ǰ� ����� ����
    Wing_Decrease_Hit_BossMonsterDamage,                // ������ ���� ���� �ǰ� ����� ����
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
    Skill,                          // ��ų�޴� > ��ų
    Pet,                            // ��޴� (��������) > ��  (��������)
    Spawn_Weapon,                   // ��ȯ�޴� > ����
    Spawn_Skill,                    // ��ȯ�޴� > ��ų
    Spawn_Pet,                      // ��ȯ�޴� > �� (��������)
    Challenge_Normal_Dungeon,       // �����޴� > ���� > �Ϲ�
    Challenge_Special_Dungeon,      // �����޴� > ���� > Ư��
    Normal_Attendance,              // ���θ޴� > �⼮ (�Ϲ�)
    Shop_AD,                        // �����޴� > ��ȭ
    Wing_Wing,                      // �����޴� > ����
    Wing_Status,                    // �����޴� > �ɷ�ġ
    Contents_Event,                 // ������ > �̺�Ʈ
    Pass,                           // �н�
    Package_Shop_Limit,             // ���� > ��Ű�� > �Ϲ� > ����
    Package_Shop_Monthly,           // ���� > ��Ű�� > �Ϲ� > ����
    Package_Shop_Costume,           // ���� > ��Ű�� > �Ϲ� > �ڽ�Ƭ
    Package_Shop_Stepup,            // ���� > ��Ű�� > ���ܾ�
    Shop,                           // ����
    Event_Exchange_Shop,            // �̺�Ʈ > ��ȯ ����
    Event_Stage_Clear,              // �̺�Ʈ > �������� ����
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
    Complete_Level_Account,                                 // ���� : ���� �޼� o
    WatchAd,                                                // ���� : ��û x
    Playtime,                                               // �÷���Ÿ�� �޼� (��) o
    BattlePower,                                            // ������ ��ġ �޼� o
    Accumulated_Damage,                                     // ���� ����� �޼� x
    Reward_Attend,                                          // �⼮ ���� �ޱ� o
    Kill_Monster,                                           // ���� óġ o
    Clear_FieldStage,                                       // �ʵ� �������� Ŭ���� o
    Clear_NormalDungeon_1,                                  // ���� : ���� ����� Ŭ���� o
    Clear_NormalDungeon_2,                                  // ���� : ������ �� Ŭ���� o
    Clear_NormalDungeon_3,                                  // ���� : Ÿõ���� ���� Ŭ���� o
    Clear_SpecialDungeon_2,                                 // ���� : ����� ���� Ŭ���� o
    Clear_Dungeon,                                          // ���� Ŭ���� o
    Clear_FieldStage_First,                                 // �ʵ� �������� ���� Ŭ���� o
    Clear_Dungeon_Wing_First,                               // ������ �÷� �������� ���� Ŭ���� o
    Clear_NormalDungeon_1_First,                            // ���� : ���� ����� ���� Ŭ���� o
    Clear_NormalDungeon_2_First,                            // ���� : ������ �� ���� Ŭ���� o
    Clear_NormalDungeon_3_First,                            // ���� : Ÿõ���� ���� ���� Ŭ���� o
    Clear_SpecialDungeon_2_First,                           // ���� : ����� ��õ ���� Ŭ���� o
    Play_SpecialDungeon_1,                                  // ���� : ��ȣ���� ����â�� �÷��� o
    Play_Arena,                                             // �Ʒ��� �÷��� x
    Perfect_Clear_DailyMission,                             // ���Ϲ̼� : ��� �Ϸ� o
    Perfect_Clear_WeeklyMission,                            // �ְ��̼� : ��� �Ϸ� o
    Complete_Level_Base_Attack,                             // �ɷ�ġ : ���ݷ� ���� �޼� o
    Complete_Level_Base_HP,                                 // �ɷ�ġ : ü�� ���� �޼� o
    Complete_Level_Base_Defense,                            // �ɷ�ġ : ���� ���� �޼� o
    Complete_Level_Base_Recovery,                           // �ɷ�ġ : ȸ���� ���� �޼� o
    Complete_Level_Base_CriticalRate,                       // �ɷ�ġ : ũ��Ƽ�� Ȯ�� ���� �޼� o
    Complete_Level_Base_CriticalDamage,                     // �ɷ�ġ : ũ��Ƽ�� ����� ���� �޼� o
    Levelup_BaseStatus,                                     // �ɷ�ġ : ������ Ƚ�� o
    Complete_Level_BaseStatus,                              // �ɷ�ġ : ���� �޼� o
    Complete_Level_Property_Increase_Attack,                // Ư�� : ���ݷ� ���� ���� �޼� o
    Complete_Level_Property_Increase_HP,                    // Ư�� : ü�� ���� ���� �޼� o
    Complete_Level_Property_Increase_Defense,               // Ư�� : ���� ���� ���� �޼� o
    Complete_Level_Property_AttackSpeed,                    // Ư�� : ���� �ӵ� ���� ���� �޼� o
    Complete_Level_Property_MoveSpeed,                      // Ư�� : �̵� �ӵ� ���� ���� �޼� o
    Complete_Level_Property_Increase_Gain_GoldAmount,       // Ư�� : ��� ȹ�淮 ���� ���� �޼� o
    Complete_Level_Property_Increase_Gain_ExpAMount,        // Ư�� : ����ġ ȹ�淮 ���� ���� �޼� o
    Levelup_PropertyStatus,                                 // Ư�� : ������ Ƚ�� o
    Complete_Level_PropertyStatus,                          // Ư�� : ���� �޼� o
    Complete_Level_Wing_Increase_Attack,                    // ���� �ɷ�ġ : ������ ���ݷ� ���� ���� �޼� o
    Complete_Level_Wing_Increase_HP,                        // ���� �ɷ�ġ : ������ ü�� ���� ���� �޼� o
    Complete_Level_Wing_Increase_Defense,                   // ���� �ɷ�ġ : ������ ���� ���� ���� �޼� o
    Levelup_WingStatus,                                     // ���� �ɷ�ġ : ������ Ƚ�� o
    Complete_Level_WingStatus,                              // ���� �ɷ�ġ : ���� �޼� o
    Levelup_Gear,                                           // ��� ������ (������) ������ Ƚ�� o
    Levelup_Wing,                                           // ���� ������ Ƚ�� o
    Levelup_SupportUnit,                                    // �� ������ Ƚ�� o
    Levelup_Extragear,                                      // �� ������ Ƚ�� o
    Levelup_Gear_or_Extragear,                              // ��� ������ (������) or �� ������ Ƚ�� o
    Complete_Level_wing,                                    // ���� ���� �޼� o
    Complete_Level_SupportUnit,                             // �� ���� �޼� o
    Complete_Level_Extragear,                               // �� ���� �޼� o
    Levelup_Skill,                                          // ��ų ������ o
    Complete_Level_Skill,                                   // ��ų ���� �޼� o
    Equipped_Mount_Item,                                    // ��� ������ (������) ���� Ƚ�� o
    Equipped_Wing,                                          // ���� ���� Ƚ�� o
    Equip_Leader_Pet,                                       // ������ ���� o
    Ride_SupportUnit,                                       // �� ž�� Ƚ�� o
    Equipped_Extragear,                                     // �� ���� o
    Equipped_Skill,                                         // ��ų ���� o
    Overlevelup_Gear,                                       // ��� ������ (������) �ʿ� o
    Overlevelup_Wing,                                       // ���� �ʿ� o
    Overlevelup_SupportUnit,                                // �������� �ʿ� o
    Overlevelup_Rune,                                       // �� �ʿ� o
    Overlevelup_Skill,                                      // ��ų �ʿ� o
    Compose_Gear,                                           // ��� ������ (������) �ռ� o
    SetPotential_Weapon,                                    // ���� ����ȿ�� �缳�� x
    GetBestValue_Potential_Weapon,                          // ���� ����ȿ�� �ִ밪 ȹ�� x
    SetPotential_Costume,                                   // �ڽ�Ƭ ����ȿ�� �缳�� o
    GetBestValue_Potential_Costume,                         // �ڽ�Ƭ ����ȿ�� �ִ밪 ȹ�� o
    Enchant_Wing,                                           // ���� ��æƮ o
    GetClass_Enchant_Grade_Rare_Wing,                       // ����: ��� ��� �̻��� ��æƮ �ɼ� ȹ�� o
    GetClass_Enchant_Grade_Hero_Wing,                       // ����: ���� ��� �̻��� ��æƮ �ɼ� ȹ�� o
    GetClass_Enchant_Grade_Legend_Wing,                     // ����: ���� ��� �̻��� ��æƮ �ɼ� ȹ�� o
    GetClass_Enchant_Grade_Ancient_Wing,                    // ����: ��� ��� �̻��� ��æƮ �ɼ� ȹ�� o
    Reset_Ability,                                          // �����Ƽ �缳�� o
    Open_AbilitySlot,                                       // �����Ƽ ���� ���� o
    GetClass_Ability_Grade_Rare,                            // ��� ��� �̻��� �����Ƽ ȹ�� o
    GetClass_Ability_Grade_Hero,                            // ���� ��� �̻��� �����Ƽ ȹ�� o
    GetClass_Ability_Grade_Legend,                          // ���� ��� �̻��� �����Ƽ ȹ�� o
    GetClass_Ability_Grade_Ancient,                         // ��� ��� �̻��� �����Ƽ ȹ�� o
    Spawn_Gears,                                            // ��� ������ ��ȯ o
    Spawn_Skill,                                            // ��ų ��ȯ o
    Spawn_Pet,                                              // �� ��ȯ o
    Spawn,                                                  // ��ȯ o
    Decompose_Skill,                                        // ��ų ���� o
    Decompose_Rune,                                         // ����Ʈ�� ���� o
    Decompose_Pet,                                          // �������� ���� o
    Wakeup_Wing,                                            // ���� ����
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

    Guild = 101,                                            // ������ ���� : ���
    Prize,                                                  // ������ ���� : �����
    Exchange,                                               // ������ ���� : ��ȯ�� (�̺�Ʈ)

    Pass = 201,                                             // �н�
    Mileage = 301,                                          // ���ϸ���

    Ruby = 401,                                             // ��ȭ : ���
    Normal,                                                 // ��ȭ : �Ϲ�
    AD,                                                     // ��ȭ : ����

    First_Purchase = 501,                                   // Ư�� ��Ű�� : ù ���� �̺�Ʈ
    Chance,                                                 // Ư�� ��Ű�� : ����

    Limit = 601,                                            // �Ϲ� ��Ű�� : ����
    Daily,                                                  // �Ϲ� ��Ű�� : ����
    Weekly,                                                 // �Ϲ� ��Ű�� : �ְ�
    Monthly,                                                // �Ϲ� ��Ű�� : ����
    Costume,                                                // �Ϲ� ��Ű�� : �ڽ�Ƭ

    Stepup_Package = 701,                                   // ���۾� ��Ű��

    Exchange_Currency = 801,                                // �̺�Ʈ : ��ȯ�� ��ȭ
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

// ĳ���� ����Ʈ ���� Ÿ��
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
    // �⺻ ��� �̸�
    public const string m_strMotion_Idle = "idle";
    public const string m_strMotion_Move = "run";
    public const string m_strMotion_Attack = "atk";
    public const string m_strMotion_Die = "die";
    public const string m_strMotion_Hit = "hit";

    // ĳ���� ��� �̸�
    public const string m_strMotion_Atk_1 = "atk1";
    public const string m_strMotion_Atk_2 = "atk2";
    public const string m_strMotion_Atk_3 = "atk3";
    public const string m_strMotion_Atk_4 = "atk4";
    public const string m_strMotion_Dash = "dash";
    public const string m_strMotion_Skill_1 = "skill_1";
    public const string m_strMotion_Skill_2 = "skill_2";

    // ��Ÿ �̺�Ʈ �̸�
    public const string m_strSpine_Event_Attack_1 = "atk1";
    public const string m_strSpine_Event_Attack_2 = "atk2";
    public const string m_strSpine_Event_Attack_3 = "atk3";
    public const string m_strSpine_Event_Attack_4 = "atk4";

    // ����� �̺�Ʈ �̸�
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

    // �޸� Ǯ
    public const int m_iMonsterPoolSize = 40;
    public const int m_iDamageFontPoolSize = 25;

    // �ػ�
    public const int m_iScreenResoultion = 1080; //540 : qHD , 720 : HD
    public const int m_iFoldScreenResoultion = 1080; //1080 : FHD

    public const string m_strRegexPattern = @"\d+";

    // Last guide quest id
    public const int m_iLastGuideQuestID = 10065800;

    // �ӽ�
    //public const float m_fMonsterSpawnDelay = 0.3f;
    public const float m_fMonsterSpawnDelay = 3.0f;
    public const int m_iSpawnMonsterMaxCount = 40;
    public const int m_iSpawnOnceCount = 4;
}
