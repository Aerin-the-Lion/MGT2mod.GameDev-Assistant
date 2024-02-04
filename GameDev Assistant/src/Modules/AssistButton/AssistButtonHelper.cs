using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.Remoting.Messaging;

namespace GameDevAssistant.Modules.AssistButton
{
    public class AssistButtonHelper
    {
        /// <summary>
        /// buttonの状態をチェックする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsInteractable<T>(GameObject gameObject) where T : MonoBehaviour
        {
            T component = gameObject.GetComponent<T>();
            if (component != null)
            {
                if (component is Button button)
                {
                    return button.interactable;
                }
                if (component is Slider slider)
                {
                    return slider.interactable;
                }
                else if (component is Toggle toggle)
                {
                    return toggle.interactable;
                }
            }
            return false;
        }

        public static bool IsInteractableUIObjectByName<T>(GameObject[] objs, string name) where T : MonoBehaviour
        {
            // Find a child object with the specified name
            GameObject obj = FindUIObjectByName(objs, name);

            // Error handling if the child object is not found
            if (obj == null)
            {
                Debug.LogError($"Child object '{name}' not found in {obj.name}.");
                return false;
            }

            // Get the component of the specified type from the child object
            T component = obj.GetComponent<T>();
            if (component != null)
            {
                return IsComponentInteractable(component);
            }
            else
            {
                T componentInChild = obj.GetComponentInChildren<T>();
                //Debug.LogError($"The component of child of '{name}' not found in {obj.name}.");
                return IsComponentInteractable(componentInChild);
            }
            // If the component is not found or does not match the check condition, return false
        }

        private static bool IsComponentInteractable(Component component)
        {
            if (component is Button button)
            {
                return button.interactable;
            }
            if (component is Slider slider)
            {
                return slider.interactable;
            }
            else if (component is Toggle toggle)
            {
                return toggle.interactable;
            }
            else if (component is InputField input)
            {
                return input.interactable;
            }
            // Add here if you need to check for other component types

            // ここに到達した場合、サポートされていないコンポーネントタイプであるため、falseを返すか、
            // 他の適切なデフォルト値や処理をここで行う。
            return false;
        }

        public static GameObject FindUIObjectByName(GameObject[] uiObjects, string name)
        {
            if (uiObjects != null)
            {
                var obj = uiObjects.FirstOrDefault(o => o != null && o.name == name);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    Debug.LogError($"Object with name {name} not found.");
                }
            }
            else
            {
                Debug.LogError("uiObjects array is null.");
            }
            return null;
        }
    }
}
