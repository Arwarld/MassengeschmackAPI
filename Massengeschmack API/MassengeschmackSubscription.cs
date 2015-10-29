namespace Massengeschmack_API
{
    public class MassengeschmackSubscription
    {
        public string title;
        public int pid;
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