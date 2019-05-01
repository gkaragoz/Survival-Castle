using System;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour {

    #region Singleton

    public static LogManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    private Queue<Log> _logs = new Queue<Log>();

    private void Start() {
        Application.logMessageReceived += LogCallback;
    }

    private void OnDestroy() {
        Application.logMessageReceived -= LogCallback;
    }

    public void AddLog(string message, LogType type) {
        Log log = new Log(
            message,
            DateTime.Now,
            type);

        _logs.Enqueue(log);
    }

    private void LogCallback(string condition, string stackTrace, LogType type) {
        if (type == LogType.Log) {
            AddLog(condition, LogType.Log);
        } else {
            AddLog("LogType: " + type, type);
            AddLog("Condition: " + condition, type);
            AddLog("StackTrace: " + stackTrace, type);
        }
    }

}
