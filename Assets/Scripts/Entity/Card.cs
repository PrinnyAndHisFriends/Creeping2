using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Card
{
    public abstract void Rotate();
    public abstract Area ToArea();
    public abstract Tile GetTile();
}

public class EmptyCard : Card
{
    public override Tile GetTile()
    {
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}

public class GapCard : Card
{
    public override Tile GetTile()
    {
        return TileManager.Instance.gap;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new GapArea();
    }
}

public class Way1_2Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_2_a;
            case 1:
                return TileManager.Instance.way1_2_b;
            case 2:
                return TileManager.Instance.way1_2_c;
            case 3:
                return TileManager.Instance.way1_2_d;
            case 4:
                return TileManager.Instance.way1_2_e;
            case 5:
                return TileManager.Instance.way1_2_f;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 6;
    }

    public override Area ToArea()
    {
        return new Way1_2Area(GetTile());
    }
}

public class Way1_3Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_3_a;
            case 1:
                return TileManager.Instance.way1_3_b;
            case 2:
                return TileManager.Instance.way1_3_c;
            case 3:
                return TileManager.Instance.way1_3_d;
            case 4:
                return TileManager.Instance.way1_3_e;
            case 5:
                return TileManager.Instance.way1_3_f;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 6;
    }

    public override Area ToArea()
    {
        return new Way1_3Area(GetTile());
    }
}

public class Way1_4Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_4_a;
            case 1:
                return TileManager.Instance.way1_4_b;
            case 2:
                return TileManager.Instance.way1_4_c;
            case 3:
                return TileManager.Instance.way1_4_d;
            case 4:
                return TileManager.Instance.way1_4_e;
            case 5:
                return TileManager.Instance.way1_4_f;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 6;
    }

    public override Area ToArea()
    {
        return new Way1_4Area(GetTile());
    }
}

public class Way1_2_4Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_2_4_a;
            case 1:
                return TileManager.Instance.way1_2_4_b;
            case 2:
                return TileManager.Instance.way1_2_4_c;
            case 3:
                return TileManager.Instance.way1_2_4_d;
            case 4:
                return TileManager.Instance.way1_2_4_e;
            case 5:
                return TileManager.Instance.way1_2_4_f;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 6;
    }

    public override Area ToArea()
    {
        return new Way1_2_4Area(GetTile());
    }
}


public class Way1_3_4Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_3_4_a;
            case 1:
                return TileManager.Instance.way1_3_4_b;
            case 2:
                return TileManager.Instance.way1_3_4_c;
            case 3:
                return TileManager.Instance.way1_3_4_d;
            case 4:
                return TileManager.Instance.way1_3_4_e;
            case 5:
                return TileManager.Instance.way1_3_4_f;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 6;
    }

    public override Area ToArea()
    {
        return new Way1_3_4Area(GetTile());
    }
}


public class Way1_3_5Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_3_5_a;
            case 1:
                return TileManager.Instance.way1_3_5_b;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 2;
    }

    public override Area ToArea()
    {
        return new Way1_3_5Area(GetTile());
    }
}


public class Way1_3_4_6Card : Card
{
    int i = 0;
    public override Tile GetTile()
    {
        switch (i)
        {
            case 0:
                return TileManager.Instance.way1_3_4_6_a;
            case 1:
                return TileManager.Instance.way1_3_4_6_b;
            case 2:
                return TileManager.Instance.way1_3_4_6_c;
            default:
                break;
        }
        return TileManager.Instance.empty;
    }

    public override void Rotate()
    {
        i++;
        i %= 3;
    }

    public override Area ToArea()
    {
        return new Way1_3_4_6Area(GetTile());
    }
}


public class Way1_2_3_4_5_6Card : Card
{
    public override Tile GetTile()
    {
        return TileManager.Instance.way1_2_3_4_5_6;
    }

    public override void Rotate()
    {

    }

    public override Area ToArea()
    {
        return new Way1_2_3_4_5_6Area(GetTile());
    }
}

