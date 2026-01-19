using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Formular : ComponentBase
{
    [Inject] private HttpClient Http { get; set; } = default!;

    private FormModel _model = new();

    private List<classesDto> _classes = new();
    private List<studentsDto> _studentsForClass = new();
    private List<ModuleDto> _modulesForTeacher = new();
    private List<prorectorsDto> _prorectorsForTeacher = new();

    protected override async Task OnInitializedAsync()
    {
        _model.teacher_id = 14; // TEST: Lindauer

        // KEIN CRASH MEHR – alle Calls abgesichert
        _classes = await TryGet<List<classesDto>>("api/classes") ?? new();
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
            _model.old_grade = result.last_grade_value;
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
        public string? old_grade { get; set; }
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
        if (!_model.enrollment_id.HasValue ||
            !_model.module_id.HasValue ||
            !_model.prorector_id.HasValue ||
            string.IsNullOrWhiteSpace(_model.new_grade))
        {
            return;
        }

        var dto = new CreategradesDto
        {
            enrollment_id = _model.enrollment_id.Value,
            grade_value = _model.new_grade!,
            comment = _model.comment ?? ""
        };

        await Http.PostAsJsonAsync("api/grades", dto);

        // optional: Formular teilweise zurücksetzen
        _model.old_grade = _model.new_grade;
        _model.new_grade = null;
        _model.comment = null;
    }

}
