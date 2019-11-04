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
                        var query = db.Blogs.OrderBy(b => b.BlogId);

                        Console.WriteLine("ID followed by the Blog: ");
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.BlogId}) {item.Name}");
                            //Console.WriteLine(item.BlogId);

                        }


                        Console.WriteLine();

                        Console.WriteLine("Enter the ID of the blog you would post to: ");
                        //Blog.Name = Console.ReadLine();
                        var blogID = Console.ReadLine();

                        //var ID = Console.ReadLine();
                        //var db = new BlogContext();
                        //var isValid = db.Blogs.Any(b => b.BlogId == Convert.ToInt32(blogID));
                        //var isValid = db.Blogs.FirstOrDefault(b => b.BlogId == Convert.ToInt32(blogID));




                        Console.Write("Enter new post title: ");
                        var title = Console.ReadLine();

                        Console.WriteLine("Enter content: ");
                        var content = Console.ReadLine();

                        var post = new Post { Title = title, Content = content, BlogId = Convert.ToInt32(blogID) };

                        db.AddPost(post);
                        logger.Info("Post Created - {title}", title);
                        db.SaveChanges();


                    }


                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    logger.Info("Program ended");


                }

                // View Blogs

                else if (userSelection == "4")
                {
                    
                    DisplaySubMenu(true);


                    //Console.WriteLine("What would you like to do?");
                    //    Console.WriteLine();
                    //    Console.WriteLine("A) Display posts from a specific blog");
                    //    Console.WriteLine("B) Display all posts from all blogs");
                    //    Console.WriteLine("C) Return to main menu");

                    string subMenuSelection = Console.ReadLine();
                    



                        do
                        {


                            if (subMenuSelection.ToUpper() == "A")

                            {


                                var db = new BlogContext();
                                var query = db.Blogs.OrderBy(b => b.BlogId);

                                Console.WriteLine("ID followed by the Blog: ");
                                foreach (var item in query)
                                {
                                    Console.WriteLine($"{item.BlogId}) {item.Name}");
                                    //Console.WriteLine(item.BlogId);

                                }
                            Console.WriteLine();

                            DisplayPostMenu(true);

                        
                                
                            var blogID = Console.ReadLine();

                            if (blogID == "done")
                            {
                                Environment.Exit(0);
                            }

                            var blogName = from b in db.Blogs.AsEnumerable()
                                           where (b.BlogId == Convert.ToInt32(blogID))
                                           select b;
                            
                            
                            //var Pdb = new BlogContext();
                            //var Pquery = db.Posts.OrderBy(p => p.PostId, blogID = Convert.ToInt32(BlogID));
                            var postQuery = from p in db.Posts.AsEnumerable()
                                                join b in db.Blogs.AsEnumerable() on p.BlogId equals b.BlogId
                                                where (p.BlogId == Convert.ToInt32(blogID))
                                                select p;



                            foreach (var titleName in blogName)
                            {
                                Console.WriteLine($"Viewing all posts from: {titleName.Name}");
                            }


                            

                                Console.WriteLine();
                                foreach (var item in postQuery)
                                {
                                    Console.WriteLine($"Post ID: {item.PostId})");
                                    Console.WriteLine($"Title: {item.Title}");
                                    Console.WriteLine($"Content: {item.Content} \n");
                                //Console.WriteLine(item.BlogId);
                                

                                }

                            Console.ReadLine();

                            
                        }



                            else if (subMenuSelection.ToUpper() == "B")
                            {
                                var db = new BlogContext();

                                var postQuery = from p in db.Posts.AsEnumerable()
                                                join b in db.Blogs.AsEnumerable() on p.BlogId equals b.BlogId
                                                select p;

                                Console.WriteLine("All Posts: ");
                                Console.WriteLine();
                            foreach (var item in postQuery)
                            {
                                Console.WriteLine($"Blog: {item.Blog.Name}");
                                Console.WriteLine($"Post: {item.PostId}) {item.Title}");
                                Console.WriteLine($"Content: {item.Content} \n");

                            }

                            

                            Console.ReadLine();
                            Environment.Exit(0);



                        }


                            else if(subMenuSelection.ToUpper() == "C")
                            {
                                
                                
                            }

                        else
                        {
                            Console.WriteLine("Bad selection, try again.");
                        }

                    } while (subMenuSelection.ToUpper() != "C");

                    
                }
                    
                



             


            

                else if (userSelection == "5")
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


            } while (userSelection != "5");


            Console.ReadLine();

        }


        public static void DisplayMenu(bool firstTime)
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine();
            Console.WriteLine("1) Display All Blogs");
            Console.WriteLine("2) Add Blog");
            Console.WriteLine("3) Create Post");
            Console.WriteLine("4) Display Posts");
            Console.WriteLine("5) Exit application");
            
        }

        public static void DisplaySubMenu(bool firstTime)
        {

            Console.WriteLine("Post Menu");
            Console.WriteLine();
            Console.WriteLine("A) Display posts from a specific blog");
            Console.WriteLine("B) Display all posts from all blogs");
            Console.WriteLine("C) Return to main menu");

        }


        public static void DisplayPostMenu(bool firstTime)
        {

            Console.WriteLine("Which Blog's posts would you like to view? ");
            Console.WriteLine();
            Console.WriteLine("Enter the ID of the Blog: ");
            Console.WriteLine("(Enter done to exit)");


        }


    }
}