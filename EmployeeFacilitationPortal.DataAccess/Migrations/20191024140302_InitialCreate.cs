using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeFacilitationPortal.DataRepository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidatesReferred",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Resume = table.Column<string>(nullable: true),
                    YearsOfExperience = table.Column<float>(nullable: false),
                    Relationship = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatesReferred", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(nullable: true),
                    WhatsNew = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrievanceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    AssignedTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LetterType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IsAlreadyRegistered = table.Column<bool>(nullable: false),
                    PasswordSetStatus = table.Column<bool>(nullable: false),
                    ExpiryInDays = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LetterRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LetterTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Urgency = table.Column<string>(nullable: true),
                    Information = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterRequest_LetterType_LetterTypeId",
                        column: x => x.LetterTypeId,
                        principalTable: "LetterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    EmpId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 400, nullable: true),
                    DateOfJoining = table.Column<DateTime>(nullable: false),
                    Designation = table.Column<string>(maxLength: 100, nullable: true),
                    ProjectAssigned = table.Column<string>(maxLength: 200, nullable: true),
                    Terminated = table.Column<string>(maxLength: 200, nullable: true),
                    IsValidated = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    LoginId = table.Column<int>(nullable: true),
                    TrainingDuringEducation = table.Column<string>(nullable: true),
                    EmployeeTechnicalSkills = table.Column<string>(nullable: true),
                    RelativeWorkingInCiklum = table.Column<string>(nullable: true),
                    SpecialArrangementsRequired = table.Column<string>(nullable: true),
                    MisconductInPreviousJob = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    FieldName = table.Column<string>(maxLength: 50, nullable: true),
                    IsAllowed = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PagePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    IsAllowed = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferredTechnicalSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TechnicalSkillId = table.Column<int>(nullable: false),
                    CandidateReferredId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferredTechnicalSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferredTechnicalSkills_CandidatesReferred_CandidateReferredId",
                        column: x => x.CandidateReferredId,
                        principalTable: "CandidatesReferred",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferredTechnicalSkills_TechnicalSkills_TechnicalSkillId",
                        column: x => x.TechnicalSkillId,
                        principalTable: "TechnicalSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    DocumentTitle = table.Column<string>(nullable: true),
                    DocumentContents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllAttachments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    CertificateScannedCopy = table.Column<string>(nullable: true),
                    CertificateName = table.Column<string>(nullable: true),
                    CertificationLink = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(maxLength: 30, nullable: true),
                    SupportingDocument = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Cnic = table.Column<string>(maxLength: 15, nullable: true),
                    Relationship = table.Column<string>(maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Degree = table.Column<string>(maxLength: 100, nullable: true),
                    Study = table.Column<string>(maxLength: 100, nullable: true),
                    InstitutionName = table.Column<string>(maxLength: 100, nullable: true),
                    Grade = table.Column<float>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCandidatesReferred",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    CandidateReferredId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCandidatesReferred", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCandidatesReferred_CandidatesReferred_CandidateReferredId",
                        column: x => x.CandidateReferredId,
                        principalTable: "CandidatesReferred",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCandidatesReferred_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTechnicalSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TechnicalSkillId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTechnicalSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTechnicalSkills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTechnicalSkills_TechnicalSkills_TechnicalSkillId",
                        column: x => x.TechnicalSkillId,
                        principalTable: "TechnicalSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForgotEmail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 2048, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForgotEmail_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grievances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    GrievanceTitle = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grievances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grievances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    ProficiencyLevel = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageSkills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Letters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(maxLength: 100, nullable: true),
                    Response = table.Column<string>(maxLength: 500, nullable: true),
                    Category = table.Column<string>(maxLength: 30, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Letters_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Ntn = table.Column<string>(maxLength: 20, nullable: true),
                    Relative = table.Column<string>(maxLength: 40, nullable: true),
                    Disability = table.Column<string>(maxLength: 200, nullable: true),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    HusbandName = table.Column<string>(maxLength: 100, nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(maxLength: 15, nullable: true),
                    MaritalStatus = table.Column<string>(maxLength: 20, nullable: true),
                    Cnic = table.Column<string>(maxLength: 15, nullable: true),
                    PassportNumber = table.Column<string>(maxLength: 10, nullable: true),
                    PassportValidityDate = table.Column<DateTime>(nullable: false),
                    BloodGroup = table.Column<string>(maxLength: 3, nullable: true),
                    PresentAddress = table.Column<string>(maxLength: 400, nullable: true),
                    PermanentAddress = table.Column<string>(maxLength: 400, nullable: true),
                    PersonalContactNumber = table.Column<string>(maxLength: 15, nullable: true),
                    EmergencyContactNumber = table.Column<string>(maxLength: 15, nullable: true),
                    EmergencyContactPerson = table.Column<string>(maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalInfos_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalReferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    OrganizationName = table.Column<string>(maxLength: 100, nullable: true),
                    Designation = table.Column<string>(maxLength: 100, nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 15, nullable: true),
                    EmailId = table.Column<string>(maxLength: 255, nullable: true),
                    Relationship = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionalReferences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    DeliveryType = table.Column<string>(nullable: true),
                    TrainingRequestTypeId = table.Column<int>(nullable: false),
                    TrainingRequestTypeTitle = table.Column<string>(nullable: true),
                    BussinessJustification = table.Column<string>(maxLength: 1000, nullable: true),
                    Detail = table.Column<string>(maxLength: 1000, nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    Cost = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(maxLength: 100, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ReasonForLeaving = table.Column<string>(maxLength: 1000, nullable: true),
                    ToolsAndTechnologies = table.Column<string>(maxLength: 500, nullable: true),
                    LastDrawnSalary = table.Column<int>(nullable: false),
                    Benefit = table.Column<string>(maxLength: 100, nullable: true),
                    Designation = table.Column<string>(maxLength: 100, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ProjectDescriptionAndResponsibilities = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrievanceMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrievanceId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrievanceMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrievanceMessage_Grievances_GrievanceId",
                        column: x => x.GrievanceId,
                        principalTable: "Grievances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueryResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    QueryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryResponse_Queries_QueryId",
                        column: x => x.QueryId,
                        principalTable: "Queries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "Age", "Name" },
                values: new object[,]
                {
                    { 1, 24, "sample-1" },
                    { 2, 23, "sample-2" },
                    { 3, 25, "sample-3" },
                    { 4, 26, "sample-4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllAttachments_EmployeeId",
                table: "AllAttachments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_EmployeeId",
                table: "Certifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_EmployeeId",
                table: "Claims",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_EmployeeId",
                table: "Dependents",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalRecords_EmployeeId",
                table: "EducationalRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCandidatesReferred_CandidateReferredId",
                table: "EmployeeCandidatesReferred",
                column: "CandidateReferredId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCandidatesReferred_EmployeeId",
                table: "EmployeeCandidatesReferred",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LoginId",
                table: "Employees",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTechnicalSkills_EmployeeId",
                table: "EmployeeTechnicalSkills",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTechnicalSkills_TechnicalSkillId",
                table: "EmployeeTechnicalSkills",
                column: "TechnicalSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldPermissions_RoleId",
                table: "FieldPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ForgotEmail_EmployeeId",
                table: "ForgotEmail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrievanceMessage_GrievanceId",
                table: "GrievanceMessage",
                column: "GrievanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Grievances_EmployeeId",
                table: "Grievances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSkills_EmployeeId",
                table: "LanguageSkills",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterRequest_LetterTypeId",
                table: "LetterRequest",
                column: "LetterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Letters_EmployeeId",
                table: "Letters",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PagePermissions_RoleId",
                table: "PagePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfos_EmployeeId",
                table: "PersonalInfos",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalReferences_EmployeeId",
                table: "ProfessionalReferences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Queries_EmployeeId",
                table: "Queries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryResponse_QueryId",
                table: "QueryResponse",
                column: "QueryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferredTechnicalSkills_CandidateReferredId",
                table: "ReferredTechnicalSkills",
                column: "CandidateReferredId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferredTechnicalSkills_TechnicalSkillId",
                table: "ReferredTechnicalSkills",
                column: "TechnicalSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRequests_EmployeeId",
                table: "TrainingRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_EmployeeId",
                table: "WorkHistories",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllAttachments");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "CompanyInformation");

            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "EducationalRecords");

            migrationBuilder.DropTable(
                name: "EmployeeCandidatesReferred");

            migrationBuilder.DropTable(
                name: "EmployeeTechnicalSkills");

            migrationBuilder.DropTable(
                name: "FieldPermissions");

            migrationBuilder.DropTable(
                name: "ForgotEmail");

            migrationBuilder.DropTable(
                name: "GrievanceMessage");

            migrationBuilder.DropTable(
                name: "GrievanceType");

            migrationBuilder.DropTable(
                name: "LanguageSkills");

            migrationBuilder.DropTable(
                name: "LetterRequest");

            migrationBuilder.DropTable(
                name: "Letters");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "PagePermissions");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropTable(
                name: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "ProfessionalReferences");

            migrationBuilder.DropTable(
                name: "QueryResponse");

            migrationBuilder.DropTable(
                name: "ReferredTechnicalSkills");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "TrainingRequests");

            migrationBuilder.DropTable(
                name: "TrainingRequestTypes");

            migrationBuilder.DropTable(
                name: "WorkHistories");

            migrationBuilder.DropTable(
                name: "Grievances");

            migrationBuilder.DropTable(
                name: "LetterType");

            migrationBuilder.DropTable(
                name: "Queries");

            migrationBuilder.DropTable(
                name: "CandidatesReferred");

            migrationBuilder.DropTable(
                name: "TechnicalSkills");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
