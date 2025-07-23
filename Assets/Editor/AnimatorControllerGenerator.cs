using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimatorControllerGenerator
{
    [MenuItem("Tools/Generate Character Animator Controller")]
    public static void GenerateAnimatorController()
    {
        string path = "Assets/Animator/Character.controller";
        var controller = AnimatorController.CreateAnimatorControllerAtPath(path);

        // Parámetros
        controller.AddParameter("Horizontal", AnimatorControllerParameterType.Float);
        controller.AddParameter("Vertical", AnimatorControllerParameterType.Float);
        controller.AddParameter("Speed", AnimatorControllerParameterType.Float);

        // Estados
        var rootStateMachine = controller.layers[0].stateMachine;

        var idle_up = rootStateMachine.AddState("idle_up");
        var idle_down = rootStateMachine.AddState("idle_down");
        var idle_left = rootStateMachine.AddState("idle_left");
        var idle_right = rootStateMachine.AddState("idle_right");
        var walk_up = rootStateMachine.AddState("walk_up");
        var walk_down = rootStateMachine.AddState("walk_down");
        var walk_left = rootStateMachine.AddState("walk_left");
        var walk_right = rootStateMachine.AddState("walk_right");

        // Helper para transiciones
        void AddTransition(AnimatorState fromState, AnimatorState toState, (string, AnimatorConditionMode, float)[] conditions)
        {
            var t = fromState.AddTransition(toState);
            t.hasExitTime = false;
            t.hasFixedDuration = true;
            t.duration = 0.05f;
            foreach (var cond in conditions)
                t.AddCondition(cond.Item2, cond.Item3, cond.Item1);
        }

        // From AnyState to Walk
        var t = rootStateMachine.AddAnyStateTransition(walk_up);
        t.AddCondition(AnimatorConditionMode.Greater, 0.1f, "Vertical");
        t.AddCondition(AnimatorConditionMode.Greater, 0.01f, "Speed");

        t = rootStateMachine.AddAnyStateTransition(walk_down);
        t.AddCondition(AnimatorConditionMode.Less, -0.1f, "Vertical");
        t.AddCondition(AnimatorConditionMode.Greater, 0.01f, "Speed");

        t = rootStateMachine.AddAnyStateTransition(walk_left);
        t.AddCondition(AnimatorConditionMode.Less, -0.1f, "Horizontal");
        t.AddCondition(AnimatorConditionMode.Greater, 0.01f, "Speed");

        t = rootStateMachine.AddAnyStateTransition(walk_right);
        t.AddCondition(AnimatorConditionMode.Greater, 0.1f, "Horizontal");
        t.AddCondition(AnimatorConditionMode.Greater, 0.01f, "Speed");

        // From Walk to Idle
        AddTransition(walk_up, idle_up, new[] { ("Speed", AnimatorConditionMode.Less, 0.01f) });
        AddTransition(walk_down, idle_down, new[] { ("Speed", AnimatorConditionMode.Less, 0.01f) });
        AddTransition(walk_left, idle_left, new[] { ("Speed", AnimatorConditionMode.Less, 0.01f) });
        AddTransition(walk_right, idle_right, new[] { ("Speed", AnimatorConditionMode.Less, 0.01f) });

        AssetDatabase.SaveAssets();
        Debug.Log("Animator Controller generated at: " + path);
    }
}
