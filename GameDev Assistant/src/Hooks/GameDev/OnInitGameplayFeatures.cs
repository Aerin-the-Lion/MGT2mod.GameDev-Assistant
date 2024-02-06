using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAssistant
{
    public partial class Hooks
    {
        //[Info   : Unity Log] BUTTON_Search took 4154 milliseconds
        [HarmonyPatch(typeof(Menu_DevGame), "BUTTON_Search")]
        public class OnBUTTON_Search
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnBUTTON_Search)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        //[Info: Unity Log] OnInit_GameplayFeatures took 28 milliseconds : First time
        // [Info: Unity Log] OnInit_GameplayFeatures took 4110 milliseconds : Second time
        [HarmonyPatch(typeof(Menu_DevGame), "Init_GameplayFeatures")]
        public class OnInit_GameplayFeatures
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnInit_GameplayFeatures)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        //[Info: Unity Log] OnSortChildrenByName took 0 milliseconds
        [HarmonyPatch(typeof(mainScript), "SortChildrenByName")]
        public class OnSortChildrenByName
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnSortChildrenByName)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        //[Info   : Unity Log] OnBUTTON_Click took 28 milliseconds これが71個のFeaturesだと、142回呼ばれる！！！！
        [HarmonyPatch(typeof(Item_DevGame_GameplayFeature), "BUTTON_Click")]
        public class OnBUTTON_Click
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnBUTTON_Click)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        [HarmonyPatch(typeof(Menu_DevGame), "DisableGameplayFeature")]
        public class OnDisableGameplayFeature
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnDisableGameplayFeature)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        //OnCalcDevCosts took 32 milliseconds こいつが犯人
        [HarmonyPatch(typeof(Menu_DevGame), "CalcDevCosts")]
        public class OnCalcDevCosts
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnCalcDevCosts)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        //OnGetGesamtDevPoints took 0 milliseconds
        [HarmonyPatch(typeof(Menu_DevGame), "GetGesamtDevPoints")]
        public class OnGetGesamtDevPoints
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnGetGesamtDevPoints)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

        //OnUpdateGesamtGameplayFeatures took 0 milliseconds
        [HarmonyPatch(typeof(Menu_DevGame), "UpdateGesamtGameplayFeatures")]
        public class OnUpdateGesamtGameplayFeatures
        {
            static void Prefix(ref Stopwatch __state)
            {
                // メソッド実行前にStopwatchを開始
                __state = new Stopwatch();
                __state.Start();
            }

            static void Postfix(ref Stopwatch __state)
            {
                // メソッド実行後にStopwatchを停止し、経過時間をログに出力
                __state.Stop();
                UnityEngine.Debug.Log($"{nameof(OnUpdateGesamtGameplayFeatures)} took {__state.ElapsedMilliseconds} milliseconds");
            }
        }

    }
}
