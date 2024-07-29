using System;
using System.Collections;
using TMPro;
using UnityEngine;

public static class Util
{
    private static MonoBehaviour actioner = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();

    //-------------------FUNCTIONS---------------------
    public static void ExecuteAfterDelay(MonoBehaviour monoBehaviour, Action action, float delay)
    {
        monoBehaviour.StartCoroutine(DelayedExecution(action, delay));
    }
    public static void ExecuteAfterDelay(Action action, float delay)
    {
        actioner.StartCoroutine(DelayedExecution(action, delay));
    }

    public static void ExecuteParallel(MonoBehaviour monoBehaviour, Action action)
    {
        monoBehaviour.StartCoroutine(Execution(action));
    }
    public static void ExecuteParallel(Action action)
    {
        actioner.StartCoroutine(Execution(action));
    }

    public static void EmitParticles(MonoBehaviour monoBehaviour, ParticleSystem particles, int emitNr)
    {
        monoBehaviour.StartCoroutine(Emit(particles, emitNr));
    }
    public static void EmitParticles(ParticleSystem particles, int emitNr)
    {
        actioner.StartCoroutine(Emit(particles, emitNr));
    }
    public static void EmitParticlesAndDestroy(MonoBehaviour monoBehaviour, ParticleSystem particles, int emitNr)
    {
        monoBehaviour.StartCoroutine(EmitAndDestroy(particles, emitNr));
    }
    public static void EmitParticlesAndDestroy(ParticleSystem particles, int emitNr)
    {
        actioner.StartCoroutine(EmitAndDestroy(particles, emitNr));
    }
    //-------------------FUNCTIONS---------------------


    //-------------------IENUMERATORS---------------------
    private static IEnumerator DelayedExecution(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    private static IEnumerator Execution(Action action)
    {
        yield return null;
        action?.Invoke();
    }

    private static IEnumerator Emit(ParticleSystem particles, int emitNr)
    {
        yield return null;
        particles.Emit(emitNr);
    }

    private static IEnumerator EmitAndDestroy(ParticleSystem particles, int emitNr)
    {
        particles.Emit(emitNr);
        yield return new WaitForSeconds(particles.main.duration);
        GameObject.Destroy(particles.gameObject);
    }
    //-------------------IENUMERATORS---------------------
}
