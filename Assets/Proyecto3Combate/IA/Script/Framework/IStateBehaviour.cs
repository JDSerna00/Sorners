using UnityEngine;
public interface IStateBehavior
{
    void OnEnter(object context);

    void OnUpdate();
}