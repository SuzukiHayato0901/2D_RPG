using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableDestroyTrigger : MonoBehaviour
    {
        bool calledDestroy = false;
        Subject<Unit> onDestroy;
        CompositeDisposable disposablesOnDestroy;

        [Obsolete("内部使用のみ。")]
        internal bool IsMonitoredActivate { get; set; }

        public bool IsActivated { get; private set; }

        /// <summary>
        /// OnDestroy が呼び出されたかどうかを確認します。
        /// このプロパティは GameObject が破棄されたことを保証するものではありません。
        /// GameObject が非アクティブの場合、OnDestroy は呼び出されません。
        /// </summary>
        public bool IsCalledOnDestroy { get { return calledDestroy; } }

        void Awake()
        {
            IsActivated = true;
        }

        /// <summary>
        /// MonoBehaviour が破棄されるときに呼び出されます。
        /// </summary>
        void OnDestroy()
        {
            if (!calledDestroy)
            {
                calledDestroy = true;
                if (disposablesOnDestroy != null) disposablesOnDestroy.Dispose();
                if (onDestroy != null) { onDestroy.OnNext(Unit.Default); onDestroy.OnCompleted(); }
            }
        }

        /// <summary>
        /// OnDestroy を強制的に呼び出します。このメソッドは内部で使用されます。
        /// </summary>
        public IObservable<Unit> OnDestroyAsObservable()
        {
            if (this == null) return Observable.Return(Unit.Default);
            if (calledDestroy) return Observable.Return(Unit.Default);
            return onDestroy ?? (onDestroy = new Subject<Unit>());
        }

        /// <summary>
        /// OnDestroy 時に破棄する IDisposable を追加します。
        /// </summary>
        public void ForceRaiseOnDestroy()
        {
            OnDestroy();
        }

        public void AddDisposableOnDestroy(IDisposable disposable)
        {
            if (calledDestroy)
            {
                disposable.Dispose();
                return;
            }

            if (disposablesOnDestroy == null) disposablesOnDestroy = new CompositeDisposable();
            disposablesOnDestroy.Add(disposable);
        }
    }
}