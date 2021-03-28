using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.UI.Extensions
{
    public static class StackExtensions
    {
        /// <summary> Вроде бы должно работать только со ссылочным типом данных </summary>
        public static void Remove<T>(this Stack<T> stack, T t)
        {
            if (stack.Count == 0)
            {
                Debug.LogError($"Stack is empty");
                return;
            }

            if (!stack.Contains(t))
            {
                Debug.LogError($"Stack doesn't contain element, which you try to remove");
                return;
            }

            T[] array = stack.ToArray();

            stack.Clear();

            foreach (T item in array)
                if (!item.Equals(t))
                    stack.Push(item);
        }
    }
}