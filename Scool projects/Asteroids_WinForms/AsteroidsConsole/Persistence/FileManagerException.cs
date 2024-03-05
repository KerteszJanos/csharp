﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.WinForms.Persistence
{
    internal class FileManagerException : Exception
    {
        public FileManagerException()
        {
        }

        public FileManagerException(string? message) : base(message)
        {
        }

        public FileManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FileManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
