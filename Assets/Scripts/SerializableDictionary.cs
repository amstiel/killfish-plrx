using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
    [Serializable]
    public struct Core {
        public Core(TKey key, TValue value) {
            this.key = key;
            this.value = value;
        }

        [SerializeField] public TKey key;
        [SerializeField] public TValue value;
    }

    [SerializeField] private List<Core> values = new List<Core>();

    // save the dictionary to lists
    public void OnBeforeSerialize() {
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this) {
            values.Add(new Core(pair.Key, pair.Value));
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize() {
        this.Clear();

        for (int i = 0; i < values.Count; i++) {
            Add(values[i].key, values[i].value);
        }
    }
}

