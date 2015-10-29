namespace Massengeschmack_API
{
    public class MassengeschmackEpisode
    {
        public string identifier;
        public int pid;
        public int contentType;
        public string title;
        public string pdesc;
        public string img;
        public string desc;
        public string duration;
        public int epnum;
        public string thumbnail;
        public string teaserfile;
        public int date;
        public bool subscribed;
        protected MassengeschmackAPI api;
        public MassengeschmackEpisode(string identifier, int pid, int contentType, string title, string pdesc, string img, string desc, string duration, int epnum, string thumbnail, string teaserfile, int date, bool subscribed, MassengeschmackAPI api)
        {
            this.identifier = identifier;
            this.pid = pid;
            this.contentType = contentType;
            this.title = title;
            this.pdesc = pdesc;
            this.img = img;
            this.desc = desc;
            this.duration = duration;
            this.epnum = epnum;
            this.thumbnail = thumbnail;
            this.teaserfile = teaserfile;
            this.date = date;
            this.subscribed = subscribed;
            this.api = api;
        }
        /// <summary>
        /// Erhalte den Clip mit Dateilinks dieser Episode.
        /// </summary>
        /// <returns>Der Clip</returns>
        public MassengeschmackClip getClip()
        {
            return api.getClip(identifier);
        }
    }
}