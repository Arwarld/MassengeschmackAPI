using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massengeschmack_API;

namespace Massengeschmack_API_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Passwort: ");
            string passwort = Console.ReadLine();
            MassengeschmackAPI mgapi = new MassengeschmackAPI(email, passwort, "beta.massengeschmack.tv");

            MassengeschmackSubscription[] erg = mgapi.listSubscriptions();
            int[] erg2 = mgapi.listSubscriptionsID();
            MassengeschmackEpisode[] mainfeed = mgapi.getMainFeed();
            MassengeschmackFeed feed = mgapi.getFeed(new int[] { 1 });
            MassengeschmackClip clip = mgapi.getClip("fktv1");

            Console.ReadLine();
        }
    }
}