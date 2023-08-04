using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [Min(0.00f)] public float transitionSpeed = .2f;

    private string state;

    private const string IDLE = "Idle";
    private const string RUN = "Running";

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayIdleAnimation()
    {
        ChangeAnimation(IDLE);
    }

    public void PlayRunAnimation()
    {
        ChangeAnimation(RUN);
    }

    private void ChangeAnimation(string newState)
    {
        if (state == newState) return;

        state = newState;
        _animator.Play(state);
    }
}
