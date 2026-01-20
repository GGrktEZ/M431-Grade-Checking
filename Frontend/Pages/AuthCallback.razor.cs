namespace Frontend.Pages
{
    public partial class AuthCallback
    {
        protected override void OnInitialized()
        {
            var uri = Nav.ToAbsoluteUri(Nav.Uri);

            // Query-Parameter lesen (token, teacherId)
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

            var token = query.Get("token");
            var teacherIdStr = query.Get("teacherId");

            if (!string.IsNullOrWhiteSpace(token))
                AuthState.Token = token;

            if (int.TryParse(teacherIdStr, out var tid))
                AuthState.TeacherId = tid;

            // Danach ins Formular
            Nav.NavigateTo("/formular", true);
        }
    }
}