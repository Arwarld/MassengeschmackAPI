using Massengeschmack_API;
using System;

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
            MassengeschmackAPI mgapi = new MassengeschmackAPI(email, passwort, "massengeschmack.tv");
            MassengeschmackSubscription[] erg = mgapi.listSubscriptions();
            int[] erg2 = mgapi.listSubscriptionsID();
            MassengeschmackEpisode[] mainfeed = mgapi.getMainFeed();
            MassengeschmackFeed feed = mgapi.getFeed(new int[] { 1 });
            MassengeschmackClip clip = mgapi.getClip("studio-132");

            Console.WriteLine(clip.Date.ToString());
            Console.WriteLine(clip.Description.ToString());
            Console.WriteLine(clip.Duration.ToString());
            Console.WriteLine(clip.Identifier.ToString());
            Console.WriteLine(clip.Image.ToString());
            Console.WriteLine(clip.ProjectDescription.ToString());
            Console.WriteLine(clip.ProjectID.ToString());
            Console.WriteLine(clip.Subscribed.ToString());
            Console.WriteLine(clip.Teaser.ToString());
            Console.WriteLine(clip.Title.ToString());
            

            Console.ReadLine();

            Console.ReadLine();
        }
    }
}