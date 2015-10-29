namespace Massengeschmack_API
{
    public class MassengeschmackClip
    {
        public string identifier;
        public int pid;
        public string title;
        public string pdesc;
        public string img;
        public string desc;
        public string duration;
        public int date;
        public string teaser;
        public bool subscribed;
        public File[] files;
        public MassengeschmackClip(string identifier, int pid, string title, string pdesc, string img, string desc, string duration, int date, bool subscribed,string teaser, File[] files)
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
            this.files = files;
        }
        public class File
        {
            public int size;
            public string t;
            public string size_readable;
            public string dimensions;
            public string desc;
            public string url;
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