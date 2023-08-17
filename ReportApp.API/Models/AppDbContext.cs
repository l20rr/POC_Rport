using Microsoft.EntityFrameworkCore;
using ReportApp.Shared;
using System.Net.Mail;

namespace ReportApp.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BugReport> Bugs { get; set; }

        public DbSet<Attachments> Attachment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
              .HasMany(u => u.Feedback)
              .WithOne(f => f.User)
              .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BugReport)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<BugReport>()
                .HasKey(b => b.BugReportId);

            modelBuilder.Entity<Feedback>()
                .HasKey(f => f.FeedbackId);

            modelBuilder.Entity<Attachments>()
                .HasKey(a => a.AttachmentId);

            modelBuilder.Entity<BugReport>()
                .HasOne(b => b.Attachment)
                .WithOne(a => a.BugReport)
                .HasForeignKey<Attachments>(a => a.BugReportId)
                .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Attachment)
                .WithOne(a => a.Feedback)
                .HasForeignKey<Attachments>(a => a.FeedbackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "Lucas",
                Email = "teste@gadsakm.com",
            });

            modelBuilder.Entity<Feedback>().HasData(new Feedback
            {
                FeedbackId = 1,
                UserId = 1,
                Timestamp = new DateTime(2023, 07, 19),
                Ranking = Ranking.Star3,
                Comments = "Great job!",
                AttachmentId = 1,
                Question1 = "Friend's recommendation",
                Question2 = 4,
                Question3 = true
            });

            modelBuilder.Entity<BugReport>().HasData(new BugReport
            {
                BugReportId = 1,
                UserId = 1,
                Description = "Application crashes when clicking on button X",
                Timestamp = new DateTime(2023, 07, 20),
                AttachmentId = 1,
            });


            modelBuilder.Entity<Attachments>().HasData(new Attachments
            {
                AttachmentId = 1,
                BugReportId = 1,
                FeedbackId = 1,
                FileName = "File1",
                FilePath = "http://api.com",
            });
        }

    }
}
