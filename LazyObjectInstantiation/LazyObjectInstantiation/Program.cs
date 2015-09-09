using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LazyObjectInstantiation
{
    class song
    {
        public string Artist { get; set; }
        public string TrackName { get; set; }
        public double TrackLength { get; set; }
    }

    class AllTracks
    {
        private song[] allSongs = new song[10000];

        public AllTracks()
        {
            WriteLine("Filling up the songs!");
        }

        public AllTracks(int count)
        {

        }
    }

    class MediaPlayer
    {
        public void Play() { }
        public void Pause() { }
        public void Stop() { }

        //private AllTracks allSongs = new AllTracks();

        //public AllTracks GetAllTracks()
        //{
        //    return allSongs;
        //}

        private Lazy<AllTracks> allSongs = new Lazy<AllTracks>(() =>
        {
            return new AllTracks(1);
        });

        public AllTracks GetAllTracks()
        {
            return allSongs.Value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MediaPlayer myPlayer = new MediaPlayer();
            myPlayer.Play();

            ReadLine();
        }
    }
}
