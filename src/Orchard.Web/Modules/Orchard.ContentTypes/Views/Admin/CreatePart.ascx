<%@ Control Language="C#" Inherits="Orchard.Mvc.ViewUserControl<Orchard.ContentTypes.ViewModels.CreatePartViewModel>" %>
<h1><%:Html.TitleForPage(T("New Content Part").ToString())%></h1><%
using (Html.BeginFormAntiForgeryPost()) { %>
    <%:Html.ValidationSummary() %>
    <fieldset>
        <label for="DisplayName"><%:T("Name") %></label>
        <%:Html.TextBoxFor(m => m.Name, new {@class = "textMedium", autofocus = "autofocus"}) %>
    </fieldset>
    <fieldset>
        <button class="primaryAction" type="submit"><%:T("Create") %></button>
    </fieldset><%
} %>