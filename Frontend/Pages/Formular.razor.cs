using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Formular : ComponentBase
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    [Inject] private Frontend.AuthState AuthState { get; set; } = default!;

    private FormModel _model = new();
    private string? _statusMessage;
    private bool _isSuccess;
    private bool _isSubmitting;

    private List<classesDto> _classes = new();
    private List<studentsDto> _studentsForClass = new();
    private List<ModuleDto> _modulesForTeacher = new();
    private List<prorectorsDto> _prorectorsForTeacher = new();

    protected override async Task OnInitializedAsync()
    {
        // ----- Guard: nur per Login zugänglich -----
        if (!AuthState.IsLoggedIn)
        {
            Navigation.NavigateTo("/login", true);
            return;
        }

        // teacher_id aus Login-State
        _model.teacher_id = AuthState.TeacherId!.Value;

        // Get classes for this specific teacher using teacher_classes junction table
        _classes = await TryGet<List<classesDto>>($"api/teachers/{_model.teacher_id}/classes") ?? new();

        _modulesForTeacher = await TryGet<List<ModuleDto>>(
            $"api/teachers/{_model.teacher_id}/modules") ?? new();

        _prorectorsForTeacher = await TryGet<List<prorectorsDto>>(
            $"api/teachers/{_model.teacher_id}/prorectors") ?? new();
    }

    private async Task OnClassChanged()
    {
        _model.student_id = null;
        _studentsForClass.Clear();

        if (!_model.class_id.HasValue) return;

        _studentsForClass = await TryGet<List<studentsDto>>(
            $"api/classes/{_model.class_id}/students") ?? new();
    }

    private async Task OnStudentChanged()
    {
        _model.old_grade = null;

        if (!_model.class_id.HasValue || !_model.student_id.HasValue) return;

        var result = await TryGet<EnrollmentAndLastGradeDto>(
            $"api/classes/{_model.class_id}/students/{_model.student_id}/lastgrade");

        if (result != null)
        {
            _model.enrollment_id = result.enrollment_id;
            // Parse the grade value to decimal if available
            if (!string.IsNullOrWhiteSpace(result.last_grade_value) &&
                decimal.TryParse(result.last_grade_value, out var gradeValue))
            {
                _model.old_grade = gradeValue;
            }
        }
    }

    // ---------- Hilfsmethode ----------
    private async Task<T?> TryGet<T>(string url)
    {
        try
        {
            return await Http.GetFromJsonAsync<T>(url);
        }
        catch
        {
            return default;
        }
    }

    private class FormModel
    {
        public int? teacher_id { get; set; }
        public int? class_id { get; set; }
        public int? student_id { get; set; }
        public int? enrollment_id { get; set; }
        public int? module_id { get; set; }
        public string? exam_name { get; set; }
        public int? prorector_id { get; set; }
        public decimal? old_grade { get; set; }
        public string? new_grade { get; set; }
        public string? comment { get; set; }
        public bool has_future_grade { get; set; }
    }

    private class EnrollmentAndLastGradeDto
    {
        public int enrollment_id { get; set; }
        public string? last_grade_value { get; set; }
    }

    private async Task HandleValidSubmit()
    {
        if (!_model.class_id.HasValue ||
            !_model.student_id.HasValue ||
            !_model.module_id.HasValue ||
            !_model.prorector_id.HasValue ||
            !_model.teacher_id.HasValue ||
            string.IsNullOrWhiteSpace(_model.exam_name) ||
            string.IsNullOrWhiteSpace(_model.new_grade))
        {
            _statusMessage = "Bitte füllen Sie alle Pflichtfelder aus.";
            _isSuccess = false;
            return;
        }

        _isSubmitting = true;
        _statusMessage = null;

        try
        {
            var dto = new SubmitGradeChangeDto
            {
                class_id = _model.class_id.Value,
                student_id = _model.student_id.Value,
                module_id = _model.module_id.Value,
                teacher_id = _model.teacher_id.Value,
                prorector_id = _model.prorector_id.Value,
                assessment_title = _model.exam_name,
                old_grade_value = _model.old_grade?.ToString("F1"),
                new_grade_value = _model.new_grade,
                comment = _model.comment,
                has_future_grade = _model.has_future_grade
            };

            var response = await Http.PostAsJsonAsync("api/gradechange", dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GradeChangeSubmissionResultDto>();

                if (result != null)
                {
                    _statusMessage = result.message;
                    _isSuccess = result.success;

                    if (result.success)
                    {
                        // Reset form on success
                        if (decimal.TryParse(_model.new_grade, out var newGradeValue))
                        {
                            _model.old_grade = newGradeValue;
                        }
                        _model.new_grade = null;
                        _model.comment = null;
                        _model.exam_name = null;
                        _model.has_future_grade = false;
                    }
                }
            }
            else
            {
                var result = await response.Content.ReadFromJsonAsync<GradeChangeSubmissionResultDto>();
                _statusMessage = result?.message ?? "Fehler beim Senden der Notenänderung.";
                _isSuccess = false;
            }
        }
        catch (Exception ex)
        {
            _statusMessage = $"Fehler: {ex.Message}";
            _isSuccess = false;
        }
        finally
        {
            _isSubmitting = false;
        }
    }
}
