using RH.Utilities.Processes.Base;
using RH.Utilities.Processes.Executing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.Processes
{
    public class ChainProcess : CustomProcess
    {
        private static SerialProcessExecutor executor
        {
            get
            {
                if (!_executor)
                    CreateLauncher();

                return _executor;
            }
        }
        private static SerialProcessExecutor _executor;

        public event Action OnCompleted;

        private readonly List<ICustomProcess> items = new List<ICustomProcess>();

        public ChainProcess(string name) : base(name) { }

        public override void Complete() => SetState(ProcessState.Complete);

        public override void Execute()
        {
            SetState(ProcessState.Execute);
            executor.ExecuteProcess(Executing());
        }

        public override void Restart() => SetState(ProcessState.Wait);

        public void AddAsyncMethod(Action<Action> action) => AddItem(new ChainAsyncItem(action));
        public void AddSyncMethod(Action action) => AddItem(new ChainSyncItem(action));
        public void AddSubChain(ChainProcess chainProcess) => AddItem(chainProcess);
        public void AddMultiItem(ChainMultiItem item) => AddItem(item);

        private IEnumerator Executing()
        {
            foreach (var item in items)
            {
                item.Execute();
                yield return new WaitUntil(() => item.IsComplete);
            }

            OnCompleted?.Invoke();
        }

        private void AddItem(ICustomProcess item) => items.Add(item);

        private static void CreateLauncher()
        {
            var launcherGO = new GameObject("SerialProcessExecutor");
            _executor = launcherGO.AddComponent<SerialProcessExecutor>();
        }
    }
}