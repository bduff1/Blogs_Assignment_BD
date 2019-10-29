using NLog;
using BlogsConsole_BD.Models;
using System;
using System.Linq;

namespace BlogsConsole_BD
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            DisplayMenu(true);
            string userSelection = Console.ReadLine();

            do
            {
                if (userSelection == "1")

                {
                    // Display all Blogs from the database
                    var db = new BlogContext();
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);


                    }
                    Console.ReadLine();

                }


                else if (userSelection == "2")
                {
                    logger.Info("Program started");
                    try
                    {

                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        var db = new BlogContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);

                        // Display all Blogs from the database
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    logger.Info("Program ended");

                }

                // This is the fun part 
                // Create a Post

                else if (userSelection == "3")
                {
                    logger.Info("Program started");
                    try
                    {


                        var db = new BlogContext();
                        var query = db.Blogs.OrderBy(b => b.Name);
             

                        Console.WriteLine("Each Blog followed by their ID: ");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                            Console.WriteLine(item.BlogId);


                        }
                        




                        Console.WriteLine("Enter the blog ID of the blog you would post to: ");
                        //Blog.Name = Console.ReadLine();
                        int blogid = 0;

                        var name = Console.ReadLine();
                        //var db = new BlogContext();
                        

                        Console.Write("Enter new post title: ");
                        var title = Console.ReadLine();

                        Console.WriteLine("Enter content: ");
                        var content = Console.ReadLine();

                        var post = new Post { Title = title, Content = content, BlogId = blogid};

                        db.AddPost(post);

                       // db.SaveChanges();

                        //db.AddBlog(blog);



                    }




                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    logger.Info("Program ended");


                }




                else if (userSelection == "4")
                {
                    // end the application
                }
                // note

                else
                {
                    Console.WriteLine("Bad selection, try again.");
                }

                DisplayMenu(false);
                userSelection = Console.ReadLine();


            } while (userSelection != "4");


            Console.ReadLine();

        }


        public static void DisplayMenu(bool firstTime)
        {

            Console.WriteLine("1) Display All Blogs");
            Console.WriteLine("2) Add Blog");
            Console.WriteLine("3) Create Post");
            Console.WriteLine("4) Exit application");
            
        }
    }
}