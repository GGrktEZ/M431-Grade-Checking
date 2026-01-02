using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Home : ComponentBase
{
    [Inject] private HttpClient Http { get; set; } = default!;

    private FormModel _model = new();
    private List<classesDto> _classes = new();
    private List<studentsDto> _studentsForClass = new();
    private string? _moduleText;

    protected override async Task OnInitializedAsync()
    {
        _classes = await Http.GetFromJsonAsync<List<classesDto>>("api/classes") ?? new();
    }

    private async Task OnClassChanged(ChangeEventArgs _)
    {
        _model.student_id = null;
        _model.enrollment_id = null;
        _model.old_grade = null;
        _studentsForClass.Clear();
        _moduleText = null;

        if (!_model.class_id.HasValue) return;

        var selectedClass = _classes.First(x => x.class_id == _model.class_id.Value);

        // Modultext (api/modules/{id})
        var mod = await Http.GetFromJsonAsync<ModuleDto>($"api/modules/{selectedClass.module_id}");
        if (mod != null)
            _moduleText = $"{mod.module_code} – {mod.module_name}";

        // Schüler in Klasse (api/classes/{classId}/students)
        _studentsForClass = await Http.GetFromJsonAsync<List<studentsDto>>($"api/classes/{selectedClass.class_id}/students") ?? new();
    }

    private async Task OnStudentChanged(ChangeEventArgs _)
    {
        _model.old_grade = null;
        _model.enrollment_id = null;

        if (!_model.class_id.HasValue || !_model.student_id.HasValue) return;

        // enrollment + letzte note via API
        var result = await Http.GetFromJsonAsync<EnrollmentAndLastGradeDto>(
            $"api/classes/{_model.class_id}/students/{_model.student_id}/lastgrade");

        if (result is null) return;

        _model.enrollment_id = result.enrollment_id;
        _model.old_grade = result.last_grade_value;
    }

    private async Task HandleValidSubmit()
    {
        if (_model.enrollment_id is null) return;

        var dto = new gradesDto
        {
            enrollment_id = _model.enrollment_id.Value,
            grade_value = _model.new_grade!,
            comment = _model.comment
        };

        var response = await Http.PostAsJsonAsync("api/grades", dto);
        response.EnsureSuccessStatusCode();

        _model.old_grade = _model.new_grade;
        _model.new_grade = null;
        _model.comment = null;
    }

    private class FormModel
    {
        public int? class_id { get; set; }
        public int? student_id { get; set; }
        public int? enrollment_id { get; set; }
        public string? exam_name { get; set; }
        public string? old_grade { get; set; }
        public string? new_grade { get; set; }
        public string? comment { get; set; }
    }
}

public class EnrollmentAndLastGradeDto
{
    public int enrollment_id { get; set; }
    public string? last_grade_value { get; set; }
}
