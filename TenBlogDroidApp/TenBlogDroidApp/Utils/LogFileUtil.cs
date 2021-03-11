using System;
using Android.Content;
using Java.IO;
using Java.Lang;
using Environment = Android.OS.Environment;

namespace TenBlogDroidApp.Utils
{
    internal class LogFileUtil
    {
        private static LogFileUtil _instance;
        private readonly string _fileName;

        protected LogFileUtil(Context context)
        {
            _fileName = DateTime.Now.ToString("yyyy-MM-dd");
            var sdCardPath = context.GetExternalFilesDir(Environment.DirectoryPictures)?.AbsolutePath;
            var absFilePath = System.IO.Path.Combine(sdCardPath ?? string.Empty, _fileName);
            var logFolder = new File(absFilePath);
            if (!logFolder.Exists() && !logFolder.IsDirectory)
            {
                logFolder.Mkdir();
            }

            _fileName = $"{absFilePath}/{_fileName}.txt";
            var file = new File(_fileName);
            if (file.Exists()) return;
            try
            {
                file.CreateNewFile();
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }
        }

        public static LogFileUtil NewInstance(Context context)
        {
            return _instance ??= new LogFileUtil(context);
        }

        public void SaveLogToFile(string message)
        {
            new LogThread(_fileName, message).Start();
        }

        private class LogThread : Thread
        {
            private readonly string _fileName;
            private readonly string _message;

            public LogThread(string fileName, string message)
            {
                _fileName = fileName;
                _message = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + " " + message + "\n";
            }

            public override void Run()
            {
                LogWriter.WriteToFile(_fileName, _message);
            }
        }

        private static class LogWriter
        {
            public static async void WriteToFile(string fileName, string message)
            {
                try
                {
                    using var stream = new FileOutputStream(new File(fileName), true);
                    var msg = new Java.Lang.String(message);
                    await stream.WriteAsync(msg.GetBytes());
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }
    }
}