﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { Empty, PlayerOne, Two };

public static class Setting 
{
    public static int LIFE = 3;
    public static int MIN_AREA = -6;
    public static int MAX_AREA = 6;

    public static int AREA_DECK_COUNT = 124;
    public static int CARD_DECK_COUNT = 126;

    public static Vector3Int girlStartIndex = new Vector3Int(-3, 6, 0);
    public static Vector3Int houseStartIndex = new Vector3Int(0, 0, 0);
    public static Vector3Int antStartIndex = new Vector3Int(3, -6, 0);

    public enum CardType
    {
        Gap, Way1_2, Way1_3, Way1_4, Way1_2_4,
        Way1_3_4, Way1_3_5, Way1_3_4_6, Way1_2_3_4_5_6,
    }
    public enum AreaType
    {
        House, Ant, Rotate, Exchange, Present, Grass, EPresent,
        Gap, Way1_2, Way1_3, Way1_4, Way1_2_4,
        Way1_3_4, Way1_3_5, Way1_3_4_6, Way1_2_3_4_5_6,
    }

    public enum EntityType
    {
        Girl, Ant, House, EPresent
    }

    public static Dictionary<CardType, int> cardCount = new Dictionary<CardType, int> {
        { CardType.Gap, 6 },
        { CardType.Way1_2, 18 },
        { CardType.Way1_3, 18 },
        { CardType.Way1_4, 48 },
        { CardType.Way1_2_4, 6 },
        { CardType.Way1_3_4, 6 },
        { CardType.Way1_3_5, 6 },
        { CardType.Way1_3_4_6, 12 },
        { CardType.Way1_2_3_4_5_6, 6 },
    };

    public static Dictionary<AreaType, int> areaDeckCount = new Dictionary<AreaType, int> {
        { AreaType.House, 1 },
        { AreaType.Ant, 10 },
        { AreaType.Rotate, 24 },
        { AreaType.Exchange, 24 },
        { AreaType.EPresent, 16 },
        { AreaType.Gap, 12 },
        { AreaType.Grass, 38 },
    };
}
