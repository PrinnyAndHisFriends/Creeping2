using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public abstract void Rotate();
    public abstract Area ToArea();
    public abstract string GetTexturePath();
}

public class EmptyCard : Card
{
    public override string GetTexturePath()
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

public class GapCard : Card
{
    public override string GetTexturePath()
    {
        return "GapCard";
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
    public override string GetTexturePath()
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

public class Way1_3Card : Card
{
    public override string GetTexturePath()
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
    public override string GetTexturePath()
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
    public override string GetTexturePath()
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
    public override string GetTexturePath()
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
    public override string GetTexturePath()
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
    public override string GetTexturePath()
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
    public override string GetTexturePath()
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

