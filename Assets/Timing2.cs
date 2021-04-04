//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TimingManager : MonoBehaviour
//{
//    private static TimingManager instance;
//    private List<UpdateBehaviour> updateBehaviours;

//    private void Awake()
//    {
//        instance = this;
//        updateBehaviours = new List<UpdateBehaviour>();
//    }

//    private void Update()
//    {
//        for (int i = 0; i < updateBehaviours.Count; i++)
//        {
//            if (updateBehaviours[i].Update())
//            {
//                updateBehaviours.RemoveAt(i);
//                i--;
//            }
//        }
//    }

//    public static void AddUpdateBehaviour(MonoBehaviour behaviour, Action onUpdate, Func<bool> updateActive = null)
//    {
//        var updateBehaviour = new UpdateBehaviour(behaviour, onUpdate, updateActive);
//        instance.updateBehaviours.Add(updateBehaviour);
//    }

//    public static void RemoveUpdateBehaviour(MonoBehaviour behaviour)
//    {
//        //for (int i = 0; i < instance.updateBehaviours.Count; i++)
//        //{
//        //    if (instance.updateBehaviours[i].GameObject == behaviour.gameObject)
//        //    {
//        //        instance.updateBehaviours.RemoveAt(i);
//        //        break;
//        //    }
//        //}
//    }

//    private struct UpdateBehaviour
//    {
//        public readonly GameObject GameObject;
//        public readonly bool Valid;

//        private readonly Action onUpdate;
//        private readonly Func<bool> updateActive;
//        private readonly int hashCode;

//        public UpdateBehaviour(MonoBehaviour behaviour, Action onUpdate, Func<bool> updateActive)
//        {
//            GameObject = behaviour.gameObject;
//            this.onUpdate = onUpdate;
//            this.updateActive = updateActive;
//            hashCode = GameObject.GetInstanceID();
//            Valid = GameObject != null && onUpdate != null && updateActive != null;
//        }

//        public bool Update()
//        {
//            if (GameObject == null)
//            {
//                return true;
//            }
//            else if (updateActive())
//            {
//                onUpdate();
//            }
//            return false;
//        }

//        public override int GetHashCode()
//        {
//            return hashCode;
//        }
//    }
//}

//public abstract class UpdateBehaviour : MonoBehaviour
//{
//    protected abstract void OnUpdate();
//    protected virtual bool UpdateActive() => true;

//    protected void OnEnable() { TimingManager.AddUpdateBehaviour(this, OnUpdate, UpdateActive); }

//    protected void OnDisable() { TimingManager.RemoveUpdateBehaviour(this); }

//    public override int GetHashCode() => GetInstanceID();
//}