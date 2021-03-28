using RH.Utilities.Processes.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.Processes
{
    public class ChainSyncItem : CustomProcess
    {
        private Action _action;

        public ChainSyncItem(Action action) : base(action.Method.Name)
        {
            _action = action;
        }

        public override void Execute()
        {
            _action.Invoke();
            Complete();
        }
    }
}