﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBINEdit
{
    class MBINFile
    {
        public MBINHeader Header;
        private readonly IO _io;
        private readonly string _filePath;

        public MBINFile(string path)
        {
            _filePath = path;
            _io = new IO(path);
        }

        public bool Load()
        {
            _io.Stream.Position = 0;
            Header = _io.Reader.ReadStruct<MBINHeader>();
            return true;
        }

        public object GetData()
        {
            if (Header == null || String.IsNullOrEmpty(Header.TemplateName))
                return null;

            _io.Stream.Position = 0x60; // MBIN data start

            switch(Header.TemplateName)
            {
                case "cGcDebugOptions": // compiled GcDebugOptions
                    return _io.Reader.ReadStruct<cGcDebugOptions>();
            }

            return null; // struct not mapped yet
        }
    }
}