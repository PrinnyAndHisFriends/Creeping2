using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICommand
{
    void Execute();

    void Cancel();

    void Finish();
}

public class EmptyCommand : ICommand
{
    public void Cancel()
    {
        Finish();
    }

    public void Execute()
    {
        Finish();
    }

    public void Finish()
    {
        GameManager.Instance.UseCard();
    }
}

public class CompositeCommand : ICommand
{
    public void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Finish()
    {
        throw new System.NotImplementedException();
    }
}

public class RotateCommand : ICommand
{
    public void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Finish()
    {
        throw new System.NotImplementedException();
    }
}