namespace Massengeschmack_API
{
    public class MassengeschmackEpisode : MassengeschmackContent
    {
        protected int contentType;
        /// <summary>
        /// Video Typ (1 = Folgen)
        /// Bei FKTV z.B. (2 = Postecke, 3 = Interviews, 4 = Extras, 5 = Sendeschluss)
        /// </summary>
        public int ContentType
        {
            get
            {
                return this.contentType;
            }
        }
        protected int epnum;
        /// <summary>
        /// Nummer der Folge
        /// </summary>
        public int EpisodeNumber
        {
            get
            {
                return this.epnum;
            }
        }
        protected string thumbnail;
        /// <summary>
        /// Vorschaubild URL
        /// </summary>
        public string Thumbnail
        {
            get
            {
                return this.thumbnail;
            }
        }
        protected MassengeschmackAPI api;
        public MassengeschmackEpisode(string identifier, int pid, int contentType, string title, string pdesc, string img, string desc, string duration, int epnum, string thumbnail, string teaserfile, int date, bool subscribed, MassengeschmackAPI api)
            : base(identifier, pid, title, pdesc, img, desc, duration, date, subscribed, teaserfile)
        {
            this.contentType = contentType;
            this.epnum = epnum;
            this.thumbnail = thumbnail;
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