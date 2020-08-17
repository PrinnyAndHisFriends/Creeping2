using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICommand
{
    void Execute();

    void Cancel();
}

public class Command : ICommand
{
    public void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
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
}
