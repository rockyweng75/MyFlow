using Microsoft.EntityFrameworkCore;
using MyFlow.Data.Models;

namespace MyFlow.Data.Connection
{
    public partial class TestDbContext : DbContext, IDbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionForm> ActionForms { get; set; } = null!;
        public virtual DbSet<ApplyData> ApplyData { get; set; } = null!;
        public virtual DbSet<ApproveData> ApproveData { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<Deadline> Deadlines { get; set; } = null!;
        public virtual DbSet<Flowchart> Flowcharts { get; set; } = null!;
        public virtual DbSet<Form> Forms { get; set; } = null!;
        public virtual DbSet<FormItem> FormItems { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobLog> JobLogs { get; set; } = null!;
        public virtual DbSet<Stage> Stages { get; set; } = null!;
        public virtual DbSet<StageValidation> StageValidations { get; set; } = null!;
        public virtual DbSet<Switch> Switches { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Integrated Security=SSPI;Trusted_Connection=True;AttachDBFilename=C:\\Users\\rocky\\source\\repos\\MyFlow\\MyFlow.Data\\Db\\TestDatabase.mdf");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionForm>(entity =>
            {
                entity.ToTable("ActionForm");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[ActionFormSeq])");
            });

            modelBuilder.Entity<ApplyData>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[ApplyDataSeq])");

                entity.Property(e => e.ApplyName).HasMaxLength(100);

                entity.Property(e => e.ApplyUser).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeptCode).HasMaxLength(50);

                entity.Property(e => e.DeptName).HasMaxLength(100);

                entity.Property(e => e.JobCode).HasMaxLength(50);

                entity.Property(e => e.JobName).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApproveData>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[ApproveDataSeq])");

                entity.Property(e => e.CloseDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.DeptCode).HasMaxLength(100);

                entity.Property(e => e.DeptName).HasMaxLength(100);

                entity.Property(e => e.JobCode).HasMaxLength(50);

                entity.Property(e => e.JobName).HasMaxLength(100);

                entity.Property(e => e.StatusCode).HasMaxLength(100);

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(200);

                entity.Property(e => e.UserName).HasMaxLength(500);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[AttachmentSeq])");
            });

            modelBuilder.Entity<Deadline>(entity =>
            {
                entity.ToTable("Deadline");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[DeadlineSeq])");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Flowchart>(entity =>
            {
                entity.ToTable("Flowchart");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[FlowchartSeq])");
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.ToTable("Form");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[FormSeq])");
            });

            modelBuilder.Entity<FormItem>(entity =>
            {
                entity.ToTable("FormItem");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[FormItemSeq])");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[JobSeq])");
            });

            modelBuilder.Entity<JobLog>(entity =>
            {
                entity.ToTable("JobLog");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[JobLogSeq])");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ReexecuteDate).HasColumnType("datetime");

                entity.Property(e => e.ReexecuteUser).HasMaxLength(50);
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.ToTable("Stage");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[StageSeq])");
            });

            modelBuilder.Entity<StageValidation>(entity =>
            {
                entity.ToTable("StageValidation");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[StageValidationSeq])");
            });

            modelBuilder.Entity<Switch>(entity =>
            {
                entity.ToTable("Switch");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[SwitchSeq])");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[TestSeq])");
                entity.Property(e => e.Column3).HasColumnType("datetime");

            });

            modelBuilder.HasSequence<int>("ActionFormSeq");

            modelBuilder.HasSequence<int>("ApplyDataSeq");

            modelBuilder.HasSequence<int>("ApproveDataSeq");

            modelBuilder.HasSequence<int>("AttachmentSeq");

            modelBuilder.HasSequence<int>("DeadlineSeq");

            modelBuilder.HasSequence<int>("FlowchartSeq");

            modelBuilder.HasSequence<int>("FormItemSeq");

            modelBuilder.HasSequence<int>("FormSeq");

            modelBuilder.HasSequence<int>("JobLogSeq");

            modelBuilder.HasSequence<int>("JobSeq");

            modelBuilder.HasSequence<int>("StageSeq");

            modelBuilder.HasSequence<int>("StageValidationSeq");

            modelBuilder.HasSequence<int>("SwitchSeq");

            modelBuilder.HasSequence<int>("TestSeq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
