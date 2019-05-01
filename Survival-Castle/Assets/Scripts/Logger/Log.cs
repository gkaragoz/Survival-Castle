using System;
using UnityEngine;

[System.Serializable]
public class Log {

    private LogType _logType;
    private string _body;
    private string _message;
    private DateTime _dateTime;

    public Log(string message, DateTime dateTime, LogType logType) {
        this._message = message;
        this._dateTime = dateTime;
        this._logType = logType;

        string _title = string.Empty;

        this._body = string.Format("[{0}] {1} {2}",
                            this._dateTime.ToLongTimeString(), 
                            _title,
                            this._message);
    }

    public string GetBody() {
        return this._body;
    }

    public string GetMessage() {
        return this._message;
    }

    public string GetFormattedDateTime() {
        return string.Format("{0:d/M/yyyy HH:mm:ss}", this._dateTime);
    }

}
