using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TP3;

namespace ConsoleApplication2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var collection = new MovieCollection().Movies;
            
            Console.WriteLine($"Count all movieds : {collection.Count}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            Console.WriteLine($"Count all movies with the letter e : {collection.Count(x=>x.Title.Contains('e'))}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            var counterF = 0;
            foreach (var itemF in collection) {
                counterF += itemF.Title.Count(x => x == 'f');
            }
            Console.WriteLine($"Count how many time the letter f is in all the titles from this list : {counterF}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            var numQuery =
                from movie in collection
                orderby movie.Budget descending 
                select movie.Title;
            Console.WriteLine($"Display the title of the film with the higher budget : {numQuery.First()}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            var numQuery2 =
                from movie in collection
                orderby movie.BoxOffice 
                select movie.Title;
            Console.WriteLine($"Display the title of the movie with the lowest box office : {numQuery2.First()}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            Console.WriteLine("Order the movies by reversed alphabetical order and print the first 11 of the list : ");
            var numQuery3 =
                from movie in collection 
                orderby movie.Title descending
                select movie.Title;
            foreach (var movie in numQuery3.Take(11))
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            var numQuery4 =
                from movie in collection 
                where movie.ReleaseDate.Year <1980
                select movie;
            Console.WriteLine($"Count all the movies made before 1980 : {numQuery4.Count()}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            var numQuery5 =
                from movie in collection
                where "AEIOUY".IndexOf(movie.Title[0]) >= 0
                select movie.RunningTime;
            Console.WriteLine($"Display the average running time of movies having a vowel as the first letter : {numQuery5.Average()}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            Console.WriteLine("Print all movies with the letter H or W in the title, but not the letter I or T : ");
            foreach (var waltDisneyMovies in 
                from movie in collection 
                where (movie.Title.ToUpper().Contains('H') || movie.Title.ToUpper().Contains('W')) && !(movie.Title.ToUpper().Contains('I') || movie.Title.ToUpper().Contains('T')) 
                select movie) {
                Console.WriteLine(waltDisneyMovies.Title);
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            Console.WriteLine("Calculate the mean of all Budget / Box Office of every movie ever : ");
            Console.WriteLine($"Average Budget : {(from item in collection select item.Budget).Average()}");
            Console.WriteLine($"Average Box Office : {(from item in collection select item.BoxOffice).Average()}");
            Console.WriteLine($"Average Budget / Average Box Office : {(from item in collection select item.Budget).Average()/(from item in collection select item.BoxOffice).Average()}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            
            Console.WriteLine("Create a simple function that create 3 threads : (enlever les commentaires de l'appel de la fonction)");
            //Enlever les commentaire pour executer l'éxercice 2.
            //CreateThread();
        }
        
        public static void CreateThread()
        {
            Thread t1 = new Thread(new ThreadStart(Thread1));
            Thread t2 = new Thread(new ThreadStart(Thread2));
            Thread t3 = new Thread(new ThreadStart(Thread3));
            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine("End of threads");
        }

        public static void Thread1()
        {
            var startTime = DateTime.UtcNow;
            while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(10))
            {
                print('_');
                Thread.Sleep(50);
            }
        }

        public static void Thread2()
        {
            var startTime = DateTime.UtcNow;
            while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(11))
            {
                print('*');
                Thread.Sleep(40);
            }
        }

        public static void Thread3()
        {
            var startTime = DateTime.UtcNow;
            while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(9))
            {
                print('°');
                Thread.Sleep(20);
            }
        }
        
        private static readonly Mutex m = new Mutex();

        public static void print(char c)
        {
            m.WaitOne();
            try
            {
                Console.WriteLine(c);
            }
            finally
            {
                m.ReleaseMutex();
            }
        }
    }
}