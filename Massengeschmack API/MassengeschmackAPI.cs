using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Massengeschmack_API
{
    public class MassengeschmackAPI
    {
        protected string email;
        protected string password;
        protected WebClient webclient;
        protected JavaScriptSerializer jsonreader;
        protected string massengeschmackBaseURL;
        protected Queue<DateTime> limiter;
        private dynamic jsonread<T>(string url)
        {
            string dl;
            try
            {
                while (limiter.Count > 0 && limiter.Peek().AddSeconds(30) < DateTime.Now)
                {
                    limiter.Dequeue();
                }
                if (limiter.Count > 10)
                {
                    throw new MassengeschmackAPIException("API Rate Limited.");
                }
                dl = webclient.DownloadString(url);
                limiter.Enqueue(DateTime.Now);
            }
            catch (WebException e)
            {
                throw new MassengeschmackAPIException("Web Exception: " + e.Message);
            }
            dynamic checkforerror = jsonreader.Deserialize<dynamic>(dl);
            if (checkforerror is Dictionary<string, dynamic>)
            {
                if (((Dictionary<string, dynamic>)checkforerror).ContainsKey("api_error"))
                {
                    throw new MassengeschmackAPIException(((Dictionary<string, dynamic>)checkforerror)["api_error"]);
                }
            }
            return jsonreader.Deserialize<T>(dl);
        }
        public MassengeschmackAPI(string email, string password, string massengeschmackBaseURL)
        {
            this.email = email.Replace("@", "--at--");
            this.password = password;
            this.massengeschmackBaseURL = massengeschmackBaseURL;
            webclient = new WebClient();
            jsonreader = new JavaScriptSerializer();
            limiter = new Queue<DateTime>();
            webclient.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", Convert.ToBase64String(Encoding.ASCII.GetBytes(email + ":" + password)));
        }
        /// <summary>
        /// Liefert eine Übersicht aller aktiven abonnierten Projekte. Diese Anfrage besteht nur noch aus Umstellungsgründen.
        /// </summary>
        /// <returns>Ein Array mit allen Subscriptions und ihrer IDs.</returns>
        [System.Obsolete("Diese Anfrage besteht nur noch aus Umstellungsgründen.")]
        public MassengeschmackSubscription[] listSubscriptions()
        {
            Dictionary<string, List<Dictionary<string, dynamic>>> result = jsonread<Dictionary<string, List<Dictionary<string, dynamic>>>>("https://" + massengeschmackBaseURL + "/api/v1/?action=listSubscriptions");
            List<MassengeschmackSubscription> subs = new List<MassengeschmackSubscription>();
            foreach (Dictionary<string, dynamic> sub in result["active_subscriptions"])
            {
                subs.Add(new MassengeschmackSubscription((string)sub["title"], (int)sub["pid"], this));
            }
            return subs.ToArray();
        }
        /// <summary>
        /// Liefert ein Int Array von den abonnierten Projekten mit ihrer Projekt ID. Diese Anfrage besteht nur noch aus Umstellungsgründen.
        /// </summary>
        /// <returns>Ein Array mit allen Subscription IDs.</returns>
        [System.Obsolete("Diese Anfrage besteht nur noch aus Umstellungsgründen.")]
        public int[] listSubscriptionsID()
        {
            return jsonread<int[]>("https://" + massengeschmackBaseURL + "/api/v1/?action=listSubscriptionsID");
        }
        /// <summary>
        /// Liefert die letzten Folgen der favorisierten Projekte. Der Filter kann in den Accounteinstellungen unter Magazine ausblenden eingestellt werden.
        /// </summary>
        /// <returns>Ein Array mit den lezten Folgen, der Magazinen, die dem Benutzer auch auf der Startseite angezeigt werden würden.</returns>
        public MassengeschmackEpisode[] getMainFeed()
        {
            Dictionary<string, dynamic>[] result = jsonread<Dictionary<string, dynamic>[]>("https://" + massengeschmackBaseURL + "/api/v1/?action=getMainFeed");
            List<MassengeschmackEpisode> mainfeed = new List<MassengeschmackEpisode>();
            foreach (Dictionary<string, dynamic> feeditem in result)
            {
                mainfeed.Add(new MassengeschmackEpisode((string)feeditem["identifier"], (int)feeditem["pid"], (int)feeditem["contentType"], (string)feeditem["title"], (string)feeditem["pdesc"], (string)feeditem["img"], (string)feeditem["desc"], (string)feeditem["duration"], (int)feeditem["enum"], (string)feeditem["thumbnail"], (string)feeditem["teaserFile"], (int)feeditem["date"], (bool)feeditem["subscribed"], this));
            }
            return mainfeed.ToArray();
        }
        /// <summary>
        /// Liefert die (maximal 10) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int[] from)
        {
            return getFeed(from, "", null, 10);
        }
        /// <summary>
        /// Liefert die (maximal 10) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <param name="contentType">Entählt nur angegebene unter IDs.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int[] from, int[] contentType)
        {
            return getFeed(from, "&contentType=[" + string.Join(",", contentType) + "]", contentType, 10);
        }
        /// <summary>
        /// Liefert (maximal 10) Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <param name="page">Seite des Feeds.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int[] from, int page)
        {
            return getFeed(from, "&page=" + page, null, 10);
        }
        /// <summary>
        /// Liefert die (maximal 10) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <param name="contentType">Entählt nur angegebene unter IDs.</param>
        /// <param name="page">Seite des Feeds.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int[] from, int[] contentType, int page)
        {
            return getFeed(from, "&contentType=[" + string.Join(",", contentType) + "]&page=" + page, contentType, 10);
        }
        /// <summary>
        /// Liefert die (maximal limit) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="limit">limitiert den Feed auf limit Einträge</param>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int limit, int[] from)
        {
            return getFeed(from, "&limit=" + limit,null,limit);
        }
        /// <summary>
        /// Liefert die (maximal limit) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="limit">limitiert den Feed auf limit Einträge</param>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <param name="contentType">Entählt nur angegebene unter IDs.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int limit, int[] from, int[] contentType)
        {
            return getFeed(from, "&contentType=[" + string.Join(",", contentType) + "]&limit=" + limit,contentType,limit);
        }
        /// <summary>
        /// Liefert die (maximal limit) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="limit">limitiert den Feed auf limit Einträge</param>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <param name="page">Seite des Feeds.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int limit, int[] from, int page)
        {
            return getFeed(from, "&page=" + page + "&limit=" + limit, null, limit);
        }
        /// <summary>
        /// Liefert die (maximal limit) letzten Folgen der angegebenen Projekte.
        /// </summary>
        /// <param name="limit">limitiert den Feed auf limit Einträge</param>
        /// <param name="from">Feed entählt nur angegebene Projekt IDs.</param>
        /// <param name="contentType">Entählt nur angegebene unter IDs.</param>
        /// <param name="page">Seite des Feeds.</param>
        /// <returns>Ein Feedobjekt, welches die gefundenen Folgen beinhatet, sowie die Informationen über weitere Seiten.</returns>
        public MassengeschmackFeed getFeed(int limit, int[] from, int[] contentType, int page)
        {
            return getFeed(from, "&contentType=[" + string.Join(",", contentType) + "]&page=" + page + "&limit=" + limit, contentType, limit);
        }
        private MassengeschmackFeed getFeed(int[] from, string getParams, int[] contentType, int limit)
        {
            Dictionary<string, dynamic> result = jsonread<Dictionary<string, dynamic>>("https://" + massengeschmackBaseURL + "/api/v1/?action=getFeed&from=[" + string.Join(",", from) + "]" + getParams);
            List<MassengeschmackEpisode> episodes = new List<MassengeschmackEpisode>();
            foreach (Dictionary<string, dynamic> ep in result["eps"])
            {
                episodes.Add(new MassengeschmackEpisode((string)ep["identifier"], (int)ep["pid"], (int)ep["contentType"], (string)ep["title"], (string)ep["pdesc"], (string)ep["img"], (string)ep["desc"], (string)ep["duration"], (int)ep["enum"], (string)ep["thumbnail"], (string)ep["teaserFile"], (int)ep["date"], (bool)ep["canAccess"], this));
            }
            return new MassengeschmackFeed(episodes, (int)result["pages"], (result["next"] is bool ? -1 : (int)result["next"]), (result["prev"] is bool ? -1 : (int)result["prev"]), from, contentType, limit, this);
        }
        /// <summary>
        /// Liefert alle Informationen zu einem bestimmten Clip. Inklusive temporärer download URL, falls Zugang vorhanden.
        /// </summary>
        /// <param name="identifier">ID des Clips</param>
        /// <returns>Ein Clip Opjekt mit allen benötigten Informationen</returns>
        public MassengeschmackClip getClip(string identifier)
        {
            Dictionary<string, dynamic> result = jsonread<Dictionary<string, dynamic>>("https://" + massengeschmackBaseURL + "/api/v1/?action=getClip&identifier=" + identifier);
            List<MassengeschmackClip.File> files = new List<MassengeschmackClip.File>();
            foreach (Dictionary<string, dynamic> file in result["files"])
            {
                files.Add(new MassengeschmackClip.File((int)file["size"], (string)file["t"], (file.ContainsKey("size_readable") ? (string)file["size_readable"] : ""), (string)file["dimensions"], (string)file["desc"], (string)file["url"]));
            }
            return new MassengeschmackClip((string)result["identifier"], (int)result["pid"], (string)result["title"], (string)result["pdesc"], (string)result["img"], (string)result["desc"], (string)result["duration"], (int)result["date"], (bool)result["subscribed"], (result["teaser"] is bool ? "" : (string)result["teaser"]), files);
        }
    }
}