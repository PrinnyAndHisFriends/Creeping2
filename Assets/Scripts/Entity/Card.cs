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
        return new GapArea(GetTile());
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
        return new EmptyArea();
    }
}

public class Way1_3Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}

public class Way1_4Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}

public class Way1_2_4Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}


public class Way1_3_4Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}


public class Way1_3_5Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}


public class Way1_3_4_6Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}


public class Way1_2_3_4_5_6Card : Card
{
    public override Tile GetTile()
    {
        return null;
    }

    public override void Rotate()
    {
    }

    public override Area ToArea()
    {
        return new EmptyArea();
    }
}

