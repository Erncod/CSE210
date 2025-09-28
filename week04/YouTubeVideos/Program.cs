using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // First Video
        Video video1 = new Video("Learning C# Basics", "John Smith", 600);
        video1.AddComment(new Comment("Alice", "This is so helpful, thanks!"));
        video1.AddComment(new Comment("Bob", "Clear explanation."));
        video1.AddComment(new Comment("Charlie", "Please make more tutorials."));
        videos.Add(video1);

        // Second Video
        Video video2 = new Video("Top 10 Programming Languages", "TechWorld", 900);
        video2.AddComment(new Comment("Diana", "Great list!"));
        video2.AddComment(new Comment("Eve", "I think Rust should be higher."));
        video2.AddComment(new Comment("Frank", "Python for the win!"));
        videos.Add(video2);

        // Third Video
        Video video3 = new Video("History of Computers", "GeekChannel", 1200);
        video3.AddComment(new Comment("Grace", "So much nostalgia."));
        video3.AddComment(new Comment("Henry", "Love this content."));
        video3.AddComment(new Comment("Isabel", "Very informative."));
        videos.Add(video3);

        // Display details for each video
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
