using DataAccess;
using DataAccess.Repository;
using Shared.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Services.Service;

public class GradeChangeNotificationService : IGradeChangeNotificationService
{
    private readonly Igrade_changesService _gradeChangesService;
    private readonly IprorectorsService _prorectorsService;
    private readonly IteachersRepository _teachersRepository;
    private readonly IstudentsRepository _studentsRepository;
    private readonly IclassesRepository _classesRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<GradeChangeNotificationService> _logger;
    private readonly AppDbContext _db;

public GradeChangeNotificationService(
      Igrade_changesService gradeChangesService,
  IprorectorsService prorectorsService,
  IteachersRepository teachersRepository,
        IstudentsRepository studentsRepository,
        IclassesRepository classesRepository,
   IEmailService emailService,
  ILogger<GradeChangeNotificationService> logger,
        AppDbContext db)
    {
 _gradeChangesService = gradeChangesService;
    _prorectorsService = prorectorsService;
   _teachersRepository = teachersRepository;
 _studentsRepository = studentsRepository;
   _classesRepository = classesRepository;
        _emailService = emailService;
_logger = logger;
        _db = db;
    }

    public async Task<GradeChangeSubmissionResultDto> SubmitGradeChangeAsync(SubmitGradeChangeDto dto)
    {
        try
        {
        // 1. Save grade change to database
      var gradeChangeDto = new Creategrade_changesDto
     {
       class_id = dto.class_id,
            student_id = dto.student_id,
            module_id = dto.module_id,
           teacher_id = dto.teacher_id,
                prorector_id = dto.prorector_id,
  assessment_title = dto.assessment_title,
   old_grade_value = dto.old_grade_value ?? "",
       new_grade_value = dto.new_grade_value,
    comment = dto.comment
        };

     var createdGradeChange = await _gradeChangesService.CreateGradeChangeAsync(gradeChangeDto);

        // 2. Get all necessary information for email
            var prorectorDto = await _prorectorsService.GetProrectorByIdAsync(dto.prorector_id);
       if (prorectorDto == null)
         {
       _logger.LogError("Prorector with ID {ProrectorId} not found", dto.prorector_id);
     return new GradeChangeSubmissionResultDto
       {
      success = true,
  message = "Notenänderung gespeichert, aber Prorektor nicht gefunden. E-Mail konnte nicht gesendet werden.",
  emailSent = false
        };
          }

     var teacher = _teachersRepository.GetteachersById(dto.teacher_id);
     var student = _studentsRepository.GetstudentsById(dto.student_id);
       var classInfo = _classesRepository.GetclassesById(dto.class_id);
       var module = await _db.modules.FirstOrDefaultAsync(m => m.module_id == dto.module_id);

 // 3. Build email content
       string emailSubject = $"Notenänderung: {student?.first_name} {student?.last_name} - {module?.module_code}";
       
     string emailBody = $@"Sehr geehrte/r {prorectorDto.first_name} {prorectorDto.last_name},

es wurde eine Notenänderung eingereicht, die Ihre Genehmigung erfordert.

DETAILS ZUR NOTENÄNDERUNG:
---------------------------
Lehrperson: {teacher?.first_name} {teacher?.last_name}
Klasse: {classInfo?.class_name}
Schüler/in: {student?.last_name} {student?.first_name}
Modul: {module?.module_code} - {module?.module_name}
Prüfungsbezeichnung: {dto.assessment_title}

Alte Note: {(string.IsNullOrWhiteSpace(dto.old_grade_value) ? "Keine" : dto.old_grade_value)}
Neue Note: {dto.new_grade_value}

{(string.IsNullOrWhiteSpace(dto.comment) ? "" : $"Bemerkung:\n{dto.comment}\n")}
Weitere Noteneinträge geplant: {(dto.has_future_grade ? "Ja" : "Nein")}

---------------------------
Diese E-Mail wurde automatisch vom Notenverwaltungssystem generiert.

Mit freundlichen Grüssen
Notenverwaltungssystem GIBZ
";

        // 4. Send email to prorector
try
  {
await _emailService.SendAsync(prorectorDto.email, emailSubject, emailBody);

    _logger.LogInformation(
      "Grade change notification email sent to prorector {ProrectorEmail} for student {StudentId}",
      prorectorDto.email, dto.student_id);

    return new GradeChangeSubmissionResultDto
    {
        success = true,
      message = "Notenänderung erfolgreich eingereicht und E-Mail an Prorektor gesendet.",
   emailSent = true
            };
            }
    catch (Exception emailEx)
 {
          _logger.LogError(emailEx, "Failed to send email to prorector {ProrectorEmail}", prorectorDto.email);
      
  return new GradeChangeSubmissionResultDto
    {
     success = true,
     message = $"Notenänderung gespeichert, aber E-Mail-Versand fehlgeschlagen: {emailEx.Message}",
        emailSent = false
       };
     }
        }
    catch (Exception ex)
  {
            _logger.LogError(ex, "Failed to submit grade change");
            
  return new GradeChangeSubmissionResultDto
{
     success = false,
         message = $"Fehler beim Speichern der Notenänderung: {ex.Message}",
      emailSent = false
  };
    }
    }
}
