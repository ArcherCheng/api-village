using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public partial class AppDbContext : BaseDbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ak0KeyCode> Ak0KeyCode { get; set; }

    public virtual DbSet<Ak0KeyRule> Ak0KeyRule { get; set; }

    public virtual DbSet<AppDataLog> AppDataLog { get; set; }

    public virtual DbSet<AppTempExcel> AppTempExcel { get; set; }

    public virtual DbSet<AppTempSql> AppTempSql { get; set; }

    public virtual DbSet<AppUserLogin> AppUserLogin { get; set; }

    public virtual DbSet<AppUserMachine> AppUserMachine { get; set; }

    public virtual DbSet<AppUserMessage> AppUserMessage { get; set; }

    public virtual DbSet<AppUserRequest> AppUserRequest { get; set; }

    public virtual DbSet<AppUserStar> AppUserStar { get; set; }

    public virtual DbSet<Au1Team> Au1Team { get; set; }

    public virtual DbSet<Au1User> Au1User { get; set; }

    public virtual DbSet<Cz2Petition> Cz2Petition { get; set; }

    public virtual DbSet<Cz2PetitionReply> Cz2PetitionReply { get; set; }

    public virtual DbSet<Cz2Repair> Cz2Repair { get; set; }

    public virtual DbSet<Cz2RepairReply> Cz2RepairReply { get; set; }

    public virtual DbSet<Ma1Master> Ma1Master { get; set; }

    public virtual DbSet<Ma2MasterEducation> Ma2MasterEducation { get; set; }

    public virtual DbSet<Ma2MasterExperience> Ma2MasterExperience { get; set; }

    public virtual DbSet<Ma2MasterPartner> Ma2MasterPartner { get; set; }

    public virtual DbSet<Ma2MasterPhoto> Ma2MasterPhoto { get; set; }

    public virtual DbSet<Ma2MasterPolicy> Ma2MasterPolicy { get; set; }

    public virtual DbSet<Pb2Bulletin> Pb2Bulletin { get; set; }

    public virtual DbSet<Pb2BulletinItem> Pb2BulletinItem { get; set; }

    public virtual DbSet<Pb2Forum> Pb2Forum { get; set; }

    public virtual DbSet<Pb2ForumReply> Pb2ForumReply { get; set; }

    public virtual DbSet<Tm2Activity> Tm2Activity { get; set; }

    public virtual DbSet<Tm2Announcement> Tm2Announcement { get; set; }

    public virtual DbSet<Tm2QuizOption> Tm2QuizOption { get; set; }

    public virtual DbSet<Tm2QuizQuestion> Tm2QuizQuestion { get; set; }

    public virtual DbSet<Tm2QuizSubject> Tm2QuizSubject { get; set; }

    public virtual DbSet<ViewCity> ViewCity { get; set; }

    public virtual DbSet<ViewTown> ViewTown { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("server=(local)\\SqlExpress01;database=VillageModel;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ak0KeyCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pkey_Ak0KeyCode");

            entity.ToTable(tb => tb.HasTrigger("Ak0KeyCode_trigger1"));

            entity.HasIndex(e => new { e.CodeGroup, e.CodeLabel, e.CodeValue }, "Inx_CodeValue").IsUnique();

            entity.Property(e => e.CodeGroup).HasMaxLength(100);
            entity.Property(e => e.CodeLabel).HasMaxLength(100);
            entity.Property(e => e.CodeValue).HasMaxLength(100);
            entity.Property(e => e.IsOnOff).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);
        });

        modelBuilder.Entity<Ak0KeyRule>(entity =>
        {
            entity.HasKey(e => e.RuleId).HasName("Ak0KeyRule_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ak0KeyRuleTrigger1"));

            entity.Property(e => e.RuleId).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.RuleGroup).HasMaxLength(100);
            entity.Property(e => e.RuleLabel).HasMaxLength(100);
            entity.Property(e => e.RuleValue).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);
        });

        modelBuilder.Entity<AppDataLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppDataLog_PrimaryKey");

            entity.HasIndex(e => new { e.TableName, e.WriteTime }, "Inx_TableName");

            entity.HasIndex(e => new { e.WriteTime, e.TableName }, "Inx_WriteTime");

            entity.Property(e => e.NewData).HasMaxLength(4000);
            entity.Property(e => e.OldData).HasMaxLength(4000);
            entity.Property(e => e.TableKey).HasMaxLength(100);
            entity.Property(e => e.TableName).HasMaxLength(100);
            entity.Property(e => e.WriteTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AppTempExcel>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("AppTempExcel_PrimaryKey");

            entity.Property(e => e.A).HasMaxLength(200);
            entity.Property(e => e.AA).HasMaxLength(200);
            entity.Property(e => e.AB).HasMaxLength(200);
            entity.Property(e => e.AC).HasMaxLength(200);
            entity.Property(e => e.AD).HasMaxLength(200);
            entity.Property(e => e.AE).HasMaxLength(200);
            entity.Property(e => e.AF).HasMaxLength(200);
            entity.Property(e => e.AG).HasMaxLength(200);
            entity.Property(e => e.AH).HasMaxLength(200);
            entity.Property(e => e.AI).HasMaxLength(200);
            entity.Property(e => e.AJ).HasMaxLength(200);
            entity.Property(e => e.AK).HasMaxLength(200);
            entity.Property(e => e.AL).HasMaxLength(200);
            entity.Property(e => e.AM).HasMaxLength(200);
            entity.Property(e => e.AN).HasMaxLength(200);
            entity.Property(e => e.AO).HasMaxLength(200);
            entity.Property(e => e.AP).HasMaxLength(200);
            entity.Property(e => e.AQ).HasMaxLength(200);
            entity.Property(e => e.AR).HasMaxLength(200);
            entity.Property(e => e.AS).HasMaxLength(200);
            entity.Property(e => e.AT).HasMaxLength(200);
            entity.Property(e => e.AU).HasMaxLength(200);
            entity.Property(e => e.AV).HasMaxLength(200);
            entity.Property(e => e.AW).HasMaxLength(200);
            entity.Property(e => e.AX).HasMaxLength(200);
            entity.Property(e => e.AY).HasMaxLength(200);
            entity.Property(e => e.AZ).HasMaxLength(200);
            entity.Property(e => e.B).HasMaxLength(200);
            entity.Property(e => e.BA).HasMaxLength(200);
            entity.Property(e => e.BB).HasMaxLength(200);
            entity.Property(e => e.BC).HasMaxLength(200);
            entity.Property(e => e.BD).HasMaxLength(200);
            entity.Property(e => e.BE).HasMaxLength(200);
            entity.Property(e => e.BF).HasMaxLength(200);
            entity.Property(e => e.BG).HasMaxLength(200);
            entity.Property(e => e.BH).HasMaxLength(200);
            entity.Property(e => e.BI).HasMaxLength(200);
            entity.Property(e => e.BJ).HasMaxLength(200);
            entity.Property(e => e.BK).HasMaxLength(200);
            entity.Property(e => e.BL).HasMaxLength(200);
            entity.Property(e => e.BM).HasMaxLength(200);
            entity.Property(e => e.BN).HasMaxLength(200);
            entity.Property(e => e.BO).HasMaxLength(200);
            entity.Property(e => e.BP).HasMaxLength(200);
            entity.Property(e => e.BQ).HasMaxLength(200);
            entity.Property(e => e.BR).HasMaxLength(200);
            entity.Property(e => e.BS).HasMaxLength(200);
            entity.Property(e => e.BT).HasMaxLength(200);
            entity.Property(e => e.BU).HasMaxLength(200);
            entity.Property(e => e.BV).HasMaxLength(200);
            entity.Property(e => e.BW).HasMaxLength(200);
            entity.Property(e => e.BX).HasMaxLength(200);
            entity.Property(e => e.BY).HasMaxLength(200);
            entity.Property(e => e.BZ).HasMaxLength(200);
            entity.Property(e => e.C).HasMaxLength(200);
            entity.Property(e => e.CA).HasMaxLength(200);
            entity.Property(e => e.CB).HasMaxLength(200);
            entity.Property(e => e.CC).HasMaxLength(200);
            entity.Property(e => e.CD).HasMaxLength(200);
            entity.Property(e => e.CE).HasMaxLength(200);
            entity.Property(e => e.CF).HasMaxLength(200);
            entity.Property(e => e.CG).HasMaxLength(200);
            entity.Property(e => e.CH).HasMaxLength(200);
            entity.Property(e => e.CI).HasMaxLength(200);
            entity.Property(e => e.CJ).HasMaxLength(200);
            entity.Property(e => e.CK).HasMaxLength(200);
            entity.Property(e => e.CL).HasMaxLength(200);
            entity.Property(e => e.CM).HasMaxLength(200);
            entity.Property(e => e.CN).HasMaxLength(200);
            entity.Property(e => e.CO).HasMaxLength(200);
            entity.Property(e => e.CP).HasMaxLength(200);
            entity.Property(e => e.CQ).HasMaxLength(200);
            entity.Property(e => e.CR).HasMaxLength(200);
            entity.Property(e => e.CS).HasMaxLength(200);
            entity.Property(e => e.CT).HasMaxLength(200);
            entity.Property(e => e.CU).HasMaxLength(200);
            entity.Property(e => e.CV).HasMaxLength(200);
            entity.Property(e => e.CW).HasMaxLength(200);
            entity.Property(e => e.CX).HasMaxLength(200);
            entity.Property(e => e.CY).HasMaxLength(200);
            entity.Property(e => e.CZ).HasMaxLength(200);
            entity.Property(e => e.D).HasMaxLength(200);
            entity.Property(e => e.D1).HasColumnType("datetime");
            entity.Property(e => e.D10).HasColumnType("datetime");
            entity.Property(e => e.D2).HasColumnType("datetime");
            entity.Property(e => e.D3).HasColumnType("datetime");
            entity.Property(e => e.D4).HasColumnType("datetime");
            entity.Property(e => e.D5).HasColumnType("datetime");
            entity.Property(e => e.D6).HasColumnType("datetime");
            entity.Property(e => e.D7).HasColumnType("datetime");
            entity.Property(e => e.D8).HasColumnType("datetime");
            entity.Property(e => e.D9).HasColumnType("datetime");
            entity.Property(e => e.DA).HasMaxLength(200);
            entity.Property(e => e.DB).HasMaxLength(200);
            entity.Property(e => e.DC).HasMaxLength(200);
            entity.Property(e => e.DD).HasMaxLength(200);
            entity.Property(e => e.DE).HasMaxLength(200);
            entity.Property(e => e.DF).HasMaxLength(200);
            entity.Property(e => e.DG).HasMaxLength(200);
            entity.Property(e => e.DH).HasMaxLength(200);
            entity.Property(e => e.DI).HasMaxLength(200);
            entity.Property(e => e.DJ).HasMaxLength(200);
            entity.Property(e => e.DK).HasMaxLength(200);
            entity.Property(e => e.DL).HasMaxLength(200);
            entity.Property(e => e.DM).HasMaxLength(200);
            entity.Property(e => e.DN).HasMaxLength(200);
            entity.Property(e => e.DO).HasMaxLength(200);
            entity.Property(e => e.DP).HasMaxLength(200);
            entity.Property(e => e.DQ).HasMaxLength(200);
            entity.Property(e => e.DR).HasMaxLength(200);
            entity.Property(e => e.DS).HasMaxLength(200);
            entity.Property(e => e.DT).HasMaxLength(200);
            entity.Property(e => e.DU).HasMaxLength(200);
            entity.Property(e => e.DV).HasMaxLength(200);
            entity.Property(e => e.DW).HasMaxLength(200);
            entity.Property(e => e.DX).HasMaxLength(200);
            entity.Property(e => e.DY).HasMaxLength(200);
            entity.Property(e => e.DZ).HasMaxLength(200);
            entity.Property(e => e.E).HasMaxLength(200);
            entity.Property(e => e.EA).HasMaxLength(200);
            entity.Property(e => e.EB).HasMaxLength(200);
            entity.Property(e => e.EC).HasMaxLength(200);
            entity.Property(e => e.ED).HasMaxLength(200);
            entity.Property(e => e.EE).HasMaxLength(200);
            entity.Property(e => e.EF).HasMaxLength(200);
            entity.Property(e => e.EG).HasMaxLength(200);
            entity.Property(e => e.EH).HasMaxLength(200);
            entity.Property(e => e.EI).HasMaxLength(200);
            entity.Property(e => e.EJ).HasMaxLength(200);
            entity.Property(e => e.EK).HasMaxLength(200);
            entity.Property(e => e.EL).HasMaxLength(200);
            entity.Property(e => e.EM).HasMaxLength(200);
            entity.Property(e => e.EN).HasMaxLength(200);
            entity.Property(e => e.EO).HasMaxLength(200);
            entity.Property(e => e.EP).HasMaxLength(200);
            entity.Property(e => e.EQ).HasMaxLength(200);
            entity.Property(e => e.ER).HasMaxLength(200);
            entity.Property(e => e.ES).HasMaxLength(200);
            entity.Property(e => e.ET).HasMaxLength(200);
            entity.Property(e => e.EU).HasMaxLength(200);
            entity.Property(e => e.EV).HasMaxLength(200);
            entity.Property(e => e.EW).HasMaxLength(200);
            entity.Property(e => e.EX).HasMaxLength(200);
            entity.Property(e => e.EY).HasMaxLength(200);
            entity.Property(e => e.EZ).HasMaxLength(200);
            entity.Property(e => e.F).HasMaxLength(200);
            entity.Property(e => e.FA).HasMaxLength(200);
            entity.Property(e => e.FB).HasMaxLength(200);
            entity.Property(e => e.FC).HasMaxLength(200);
            entity.Property(e => e.FD).HasMaxLength(200);
            entity.Property(e => e.FE).HasMaxLength(200);
            entity.Property(e => e.FF).HasMaxLength(200);
            entity.Property(e => e.FG).HasMaxLength(200);
            entity.Property(e => e.FH).HasMaxLength(200);
            entity.Property(e => e.FI).HasMaxLength(200);
            entity.Property(e => e.FJ).HasMaxLength(200);
            entity.Property(e => e.FK).HasMaxLength(200);
            entity.Property(e => e.FL).HasMaxLength(200);
            entity.Property(e => e.FM).HasMaxLength(200);
            entity.Property(e => e.FN).HasMaxLength(200);
            entity.Property(e => e.FO).HasMaxLength(200);
            entity.Property(e => e.FP).HasMaxLength(200);
            entity.Property(e => e.FQ).HasMaxLength(200);
            entity.Property(e => e.FR).HasMaxLength(200);
            entity.Property(e => e.FS).HasMaxLength(200);
            entity.Property(e => e.FT).HasMaxLength(200);
            entity.Property(e => e.FU).HasMaxLength(200);
            entity.Property(e => e.FV).HasMaxLength(200);
            entity.Property(e => e.FW).HasMaxLength(200);
            entity.Property(e => e.FX).HasMaxLength(200);
            entity.Property(e => e.FY).HasMaxLength(200);
            entity.Property(e => e.FZ).HasMaxLength(200);
            entity.Property(e => e.G).HasMaxLength(200);
            entity.Property(e => e.GA).HasMaxLength(200);
            entity.Property(e => e.GB).HasMaxLength(200);
            entity.Property(e => e.GC).HasMaxLength(200);
            entity.Property(e => e.GD).HasMaxLength(200);
            entity.Property(e => e.GE).HasMaxLength(200);
            entity.Property(e => e.GF).HasMaxLength(200);
            entity.Property(e => e.GG).HasMaxLength(200);
            entity.Property(e => e.GH).HasMaxLength(200);
            entity.Property(e => e.GI).HasMaxLength(200);
            entity.Property(e => e.GJ).HasMaxLength(200);
            entity.Property(e => e.GK).HasMaxLength(200);
            entity.Property(e => e.GL).HasMaxLength(200);
            entity.Property(e => e.GM).HasMaxLength(200);
            entity.Property(e => e.GN).HasMaxLength(200);
            entity.Property(e => e.GO).HasMaxLength(200);
            entity.Property(e => e.GP).HasMaxLength(200);
            entity.Property(e => e.GQ).HasMaxLength(200);
            entity.Property(e => e.GR).HasMaxLength(200);
            entity.Property(e => e.GS).HasMaxLength(200);
            entity.Property(e => e.GT).HasMaxLength(200);
            entity.Property(e => e.GU).HasMaxLength(200);
            entity.Property(e => e.GV).HasMaxLength(200);
            entity.Property(e => e.GW).HasMaxLength(200);
            entity.Property(e => e.GX).HasMaxLength(200);
            entity.Property(e => e.GY).HasMaxLength(200);
            entity.Property(e => e.GZ).HasMaxLength(200);
            entity.Property(e => e.H).HasMaxLength(200);
            entity.Property(e => e.HA).HasMaxLength(200);
            entity.Property(e => e.HB).HasMaxLength(200);
            entity.Property(e => e.HC).HasMaxLength(200);
            entity.Property(e => e.HD).HasMaxLength(200);
            entity.Property(e => e.HE).HasMaxLength(200);
            entity.Property(e => e.HF).HasMaxLength(200);
            entity.Property(e => e.HG).HasMaxLength(200);
            entity.Property(e => e.HH).HasMaxLength(200);
            entity.Property(e => e.HI).HasMaxLength(200);
            entity.Property(e => e.HJ).HasMaxLength(200);
            entity.Property(e => e.HK).HasMaxLength(200);
            entity.Property(e => e.HL).HasMaxLength(200);
            entity.Property(e => e.HM).HasMaxLength(200);
            entity.Property(e => e.HN).HasMaxLength(200);
            entity.Property(e => e.HO).HasMaxLength(200);
            entity.Property(e => e.HP).HasMaxLength(200);
            entity.Property(e => e.HQ).HasMaxLength(200);
            entity.Property(e => e.HR).HasMaxLength(200);
            entity.Property(e => e.HS).HasMaxLength(200);
            entity.Property(e => e.HT).HasMaxLength(200);
            entity.Property(e => e.HU).HasMaxLength(200);
            entity.Property(e => e.HV).HasMaxLength(200);
            entity.Property(e => e.HW).HasMaxLength(200);
            entity.Property(e => e.HX).HasMaxLength(200);
            entity.Property(e => e.HY).HasMaxLength(200);
            entity.Property(e => e.HZ).HasMaxLength(200);
            entity.Property(e => e.I).HasMaxLength(200);
            entity.Property(e => e.J).HasMaxLength(200);
            entity.Property(e => e.K).HasMaxLength(200);
            entity.Property(e => e.L).HasMaxLength(200);
            entity.Property(e => e.M).HasMaxLength(200);
            entity.Property(e => e.N).HasMaxLength(200);
            entity.Property(e => e.N1)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N10)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N2)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N3)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N4)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N5)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N6)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N7)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N8)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.N9)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.O).HasMaxLength(200);
            entity.Property(e => e.P).HasMaxLength(200);
            entity.Property(e => e.Q).HasMaxLength(200);
            entity.Property(e => e.R).HasMaxLength(200);
            entity.Property(e => e.S).HasMaxLength(200);
            entity.Property(e => e.T).HasMaxLength(200);
            entity.Property(e => e.U).HasMaxLength(200);
            entity.Property(e => e.V).HasMaxLength(200);
            entity.Property(e => e.W).HasMaxLength(200);
            entity.Property(e => e.X).HasMaxLength(200);
            entity.Property(e => e.Y).HasMaxLength(200);
            entity.Property(e => e.Z).HasMaxLength(200);
        });

        modelBuilder.Entity<AppTempSql>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppTempSql_PrimaryKey");

            entity.Property(e => e.SqlDesc).HasMaxLength(100);
            entity.Property(e => e.SqlExpress).HasMaxLength(2000);
        });

        modelBuilder.Entity<AppUserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppUserLogin_PrimaryKey");

            entity.HasIndex(e => e.IpAddress, "Inx_IpAddress");

            entity.HasIndex(e => e.LoginNname, "Inx_LoginName");

            entity.HasIndex(e => e.MacGuid, "Inx_MacGuid");

            entity.HasIndex(e => e.WriteTime, "Inx_WriteTime");

            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.LoginNname).HasMaxLength(100);
            entity.Property(e => e.LoginStatus).HasMaxLength(100);
            entity.Property(e => e.MacGuid).HasMaxLength(100);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AppUserMachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppUserMachine_PrimaryKey");

            entity.HasIndex(e => new { e.UserId, e.MacGuid, e.IpAddress }, "Inx_UserId").IsUnique();

            entity.Property(e => e.CanVerifyTime)
                .HasComputedColumnSql("(dateadd(minute,[VerifyMinutes],[WriteTime]))", false)
                .HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.MacGuid).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.VerifyCode).HasMaxLength(100);
            entity.Property(e => e.WriteTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AppUserMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppUserMessage_PrimaryKey");

            entity.HasIndex(e => e.SendDate, "Inx_SendDate");

            entity.HasIndex(e => new { e.SendNo, e.SendDate }, "Inx_SendNo");

            entity.Property(e => e.ErrorMessage).HasMaxLength(4000);
            entity.Property(e => e.SendDate).HasColumnType("datetime");
            entity.Property(e => e.SendMessage).HasMaxLength(4000);
            entity.Property(e => e.SendNo).HasMaxLength(50);
            entity.Property(e => e.SendSubject).HasMaxLength(50);
        });

        modelBuilder.Entity<AppUserRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppUserRequest_PrimaryKey");

            entity.HasIndex(e => new { e.ComponentId, e.UserIdName, e.WriteTime }, "Inx_ComponentId");

            entity.HasIndex(e => new { e.ControllerId, e.UserIdName, e.WriteTime }, "Inx_ControllerId");

            entity.HasIndex(e => new { e.TeamId, e.UserIdName, e.WriteTime }, "Inx_TeamId");

            entity.HasIndex(e => new { e.UserIdName, e.WriteTime }, "Inx_UserId");

            entity.Property(e => e.ActionId).HasMaxLength(100);
            entity.Property(e => e.ComponentId).HasMaxLength(100);
            entity.Property(e => e.ControllerId).HasMaxLength(100);
            entity.Property(e => e.HttpRoute).HasMaxLength(400);
            entity.Property(e => e.HttpVerb).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.MacGuid).HasMaxLength(100);
            entity.Property(e => e.QueryString).HasMaxLength(2000);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.UserIdName).HasMaxLength(100);
            entity.Property(e => e.WriteTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AppUserStar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AppUserStar_PrimaryKey");

            entity.HasIndex(e => e.SourceId, "AppUserStar_SourceId");

            entity.HasIndex(e => e.TeamId, "AppUserStar_TeamId");

            entity.HasIndex(e => e.UserId, "AppUserStar_UserId");

            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.MacGuid).HasMaxLength(100);
            entity.Property(e => e.SourceTable).HasMaxLength(100);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Au1Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("Au1Team_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Au1Team_Trigger1"));

            entity.HasIndex(e => new { e.City, e.Town, e.Village }, "Inx_City");

            entity.HasIndex(e => new { e.CityId, e.TownId, e.VillageId }, "Inx_CityId");

            entity.HasIndex(e => e.VillageId, "Inx_VillageId");

            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CityCode).HasMaxLength(1);
            entity.Property(e => e.CityId).HasMaxLength(100);
            entity.Property(e => e.CityShort).HasMaxLength(3);
            entity.Property(e => e.NationId).HasMaxLength(10);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.PostalCode).HasMaxLength(100);
            entity.Property(e => e.Town).HasMaxLength(100);
            entity.Property(e => e.TownId).HasMaxLength(100);
            entity.Property(e => e.Village).HasMaxLength(100);
            entity.Property(e => e.VillageId).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);
        });

        modelBuilder.Entity<Au1User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Au1User_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Au1User_trigger1"));

            entity.HasIndex(e => e.Email, "Inx_Email");

            entity.HasIndex(e => e.MobileTel, "Inx_MobileTel").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LastDate).HasColumnType("datetime");
            entity.Property(e => e.LoginDate).HasColumnType("datetime");
            entity.Property(e => e.MobileTel).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.PasswordChangeDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(2000);
            entity.Property(e => e.PasswordSalt).HasMaxLength(2000);
            entity.Property(e => e.PhotoUrl).HasMaxLength(200);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.UserData).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserRole).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);
        });

        modelBuilder.Entity<Cz2Petition>(entity =>
        {
            entity.HasKey(e => e.PetitionId).HasName("Cz2Petition_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Cz2Petition_Trigger1"));

            entity.HasIndex(e => e.Category, "Inx_Category");

            entity.HasIndex(e => e.TeamId, "Inx_TeamId");

            entity.HasIndex(e => e.UserId, "Inx_UserId");

            entity.Property(e => e.PetitionId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CitizenLineUserId).HasMaxLength(100);
            entity.Property(e => e.CitizenName).HasMaxLength(100);
            entity.Property(e => e.CitizenPhone).HasMaxLength(100);
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsTop).HasDefaultValue(false);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.TopDays).HasDefaultValue(0);
            entity.Property(e => e.UpadteDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Cz2Petition)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cz2Petition_ref_Au1Team");

            entity.HasOne(d => d.User).WithMany(p => p.Cz2Petition)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Cz2Petition_ref_Au1User");
        });

        modelBuilder.Entity<Cz2PetitionReply>(entity =>
        {
            entity.HasKey(e => e.ReplyId).HasName("Cz2PetitionReply_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Cz2PetitionReply_Trigger1"));

            entity.HasIndex(e => e.PetitionId, "inx_PetitionId");

            entity.Property(e => e.ReplyId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Petition).WithMany(p => p.Cz2PetitionReply)
                .HasForeignKey(d => d.PetitionId)
                .HasConstraintName("Cz2PetitionReply_ref_Cz2Petition");
        });

        modelBuilder.Entity<Cz2Repair>(entity =>
        {
            entity.HasKey(e => e.RepairId).HasName("Cz2Repair_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Cz2Repair_Trigger1"));

            entity.HasIndex(e => e.Category, "Inx_Category");

            entity.HasIndex(e => e.TeamId, "Inx_TeamId");

            entity.HasIndex(e => e.UserId, "Inx_UserId");

            entity.Property(e => e.RepairId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.AiSummary).HasMaxLength(500);
            entity.Property(e => e.Arrdess).HasMaxLength(200);
            entity.Property(e => e.AtDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CitizenLineUserId).HasMaxLength(100);
            entity.Property(e => e.CitizenName).HasMaxLength(100);
            entity.Property(e => e.CitizenPhone).HasMaxLength(100);
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.IsTop).HasDefaultValue(false);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.TopDays).HasDefaultValue(0);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Cz2Repair)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cz2Repair_ref_Au1Team");

            entity.HasOne(d => d.User).WithMany(p => p.Cz2Repair)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Cz2Repair_ref_Au1User");
        });

        modelBuilder.Entity<Cz2RepairReply>(entity =>
        {
            entity.HasKey(e => e.ReplyId).HasName("Cz2RepairReply_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Cz2RepairReply_Trigger1"));

            entity.HasIndex(e => e.RepairId, "inx_RepairId");

            entity.Property(e => e.ReplyId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Repair).WithMany(p => p.Cz2RepairReply)
                .HasForeignKey(d => d.RepairId)
                .HasConstraintName("Cz2RepairReply_ref_Cz2Repair");
        });

        modelBuilder.Entity<Ma1Master>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("Ma1Master_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ma1Master_Trigger1"));

            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.BirtCity).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Facebook).HasMaxLength(100);
            entity.Property(e => e.LineId).HasMaxLength(100);
            entity.Property(e => e.MasterName).HasMaxLength(100);
            entity.Property(e => e.MobileTel).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.OfficeTel).HasMaxLength(100);
            entity.Property(e => e.PhotoUrl).HasMaxLength(100);
            entity.Property(e => e.ServiceTime).HasMaxLength(100);
            entity.Property(e => e.Sex).HasMaxLength(1);
            entity.Property(e => e.Threads).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithOne(p => p.Ma1Master)
                .HasForeignKey<Ma1Master>(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ma1Master_ref_Au1Team");
        });

        modelBuilder.Entity<Ma2MasterEducation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ma2MasterEducation_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ma2MasterEducation_Trigger1"));

            entity.HasIndex(e => new { e.TeamId, e.OrderNo }, "Inx_TeamId");

            entity.Property(e => e.Descriptions).HasMaxLength(1000);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.OrderNo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderTitle).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Ma2MasterEducation)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ma2MasterEducation_ref_Ma1Master");
        });

        modelBuilder.Entity<Ma2MasterExperience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ma2MasterExperience_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ma2MasterExperience_Trigger1"));

            entity.HasIndex(e => new { e.TeamId, e.OrderNo }, "Inx_TeamId");

            entity.Property(e => e.Descriptions).HasMaxLength(1000);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.OrderNo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderTitle).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Ma2MasterExperience)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ma2MasterExperience_ref_Ma1Master");
        });

        modelBuilder.Entity<Ma2MasterPartner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("Ma2MasterPartner_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ma2MasterPartner_Trigger1"));

            entity.HasIndex(e => new { e.TeamId, e.OrderNo }, "Ma2MasterPartner_TeamId");

            entity.Property(e => e.PartnerId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.MobileTel).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.OrderNo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PartnerName).HasMaxLength(100);
            entity.Property(e => e.PhotoUrl).HasMaxLength(200);
            entity.Property(e => e.Sex).HasMaxLength(1);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Ma2MasterPartner)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("Ma2MasterPartner_ref_Ma1Master");
        });

        modelBuilder.Entity<Ma2MasterPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ma2MasterPhoto_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ma2MasterPhoto_Trigger1"));

            entity.HasIndex(e => new { e.TeamId, e.OrderNo }, "Ma2MasterPhoto_TeamId");

            entity.Property(e => e.Descriptions).HasMaxLength(1000);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.OrderNo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PhotoUrl).HasMaxLength(200);
            entity.Property(e => e.PublicKey).HasMaxLength(200);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Ma2MasterPhoto)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("Ma2MasterPhoto_ref_Ma1Master");
        });

        modelBuilder.Entity<Ma2MasterPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ma2MasterPolicy_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Ma2MasterPolicy_Trigger1"));

            entity.HasIndex(e => new { e.TeamId, e.OrderNo }, "Ma2MasterPolicy_TeamId");

            entity.Property(e => e.Descriptions).HasMaxLength(1000);
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.OrderNo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderTitle).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Ma2MasterPolicy)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("Ma2MasterPolicy_ref_Ma1Master");
        });

        modelBuilder.Entity<Pb2Bulletin>(entity =>
        {
            entity.HasKey(e => e.BbsId).HasName("Pb2Bulletin_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Pb2Bulletin_TriggerLog"));

            entity.HasIndex(e => new { e.Subject, e.AtDate }, "Pb2Bulletin_Subject");

            entity.Property(e => e.BbsId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.AtDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUser).HasMaxLength(100);
            entity.Property(e => e.DocNo).HasMaxLength(200);
            entity.Property(e => e.PdfFileUrl).HasMaxLength(200);
            entity.Property(e => e.Recipient).HasMaxLength(200);
            entity.Property(e => e.Secondary).HasMaxLength(200);
            entity.Property(e => e.SecretType).HasMaxLength(200);
            entity.Property(e => e.SpeedType).HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(200);
            entity.Property(e => e.TopDays).HasDefaultValue(0);
            entity.Property(e => e.UpdateUser).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Pb2Bulletin)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pb2Bulletin_Au1User");
        });

        modelBuilder.Entity<Pb2BulletinItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pb2BulletinItem_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Pb2BulletinItem_TriggerLog"));

            entity.HasIndex(e => e.BbsId, "Pb2BulletinItem_BbsId");

            entity.Property(e => e.Contents).HasMaxLength(4000);
            entity.Property(e => e.CreateUser).HasMaxLength(100);
            entity.Property(e => e.DocOrder).HasMaxLength(30);
            entity.Property(e => e.UpdateUser).HasMaxLength(100);

            entity.HasOne(d => d.Bbs).WithMany(p => p.Pb2BulletinItem)
                .HasForeignKey(d => d.BbsId)
                .HasConstraintName("Pb2BulletinItem_Pb2Bulletin");
        });

        modelBuilder.Entity<Pb2Forum>(entity =>
        {
            entity.HasKey(e => e.ForumId).HasName("Pb2Forum_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Pb2Forum_TriggerLog"));

            entity.HasIndex(e => new { e.Category, e.CreateTime }, "Pb2Forum_Category");

            entity.HasIndex(e => new { e.Title, e.CreateTime }, "Pb2Forum_Title");

            entity.HasIndex(e => new { e.UserId, e.CreateTime }, "Pb2Forum_UserId");

            entity.Property(e => e.ForumId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.TopDays).HasDefaultValue(0);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Pb2Forum)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pb2Forum_Au1User");
        });

        modelBuilder.Entity<Pb2ForumReply>(entity =>
        {
            entity.HasKey(e => e.ReplyId).HasName("Pb2ForumReply_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Pb2ForumReply_TriggerLog"));

            entity.HasIndex(e => new { e.ForumId, e.UserId }, "Pb2ForumReply_ForumId");

            entity.HasIndex(e => new { e.UserId, e.ForumId }, "Pb2ForumReply_UserId");

            entity.Property(e => e.ReplyId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Contents).HasMaxLength(4000);
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Forum).WithMany(p => p.Pb2ForumReply)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pb2ForumReply_Pb2Forum");

            entity.HasOne(d => d.User).WithMany(p => p.Pb2ForumReply)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pb2ForumReply_Au1User");
        });

        modelBuilder.Entity<Tm2Activity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("Tm2Activity_PrimaryKey");

            entity.HasIndex(e => e.Category, "Inx_Category");

            entity.HasIndex(e => e.TeamId, "Inx_TeamId");

            entity.HasIndex(e => e.UserId, "Inx_UserId");

            entity.Property(e => e.ActivityId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ActivityDate).HasColumnType("datetime");
            entity.Property(e => e.ActivityPns).HasDefaultValue(0);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Tm2Activity)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tm2Activity_ref_Au1Team");

            entity.HasOne(d => d.User).WithMany(p => p.Tm2Activity)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Tm2Activity_ref_Au1User");
        });

        modelBuilder.Entity<Tm2Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnounceId).HasName("Tm2Announcement_PrimaryKey");

            entity.ToTable(tb => tb.HasTrigger("Tm2Announcement_Trigger1"));

            entity.HasIndex(e => e.Category, "Inx_Category");

            entity.HasIndex(e => e.TeamId, "Inx_TeamId");

            entity.HasIndex(e => e.UserId, "Inx_UserId");

            entity.Property(e => e.AnnounceId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.AtDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AttachmentUrl).HasMaxLength(500);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.IsTop).HasDefaultValue(false);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.TopDays).HasDefaultValue(0);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Tm2Announcement)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tm2Announcement_ref_Au1Team");

            entity.HasOne(d => d.User).WithMany(p => p.Tm2Announcement)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Tm2Announcement_ref_Au1User");
        });

        modelBuilder.Entity<Tm2QuizOption>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("QuestionOption_PrimaryKey");

            entity.HasIndex(e => new { e.QuestionId, e.OptionDesc }, "QuestionOption_OptionDesc").IsUnique();

            entity.Property(e => e.OptionId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.OptionDesc).HasMaxLength(200);
            entity.Property(e => e.SortOrder).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Question).WithMany(p => p.Tm2QuizOption)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QuestionOption_Question");
        });

        modelBuilder.Entity<Tm2QuizQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("Question_PrimaryKey");

            entity.Property(e => e.QuestionId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.QuestionDesc).HasMaxLength(200);
            entity.Property(e => e.SortOrder).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Subject).WithMany(p => p.Tm2QuizQuestion)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Question_Subject");
        });

        modelBuilder.Entity<Tm2QuizSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("Tm2QuizSubject_PrimaryKey");

            entity.Property(e => e.SubjectId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Notes).HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(200);
            entity.Property(e => e.TeamId).HasMaxLength(100);
            entity.Property(e => e.WriteInfo).HasMaxLength(100);

            entity.HasOne(d => d.Team).WithMany(p => p.Tm2QuizSubject)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tm2QuizSubject_ref_Au1Team");
        });

        modelBuilder.Entity<ViewCity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewCity");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CityId).HasMaxLength(100);
            entity.Property(e => e.NationId).HasMaxLength(10);
        });

        modelBuilder.Entity<ViewTown>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTown");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CityId).HasMaxLength(100);
            entity.Property(e => e.NationId).HasMaxLength(10);
            entity.Property(e => e.Town).HasMaxLength(100);
            entity.Property(e => e.TownId).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
