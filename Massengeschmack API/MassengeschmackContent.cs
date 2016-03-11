using System;

namespace Massengeschmack_API
{
    public abstract class MassengeschmackContent
    {
        private static DateTime utcEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        protected string identifier;
        /// <summary>
        /// Clip ID
        /// </summary>
        public string Identifier
        {
            get
            {
                return this.identifier;
            }
        }
        protected int pid;
        /// <summary>
        /// Projekt ID
        /// </summary>
        public int ProjectID
        {
            get
            {
                return this.pid;
            }
        }
        protected string title;
        /// <summary>
        /// Clip Titel
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
        }
        protected string pdesc;
        /// <summary>
        /// Projekt Titel
        /// </summary>
        public string ProjectDescription
        {
            get
            {
                return this.pdesc;
            }
        }
        protected string img;
        /// <summary>
        /// Vorschaubild URL
        /// </summary>
        public string Image
        {
            get
            {
                return this.img;
            }
        }
        protected string desc;
        /// <summary>
        /// Beschreibung des Videos
        /// </summary>
        public string Description
        {
            get
            {
                return this.desc;
            }
        }
        protected string duration;
        /// <summary>
        /// Länge des Clips in HH:MM:SS
        /// </summary>
        public string Duration
        {
            get
            {
                return this.duration;
            }
        }
        protected string teaser;
        /// <summary>
        /// Teaservideo des Clips, falls vorhanden, sonst ein leerer String.
        /// </summary>
        public string Teaser
        {
            get
            {
                return this.teaser;
            }
        }
        protected int date;
        /// <summary>
        /// Erscheinungsdatum des Clips
        /// </summary>
        public DateTime Date
        {
            get
            {
                return utcEpochStart.AddSeconds(date);
            }
        }
        protected bool subscribed;
        /// <summary>
        /// User kann Clip downloaden.
        /// </summary>
        public bool Subscribed
        {
            get
            {
                return this.subscribed;
            }
        }
        public MassengeschmackContent(string identifier, int pid, string title, string pdesc, string img, string desc, string duration, int date, bool subscribed,string teaser)
        {
            this.identifier = identifier;
            this.pid = pid;
            this.title = title;
            this.pdesc = pdesc;
            this.img = img;
            this.desc = desc;
            this.duration = duration;
            this.teaser = teaser;
            this.date = date;
            this.subscribed = subscribed;
        }
    }
}