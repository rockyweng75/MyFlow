// using Microsoft.EntityFrameworkCore;
// using MyFlow.Data.Models;

// namespace MyFlow.Data.Connection
// {
    
//     public partial class TestDbContext
//     {

//         public virtual DbSet<StageMix> StageMix { get; set; } = null!;

//         public virtual DbSet<FlowchartMix> FlowchartMix { get; set; } = null!;


//         partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<FlowchartMix>(entity =>
//             {
//                 entity.ToTable("Flowchart");

//                 entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[FlowchartSeq])");

//                 entity
//                     .HasMany<StageMix>(e => e.StageList)
//                     .WithOne()
//                     .HasForeignKey(e => e.FlowId);

//             });

//             modelBuilder.Entity<StageMix>(entity =>
//             {
//                 // entity.ToTable("Stage");

//                 // entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [dbo].[StageSeq])");

//                 entity
//                     .HasMany(e => e.StageRouteList)
//                     .WithOne()
//                     .HasForeignKey(e => e.StageId);

//                 entity
//                     .HasMany(e => e.StageJobList)
//                     .WithOne()
//                     .HasForeignKey(e => e.StageId);

//                 entity
//                     .HasMany(e => e.StageValidationList)
//                     .WithOne()
//                     .HasForeignKey(e => e.StageId);
//             });
//         }
//     }
// }