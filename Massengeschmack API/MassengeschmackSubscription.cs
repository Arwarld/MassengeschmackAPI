namespace Massengeschmack_API
{
    public class MassengeschmackSubscription
    {
        protected string title;
        /// <summary>
        /// Der Titels des Projekts
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
        }
        protected int pid;
        /// <summary>
        /// Die ID des Projektes, für alle weteren Feedanfragen relevant
        /// </summary>
        public int ProjectID
        {
            get
            {
                return this.pid;
            }
        }
        protected MassengeschmackAPI api;
        public MassengeschmackSubscription(string title, int pid, MassengeschmackAPI api)
        {
            this.title = title;
            this.pid = pid;
            this.api = api;
        }
        /// <summary>
        /// Liefert die erste Seite des Feeds für dieses Magazin mit allen Extras des Magazins.
        /// </summary>
        /// <returns></returns>
        public MassengeschmackFeed getFeed()
        {
            return api.getFeed(new int[] { pid });
        }
    }
}