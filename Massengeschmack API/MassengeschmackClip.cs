using System;
using System.Collections.Generic;

namespace Massengeschmack_API
{
    public class MassengeschmackClip : MassengeschmackContent
    {
        protected List<File> files;
        /// <summary>
        /// Downloaddateien, verschiedene Formate (720p, 1080p, etc.) falls vorhanden.
        /// </summary>
        public File[] Files
        {
            get
            {
                return this.files.ToArray();
            }
        }
        public MassengeschmackClip(string identifier, int pid, string title, string pdesc, string img, string desc, string duration, int date, bool subscribed, string teaser, List<File> files)
            : base(identifier, pid, title, pdesc, img, desc, duration, date, subscribed, teaser)
        {
            this.files = files;
        }
        public class File
        {
            protected int size;
            /// <summary>
            /// Die Dateigröße in Bytes.
            /// </summary>
            public int Size
            {
                get
                {
                    return this.size;
                }
            }
            protected string t;
            /// <summary>
            /// Dateityp (film, music)
            /// </summary>
            public string Type
            {
                get
                {
                    return this.t;
                }
            }
            protected string size_readable;
            /// <summary>
            /// Dateigröße menschliche Representation.
            /// 
            /// Feld nicht garantiert vorhanden, Dann ist der String leer.
            /// </summary>
            public string SizeReadable
            {
                get
                {
                    return this.size_readable;
                }
            }
            protected string dimensions;
            /// <summary>
            /// Bildgröße (z.B. 1280x720)
            /// </summary>
            public string Dimensions
            {
                get
                {
                    return this.dimensions;
                }
            }
            protected string desc;
            /// <summary>
            /// Beschreibung der Bildgröße (z.B. HD 720p)
            /// </summary>
            public string Description
            {
                get
                {
                    return this.desc;
                }
            }
            protected string url;
            /// <summary>
            /// Download URL der Datei. Temporärer Zugang.
            /// </summary>
            public string URL
            {
                get
                {
                    return this.url;
                }
            }
            public File(int size, string t, string size_readable, string dimensions, string desc, string url)
            {
                this.size = size;
                this.t = t;
                this.size_readable = size_readable;
                this.dimensions = dimensions;
                this.desc = desc;
                this.url = url;
            }
        }
    }
}