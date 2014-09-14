using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace TileMapDoc
{
    public delegate void LoadFileEventHandler(object sender, FileSerializationEventArgs e);
    public delegate void SaveFileEventHandler(object sender, FileSerializationEventArgs e);
    public class FileSerializationEventArgs : EventArgs
    {
        private IFormatter _formatter;
        private Stream _stream;
        private string _fileName;
        private bool _success;

        public IFormatter Formatter
        {
            get { return _formatter; }
        }
        public Stream FileSerializationStream
        {
            get { return _stream; }
        }
        public bool IsSucceeded
        {
            get { return _success; }
            set { _success = value; }
        }

        public FileSerializationEventArgs(IFormatter formatter, 
            Stream stream, string fileName)
        {
            _formatter = formatter;
            _stream = stream;
            _fileName = fileName;
            _success = false;
        }
    }

    public delegate void OpenFileEventHandler(object sender, OpenFileEventArgs e);
    public class OpenFileEventArgs : EventArgs
    {
        private string _fileName;
        private bool _success;
        public string FileName
        {
            get { return _fileName; }
        }
        public bool IsSucceeded
        {
            get { return _success; }
        }

        public OpenFileEventArgs(string fileName, bool success)
        {
            _fileName = fileName;
            _success = success;
        }
    }

    public delegate void MruFileOpenEventHandler(object sender,MruFileOpenEventArgs e);
    public class MruFileOpenEventArgs : EventArgs
    {
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
        }
        public MruFileOpenEventArgs(string fileName)
        {
            _fileName = fileName;
        }
    }

    public delegate void FileDragDropEventHandler(object sender,FileDragDropEventArgs e);
    public class FileDragDropEventArgs : EventArgs
    {
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
        }
        public FileDragDropEventArgs(string fileName)
        {
            _fileName = fileName;
        }
    }
}
