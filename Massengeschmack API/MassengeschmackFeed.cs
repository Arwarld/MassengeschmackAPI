namespace Massengeschmack_API
{
    public class MassengeschmackFeed
    {
        public MassengeschmackEpisode[] eps;
        public int pages;
        public int next;
        public int prev;
        protected int[] from;
        protected int[] contentType;
        protected int limit;
        protected MassengeschmackAPI api;
        public MassengeschmackFeed(MassengeschmackEpisode[] eps, int pages, int next, int prev, int[] from, int[] contentType, int limit, MassengeschmackAPI api)
        {
            this.eps = eps;
            this.pages = pages;
            this.next = next;
            this.prev = prev;
            this.from = from;
            this.contentType = contentType;
            this.limit = limit;
            this.api = api;
        }
        /// <summary>
        /// Holt die vorherige Seite dieses Feeds. Mit den gleichen Parametern.
        /// </summary>
        /// <returns>Die vorherige Seite das Feeds, falls vorhanden.</returns>
        public MassengeschmackFeed GetPrev()
        {
            return GetPage(prev);
        }
        /// <summary>
        /// Holt die nächste Seite dieses Feeds. Mit den gleichen Parametern.
        /// </summary>
        /// <returns>Die nächste Seite das Feeds, falls vorhanden.</returns>
        public MassengeschmackFeed GetNext()
        {
            return GetPage(next);
        }
        /// <summary>
        /// Holt eine Seite dieses Feeds. Mit den gleichen Parametern.
        /// </summary>
        /// <param name="page">Die gewünschte Seite</param>
        /// <returns>Die gewünschte Seite das Feeds, falls vorhanden.</returns>
        public MassengeschmackFeed GetPage(int page)
        {
            if (page < 1 || page > pages)
                throw new MassengeschmackAPIException("Seite außerhalb des Feedbereiches");
            if (this.contentType == null)
                return api.getFeed(limit, from, page);
            else
                return api.getFeed(limit, from, contentType, page);
        }
    }
}