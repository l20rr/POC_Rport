using Microsoft.EntityFrameworkCore;
using ReportApp.Shared;
using System.Net.Mail;

namespace ReportApp.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BugReport> Bugs { get; set; }

        public DbSet<Attachments> Attachment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //database relation

            //User with feedback 1 - m
            modelBuilder.Entity<User>()
              .HasMany(u => u.Feedback)
              .WithOne(f => f.User)
              .HasForeignKey(f => f.UserId);

            //User with Bug 1 - m
            modelBuilder.Entity<User>()
                .HasMany(u => u.BugReport)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
            //Bug key
            modelBuilder.Entity<BugReport>()
                .HasKey(b => b.BugReportId);
            //Feedback key
            modelBuilder.Entity<Feedback>()
                .HasKey(f => f.FeedbackId);
            //Attachment key
            modelBuilder.Entity<Attachments>()
                .HasKey(a => a.AttachmentId);

            //Bug with Attachment 1 - 1
            modelBuilder.Entity<BugReport>()
                .HasOne(b => b.Attachment)
                .WithOne(a => a.BugReport)
                .HasForeignKey<Attachments>(a => a.BugReportId)
                .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata
            //Feedback with Attachment 1 - 1
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Attachment)
                .WithOne(a => a.Feedback)
                .HasForeignKey<Attachments>(a => a.FeedbackId)
                .OnDelete(DeleteBehavior.Restrict);

            //inicail data
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
                Ranking = 4,
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
                Base64data = "base64-encoded-data-goes-here",
                ContentType = "application/pdf" // Altere para o tipo MIME correto
            });
        }

    }
}
